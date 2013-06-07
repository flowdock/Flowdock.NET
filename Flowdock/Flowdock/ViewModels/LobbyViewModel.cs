using Caliburn.Micro;
using Flowdock.Client.Context;
using Flowdock.Client.Domain;
using Flowdock.Extensions;
using Flowdock.Settings;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Flowdock.ViewModels {
	public class LobbyViewModel : PropertyChangedBase {
		private IFlowdockContext _context;
		private INavigationService _navigationService;
		private IAppSettings _appSettings;

		private ObservableCollection<LobbyFlowViewModel> _flows;
		private bool _isLoading;

		private async void GetFlows() {
			IsLoading = true;
			IEnumerable<Flow> flows = await _context.GetCurrentFlows();

			if (flows != null) {
				Flows = new ObservableCollection<LobbyFlowViewModel>(flows
					.Where(f => f.Open)// && f.Name.Contains("Testing"))
					.Select(f => new LobbyFlowViewModel(f, _navigationService, _appSettings))
					//.Take(1)
				);
			}
			IsLoading = false;
		}

		public LobbyViewModel(IFlowdockContext context, INavigationService navigationService, IAppSettings appSettings) {
			_context = context.ThrowIfNull("context");
			_navigationService = navigationService.ThrowIfNull("navigationService");
			_appSettings = appSettings.ThrowIfNull("appSettings");

			GetFlows();
		}

		public ObservableCollection<LobbyFlowViewModel> Flows {
			get {
				return _flows;
			}
			private set {
				_flows = value;
				NotifyOfPropertyChange(() => Flows);
			}
		}

		public bool IsLoading {
			get {
				return _isLoading;
			}
			set {
				_isLoading = value;
				NotifyOfPropertyChange(() => IsLoading);
			}
		}
	}
}
