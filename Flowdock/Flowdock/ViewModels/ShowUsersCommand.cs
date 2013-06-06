using Flowdock.Client.Domain;
using Flowdock.MessageBoxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Flowdock.Extensions;

namespace Flowdock.ViewModels {
	public class ShowUsersCommand : ICommand {
		private FlowViewModel _source;
		private IMessageBoxService _messageBoxService;

		public event EventHandler CanExecuteChanged;

		private void OnSourcePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if (e.PropertyName == "Users" && CanExecuteChanged != null) {
				CanExecuteChanged(this, EventArgs.Empty);
			}
		}

		public ShowUsersCommand(FlowViewModel source, IMessageBoxService messageBoxService) {
			_source = source.ThrowIfNull("source");
			_messageBoxService = messageBoxService.ThrowIfNull("messageBoxService");

			_source.PropertyChanged += OnSourcePropertyChanged;
		}

		public ShowUsersCommand(FlowViewModel source)
			: this(source, new MessageBoxService()) {
		}

		public bool CanExecute(object parameter) {
			return _source.Users != null;
		}

		public void Execute(object parameter) {
			if (CanExecute(null)) {
				_messageBoxService.ShowUsersMessageBox(_source.Users);
			}
		}
	}
}
