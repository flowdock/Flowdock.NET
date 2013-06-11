using Caliburn.Micro;
using Flowdock.Client.Domain;
using Flowdock.Extensions;
using Flowdock.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Services.Navigation {
	public class NavigationManager : INavigationManager {
		private INavigationService _navigationService;

		public NavigationManager(INavigationService navigationService) {
			_navigationService = navigationService.ThrowIfNull("navigationService");
		}

		public void GoToLobby() {
			_navigationService.UriFor<LobbyViewModel>().Navigate();
		}

		public void GoToFlow(Flow flow) {
			flow.ThrowIfNull("flow");

			_navigationService.UriFor<FlowViewModel>()
				.WithParam<string>(fvm => fvm.FlowId, flow.Id)
				.WithParam<string>(fvm => fvm.FlowName, flow.Name)
				.Navigate();
		}

		public void GoToUsers(string flowId) {
			_navigationService.UriFor<UsersViewModel>()
				.WithParam<string>(uvm => uvm.FlowId, flowId)
				.Navigate();
		}

        public void GoToMessageThread(string flowId, int threadId) {
            _navigationService.UriFor<MessageThreadViewModel>()
                .WithParam<string>(mtvm => mtvm.FlowId, flowId)
                .WithParam<int>(mtvm => mtvm.ThreadId, threadId)
                .Navigate();
        }
	}
}
