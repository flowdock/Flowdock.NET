using Caliburn.Micro;
using Flowdock.Client.Domain;
using Flowdock.Extensions;
using System;
using System.Windows.Media;

using Message = Flowdock.Client.Domain.Message;

namespace Flowdock.ViewModels {
	public class MessageViewModel : PropertyChangedBase {
		private Message _message;
		private Uri _avatar;
		private Color? _threadColor;
		private bool _wasEdited;

		public MessageViewModel(Message message, Color? threadColor) {
			_message = message.ThrowIfNull("message");
			_threadColor = threadColor;
		}

		public DateTime TimeStamp {
			get {
				return _message.TimeStamp;
			}
		}

		public int Id {
			get {
				return _message.Id;
			}
		}

		public int UserId {
			get {
				return _message.User;
			}
		}

		public Uri Avatar {
			get {
				return _avatar;
			}
			set {
				_avatar = value;
				NotifyOfPropertyChange(() => Avatar);
			}
		}

		public Color? ThreadColor {
			get {
				return _threadColor;
			}
			set {
				_threadColor = value;
				NotifyOfPropertyChange(() => ThreadColor);
			}
		}

		public bool WasEdited {
			get {
				return _wasEdited;
			}
			set {
				_wasEdited = value;
				NotifyOfPropertyChange(() => WasEdited);
			}
		}

		public string Body {
			get {
				return _message.ExtractedBody;
			}
			set {
				_message.ExtractedBody = value;
				NotifyOfPropertyChange(() => Body);
			}
		}
	}
}
