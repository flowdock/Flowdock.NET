using Flowdock.Client.Domain;

namespace Flowdock.Navigation {
	public interface INavigationManager
	{
		void GoToLobby();
		void GoToFlow(Flow flow);
	}
}
