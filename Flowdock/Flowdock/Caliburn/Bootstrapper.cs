using Caliburn.Micro;
using Flowdock.Client.Context;
using Flowdock.Settings;
using Flowdock.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO user Caliburn's navigation service
using INavigationService = Flowdock.Navigation.INavigationManager;
using NavigationService = Flowdock.Navigation.NavigationManager;

namespace Flowdock.Caliburn {
	public class Bootstrapper : PhoneBootstrapper {
		PhoneContainer container;

		protected override void Configure() {
			container = new PhoneContainer(RootFrame);

			container.RegisterPhoneServices();

			// services
			container.RegisterSingleton(typeof(IAppSettings), "AppSettings", typeof(AppSettings));
			container.PerRequest<IFlowdockContext, LoggedInFlowdockContext>();
			container.RegisterSingleton(typeof(INavigationService), "NavigationService", typeof(NavigationService));

			// viewmodels
			container.PerRequest<LoginViewModel>();
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
