using Flowdock.Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.MessageBoxes {
	public interface IMessageBoxService
	{
		void ShowUsersMessageBox(IEnumerable<User> users);
	}
}
