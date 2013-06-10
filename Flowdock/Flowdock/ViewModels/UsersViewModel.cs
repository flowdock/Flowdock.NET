using Caliburn.Micro;
using Flowdock.Client.Context;
using Flowdock.Client.Domain;
using Flowdock.Services.Progress;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Flowdock.Extensions;
using System.Linq;

namespace Flowdock.ViewModels {
	public class UsersViewModel : PropertyChangedBase, IActivate {
		public string FlowId { get; set; }

		private IFlowdockContext _context;
		private IProgressService _progressService;

		private string _flowName;

		private ObservableCollection<User> _onlineUsers;
		private ObservableCollection<User> _offlineUsers;

		public UsersViewModel(IFlowdockContext context, IProgressService progressService) {
			_context = context;
			_progressService = progressService.ThrowIfNull("progressService");
		}

		public string FlowName {
			get {
				return _flowName;
			}
			private set {
				_flowName = value;
				NotifyOfPropertyChange(() => FlowName);
			}
		}

		public ObservableCollection<User> OnlineUsers {
			get {
				return _onlineUsers;
			}
			private set {
				_onlineUsers = value;
				NotifyOfPropertyChange(() => OnlineUsers);
			}
		}

		public ObservableCollection<User> OfflineUsers {
			get {
				return _offlineUsers;
			}
			private set {
				_offlineUsers = value;
				NotifyOfPropertyChange(() => OfflineUsers);
			}
		}

		public async void Activate() {
			_progressService.Show("");

			try {
				Flow flow = await _context.GetFlow(FlowId);
				FlowName = flow.Name;
				OnlineUsers = new ObservableCollection<User>(flow.Users.OrderBy((u) => u.Nick)); // TODO: separate online and offline users
			} finally {
				_progressService.Hide();
			}
		}

		public event System.EventHandler<ActivationEventArgs> Activated;

		public bool IsActive {
			get { return OnlineUsers != null || OfflineUsers != null; }
		}
	}
}
