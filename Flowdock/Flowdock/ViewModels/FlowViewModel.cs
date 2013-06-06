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
using Flowdock.MessageBoxes;
using System.Windows.Media;
using System;

namespace Flowdock.ViewModels {
	public class FlowViewModel : ViewModelBase {
		private static readonly Random rand = new Random();

		private const int MessageLimit = 20;

		private bool _isLoading = false;

		private Flow _flow;
		private IFlowdockContext _context;
		private IAppSettings _settings;

		private ObservableCollection<User> _users;
		private ObservableCollection<MessageViewModel> _messages;

		private FlowStreamingConnection _stream;

		private string _newMessage;
		private SendMessageCommand _sendMessageCommand;
		private ShowUsersCommand _showUsersCommand;

		private Dictionary<string, Color> _threadColors = new Dictionary<string, Color>();

		private void AssignThreadStartersTheirColor() {
			if (_messages == null) {
				return;
			}

			foreach (var title in _threadColors.Keys) {
				MessageViewModel msg = _messages.FirstOrDefault(m => m.Body == title);

				if (msg != null) {
					msg.ThreadColor = _threadColors[title];
				}
			}
		}

		private Color? GetThreadColor(Message msg) {
			if(msg.MessageContent == null || msg.MessageContent.Title == null) {
				return null;
			}

			if (!_threadColors.ContainsKey(msg.MessageContent.Title)) {
				Color newColor = Color.FromArgb(255, (byte)rand.Next(5, 256), (byte)rand.Next(5, 256), (byte)rand.Next(5, 256));
				_threadColors[msg.MessageContent.Title] = newColor;
			}

			var color = _threadColors[msg.MessageContent.Title];
			return color;
		}

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
					var viewModel = new MessageViewModel(msg, GetThreadColor(msg));
					FindAvatar(viewModel);
					Messages.Add(viewModel);
					TrimMessages();

					// TODO: this is very inefficient
					AssignThreadStartersTheirColor();
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
			IsLoading = true;

			// load the flow to grab the users
			_flow = await _context.GetFlow(_flow.Id);
			Users = new ObservableCollection<User>(_flow.Users);


			IEnumerable<Message> messages = await _context.GetMessagesForFlow(_flow.Id);

			if (messages != null) {
				Messages = new ObservableCollection<MessageViewModel>(
					messages.Where(m => m.Displayable).Select(m => new MessageViewModel(m, GetThreadColor(m)))
				);
			}

			TrimMessages();

			AssociateAvatarsToMessages();
			AssignThreadStartersTheirColor();

			StartStream();
			IsLoading = false;
		}

		public FlowViewModel(IAppSettings settings, IFlowdockContext context) {
			_settings = settings.ThrowIfNull("settings");
			_context = context.ThrowIfNull("context");

			_flow = _settings.CurrentFlow;

			_sendMessageCommand = new SendMessageCommand(this, _context, _flow.Id);
			_showUsersCommand = new ShowUsersCommand(this);

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
				OnPropertyChanged(() => FlowStatusMessage);
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

		public ICommand ShowUsersCommand {
			get {
				return _showUsersCommand;
			}
		}

		public bool IsLoading {
			get {
				return _isLoading;
			}
			private set {
				_isLoading = value;
				OnPropertyChanged(() => IsLoading);
				OnPropertyChanged(() => FlowStatusMessage);
			}
		}

		public string FlowStatusMessage {
			get {
				if (IsLoading) {
					return "loading...";
				}
				if (Messages != null && Messages.Count == 0) {
					return "empty flow";
				}
				return "";
			}
		}
	}
}
