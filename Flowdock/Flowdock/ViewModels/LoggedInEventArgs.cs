using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.ViewModels {
	public class LoggedInEventArgs : EventArgs {
		public bool Success { get; set; }
		public string FailureMessage { get; set; }

		public LoggedInEventArgs(string message) {
			Success = message == null;
			FailureMessage = message;
		}
	}
}
