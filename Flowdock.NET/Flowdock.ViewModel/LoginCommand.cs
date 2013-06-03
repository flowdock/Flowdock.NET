using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Flowdock.Extensions;
using Flowdock.Domain;
using Flowdock.Data;

namespace Flowdock.ViewModel {
	public class LoginCommand : ICommand {
		private IFlowdockContext _context;
		private LoginViewModel _source;

		public event EventHandler CanExecuteChanged;

		private void OnSourcePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if (CanExecuteChanged != null) {
				CanExecuteChanged(this, EventArgs.Empty);
			}
		}

		public LoginCommand(LoginViewModel source) {
			_source = source.ThrowIfNull("source");

			_source.PropertyChanged += OnSourcePropertyChanged;

			_context = new FlowdockContext();
		}

		public bool CanExecute(object parameter) {
			return !string.IsNullOrWhiteSpace(_source.Username)
				&& !string.IsNullOrWhiteSpace(_source.Password);
		}

		public async void Execute(object parameter) {
			_source.LoginMessage = "Logging in...";
			_source.LoginMessage = await _context.Login(_source.Username, _source.Password);
		}
	}
}
