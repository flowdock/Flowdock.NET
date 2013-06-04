using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Flowdock.Navigation {
	public class NavigationManager : INavigationManager {
		private void GoTo(string path) {
			Flowdock.App.RootFrame.Navigate(new Uri(path, UriKind.Relative));
		}

		public void GoToFlows() {
			GoTo("/Views/FlowView.xaml");
		}
	}
}
