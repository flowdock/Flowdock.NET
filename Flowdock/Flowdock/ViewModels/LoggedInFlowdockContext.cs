using Flowdock.Client.Context;
using Flowdock.Settings;

namespace Flowdock.ViewModels {
	public class LoggedInFlowdockContext : FlowdockContext {
		private AppSettings _settings = new AppSettings();

		public LoggedInFlowdockContext()
			: base(new AppSettings().Username, new AppSettings().Password) {
		}
	}
}
