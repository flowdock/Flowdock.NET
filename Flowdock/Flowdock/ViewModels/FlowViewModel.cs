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
		private const int MessageLimit = 20;

		private Flow _flow;
		private IFlowdockContext _context;
		private IAppSettings _settings;

		private ObservableCollection<User> _users;
		private ObservableCollection<MessageViewModel> _messages;

		private FlowStreamingConnection _stream;

		private string _newMessage;
		private SendMessageCommand _sendMessageCommand;

		private void TrimMessages() {
			while (_messages.Count > MessageLimit) {
				_messages.RemoveAt(0);
			}
		}

		private void AssociateAvatarsToMessages() {
			if (_messages != null) {
				foreach (var msg in _messages) {
					FindAvatar(msg);
				}
			}
		}

		private void FindAvatar(MessageViewModel msg) {
			var user = _users.FirstOrDefault(u => u.Id == msg.UserId);
			if (user != null) {
				msg.Avatar = user.Avatar;
			}
		}

		private void OnMessageReceived(Message msg) {
			UIThread.Invoke(() => {
				if (msg.Displayable) {
					var viewModel = new MessageViewModel(msg);
					FindAvatar(viewModel);
					Messages.Add(viewModel);
					TrimMessages();
				}
			});
		}

		private void StartStream() {
			_stream = new FlowStreamingConnection();
			_stream.Start(_settings.Username, _settings.Password, _flow.Id, OnMessageReceived);
		}

		private void StopStream() {
			_stream.Stop();
		}

		private async void LoadFlow() {
			// load the flow to grab the users
			Flow flow = await _context.GetFlow(_flow.Id);
			Users = new ObservableCollection<User>(flow.Users);


			IEnumerable<Message> messages = await _context.GetMessagesForFlow(_flow.Id);

			if (messages != null) {
				Messages = new ObservableCollection<MessageViewModel>(
					messages.Where(m => m.Displayable).Select(m => new MessageViewModel(m))
				);
			}

			TrimMessages();

			AssociateAvatarsToMessages();

			StartStream();
		}

		public FlowViewModel(IAppSettings settings, IFlowdockContext context) {
			_settings = settings.ThrowIfNull("settings");
			_context = context.ThrowIfNull("context");
			_flow = _settings.CurrentFlow;

			_sendMessageCommand = new SendMessageCommand(this, _context, _flow.Id);

			LoadFlow();
		}

		public FlowViewModel()
			: this(new AppSettings(), new LoggedInFlowdockContext()) {
		}

		public void Unload() {
			StopStream();
			Users = null;
			Messages = null;
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

		public ObservableCollection<MessageViewModel> Messages {
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
