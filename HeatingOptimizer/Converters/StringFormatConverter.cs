using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace HeatingOptimizer.Converters;
public class StringFormatConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
                => new Bitmap(AssetLoader.Open(new Uri (string.Format((string)parameter, value))));

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
                => throw new NotSupportedException();
        }