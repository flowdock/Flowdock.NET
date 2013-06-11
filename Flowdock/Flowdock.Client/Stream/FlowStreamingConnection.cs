using Flowdock.Client.Domain;
using System;
using System.Net;

namespace Flowdock.Client.Stream {
	public class FlowStreamingConnection : IFlowStreamingConnection, IDisposable {
		private const string StreamUrl = "https://stream.flowdock.com";

		private HttpWebRequest _request;
		private HttpWebResponse _response;
		private MessageParser _messageParser;

		private byte[] _buffer = new byte[1024*4];
		private bool _stopped = false;

		private bool _disposed;


		private string BuildUrl(string flowId) {
			return string.Format("{0}/flows/{1}", StreamUrl, flowId.Replace(":", "/"));
		}

		private void OnRead(IAsyncResult result) {
			if (_stopped) {
				return;
			}

			int bytesRead = _response.GetResponseStream().EndRead(result);

			// Flowdock uses UTF8
			string readString = System.Text.Encoding.UTF8.GetString(_buffer, 0, bytesRead);
			_messageParser.Push(readString);

			// keep pulling down from the stream
			Read();
		}

		private void Read() {
			if (_stopped) {
				return;
			}
			_response.GetResponseStream().BeginRead(_buffer, 0, _buffer.Length, OnRead, null);
		}

		private void OnGetResponse(IAsyncResult result) {
			_response = (HttpWebResponse)_request.EndGetResponse(result);

			Read();
		}

		protected virtual void Dispose(bool disposing) {
			if (!_disposed) {
				if (disposing) {
					if (_response != null) {
						_response.Dispose();
					}
					_disposed = true;
				}
			}
		}

		~FlowStreamingConnection() {
			Dispose(false);
		}

		public void Start(string username, string password, string flowId, Action<Message> callback) {
			_messageParser = new MessageParser(callback);

			_request = HttpWebRequest.CreateHttp(BuildUrl(flowId));
			_request.Credentials = new NetworkCredential(username, password);

			_request.BeginGetResponse(OnGetResponse, null);
		}

		public void Stop() {
			if (_response != null) {
				_response.Dispose();
				_response = null;
			}
			_stopped = true;
		}

		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
