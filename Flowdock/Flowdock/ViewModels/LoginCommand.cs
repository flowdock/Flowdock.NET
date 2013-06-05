using Flowdock.Client.Context;
using Flowdock.Extensions;
using System;
using System.Windows.Input;

namespace Flowdock.ViewModels {
	public class LoginCommand : ICommand {
		private IFlowdockContext _context;
		private LoginViewModel _source;

		public event EventHandler CanExecuteChanged;
		public event EventHandler<LoggedInEventArgs> LoggedIn;

		private void OnSourcePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if (CanExecuteChanged != null) {
				CanExecuteChanged(this, EventArgs.Empty);
			}
		}

		public LoginCommand(LoginViewModel source)
			: this(source, new FlowdockContext()) {
		}

		public LoginCommand(LoginViewModel source, IFlowdockContext context) {
			_source = source.ThrowIfNull("source");
			_context = context.ThrowIfNull("context");

			_source.PropertyChanged += OnSourcePropertyChanged;
		}

		public bool CanExecute(object parameter) {
			return !string.IsNullOrWhiteSpace(_source.Username)
				&& !string.IsNullOrWhiteSpace(_source.Password);
		}

		public async void Execute(object parameter) {
			if (CanExecute(null)) {
				var loginResult = await _context.Login(_source.Username, _source.Password);

				if (LoggedIn != null) {
					LoggedIn(this, new LoggedInEventArgs(loginResult));
				}
			}
		}
	}
}
