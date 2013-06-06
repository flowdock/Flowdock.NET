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

		public MessageViewModel(Message message, Color? threadColor) {
			_message = message.ThrowIfNull("message");
			_threadColor = threadColor;
		}

		public string Body {
			get {
				return _message.ExtractedBody;
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
