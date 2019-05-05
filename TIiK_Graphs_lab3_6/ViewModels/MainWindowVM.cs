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

namespace TIiK_Graphs_lab3_6.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        public MainWindowVM()
        {
            //_matrixPageVm = new MatrixPageVM();
            //_efficiencyPageVm = new EfficiencyPageVM();
            //MatrixAdjacency = VertexFactory.GetWeightMatrixCost();
            //AddColumnCollection(MatrixAdjacency.Count);
            //VertexNodes = VertexFactory.GetVertexDijkstra();
            VertexNodes = new ObservableCollection<VertexNode>();
            BypassCollection = new ObservableCollection<VertexNode>();
            AddVertexCommand = new DelegateCommand<string>(AddVertex, CanAddVertex);
            RandomMatrixCommand = new DelegateCommand(RandomMatrix, CanRandom);
            BypassCommand = new DelegateCommand(PerformBypass,CanPerformBypass);

            CollectionViewVertexNumber.CurrentChanged += VertexNumber_CurrentChanged;
        }

        private bool CanPerformBypass()
        {
            if (SelectedBypass == -1)
                return false;
            return true;
        }

        private void PerformBypass()
        {
            foreach (var node in VertexNodes){node.VStatus = VStatEnum.NoVisited;}
            BypassService.DepthBypass(VertexNodes, MatrixAdjacency,BypassCollection);
        }

        private void VertexNumber_CurrentChanged(object sender, EventArgs e)
        {
            int num = (int)((CollectionView)sender).CurrentItem;
            MatrixAdjacency = VertexFactory.GetWeightMatrix(num);
            AddColumnCollection(num);
        }

        private bool CanRandom()
        {
            if (SelectedVNumber == 0)
                return false;
            return true;
        }

        private void RandomMatrix()
        {
            MatrixAdjacency = VertexFactory.GetRandomMatrix(SelectedVNumber);
            VertexNodes = VertexFactory.GetVertexes(MatrixAdjacency.Count);
        }

        private bool CanAddVertex(string par)
        {
            return !(VertexNodes.Any(item => item.Name == par) ||
                                             string.IsNullOrEmpty(par));
        }

        private void AddVertex(string par)
        {
            if (VertexNodes.Count != 0)
                NewVertexId = VertexNodes.Max(x => x.VertexId);
            if (VertexNodes.Count < (int)CollectionViewVertexNumber.CurrentItem)
            {
                VertexNodes.Add(new VertexNode(NewVertexId + 1, par));
            }
            else
            {
                new ModernDialog()
                {
                    Title = "Внимание",
                    Content = "Выберите другой размер матрицы смежности, чтобы добавить ещё вершин"
                }.ShowDialog();
            }
        }

        #region Backing Fields
        private readonly MatrixPageVM _matrixPageVm;
        private readonly EfficiencyPageVM _efficiencyPageVm;
        private ObservableCollection<ObservableCollection<int>> _matrixAdjaency;
        private readonly ObservableCollection<DataGridColumn> _columnCollection;
        //private ObservableCollection<VertexNode> _vertexNodes;
        private string _newVertexName;
        private string _newVertexId;


        #endregion

        #region Properties

        public CollectionView CollectionViewVertexNumber { get; private set; } = new CollectionView(new List<int>(Enumerable.Range(2, 12)));

        public CollectionView CollectionViewBypassType { get; private set; } = new CollectionView(new List<string>{"В глубину","В ширину","Дейкстры","A-star"});

        public int SelectedVNumber
        {
            get { return GetProperty(() => SelectedVNumber); }
            set { SetProperty(() => SelectedVNumber, value); }
        }

        public int SelectedBypass
        {
            get { return GetProperty(() => SelectedBypass); }
            set { SetProperty(() => SelectedBypass, value); }
        }


        public ObservableCollection<VertexNode> VertexNodes
        {
            get { return GetProperty(() => VertexNodes); }
            set
            {
                SetProperty(() => VertexNodes, value);
                //RaisePropertyChanged("VertexNodes");
            }
        }



        public ObservableCollection<VertexNode> BypassCollection
        {
            get { return GetProperty(() => BypassCollection); }
            set { SetProperty(() => BypassCollection, value); }
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

        public ObservableCollection<DataGridColumn> ColumnCollection
        {
            get { return GetProperty(() => ColumnCollection); }
            set { SetProperty(() => ColumnCollection, value); }
        }

        public int NewVertexId
        {
            get { return GetProperty(() => NewVertexId); }
            set { SetProperty(() => NewVertexId, value); }
        }

        #endregion

        #region DelegateCommands
        public DelegateCommand<string> AddVertexCommand { get; private set; }
        public DelegateCommand RandomMatrixCommand { get; private set; }
        public DelegateCommand BypassCommand { get; private set; }

        #endregion

        private void AddColumnCollection(int count)
        {
            ColumnCollection = new ObservableCollection<DataGridColumn>();
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
    }
}
