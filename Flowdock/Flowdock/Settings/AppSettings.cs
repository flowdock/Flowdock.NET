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
				if (Settings.Contains(UsernameKey)) {
					return Settings[UsernameKey] as string;
				}
				return null;
			}
			set {
				Settings[UsernameKey] = value;
				Settings.Save();
			}
		}

		public string Password {
			get {
				if (Settings.Contains(PasswordKey)) {
					return Settings[PasswordKey] as string;
				}
				return null;
			}
			set {
				Settings[PasswordKey] = value;
				Settings.Save();
			}
		}
	}
}
