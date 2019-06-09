using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TIiK_Graphs_lab3_6.ViewModels
{
    class SearchToVisibilityConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility vis = Visibility.Hidden;
            try
            {
                if (value != null)
                {
                    var index = (int)value;

                    if (index == 2 || index == 3)
                    {
                        vis = Visibility.Visible;
                    }
                }
            }
            catch { }
            return vis;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
