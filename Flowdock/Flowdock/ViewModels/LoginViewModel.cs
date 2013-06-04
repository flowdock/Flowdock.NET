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
using Flowdock.Navigation;

namespace Flowdock.ViewModels {
	public class LoginViewModel : ViewModelBase {
		private const string PasswordKey = "User.Password.Key";
		private const string UsernameKey = "User.Username.Key";

		private IFlowdockContext _context;
		private IIsolatedStorageProxy _storage;
		private INavigationManager _navigationManager;

		private string _errorMessage;
		private LoginCommand _loginCommand;

		private void OnLoggedIn(object sender, LoggedInEventArgs e) {
			LoginMessage = e.FailureMessage;

			if (e.Success) {
				Flowdock.App.Username = Username;
				Flowdock.App.Password = Password;
				_navigationManager.GoToFlows();
			}
		}

		public LoginViewModel(IFlowdockContext context, IIsolatedStorageProxy storage, INavigationManager navigationManager) {
			_context = context.ThrowIfNull("context");
			_storage = storage.ThrowIfNull("storage");
			_navigationManager = navigationManager.ThrowIfNull("navigationManager");

			_loginCommand = new LoginCommand(this);
			_loginCommand.LoggedIn += OnLoggedIn;

#if DEBUG
			_storage.Put<string>(UsernameKey, DebugLoginInfo.Username);
			_storage.Put<string>(PasswordKey, DebugLoginInfo.Password);
#endif
		}

		public LoginViewModel() 
			: this(new FlowdockContext(), new IsolatedStorageProxy(), new NavigationManager()) {
		}

		public string Username {
			get {
				return _storage.Get<string>(UsernameKey);
			}
			set {
				_storage.Put<string>(UsernameKey, value);
				OnPropertyChanged(() => Username);
			}
		}

		public string Password {
			get {
				return _storage.Get<string>(PasswordKey);
			}
			set {
				_storage.Put<string>(PasswordKey, value);
				OnPropertyChanged(() => Password);
			}
		}

		public string LoginMessage {
			get {
				return _errorMessage;
			}
			private set {
				_errorMessage = value;
				OnPropertyChanged(() => LoginMessage);
			}
		}

		public ICommand LoginCommand {
			get {
				return _loginCommand;
			}
		}
	}
}
