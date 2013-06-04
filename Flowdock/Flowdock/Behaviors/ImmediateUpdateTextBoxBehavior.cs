using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Flowdock.Behaviors {
	public class ImmediateUpdateTextBoxBehavior : Behavior<TextBox> {

		private void OnAssociatedTextChanged(object sender, EventArgs e) {
			AssociatedObject.GetBindingExpression(TextBox.TextProperty).UpdateSource();
		}

		protected override void OnAttached() {
			base.OnAttached();
			AssociatedObject.TextChanged += OnAssociatedTextChanged;
		}
	}
}