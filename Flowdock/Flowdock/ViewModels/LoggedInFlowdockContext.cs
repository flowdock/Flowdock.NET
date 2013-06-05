using Flowdock.Domain;
using Flowdock.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.ViewModels {
	public class LoggedInFlowdockContext : FlowdockContext {
		private AppSettings _settings = new AppSettings();

		public LoggedInFlowdockContext()
			: base(new AppSettings().Username, new AppSettings().Password) {
		}
	}
}
