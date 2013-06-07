using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Flowdock.Services.Progress {
	public class ProgressService : IProgressService {
		private readonly ProgressIndicator progressIndicator;

		private void OnRootFrameOnNavigated(object sender, NavigationEventArgs args) {
			var content = args.Content;
			var page = content as PhoneApplicationPage;
			if (page == null)
				return;

			page.SetValue(SystemTray.ProgressIndicatorProperty, progressIndicator);
		}

		public ProgressService(PhoneApplicationFrame rootFrame) {
			progressIndicator = new ProgressIndicator();

			rootFrame.Navigated += OnRootFrameOnNavigated;
		}

		public bool IsVisible {
			get {
				return progressIndicator.IsVisible;
			}
		}

		public void Show(string text) {
			progressIndicator.Text = text;
			progressIndicator.IsIndeterminate = true;
			progressIndicator.IsVisible = true;
		}

		public void Hide() {
			progressIndicator.IsIndeterminate = false;
			progressIndicator.IsVisible = false;
		}
	}
}
