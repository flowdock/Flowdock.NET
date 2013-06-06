using Flowdock.Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flowdock.Extensions;

namespace Flowdock.ViewModels {
	public class MessageViewModel :ViewModelBase {
		private Message _message;
		private Uri _avatar;

		public MessageViewModel(Message message) {
			_message = message.ThrowIfNull("message");
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
	}
}
