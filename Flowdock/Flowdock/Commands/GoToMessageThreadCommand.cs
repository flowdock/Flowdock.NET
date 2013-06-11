using Flowdock.Client.Domain;
using Flowdock.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Flowdock.Extensions;

namespace Flowdock.Commands {
    public class GoToMessageThreadCommand : ICommand {
        private string _flowId;
        private INavigationManager _navigationManager;

        public GoToMessageThreadCommand(string flowId, INavigationManager navigationManager) {
            _flowId = flowId.ThrowIfNull("flow");
            _navigationManager = navigationManager.ThrowIfNull("navigationManager");
        }

        public bool CanExecute(object threadId) {
            return threadId != null;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object threadId) {
            _navigationManager.GoToMessageThread(_flowId, (int)threadId);
        }
    }
}
