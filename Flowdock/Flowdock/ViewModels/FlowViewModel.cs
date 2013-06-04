using Flowdock.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flowdock.Extensions;
using System.Collections.ObjectModel;

namespace Flowdock.ViewModels {
	public class FlowViewModel : ViewModelBase {
		private Flow _flow;
		private IFlowdockContext _context;

		private ObservableCollection<User> _users;

		private async void GetUsers() {
			Flow reloadedFlow = await _context.GetFlow(_flow.Id);
			_flow = reloadedFlow;
			Users = new ObservableCollection<User>(_flow.Users);
		}

		public FlowViewModel(Flow flow, IFlowdockContext context) {
			_flow = flow.ThrowIfNull("flow");
			_context = context.ThrowIfNull("context");

			GetUsers();
		}

		public ObservableCollection<User> Users {
			get {
				return _users;
			}
			private set {
				_users = value;
				OnPropertyChanged(() => Users);
				OnPropertyChanged(() => UserCount);
			}
		}

		public int UserCount {
			get {
				if(_users == null) {
					return 0;
				}
				return _users.Count;
			}
		}

		public string Name {
			get {
				return _flow.Name;
			}
		}
	}
}
