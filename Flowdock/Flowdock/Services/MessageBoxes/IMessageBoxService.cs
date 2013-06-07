using Flowdock.Client.Domain;
using System.Collections.Generic;

namespace Flowdock.MessageBoxes {
	public interface IMessageBoxService
	{
		void ShowUsersMessageBox(IEnumerable<User> users);
	}
}
