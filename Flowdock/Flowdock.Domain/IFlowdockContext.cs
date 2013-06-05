using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Domain {
	public interface IFlowdockContext
	{
		Task<string> Login(string username, string password);

		Task<IEnumerable<Flow>> GetCurrentFlows();
		Task<Flow> GetFlow(string id);

		Task<IEnumerable<Message>> GetMessagesForFlow(string id);
	}
	
}
