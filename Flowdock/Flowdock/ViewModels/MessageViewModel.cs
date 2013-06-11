using Caliburn.Micro;
using Flowdock.Client.Domain;
using Flowdock.Commands;
using Flowdock.Extensions;
using System;
using System.Windows.Input;
using System.Windows.Media;
using Message = Flowdock.Client.Domain.Message;
using System.Linq;
using Flowdock.Services.Navigation;

namespace Flowdock.ViewModels {
	public class MessageViewModel : PropertyChangedBase {
		private Message _message;
		private Uri _avatar;
		private Color? _threadColor;
		private bool _wasEdited;
        private GoToMessageThreadCommand _goToMessageThreadCommand;

		public MessageViewModel(Message message, string flowId, Color? threadColor, INavigationManager navigationManager) {
			_message = message.ThrowIfNull("message");
			_threadColor = threadColor;
            _goToMessageThreadCommand = new GoToMessageThreadCommand(flowId, navigationManager);
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

        public ICommand GoToMessageThreadCommand {
            get {
                return _goToMessageThreadCommand;
            }
        }

        public int? MessageThreadId {
            get {
                var threadTag = _message.Tags.FirstOrDefault(t => t.StartsWith("influx"));

                if (threadTag == null) {
                    return null;
                }

                return int.Parse(threadTag.Replace("influx:", ""));
            }
        }
	}
}
