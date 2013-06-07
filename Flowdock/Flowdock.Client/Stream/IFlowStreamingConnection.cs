using Flowdock.Client.Domain;
using System;

namespace Flowdock.Client.Stream {
	public interface IFlowStreamingConnection
	{
		void Start(string username, string password, string flowId, Action<Message> callback);
		void Stop();
	}
}
