using Caliburn.Micro;
using Flowdock.Client.Context;
using Flowdock.Services.Navigation;
using Flowdock.Services.Progress;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Message = Flowdock.Client.Domain.Message;

using Flowdock.Extensions;

namespace Flowdock.ViewModels {
    public class MessageThreadViewModel : PropertyChangedBase, IActivate {
        private IFlowdockContext _context;
        private IProgressService _progressService;
        private INavigationManager _navigationManager;

        private ObservableCollection<MessageViewModel> _messages;

        public MessageThreadViewModel(IFlowdockContext context, INavigationManager navigationManager, IProgressService progressService) {
            _context = context.ThrowIfNull("context");
            _navigationManager = navigationManager.ThrowIfNull("navigationManager");
            _progressService = progressService.ThrowIfNull("progressService");
        }

        private async void LoadMessageThread() {
            _progressService.Show("");

            try {
                IEnumerable<Message> messages = await _context.GetMessagesForThread(FlowId, ThreadId);

                if (messages != null) {
                    Messages = new ObservableCollection<MessageViewModel>(
                        messages.Where(m => m.Displayable).Select(m => new MessageViewModel(m, FlowId, null, _navigationManager))
                    );
                }
            } finally {
                _progressService.Hide();
            }
        }

        public string FlowId { get; set; }
        public int ThreadId { get; set; }

        public ObservableCollection<MessageViewModel> Messages {
            get {
                return _messages;
            }
            private set {
                _messages = value;
                NotifyOfPropertyChange(() => Messages);
            }
        }

        public void Activate() {
            LoadMessageThread();
        }

        public event EventHandler<ActivationEventArgs> Activated;

        public bool IsActive {
            get {
                return Messages != null;
            }
        }
    }
}
