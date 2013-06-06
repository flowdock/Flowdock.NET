using Flowdock.Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flowdock.Extensions;
using System.Windows.Media;

namespace Flowdock.ViewModels {
	public class MessageViewModel :ViewModelBase {
		private Message _message;
		private Uri _avatar;
		private Color? _threadColor;
		private bool _wasEdited;

		public MessageViewModel(Message message, Color? threadColor) {
			_message = message.ThrowIfNull("message");
			_threadColor = threadColor;
		}

		public bool WasEdited {
			get {
				return _wasEdited;
			}
			set {
				_wasEdited = value;
				OnPropertyChanged(() => WasEdited);
			}
		}

		public string Body {
			get {
				return _message.ExtractedBody;
			}
			set {
				_message.ExtractedBody = value;
				OnPropertyChanged(() => Body);
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
				OnPropertyChanged(() => Avatar);
			}
		}

		public Color? ThreadColor {
			get {
				return _threadColor;
			}
			set {
				_threadColor = value;
				OnPropertyChanged(() => ThreadColor);
			}
		}

		public DateTime TimeStamp {
			get {
				return _message.TimeStamp;
			}
		}
	}
}
