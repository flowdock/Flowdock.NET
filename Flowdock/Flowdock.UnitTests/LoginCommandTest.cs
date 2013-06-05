using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Flowdock.ViewModels;
using Flowdock.UnitTests.Mocks;

namespace Flowdock.ViewModel.UnitTests {
	public class LoginCommandTest {
		private LoginViewModel _loginViewModel;
		private LoginCommand _command;

		[SetUp]
		public void BeforeEach() {
			_loginViewModel = new LoginViewModel(new MockFlowdockContext(), new MockAppSettings(), new MockNavigationManager());
			_command = new LoginCommand(_loginViewModel);
		}

		[Test]
		public void CanExecute_false_if_username_and_password_are_null() {
			_loginViewModel.Username = null;
			_loginViewModel.Password = null;

			Assert.That(_command.CanExecute(null), Is.False);
		}

		[Test]
		public void CanExecute_false_if_username_and_password_are_blank() {
			_loginViewModel.Username = "";
			_loginViewModel.Password = "";

			Assert.That(_command.CanExecute(null), Is.False);
		}

		[Test]
		public void CanExecute_false_if_username_and_password_are_whitespace() {
			_loginViewModel.Username = "   ";
			_loginViewModel.Password = "      ";

			Assert.That(_command.CanExecute(null), Is.False);
		}

		[Test]
		public void CanExecute_true_if_username_and_password_are_set() {
			_loginViewModel.Username = "hello@kitty.com";
			_loginViewModel.Password = "somepassword";

			Assert.That(_command.CanExecute(null), Is.True);
		}
	}
}
