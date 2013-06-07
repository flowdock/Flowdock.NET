using Caliburn.Micro;
using Flowdock.Client.Context;
using Flowdock.Client.Domain;
using Flowdock.Client.Stream;
using Flowdock.Extensions;
using Flowdock.Services.Progress;
using Flowdock.Settings;
using Flowdock.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using Message = Flowdock.Client.Domain.Message;

namespace Flowdock.ViewModels {
	public class FlowViewModel : PropertyChangedBase, IActivate {
		private static readonly Random rand = new Random();

		private const int MessageLimit = 20;

		private IFlowdockContext _context;
		private IAppSettings _settings;
		private IProgressService _progressService;

		private ObservableCollection<User> _users;
		private ObservableCollection<MessageViewModel> _messages;

		private FlowStreamingConnection _stream;

		private string _newMessage;

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

		private void EditMessage(Message msg) {
			if (_messages == null) {
				return;
			}

			var existingMsg = _messages.FirstOrDefault(m => m.Id == msg.MessageContent.Message);

			if (existingMsg != null) {
				existingMsg.Body = msg.MessageContent.UpdatedContent;
				existingMsg.WasEdited = true;
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
				} else if (msg.Event == "message-edit") {
					EditMessage(msg);
				}
			});
		}

		private void StartStream() {
			_stream = new FlowStreamingConnection();
			_stream.Start(_settings.Username, _settings.Password, FlowId, OnMessageReceived);
		}

		private void StopStream() {
			if (_stream != null) {
				_stream.Stop();
			}
		}

		private async void LoadFlow() {
			_progressService.Show();

			try {
				// load the flow to grab the users
				Flow flow = await _context.GetFlow(FlowId);
				Users = new ObservableCollection<User>(flow.Users);


				IEnumerable<Message> messages = await _context.GetMessagesForFlow(FlowId);

				if (messages != null) {
					Messages = new ObservableCollection<MessageViewModel>(
						messages.Where(m => m.Displayable).Select(m => new MessageViewModel(m, GetThreadColor(m)))
					);
				}

				TrimMessages();

				AssociateAvatarsToMessages();
				AssignThreadStartersTheirColor();

				StartStream();
			} finally {
				_progressService.Hide();
				NotifyOfPropertyChange(() => CanSendMessageToFlow);
			}
		}

		public string FlowId { get; set; }
		public string FlowName { get; set; }

		public FlowViewModel(IAppSettings settings, IFlowdockContext context, IProgressService progressService) {
			_settings = settings.ThrowIfNull("settings");
			_context = context.ThrowIfNull("context");
			_progressService = progressService.ThrowIfNull("progressService");
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
				NotifyOfPropertyChange(() => Users);
				NotifyOfPropertyChange(() => CanShowUsers);
			}
		}

		public ObservableCollection<MessageViewModel> Messages {
			get {
				return _messages;
			}
			private set {
				_messages = value;
				NotifyOfPropertyChange(() => Messages);
				NotifyOfPropertyChange(() => FlowStatusMessage);
			}
		}

		public string Name {
			get {
				return FlowName;
			}
		}

		public string NewMessage {
			get {
				return _newMessage;
			}
			set {
				_newMessage = value;
				NotifyOfPropertyChange(() => NewMessage);
			}
		}

		public void SendMessageToFlow() {
			_context.SendMessage(FlowId, NewMessage);
			NewMessage = "";
		}

		public bool CanSendMessageToFlow {
			get {
				return !_progressService.IsVisible;
			}
		}

		public void ShowUsers() {
			System.Windows.MessageBox.Show("Show Users");
		}

		public bool CanShowUsers {
			get {
				return Users != null;
			}
		}

		public string FlowStatusMessage {
			get {
				if (_progressService.IsVisible) {
					return "loading...";
				}
				if (Messages != null && Messages.Count == 0) {
					return "empty flow";
				}
				return "";
			}
		}

		public void Activate() {
			LoadFlow();
		}

		public event EventHandler<ActivationEventArgs> Activated;

		public bool IsActive {
			get { 
				return Messages != null && Users != null; 
			}
		}
	}
}
