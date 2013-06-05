using Flowdock.Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Client.Stream {
	public interface IFlowStreamingConnection
	{
		void Start(string username, string password, string flowId, Action<Message> callback);
		void Stop();
	}
}
