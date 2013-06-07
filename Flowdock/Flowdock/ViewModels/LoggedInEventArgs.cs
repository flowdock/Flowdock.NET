using System;

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
