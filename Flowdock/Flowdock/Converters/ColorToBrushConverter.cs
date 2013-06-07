using System;
using System.Windows.Data;
using System.Windows.Media;

namespace Flowdock.Converters {
	public class ColorToBrushConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			Color? color = (Color?)value;

			if (color.HasValue) {
				return new SolidColorBrush(color.Value);
			}
			return new SolidColorBrush();
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
