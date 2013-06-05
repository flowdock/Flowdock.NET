using Flowdock.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flowdock.Extensions;
using System.Collections.ObjectModel;
using Flowdock.Client.Domain;

namespace Flowdock.ViewModels {
	public class FlowViewModel : ViewModelBase {
		private Flow _flow;
		private IFlowdockContext _context;

		private ObservableCollection<User> _users;
		private ObservableCollection<Message> _messages;

		private async void GetUsers() {
			Flow reloadedFlow = await _context.GetFlow(_flow.Id);
			_flow = reloadedFlow;
			Users = new ObservableCollection<User>(_flow.Users);
		}

		private async void GetMessages() {
			IEnumerable<Message> messages = await _context.GetMessagesForFlow(_flow.Id);

			if (messages != null) {
				Messages = new ObservableCollection<Message>(messages);
			}
		}

		public FlowViewModel(Flow flow, IFlowdockContext context) {
			_flow = flow.ThrowIfNull("flow");
			_context = context.ThrowIfNull("context");

			GetUsers();
			GetMessages();
		}

		public ObservableCollection<User> Users {
			get {
				return _users;
			}
			private set {
				_users = value;
				OnPropertyChanged(() => Users);
			}
		}

		public ObservableCollection<Message> Messages {
			get {
				return _messages;
			}
			private set {
				_messages = value;
				OnPropertyChanged(() => Messages);
			}
		}

		public string Name {
			get {
				return _flow.Name;
			}
		}
	}
}
