using System;

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