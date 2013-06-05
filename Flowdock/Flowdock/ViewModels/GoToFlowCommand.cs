using Flowdock.Client.Domain;
using Flowdock.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Flowdock.ViewModels {
	public class GoToFlowCommand : ICommand {
		private Flow _flow;
		private INavigationManager _navigationManager;

		public GoToFlowCommand(Flow flow, INavigationManager navigationManager) {
			_flow = flow;
			_navigationManager = navigationManager;
		}

		public GoToFlowCommand(Flow flow)
			: this(flow, new NavigationManager()) {
		}

		public bool CanExecute(object parameter) {
			return true;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter) {
			_navigationManager.GoToFlow(_flow.Id);
		}
	}
}
