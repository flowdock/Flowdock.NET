using System;
using System.Collections;
using System.Windows;
using System.Windows.Data;

namespace Flowdock.Converters {
	public class ShowHideIfEmptyConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			IList list = value as IList;

			if ("Hide".Equals(parameter)) {
				return (list == null || list.Count == 0) ? Visibility.Collapsed : Visibility.Visible;
			} else {
				return (list == null || list.Count == 0) ? Visibility.Visible : Visibility.Collapsed;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
