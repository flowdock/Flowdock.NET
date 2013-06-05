using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Flowdock.Extensions;
using Flowdock.Navigation;
using Flowdock.Domain;
using Flowdock.Settings;

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
				_navigationManager.GoToFlows();
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
