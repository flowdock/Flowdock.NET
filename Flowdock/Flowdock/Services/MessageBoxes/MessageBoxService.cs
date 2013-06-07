using Flowdock.Client.Domain;
using Flowdock.ViewModels;
using Microsoft.Phone.Controls;
using System.Collections.Generic;
using System.Windows;

namespace Flowdock.MessageBoxes {
	public class MessageBoxService : IMessageBoxService {

		public void ShowUsersMessageBox(IEnumerable<User> users) {
			ResourceDictionary appResources = Application.Current.Resources["applicationResources"] as ResourceDictionary;

			CustomMessageBox messageBox = new CustomMessageBox() {
				Content = ((DataTemplate)appResources["usersPivotDataTemplate"]).LoadContent(),
				DataContext = new UsersViewModel(users),
				RightButtonContent = "OK",
				IsFullScreen = true
			};

			messageBox.Show();
		}
	}
}
