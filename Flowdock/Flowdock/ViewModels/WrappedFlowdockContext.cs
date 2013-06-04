using Flowdock.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.ViewModels {
	public class WrappedFlowdockContext : FlowdockContext {
		public WrappedFlowdockContext()
			: base(Flowdock.App.Username, Flowdock.App.Password) {
		}
	}
}
