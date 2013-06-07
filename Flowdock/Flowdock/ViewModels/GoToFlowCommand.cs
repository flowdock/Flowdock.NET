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

		public event EventHandler CanExecuteChanged;

		public GoToFlowCommand(Flow flow, INavigationService navigationService, IAppSettings settings) {
			_flow = flow;
			_navigationService = navigationService;
			_appSettings = settings;
		}

		public bool CanExecute(object parameter) {
			return true;
		}

		public void Execute(object parameter) {
			_navigationService.UriFor<FlowViewModel>()
				.WithParam<string>(fvm => fvm.FlowId, _flow.Id)
				.WithParam<string>(fvm => fvm.FlowName, _flow.Name)
				.Navigate();
		}
	}
}
