using Flowdock.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flowdock.Extensions;
using System.Collections.ObjectModel;

namespace Flowdock.ViewModels {
	public class FlowsViewModel : ViewModelBase {
		private IFlowdockContext _context;

		private ObservableCollection<FlowViewModel> _flows;
		private bool _isLoading;

		private async void GetFlows() {
			IsLoading = true;
			IEnumerable<Flow> flows = await _context.GetCurrentFlows();

			if (flows != null) {
				Flows = new ObservableCollection<FlowViewModel>(flows.Where(f => f.Open).Select(f => new FlowViewModel(f, _context)));
			}
			IsLoading = false;
		}

		public FlowsViewModel(IFlowdockContext context) {
			_context = context.ThrowIfNull("context");

			GetFlows();
		}

		public FlowsViewModel()
			: this(new LoggedInFlowdockContext()) {
		}

		public ObservableCollection<FlowViewModel> Flows {
			get {
				return _flows;
			}
			private set {
				_flows = value;
				OnPropertyChanged(() => Flows);
			}
		}

		public bool IsLoading {
			get {
				return _isLoading;
			}
			set {
				_isLoading = value;
				OnPropertyChanged(() => IsLoading);
			}
		}
	}
}
