using Flowdock.Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Flowdock.ViewModels {
	public class LobbyFlowViewModel : ViewModelBase {
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
