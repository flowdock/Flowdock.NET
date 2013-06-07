using Flowdock.Client.Domain;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;

namespace Flowdock.Client.Stream {
	public class MessageParser {
		private Action<Message> _callback;

		private List<string> _buildingMessage = new List<string>();

		private void Seal() {
			string messageData = string.Join("", _buildingMessage).Trim();

			if (messageData.Length == 0) {
				_buildingMessage.Clear();
				return;
			}

			JsonDeserializer deserializer = new JsonDeserializer();

			var response = new RestResponse();
			response.Content = messageData;
			var message = deserializer.Deserialize<Message>(response);

			_buildingMessage.Clear();

			_callback(message);
		}

		public MessageParser(Action<Message> callback) {
			_callback = callback;
		}

		public void Push(string rawData) {
			int carriageReturnIndex = rawData.IndexOf('\r');
			if (carriageReturnIndex > -1) {
				_buildingMessage.Add(rawData.Substring(0, carriageReturnIndex));
				rawData = rawData.Substring(carriageReturnIndex + 1);
				Seal();
			} else {
				_buildingMessage.Add(rawData);
			}

		}
	}
}
