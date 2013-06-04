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

		private ObservableCollection<Flow> _flows;

		private async void GetFlows() {
			IEnumerable<Flow> flows = await _context.GetJoinedFlows();
			Flows = new ObservableCollection<Flow>(flows);
		}

		public FlowsViewModel(IFlowdockContext context) {
			_context = context.ThrowIfNull("context");

			GetFlows();
		}

		public ObservableCollection<Flow> Flows {
			get {
				return _flows;
			}
			private set {
				_flows = value;
				OnPropertyChanged(() => Flows);
			}
		}
	}
}
