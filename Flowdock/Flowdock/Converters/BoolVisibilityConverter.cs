using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Flowdock.Converters {
	public class BoolVisibilityConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			bool asBool = (bool)value;

			if ("Hide".Equals(parameter)) {
				return asBool ? Visibility.Collapsed : Visibility.Visible;
			} else {
				return asBool ? Visibility.Visible : Visibility.Collapsed;
			}

		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
