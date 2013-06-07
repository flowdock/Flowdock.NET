using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Flowdock.Behaviors {
	public class ScrollToBottomTrigger : TargetedTriggerAction<ScrollViewer> {

		protected override void Invoke(object parameter) {
			Target.UpdateLayout();
			Target.ScrollToVerticalOffset(Target.ScrollableHeight);
		}
	}
}
