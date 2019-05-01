using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using FirstFloor.ModernUI.Windows.Controls;
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
            for(var i=0; i<MatrixAdjacency.Count;i++)
            {
                ColumnCollection.Add(
                    new FirstFloor.ModernUI.Windows.Controls.DataGridTextColumn()
                    {
                        Binding = new Binding($"[{i}]"),
                        Header = i+1,
                    });
            }

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

        private readonly ObservableCollection<DataGridColumn> _columnCollection;

        public ObservableCollection<DataGridColumn> ColumnCollection { get; private set; } = new ObservableCollection<DataGridColumn>();
    }
}
