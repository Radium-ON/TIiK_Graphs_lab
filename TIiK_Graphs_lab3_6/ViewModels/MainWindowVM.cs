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
using DevExpress.Mvvm;
using TIiK_Graphs_lab3_6.Models;
using BindableBase = Prism.Mvvm.BindableBase;

namespace TIiK_Graphs_lab3_6.ViewModels
{
    public class MainWindowVM : BindableBase
    {
        public MainWindowVM()
        {
            //_matrixPageVm = new MatrixPageVM();
            //_efficiencyPageVm = new EfficiencyPageVM();
            MatrixAdjacency = VertexFactory.GetWeightMatrixCost();
            VertexNodes = VertexFactory.GetVertexDijkstra();
            AddColumnCollection(MatrixAdjacency.Count);

            AddVertexCommand = new Prism.Commands.DelegateCommand<string>(AddVertex, CanAddVertex);
        }

        private bool CanAddVertex(string par)
        {
            //return par != null && !(VertexNodes.Any(item => item.Name == par)|| par.Length==0);
            return !string.IsNullOrEmpty(par);

        }

        private void AddVertex(string par)
        {

            VertexNodes.Add(new VertexNode(13, par));
        }

        #region Backing Fields
        private readonly MatrixPageVM _matrixPageVm;
        private readonly EfficiencyPageVM _efficiencyPageVm;
        private ObservableCollection<ObservableCollection<int>> _matrixAdjaency;
        private readonly ObservableCollection<DataGridColumn> _columnCollection;
        private ObservableCollection<VertexNode> _vertexNodes;
        private string _newVertexName;
        private string _newVertexId;


        #endregion

        #region Properties
        public ObservableCollection<VertexNode> VertexNodes
        {
            get { return _vertexNodes; }
            set
            {
                SetProperty(ref _vertexNodes, value);
                RaisePropertyChanged("VertexNodes");
            }
        }

        public ObservableCollection<ObservableCollection<int>> MatrixAdjacency
        {
            get { return _matrixAdjaency; }
            set
            {
                _matrixAdjaency = value;
                RaisePropertyChanged("MatrixAdjacency");
            }
        }

        public MatrixPageVM MatrixPageVm { get; }

        public EfficiencyPageVM EfficiencyPageVM { get; }

        public ObservableCollection<DataGridColumn> ColumnCollection { get; private set; } = new ObservableCollection<DataGridColumn>();

        //public string NewVertexName
        //{
        //    get => _newVertexName;
        //    set
        //    {
        //        SetProperty(ref _newVertexName, value);
        //        AddVertexCommand.RaiseCanExecuteChanged();
        //    }
        //}

        public string NewVertexId
        {
            get => _newVertexId;
            set
            {
                SetProperty(ref _newVertexId, value);
                AddVertexCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion
        private void AddColumnCollection(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ColumnCollection.Add(
                    new FirstFloor.ModernUI.Windows.Controls.DataGridTextColumn()
                    {
                        Binding = new Binding($"[{i}]"),
                        Header = i + 1,
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                        MinWidth = 50
                    });
            }
        }


        #region DelegateCommands
        public Prism.Commands.DelegateCommand<string> AddVertexCommand { get; private set; }


        #endregion
    }
}
