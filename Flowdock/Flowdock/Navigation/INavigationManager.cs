using Flowdock.Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Navigation {
	public interface INavigationManager
	{
		void GoToLobby();
		void GoToFlow(Flow flow);
	}
}
