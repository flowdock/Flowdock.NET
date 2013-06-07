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
	public class LobbyViewModel : PropertyChangedBase, IActivate {
		private IFlowdockContext _context;
		private INavigationManager _navigationManager;
		private IProgressService _progressService;

		private ObservableCollection<LobbyFlowViewModel> _flows;

		private async void GetFlows() {
			_progressService.Show("");
			try {
				IEnumerable<Flow> flows = await _context.GetCurrentFlows();

				if (flows != null) {
					Flows = new ObservableCollection<LobbyFlowViewModel>(flows
						.Where(f => f.Open)// && f.Name.Contains("Testing"))
						.Select(f => new LobbyFlowViewModel(f, _navigationManager))
						//.Take(1)
					);
				}
			} finally {
				_progressService.Hide();
			}
		}

		public LobbyViewModel(IFlowdockContext context, INavigationManager navigationManager, IProgressService progressService) {
			_context = context.ThrowIfNull("context");
			_navigationManager = navigationManager.ThrowIfNull("navigationManager");
			_progressService = progressService.ThrowIfNull("progressService");
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

		public void Activate() {
			GetFlows();
		}

		public event System.EventHandler<ActivationEventArgs> Activated;

		public bool IsActive {
			get {
				return Flows != null;
			}
		}
	}
}
