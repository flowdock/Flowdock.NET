using Flowdock.Client.Context;
using Flowdock.Settings;

namespace Flowdock.ViewModels {
	public class LoggedInFlowdockContext : FlowdockContext {


		public LoggedInFlowdockContext(IAppSettings appSettings)
			: base(appSettings.Username, appSettings.Password) {
		}
	}
}
