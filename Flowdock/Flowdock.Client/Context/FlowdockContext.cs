using Flowdock.Client.Domain;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flowdock.Client.Context {
	public class FlowdockContext : IFlowdockContext {
		private const string FlowdockApiBaseUrl = "https://api.flowdock.com";

		private string _username;
		private string _password;

		private Task<IEnumerable<T>> GetCollection<T>(string resource) {
			var tcs = new TaskCompletionSource<IEnumerable<T>>();
			GetClient().ExecuteAsync<List<T>>(new RestRequest(resource), response => {
				tcs.SetResult(response.Data);
			});

			return tcs.Task;
		}

        private RestClient GetClient() {
            var client = new RestClient();
            client.BaseUrl = FlowdockApiBaseUrl;
            client.Authenticator = new HttpBasicAuthenticator(_username, _password);

            return client;
        }

		public FlowdockContext(string username, string password) {
			_username = username;
			_password = password;
		}

		public FlowdockContext() {
		}

		public Task<IEnumerable<Flow>> GetCurrentFlows() {
			return GetCollection<Flow>("flows");
		}

		public virtual Task<Flow> GetFlow(string flowId) {
			string resource = string.Format("flows/{0}",flowId.Replace(":", "/"));

			var tcs = new TaskCompletionSource<Flow>();
			GetClient().ExecuteAsync<Flow>(new RestRequest(resource), response => {
				tcs.SetResult(response.Data);
			});

			return tcs.Task;
		}

        public Task<IEnumerable<Message>> GetMessagesForThread(string flowId, int threadId) {
            string resource = string.Format("flows/{0}/messages?tags=influx:{1}", flowId.Replace(":", "/"), threadId);
            return GetCollection<Message>(resource);
        }

		public virtual Task<IEnumerable<Message>> GetMessagesForFlow(string flowId) {
			// TODO: handle eventType, for now returning everything
			string resource = string.Format("flows/{0}/messages", flowId.Replace(":", "/"));
			return GetCollection<Message>(resource);
		}

		public Task<string> Login(string username, string password) {
			// temporary/spike/hack code. I currently know of a simple way to just verify 
			// username/password are valid, so getting the user's flows. A null result usually
			// means failed to login

			var tcs = new TaskCompletionSource<string>();
			GetClient().ExecuteAsync<List<Flow>>(new RestRequest("flows"), response => {
				if (response.Data != null && response.Data.Any()) {
					tcs.SetResult(null);
				} else {
					tcs.SetResult("Failed to login");
				}
			});

			return tcs.Task;		
		}

		public void SendMessage(string flowId, string message) {
			//POST /flows/:organization/:flow/messages
			//{
			//  "event": "message",
			//  "content": "Howdy-Doo @Jackie #awesome",
			//  "tags":  ["todo", "#feedback", "@all"]
			//}

			string resource = string.Format("flows/{0}/messages", flowId.Replace(":", "/"));

			RestRequest request = new RestRequest(resource, Method.POST);
			request.AddParameter("event", "message");
			request.AddParameter("content", message);

			GetClient().PostAsync(request, (response, handle) => { });
		}
	}
}
