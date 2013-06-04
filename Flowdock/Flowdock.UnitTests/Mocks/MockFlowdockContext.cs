using Flowdock.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.UnitTests.Mocks {
	public class MockFlowdockContext : IFlowdockContext {
		public Task<string> Login(string username, string password) {
			return null;
		}

		public Task<IEnumerable<Flow>> GetCurrentFlows() {
			return null;
		}


		public Task<Flow> GetFlow(string id) {
			throw new NotImplementedException();
		}


		public Task<IEnumerable<Message>> GetMessagesForFlow(string id, string eventType = "message") {
			throw new NotImplementedException();
		}
	}
}
