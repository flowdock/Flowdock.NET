using Flowdock.Client.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flowdock.Client.Context {
	public interface IFlowdockContext
	{
		Task<string> Login(string username, string password);

		Task<IEnumerable<Flow>> GetCurrentFlows();
		Task<Flow> GetFlow(string id);

		Task<IEnumerable<Message>> GetMessagesForFlow(string id);
	}
	
}
