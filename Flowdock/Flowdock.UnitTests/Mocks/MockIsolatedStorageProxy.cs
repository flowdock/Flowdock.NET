using Flowdock.ViewModel.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.ViewModel.UnitTests.Mocks {
	public class MockIsolatedStorageProxy : IIsolatedStorageProxy {
		private Dictionary<string, object> _storage = new Dictionary<string, object>();

		public T Get<T>(string key) {
			if (_storage.ContainsKey(key)) {
				return (T)_storage[key];
			}
			return default(T);
		}

		public void Put<T>(string key, T value) {
			_storage[key] = value;
		}
	}
}
