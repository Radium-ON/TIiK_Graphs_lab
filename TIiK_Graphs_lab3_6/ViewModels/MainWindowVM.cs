using DevExpress.Mvvm;
using FirstFloor.ModernUI.Windows.Controls;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using TIiK_Graphs_lab3_6.Models;
using TIiK_Graphs_lab3_6.Pages;

namespace TIiK_Graphs_lab3_6.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            _matrixPageVm = new MatrixPageVM();
            _efficiencyPageVm = new EfficiencyPageVM();  
        }

        

        #region Backing Fields
        private readonly MatrixPageVM _matrixPageVm;
        private readonly EfficiencyPageVM _efficiencyPageVm;
        

        #endregion

        

    }
}
