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

namespace Flowdock.Views {
	public partial class FlowView : PhoneApplicationPage {
		public FlowView() {
			InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e) {
			base.OnNavigatedTo(e);
			var flowId = NavigationContext.QueryString["flowId"];

			this.LayoutRoot.DataContext = new FlowViewModel(flowId);
		}
	}
}