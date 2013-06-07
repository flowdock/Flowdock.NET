using Caliburn.Micro;
using Flowdock.Caliburn;
using Flowdock.Client.Domain;
using Flowdock.Services.Navigation;
using Flowdock.Settings;
using System.Windows.Input;

namespace Flowdock.ViewModels {
	public class LobbyFlowViewModel : PropertyChangedBase {
		private Flow _flow;
		private GoToFlowCommand _goToFlowCommand;

		public LobbyFlowViewModel(Flow flow, INavigationManager navigationManager, IAppSettings appSettings) {
			_flow = flow;

			_goToFlowCommand = new GoToFlowCommand(flow, navigationManager, appSettings);
		}

		public string Name {
			get {
				return _flow.Name;
			}
		}

		public ICommand GoToFlowCommand {
			get {
				return _goToFlowCommand;
			}
		}
	}
}
