using Flowdock.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.UnitTests.Mocks {
	public class MockFlowdockContext : IFlowdockContext {
		public Task<string> Login(string username, string password) {
			throw new NotImplementedException();
		}
	}
}
