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
		private Flow _flow;
		private IFlowdockContext _context;
		private IAppSettings _settings;

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
				Flow flow = await _context.GetFlow(_flow.Id);
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
			IEnumerable<Message> messages = await _context.GetMessagesForFlow(_flow.Id);

			if (messages != null) {
				Messages = new ObservableCollection<MessageViewModel>(messages
					.Where(m => m.Displayable)
					.Select(m => new MessageViewModel(m))
				);
			}

			var appsettings = new AppSettings();
			new FlowStreamingConnection().Start(appsettings.Username, appsettings.Password, _flow.Id, (msg) => {
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

		public FlowViewModel(IAppSettings settings, IFlowdockContext context) {
			_settings = settings.ThrowIfNull("settings");
			_context = context.ThrowIfNull("context");
			_flow = _settings.CurrentFlow;

			_sendMessageCommand = new SendMessageCommand(this, _context, _flow.Id);
		}

		public FlowViewModel()
			: this(new AppSettings(), new LoggedInFlowdockContext()) {
		}

		public ObservableCollection<User> Users {
			get {
				GetUsers();
				return _users;
			}
			private set {
				_users = value;
				OnPropertyChanged(() => Users);
			}
		}

		public ObservableCollection<MessageViewModel> Messages {
			get {
				GetMessages();
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
