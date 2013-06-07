using Flowdock.Client.Domain;
using Flowdock.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Flowdock.VSUnitTests {
	[TestClass]
	public class MessageViewModelTest {

		[TestMethod]
		public void Constructor_passes_message_properties_through() {
			var content = "a message";
			var message = new Message {
				Event = "message",
				Sent = 55555,
				Id = 123,
				User = 44
			};
			message.Content = content;

			var viewModel = new MessageViewModel(message, null);

			Assert.AreEqual(message.ExtractedBody, viewModel.Body);
			Assert.AreEqual(content, viewModel.Body);
			Assert.AreEqual(message.Id, viewModel.Id);
			Assert.AreEqual(message.TimeStamp, viewModel.TimeStamp);
			Assert.AreEqual(message.User, viewModel.UserId);
		}

		[TestMethod]
		public void Constructor_ThreadColor_gets_set_from_color() {
			Color color = Color.FromArgb(1, 2, 3, 4);

			var viewModel = new MessageViewModel(new Message(), color);

			Assert.AreEqual(color, viewModel.ThreadColor);

		}

		[TestMethod]
		public void Constructor_ThreadColor_gets_set_from_null() {
			var viewModel = new MessageViewModel(new Message(), null);
			Assert.IsNull(viewModel.ThreadColor);
		}

		private MessageViewModel _viewModel;

		[TestInitialize]
		public void BeforeEach() {
			_viewModel = new MessageViewModel(new Message(), null);
		}

		[TestMethod]
		public void PropertyChanged_notifies_when_Body_changed() {
			string propertyName = null;

			_viewModel.PropertyChanged += (o, e) => propertyName = e.PropertyName;

			_viewModel.Body = "new body";

			Assert.AreEqual("Body", propertyName);
		}

		[TestMethod]
		public void PropertyChanged_notifies_when_WasEdited_changed() {
			string propertyName = null;

			_viewModel.PropertyChanged += (o, e) => propertyName = e.PropertyName;

			_viewModel.WasEdited = true;

			Assert.AreEqual("WasEdited", propertyName);
		}

		[TestMethod]
		public void PropertyChanged_notifies_when_Avatar_changed() {
			string propertyName = null;

			_viewModel.PropertyChanged += (o, e) => propertyName = e.PropertyName;

			_viewModel.Avatar = new Uri("http://www.google.com", UriKind.Absolute);

			Assert.AreEqual("Avatar", propertyName);
		}

		[TestMethod]
		public void PropertyChanged_notifies_when_ThreadColor_changed() {
			string propertyName = null;

			_viewModel.PropertyChanged += (o, e) => propertyName = e.PropertyName;

			_viewModel.ThreadColor = Color.FromArgb(1, 2, 3, 5);

			Assert.AreEqual("ThreadColor", propertyName);
		}
	}
}
