using Caliburn.Micro;
using Flowdock.Client.Domain;
using Flowdock.Settings;
using System;
using System.Windows.Input;

namespace Flowdock.ViewModels {
	public class GoToFlowCommand : ICommand {
		private Flow _flow;
		private INavigationService _navigationService;
		private IAppSettings _appSettings;

		public GoToFlowCommand(Flow flow, INavigationService navigationService, IAppSettings settings) {
			_flow = flow;
			_navigationService = navigationService;
			_appSettings = settings;
		}

		public bool CanExecute(object parameter) {
			return true;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter) {
			_appSettings.CurrentFlow = _flow;
			_navigationService.UriFor<FlowViewModel>().Navigate();
		}
	}
}
