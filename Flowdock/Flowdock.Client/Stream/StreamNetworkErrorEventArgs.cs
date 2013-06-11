using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Client.Stream {
	public class StreamNetworkErrorEventArgs : EventArgs {
		public Exception Exception { get; set; }

		public StreamNetworkErrorEventArgs(Exception exception) {
            Exception = exception;
		}
	}
}
