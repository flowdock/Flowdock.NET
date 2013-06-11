using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Client.Stream {
	public class StreamNetworkErrorEventArgs : EventArgs {
		public string ErrorMessage { get; set; }

		public bool TryReconnect { get; set; }

		public StreamNetworkErrorEventArgs(string errorMessage) {
			ErrorMessage = errorMessage;
		}
	}
}
