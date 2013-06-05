using Flowdock.Client.Context;
using Flowdock.Client.Domain;
using Flowdock.Client.Stream;
using Flowdock.Extensions;
using Flowdock.Settings;
using Flowdock.Util;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Flowdock.ViewModels {
	public class FlowViewModel : ViewModelBase {
		private Flow _flow;
		private IFlowdockContext _context;

		private ObservableCollection<User> _users;
		private ObservableCollection<Message> _messages;

		private string _newMessage;
		private SendMessageCommand _sendMessageCommand;

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

			var appsettings = new AppSettings();
			new FlowStreamingConnection().Start(appsettings.Username, appsettings.Password, _flow, (msg) => {
				UIThread.Invoke(() => {
					if (Messages == null) {
						Messages = new ObservableCollection<Message>(new Message[] { msg });
					} else {
						Messages.Add(msg);
					}
				});
			});
		}

		public FlowViewModel(Flow flow, IFlowdockContext context) {
			_flow = flow.ThrowIfNull("flow");
			_context = context.ThrowIfNull("context");
			_sendMessageCommand = new SendMessageCommand(this, _context, _flow);
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

		public ObservableCollection<Message> Messages {
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
