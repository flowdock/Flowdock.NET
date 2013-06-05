using Flowdock.Client.Domain;
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

		public void GoToLobby() {
			GoTo("/Views/LobbyView.xaml");
		}

		public void GoToFlow(Flow flow) {
			string url = string.Format("/Views/FlowView.xaml?flowId={0}&flowName={1}", flow.Id, flow.Name);
			GoTo(url);
		}
	}
}
