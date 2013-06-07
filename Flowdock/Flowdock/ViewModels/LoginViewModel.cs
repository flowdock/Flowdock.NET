using Caliburn.Micro;
using Flowdock.Client.Context;
using Flowdock.Extensions;
using Flowdock.Settings;
using System.Windows.Input;

namespace Flowdock.ViewModels {
	public class LoginViewModel : PropertyChangedBase {
		private IFlowdockContext _context;
		private IAppSettings _settings;
		private INavigationService _navigationService;

		private string _errorMessage;
		private LoginCommand _loginCommand;

		private void OnLoggedIn(object sender, LoggedInEventArgs e) {
			LoginMessage = e.FailureMessage;

			if (e.Success) {
				_navigationService.UriFor<LobbyViewModel>().Navigate();
			}
		}

		public LoginViewModel(IFlowdockContext context, IAppSettings settings, INavigationService navigationService) {
			_context = context.ThrowIfNull("context");
			_settings = settings.ThrowIfNull("storage");
			_navigationService = navigationService.ThrowIfNull("navigationService");

			_loginCommand = new LoginCommand(this, context);
			_loginCommand.LoggedIn += OnLoggedIn;
		}

		public string Username {
			get {
				return _settings.Username;
			}
			set {
				_settings.Username = value;
				NotifyOfPropertyChange(() => Username);
			}
		}

		public string Password {
			get {
				return _settings.Password;
			}
			set {
				_settings.Password = value;
				NotifyOfPropertyChange(() => Password);
			}
		}

		public string LoginMessage {
			get {
				return _errorMessage;
			}
			private set {
				_errorMessage = value;
				NotifyOfPropertyChange(() => LoginMessage);
			}
		}

		public ICommand LoginCommand {
			get {
				return _loginCommand;
			}
		}
	}
}
