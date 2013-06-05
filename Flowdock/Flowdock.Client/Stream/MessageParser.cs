using Flowdock.Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Client.Stream {
	public class MessageParser {
		private static int _numPublished = 0;

		private Action<Message> _callback;

		public MessageParser(Action<Message> callback) {
			_callback = callback;
		}

		public void Push(string rawData) {
			if (_callback != null && _numPublished < 10) {
				_callback(new Message {
					Content = "Test message :::: " + rawData
				});
				++_numPublished;
			}
		}
	}
}
