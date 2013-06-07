using Flowdock.Client.Domain;
using Flowdock.Extensions;
using Flowdock.Services.Navigation;
using Flowdock.Settings;
using System;
using System.Windows.Input;

namespace Flowdock.ViewModels {
	public class GoToFlowCommand : ICommand {
		private Flow _flow;
		private INavigationManager _navigationManager;
		private IAppSettings _appSettings;

		public event EventHandler CanExecuteChanged;

		public GoToFlowCommand(Flow flow, INavigationManager navigationManager, IAppSettings settings) {
			_flow = flow.ThrowIfNull("flow");
			_navigationManager = navigationManager.ThrowIfNull("navigationManager");
			_appSettings = settings.ThrowIfNull("settings");
		}

		public bool CanExecute(object parameter) {
			return true;
		}

		public void Execute(object parameter) {
			_navigationManager.GoToFlow(_flow);
		}
	}
}
