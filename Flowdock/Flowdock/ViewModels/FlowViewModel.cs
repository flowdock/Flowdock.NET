using Flowdock.Client.Context;
using Flowdock.Client.Domain;
using Flowdock.Client.Stream;
using Flowdock.Extensions;
using Flowdock.Settings;
using Flowdock.Util;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;

namespace Flowdock.ViewModels {
	public class FlowViewModel : ViewModelBase {
		private string _flowId;
		private string _flowName;
		private IFlowdockContext _context;

		private ObservableCollection<User> _users;
		private bool _gettingUsers;
		private ObservableCollection<MessageViewModel> _messages;

		private string _newMessage;
		private SendMessageCommand _sendMessageCommand;

		private void AssociateAvatarsToMessages() {
			if (_messages != null) {
				foreach (var msg in _messages) {
					FindAvatar(msg);
				}
			}
		}

		private async void GetUsers() {
			if (_users == null && !_gettingUsers) {
				_gettingUsers = true;
				Flow flow = await _context.GetFlow(_flowId);
				Users = new ObservableCollection<User>(flow.Users);
				AssociateAvatarsToMessages();
			}
		}

		private void FindAvatar(MessageViewModel msg) {
			var user = _users.FirstOrDefault(u => u.Id == msg.UserId);
			if (user != null) {
				msg.Avatar = user.Avatar;
			}
		}

		private async void GetMessages() {
			IEnumerable<Message> messages = await _context.GetMessagesForFlow(_flowId);

			if (messages != null) {
				Messages = new ObservableCollection<MessageViewModel>(messages
					.Where(m => m.Displayable)
					.Select(m => new MessageViewModel(m))
				);
			}

			var appsettings = new AppSettings();
			new FlowStreamingConnection().Start(appsettings.Username, appsettings.Password, _flowId, (msg) => {
				UIThread.Invoke(() => {
					if (msg.Displayable) {
						var viewModel = new MessageViewModel(msg);
						FindAvatar(viewModel);
						if (Messages == null) {
							Messages = new ObservableCollection<MessageViewModel>(new MessageViewModel[] { viewModel });
						} else {
							Messages.Add(viewModel);
						}
					}
				});
			});
			GetUsers();
		}

		public FlowViewModel(string flowId, string flowName, IFlowdockContext context) {
			_flowId = flowId.ThrowIfNull("flowId");
			_flowName = flowName.ThrowIfNull("flowName");
			_context = context.ThrowIfNull("context");
			_sendMessageCommand = new SendMessageCommand(this, _context, _flowId);
		}

		public FlowViewModel(string flowId, string flowName)
			: this(flowId, flowName, new LoggedInFlowdockContext()) {
		}

		public ObservableCollection<User> Users {
			get {
				if (_users == null) {
					GetUsers();
				}
				return _users;
			}
			private set {
				_users = value;
				OnPropertyChanged(() => Users);
			}
		}

		public ObservableCollection<MessageViewModel> Messages {
			get {
				if (_messages == null) {
					GetMessages();
				}
				return _messages;
			}
			private set {
				_messages = value;
				OnPropertyChanged(() => Messages);
			}
		}

		public string Name {
			get {
				return _flowName;
			}
		}

		public string NewMessage {
			get {
				return _newMessage;
			}
			set {
				_newMessage = value;
				OnPropertyChanged(() => NewMessage);
			}
		}

		public ICommand SendMessageCommand {
			get {
				return _sendMessageCommand;
			}
		}
	}
}
