using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.ViewModel.Storage {
	public interface IIsolatedStorageProxy {
		T Get<T>(string key);
		void Put<T>(string key, T value);
	}
}
