using Flowdock.Client.Domain;
using System.IO.IsolatedStorage;

namespace Flowdock.Settings {
	public class AppSettings : IAppSettings {
		private const string PasswordKey = "User.Password.Key";
		private const string UsernameKey = "User.Username.Key";
		private const string CurrentFlowKey = "Flow.Current.Key";

		private void Set<T>(string key, T value) {
			if (!System.ComponentModel.DesignerProperties.IsInDesignTool) {
				Settings[key] = value;
				Settings.Save();
			}
		}

		private T Get<T>(string key) {
			if (!System.ComponentModel.DesignerProperties.IsInDesignTool) {
				if (Settings.Contains(key)) {
					return (T)Settings[key];
				}
			}

			return default(T);
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
				return Get<string>(UsernameKey);
			}
			set {
				Set(UsernameKey, value);
			}
		}

		public string Password {
			get {
				return Get<string>(PasswordKey);
			}
			set {
				Set(PasswordKey, value);
			}
		}
	}
}
