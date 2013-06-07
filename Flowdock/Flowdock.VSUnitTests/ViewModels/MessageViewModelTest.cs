using Flowdock.Client.Domain;
using Flowdock.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.VSUnitTests {
	[TestClass]
	public class MessageViewModelTest {

		[TestMethod]
		public void Test() {
			var m = new MessageViewModel(new Message(), null);

			Assert.IsNotNull(m);
		}
	}
}
