using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.ViewModel.Storage {
	public class IsolatedStorageProxy : IIsolatedStorageProxy {
		private IsolatedStorageSettings _settings = IsolatedStorageSettings.ApplicationSettings;

		public T Get<T>(string key) {
			if (_settings.Contains(key)) {
				return (T)_settings[key];
			}
			return default(T);
		}

		public void Put<T>(string key, T value) {
			_settings[key] = value;
		}
	}
}
