using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows;
using FragmentNavigationEventArgs = FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs;
using NavigatingCancelEventArgs = FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs;
using NavigationEventArgs = FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs;

namespace TIiK_Graphs_lab3_6.Pages
{
    /// <summary>
    /// Interaction logic for EfficiencyPage.xaml
    /// </summary>
    public partial class EfficiencyPage : UserControl, IContent
    {
        public EfficiencyPage()
        {
            InitializeComponent();
        }

        #region Implementation of IContent

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}
