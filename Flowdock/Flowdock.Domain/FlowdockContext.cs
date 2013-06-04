﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Flowdock.Extensions;
using Flowdock.Domain;
using RestSharp;

namespace Flowdock.Data {
	public class FlowdockContext : IFlowdockContext {
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

		public Task<IEnumerable<Flow>> GetFlows() {
			return GetCollection<Flow>("flows");
		}

		public Task<string> Login(string username, string password) {
			// temporary/spike/hack code. I currently know of a simple way to just verify 
			// username/password are valid, so getting the user's flows. A null result usually
			// means failed to login

			var client = new RestClient();
			client.BaseUrl = FlowdockApiBaseUrl;
			client.Authenticator = new HttpBasicAuthenticator(username, password);

			var tcs = new TaskCompletionSource<string>();
			client.ExecuteAsync<List<Flow>>(new RestRequest("flows/all"), response => {
				if (response.Data != null && response.Data.Any()) {
					tcs.SetResult(null);
					_username = username;
					_password = password;
				} else {
					tcs.SetResult("Failed to login");
				}
			});

			return tcs.Task;		
		}
	}
}