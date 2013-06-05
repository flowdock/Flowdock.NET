using Flowdock.Client.Domain;
using System;
using System.Net;

namespace Flowdock.Client.Stream {
	public class FlowStreamingConnection : IFlowStreamingConnection {
		private const string StreamUrl = "https://stream.flowdock.com";

		private HttpWebRequest _request;
		private HttpWebResponse _response;
		private MessageParser _messageParser;

		private byte[] _buffer = new byte[10240];


		private string BuildUrl(Flow flow) {
			return string.Format("{0}/flows/{1}", StreamUrl, flow.Id.Replace(":", "/"));
		}

		private void OnRead(IAsyncResult result) {
			int bytesRead = _response.GetResponseStream().EndRead(result);

			// Flowdock uses UTF8
			string readString = System.Text.Encoding.UTF8.GetString(_buffer, 0, bytesRead);
			_messageParser.Push(readString);
		}

		private void Read() {
			_response.GetResponseStream().BeginRead(_buffer, 0, _buffer.Length, OnRead, null);
		}

		private void OnGetResponse(IAsyncResult result) {
			_response = (HttpWebResponse)_request.EndGetResponse(result);

			Read();
		}

		public void Start(string username, string password, Flow flow, Action<Message> callback) {
			_messageParser = new MessageParser(callback);

			_request = HttpWebRequest.CreateHttp(BuildUrl(flow));
			_request.Credentials = new NetworkCredential(username, password);

			_request.BeginGetResponse(OnGetResponse, null);
		}

		public void Stop() {
			if (_response != null) {
				_response.Close();
			}
		}
	}
}
