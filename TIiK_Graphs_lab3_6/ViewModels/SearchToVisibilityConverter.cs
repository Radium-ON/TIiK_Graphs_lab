using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
                    ComboBoxItem cmb = (ComboBoxItem)value;


                    if (cmb.Name=="cmb_item_dijkstra" || cmb.Name=="cmb_item_a_star")
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
