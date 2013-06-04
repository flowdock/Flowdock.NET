using Flowdock.Data;
using Flowdock.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Flowdock.Extensions;
using System.IO.IsolatedStorage;
using Flowdock.ViewModel.Storage;

namespace Flowdock.ViewModels {
	public class LoginViewModel : ViewModelBase {
		private const string PasswordKey = "User.Password.Key";
		private const string UsernameKey = "User.Username.Key";

		private IFlowdockContext _context;
		private IIsolatedStorageProxy _storage;

		private string _username;
		private string _password;
		private string _errorMessage;

		private void InitFromIsolatedStorage() {
			_username = _storage.Get<string>(UsernameKey);
			_password = _storage.Get<string>(PasswordKey);
		}

		public LoginViewModel(IFlowdockContext context, IIsolatedStorageProxy storage) {
			_context = context.ThrowIfNull("context");
			_storage = storage.ThrowIfNull("storage");

			InitFromIsolatedStorage();
		}

		public LoginViewModel() : this(new FlowdockContext(), new IsolatedStorageProxy()) {
		}

		public string Username {
			get {
				return _username;
			}
			set {
				_username = value;
				_storage.Put<string>(UsernameKey, value);
				OnPropertyChanged(() => Username);
			}
		}

		public string Password {
			get {
				return _password;
			}
			set {
				_password = value;
				_storage.Put<string>(PasswordKey, value);
				OnPropertyChanged(() => Password);
			}
		}

		public string LoginMessage {
			get {
				return _errorMessage;
			}
			set {
				_errorMessage = value;
				OnPropertyChanged(() => LoginMessage);
			}
		}

		public ICommand LoginCommand {
			get {
				return new LoginCommand(this);
			}
		}
	}
}
