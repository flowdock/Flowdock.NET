using Flowdock.Client.Context;
using Flowdock.Client.Domain;
using Flowdock.Extensions;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Flowdock.ViewModels {
	public class SendMessageCommand : ICommand {
		private FlowViewModel _source;
		private IFlowdockContext _context;
		private string _flowId;

		private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e) {
			if (CanExecuteChanged != null) {
				CanExecuteChanged(this, EventArgs.Empty);
			}
		}

		public event EventHandler CanExecuteChanged;

		public SendMessageCommand(FlowViewModel source, IFlowdockContext context, string flowId) {
			_source = source.ThrowIfNull("source");
			_context = context.ThrowIfNull("context");
			_flowId = flowId.ThrowIfNull("flow");

			_source.PropertyChanged += OnSourcePropertyChanged;
		}

		public bool CanExecute(object parameter) {
			return !string.IsNullOrWhiteSpace(_source.NewMessage);
		}

		public void Execute(object parameter) {
			_context.SendMessage(_flowId, _source.NewMessage);
			_source.NewMessage = "";
		}
	}
}
