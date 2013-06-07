using System;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Flowdock.Behaviors {
	public class ImmediateUpdatePasswordBoxBehavior : Behavior<PasswordBox> {

		private void OnAssociatedPasswordChanged(object sender, EventArgs e) {
			AssociatedObject.GetBindingExpression(PasswordBox.PasswordProperty).UpdateSource();
		}

		protected override void OnAttached() {
			base.OnAttached();
			AssociatedObject.PasswordChanged += OnAssociatedPasswordChanged;
		}
	}
}
