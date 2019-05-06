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

            VertexNodes = new ObservableCollection<VertexNode>();
            RelaxationScore = new ObservableCollection<RelaxationStats>();
            BypassCollection = new ObservableCollection<VertexNode>();
            AddVertexCommand = new DelegateCommand<string>(AddVertex, CanAddVertex);
            RandomMatrixCommand = new DelegateCommand(RandomMatrix, CanRandom);
            BypassCommand = new DelegateCommand(PerformBypass, CanPerformBypass);
            LoadCostSetsCommand = new DelegateCommand(LoadCostSets);
            LoadQualitySetsCommand = new DelegateCommand(LoadQualitySets);

            CollectionViewVertexNumber.CurrentChanged += VertexNumber_CurrentChanged;
        }

        private void LoadQualitySets()
        {
            MatrixAdjacency = VertexFactory.GetWeightMatrixQuality();
            VertexNodes = VertexFactory.GetVertexDijkstra();
            AddColumnCollection(13);
        }

        private void LoadCostSets()
        {
            MatrixAdjacency = VertexFactory.GetWeightMatrixCost();
            VertexNodes = VertexFactory.GetVertexDijkstra();
            AddColumnCollection(13);
        }

        private bool CanPerformBypass()
        {
            if (VertexNodes.Count == 0 || MatrixAdjacency.Count == 0)
                return false;
            if (StartBVertex == 0 && (SelectedBypass == 2 || SelectedBypass == 3))
                return false;
            return true;
        }

        private void PerformBypass()
        {
            foreach (var node in VertexNodes) { node.VStatus = VStatEnum.NoViewed; }

            switch (SelectedBypass)
            {
                case 0:
                    BypassService.DepthBypass(VertexNodes, MatrixAdjacency, BypassCollection);
                    break;
                case 1:
                    BypassService.WidthBypass(VertexNodes, MatrixAdjacency, BypassCollection);
                    break;
                case 2:
                    BypassService.DijkstraBypass(VertexNodes, MatrixAdjacency, BypassCollection, StartBVertex, FinishBVertex);
                    break;
                case 3:
                    break;
            }

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

        private void VertexNumber_CurrentChanged(object sender, EventArgs e)
        {
            int num = (int)((CollectionView)sender).CurrentItem;
            MatrixAdjacency = VertexFactory.GetWeightMatrix(num);
            AddColumnCollection(num);
        }

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
                        //Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                        //MinWidth = 50
                    });
            }

        }

        #region Backing Fields
        private readonly MatrixPageVM _matrixPageVm;
        private readonly EfficiencyPageVM _efficiencyPageVm;
        private ObservableCollection<ObservableCollection<int>> _matrixAdjaency;

        #endregion

        #region Properties

        //public MatrixPageVM MatrixPageVm { get; }

        //public EfficiencyPageVM EfficiencyPageVM { get; }

        public CollectionView CollectionViewVertexNumber { get; private set; } = new CollectionView(new List<int>(Enumerable.Range(2, 19)));

        public CollectionView CollectionViewBypassType { get; private set; } = new CollectionView(new List<string> { "В глубину", "В ширину", "Дейкстры", "A-star" });

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

        public int StartBVertex
        {
            get { return GetProperty(() => StartBVertex); }
            set
            {
                if (value <= 0)
                    SetProperty(() => StartBVertex, 1);
                else
                    SetProperty(() => StartBVertex, value);
            }
        }

        public int FinishBVertex
        {
            get { return GetProperty(() => FinishBVertex); }
            set
            {
                if (value <= 0)
                    SetProperty(() => FinishBVertex, 1);
                else
                    SetProperty(() => FinishBVertex, value);
            }
        }

        public int NewVertexId
        {
            get { return GetProperty(() => NewVertexId); }
            set { SetProperty(() => NewVertexId, value); }
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

        public ObservableCollection<DataGridColumn> ColumnCollection
        {
            get { return GetProperty(() => ColumnCollection); }
            set { SetProperty(() => ColumnCollection, value); }
        }

        public ObservableCollection<RelaxationStats> RelaxationScore
        {
            get { return GetProperty(() => RelaxationScore); }
            set { SetProperty(() => RelaxationScore, value); }
        }

        #endregion

        #region DelegateCommands

        public DelegateCommand<string> AddVertexCommand { get; private set; }
        public DelegateCommand RandomMatrixCommand { get; private set; }
        public DelegateCommand BypassCommand { get; private set; }
        public DelegateCommand LoadCostSetsCommand { get; private set; }
        public DelegateCommand LoadQualitySetsCommand { get; private set; }

        #endregion

    }
}
