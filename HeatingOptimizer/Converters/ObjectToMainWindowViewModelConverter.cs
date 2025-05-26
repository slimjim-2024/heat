using System;
using System.Globalization;
using Avalonia.Data.Converters;
using HeatingOptimizer.ViewModels;

namespace HeatingOptimizer.Converters;

public class ObjectToMainWindowViewModelConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // Try to cast the input value to MainWindowViewModel
        if (value is MainWindowViewModel viewModel)
        {
            return viewModel;
        }
            
        // If the value isn't a MainWindowViewModel, return null or a default value
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // For most converters, you can just return the value for ConvertBack if you don't
        // need to convert in the other direction
        return value;
    }

}