using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Flowdock.ViewModels;
using BindableApplicationBar;

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