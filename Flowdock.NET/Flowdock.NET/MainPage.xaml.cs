using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Flowdock.NET.Resources;
using Flowdock.Data;
using Flowdock.Domain;

namespace Flowdock.NET {
	public partial class MainPage : PhoneApplicationPage {
		public MainPage() {
			InitializeComponent();
		}

		private async void GetSomeFlows(string password) {
			Context c = new Context("mgreer@rallydev.com", password);
			IEnumerable<Flow> flows = await c.GetFlows();

			if (flows == null) {
				MessageBox.Show("Got null back (wrong password?)");
			} else {
				MessageBox.Show(string.Format("First flow: {0}, url: {1}", flows.First().Name, flows.First().Url));
			}
		}

		private void GoButton_Click(object sender, RoutedEventArgs e) {
			GetSomeFlows(PasswordBox.Password);
		}
	}
}
