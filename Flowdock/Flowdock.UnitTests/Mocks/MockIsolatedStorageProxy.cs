using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flowdock.Settings;


namespace Flowdock.UnitTests.Mocks {
	public class MockAppSettings : IAppSettings {
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
