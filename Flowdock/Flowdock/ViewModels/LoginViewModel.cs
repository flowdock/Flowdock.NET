using Flowdock.Client.Context;
using Flowdock.Extensions;
using Flowdock.Navigation;
using Flowdock.Settings;
using System.Windows.Input;

namespace Flowdock.ViewModels {
	public class LoginViewModel : ViewModelBase {
		private IFlowdockContext _context;
		private IAppSettings _settings;
		private INavigationManager _navigationManager;

		private string _errorMessage;
		private LoginCommand _loginCommand;

		private void OnLoggedIn(object sender, LoggedInEventArgs e) {
			LoginMessage = e.FailureMessage;

			if (e.Success) {
				_navigationManager.GoToLobby();
			}
		}

		public LoginViewModel(IFlowdockContext context, IAppSettings settings, INavigationManager navigationManager) {
			_context = context.ThrowIfNull("context");
			_settings = settings.ThrowIfNull("storage");
			_navigationManager = navigationManager.ThrowIfNull("navigationManager");

			_loginCommand = new LoginCommand(this);
			_loginCommand.LoggedIn += OnLoggedIn;
		}

		public LoginViewModel() 
			: this(new FlowdockContext(), new AppSettings(), new NavigationManager()) {
		}

		public string Username {
			get {
				return _settings.Username;
			}
			set {
				_settings.Username = value;
				OnPropertyChanged(() => Username);
			}
		}

		public string Password {
			get {
				return _settings.Password;
			}
			set {
				_settings.Password = value;
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
