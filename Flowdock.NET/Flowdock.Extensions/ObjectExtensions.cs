using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Extensions {

	public static class ObjectExtensions {

		public static T ThrowIfNull<T>(this T obj, string msg) {
			if (obj == null) {
				throw new ArgumentNullException(msg);
			}
			return obj;
		}
	}

}