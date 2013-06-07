using Caliburn.Micro;
using Flowdock.Client.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Flowdock.ViewModels {
	public class UsersViewModel : PropertyChangedBase {

		private ObservableCollection<User> _onlineUsers;
		private ObservableCollection<User> _offlineUsers;

		public UsersViewModel(IEnumerable<User> users) {
			OnlineUsers = new ObservableCollection<User>(users);
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
	}
}
