using Flowdock.Data;
using Flowdock.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Flowdock.Extensions;

namespace Flowdock.ViewModel {
	public class LoginViewModel : ViewModelBase {
		private IFlowdockContext _context;
		private string _username;
		private string _password;
		private string _errorMessage;

		public LoginViewModel(IFlowdockContext context) {
			_context = context.ThrowIfNull("context");
		}

		public LoginViewModel() : this(new FlowdockContext()) {
		}

		public string Username {
			get {
				return _username;
			}
			set {
				_username = value;
				OnPropertyChanged(() => Username);
			}
		}

		public string Password {
			get {
				return _password;
			}
			set {
				_password = value;
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
