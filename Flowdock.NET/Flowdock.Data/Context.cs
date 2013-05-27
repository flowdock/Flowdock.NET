using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flowdock.Extensions;
using Flowdock.Domain;
using RestSharp;

namespace Flowdock.Data {
	public class Context {
		private const string FlowdockApiBaseUrl = "https://api.flowdock.com";

		private string _username;
		private string _password;

		private Task<IEnumerable<T>> GetCollection<T>(string resource) {
			var client = new RestClient();
			client.BaseUrl = FlowdockApiBaseUrl;
			client.Authenticator = new HttpBasicAuthenticator(_username, _password);

			var tcs = new TaskCompletionSource<IEnumerable<T>>();
			client.ExecuteAsync<List<T>>(new RestRequest(resource), response => {
				tcs.SetResult(response.Data);
			});

			return tcs.Task;
		}

		public Context(string username, string password) {
			_username = username.ThrowIfNull("username");
			_password = password.ThrowIfNull("password");
		}

		public Task<IEnumerable<Flow>> GetFlows() {
			return GetCollection<Flow>("flows");
		}
	}
}
