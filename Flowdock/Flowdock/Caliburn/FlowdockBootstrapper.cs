using Caliburn.Micro;
using Flowdock.Client.Context;
using Flowdock.Client.Stream;
using Flowdock.Services.Message;
using Flowdock.Services.Navigation;
using Flowdock.Services.Progress;
using Flowdock.Settings;
using Flowdock.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Caliburn {
	public class FlowdockBootstrapper : PhoneBootstrapper {
		private PhoneContainer container;

		protected override void Configure() {
			container = new PhoneContainer(RootFrame);

			container.RegisterPhoneServices();

			// services
			container.RegisterSingleton(typeof(IAppSettings), "AppSettings", typeof(AppSettings));
			container.RegisterSingleton(typeof(INavigationManager), "NavigationManager", typeof(NavigationManager));
            container.RegisterSingleton(typeof(IMessageService), "MessageService", typeof(MessageService));

			container.Instance<IProgressService>(new ProgressService(RootFrame));
			container.PerRequest<IFlowdockContext, LoggedInFlowdockContext>();
            container.PerRequest<IFlowStreamingConnection, FlowStreamingConnection>();
			

			// viewmodels
			container.PerRequest<LoginViewModel>();
			container.PerRequest<LobbyViewModel>();
			container.PerRequest<FlowViewModel>();
			container.PerRequest<UsersViewModel>();
            container.PerRequest<MessageThreadViewModel>();
		}

		protected override object GetInstance(Type service, string key) {
			return container.GetInstance(service, key);
		}

		protected override IEnumerable<object> GetAllInstances(Type service) {
			return container.GetAllInstances(service);
		}

		protected override void BuildUp(object instance) {
			container.BuildUp(instance);
		}
	}
}
