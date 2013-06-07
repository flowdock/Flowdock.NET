using Flowdock.ViewModels;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;

namespace Flowdock.Views {
	public partial class FlowView : PhoneApplicationPage {
		public FlowView() {
			InitializeComponent();
		}

		// TODO: find a better way to do this
		protected override void OnNavigatedFrom(NavigationEventArgs e) {
			base.OnNavigatedFrom(e);
			FlowViewModel viewModel = LayoutRoot.DataContext as FlowViewModel;

			if (viewModel != null) {
				viewModel.Unload();
			}
		}
	}
}