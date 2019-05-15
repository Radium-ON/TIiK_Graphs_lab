
using DevExpress.Mvvm;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIiK_Graphs_lab3_6.Models;

namespace TIiK_Graphs_lab3_6.ViewModels
{
    public class EfficiencyPageVM : ViewModelBase
    {
        public EfficiencyPageVM()
        {
            RelaxationScore = new ObservableCollection<RelaxationStats>();

        }


        #region Properties
        public ObservableCollection<RelaxationStats> RelaxationScore
        {
            get { return GetProperty(() => RelaxationScore); }
            set { SetProperty(() => RelaxationScore, value); }
        }


        #endregion


        #region DelegateCommands



        #endregion
    }
}
