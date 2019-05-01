using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using TIiK_Graphs_lab3_6.Models;

namespace TIiK_Graphs_lab3_6.ViewModels
{
    public class MainWindowVM : BindableBase
    {
        public MainWindowVM()
        {
            //_matrixPageVm = new MatrixPageVM();
            //_efficiencyPageVm = new EfficiencyPageVM();
            MatrixAdjacency = VertexFactory.GetWeightMatrixCost();            

        }

        private ObservableCollection<ObservableCollection<int>> _matrixAdjaency;
        public ObservableCollection <ObservableCollection<int>> MatrixAdjacency
        {
            get { return _matrixAdjaency;}
            set {_matrixAdjaency = value ;
                RaisePropertyChanged("MatrixAdjacency");
            }
        }

        private readonly MatrixPageVM _matrixPageVm;

        public MatrixPageVM MatrixPageVm { get; }

        private readonly EfficiencyPageVM _efficiencyPageVm;

        public EfficiencyPageVM EfficiencyPageVM { get; }
    }
}
