using Caliburn.Micro;
using Flowdock.Client.Context;
using Flowdock.Client.Domain;
using Flowdock.Extensions;
using Flowdock.Services.Navigation;
using Flowdock.Services.Progress;
using Flowdock.Settings;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Flowdock.ViewModels {
	public class LobbyViewModel : PropertyChangedBase {
		private IFlowdockContext _context;
		private INavigationManager _navigationManager;
		private IAppSettings _appSettings;
		private IProgressService _progressService;

		private ObservableCollection<LobbyFlowViewModel> _flows;

		private async void GetFlows() {
			_progressService.Show("");
			try {
				IEnumerable<Flow> flows = await _context.GetCurrentFlows();

				if (flows != null) {
					Flows = new ObservableCollection<LobbyFlowViewModel>(flows
						.Where(f => f.Open)// && f.Name.Contains("Testing"))
						.Select(f => new LobbyFlowViewModel(f, _navigationManager, _appSettings))
						//.Take(1)
					);
				}
			} finally {
				_progressService.Hide();
			}
		}

		public LobbyViewModel(IFlowdockContext context, INavigationManager navigationManager, IAppSettings appSettings, IProgressService progressService) {
			_context = context.ThrowIfNull("context");
			_navigationManager = navigationManager.ThrowIfNull("navigationManager");
			_appSettings = appSettings.ThrowIfNull("appSettings");
			_progressService = progressService.ThrowIfNull("progressService");

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
	}
}
