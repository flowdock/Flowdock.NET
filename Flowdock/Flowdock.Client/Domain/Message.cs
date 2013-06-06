using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Client.Domain {
	public class Message {
		private string _content;

		private void Extract(string content) {
			JsonDeserializer deserializer = new JsonDeserializer();

			var response = new RestResponse();
			response.Content = content;

			MessageContent messageContent = null;
			try {
				messageContent = deserializer.Deserialize<MessageContent>(response);
			} catch {
				MessageContent = null;
			}

			if (messageContent != null) {
				MessageContent = messageContent;
				ExtractedBody = messageContent.Text;
			} else {
				ExtractedBody = content;
			}

			Displayable = ExtractedBody != null;
		}

		public int Id { get; set; }
		public string App { get; set; }
		public string Event { get; set; }
		public List<string> Tags { get; set; }
		public string Uuid { get; set; }
		public string Flow { get; set; }
		public long Sent { get; set; }
		public int User { get; set; }

		public string Content {
			get {
				return _content;
			}
			set {
				_content = value;
				Extract(value);
			}
		}

		public MessageContent MessageContent { get; private set; }
		public string ExtractedBody { get; private set; }
		public bool Displayable { get; private set; }
	}
}
