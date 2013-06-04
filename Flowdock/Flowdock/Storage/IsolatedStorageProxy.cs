using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.ViewModel.Storage {
	public class IsolatedStorageProxy : IIsolatedStorageProxy {

		private IsolatedStorageSettings Settings {
			get {
				return IsolatedStorageSettings.ApplicationSettings;
			}
		}

		public T Get<T>(string key) {
			if (!System.ComponentModel.DesignerProperties.IsInDesignTool) {
				if (Settings.Contains(key)) {
					return (T)Settings[key];
				}
			}
			return default(T);
		}

		public void Put<T>(string key, T value) {
			if (!System.ComponentModel.DesignerProperties.IsInDesignTool) {
				Settings[key] = value;
				Settings.Save();
			}
		}
	}
}
