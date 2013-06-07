using System;
using System.Windows;
using System.Windows.Threading;

namespace Flowdock.Util {
	public static class UIThread {
		private static readonly Dispatcher Dispatcher;

		static UIThread() {
			// Store a reference to the current Dispatcher once per application
			Dispatcher = Deployment.Current.Dispatcher;
		}

		public static void Invoke(Action action) {
			if (Dispatcher.CheckAccess()) {
				action.Invoke();
			} else {
				Dispatcher.BeginInvoke(action);
			}
		}
	}
}
