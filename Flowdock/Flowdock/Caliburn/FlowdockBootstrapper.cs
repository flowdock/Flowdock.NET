using Caliburn.Micro;
using Flowdock.Client.Context;
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
			container.PerRequest<IFlowdockContext, LoggedInFlowdockContext>();

			// viewmodels
			container.PerRequest<LoginViewModel>();
			container.PerRequest<LobbyViewModel>();
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
