using Flowdock.ViewModels;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Settings {
	public class AppSettings : IAppSettings {
		private const string PasswordKey = "User.Password.Key";
		private const string UsernameKey = "User.Username.Key";

		private void Set(string key, object value) {
			if (!System.ComponentModel.DesignerProperties.IsInDesignTool) {
				Settings[key] = value;
			}
		}

		private string Get(string key) {
			if (!System.ComponentModel.DesignerProperties.IsInDesignTool && Settings.Contains(key)) {
				return Settings[key] as string;
			}
			return null;
		}

		private IsolatedStorageSettings Settings {
			get {
				return IsolatedStorageSettings.ApplicationSettings;
			}
		}

		public AppSettings() {
#if DEBUG
			Username = DebugLoginInfo.Username;
			Password = DebugLoginInfo.Password;
#endif
		}

		public string Username {
			get {
				return Get(UsernameKey);
			}
			set {
				Set(UsernameKey, value);
			}
		}

		public string Password {
			get {
				return Get(PasswordKey);
			}
			set {
				Set(PasswordKey, value);
			}
		}
	}
}
