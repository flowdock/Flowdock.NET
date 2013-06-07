using Caliburn.Micro;
using Flowdock.Client.Domain;
using System.Windows.Input;

namespace Flowdock.ViewModels {
	public class LobbyFlowViewModel : PropertyChangedBase {
		private Flow _flow;
		private GoToFlowCommand _goToFlowCommand;

		public LobbyFlowViewModel(Flow flow) {
			_flow = flow;
			_goToFlowCommand = new GoToFlowCommand(flow);
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
