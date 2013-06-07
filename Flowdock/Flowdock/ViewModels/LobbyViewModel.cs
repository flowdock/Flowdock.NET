using Caliburn.Micro;
using Flowdock.Client.Context;
using Flowdock.Client.Domain;
using Flowdock.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Flowdock.ViewModels {
	public class LobbyViewModel : PropertyChangedBase {
		private IFlowdockContext _context;

		private ObservableCollection<LobbyFlowViewModel> _flows;
		private bool _isLoading;

		private async void GetFlows() {
			IsLoading = true;
			IEnumerable<Flow> flows = await _context.GetCurrentFlows();

			if (flows != null) {
				Flows = new ObservableCollection<LobbyFlowViewModel>(flows
					.Where(f => f.Open)// && f.Name.Contains("Testing"))
					.Select(f => new LobbyFlowViewModel(f))
					//.Take(1)
				);
			}
			IsLoading = false;
		}

		public LobbyViewModel(IFlowdockContext context) {
			_context = context.ThrowIfNull("context");

			GetFlows();
		}

		public LobbyViewModel()
			: this(new LoggedInFlowdockContext()) {
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
