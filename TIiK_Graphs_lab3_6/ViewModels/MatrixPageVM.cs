using DevExpress.Mvvm;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using TIiK_Graphs_lab3_6.Models;
using TIiK_Graphs_lab3_6.Services;

namespace TIiK_Graphs_lab3_6.ViewModels
{
    public class MatrixPageVM : ViewModelBase
    {
        public MatrixPageVM()
        {
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
            VertexNodes = new ObservableCollection<VertexNode>(VertexFactory.GetVerticesDijkstra()); VertexFactory.GetVerticesDijkstra();
            AddColumnCollection(13);
        }

        private void LoadCostSets()
        {
            MatrixAdjacency = VertexFactory.GetWeightMatrixCost();
            VertexNodes = new ObservableCollection<VertexNode>(VertexFactory.GetVerticesDijkstra()); VertexFactory.GetVerticesDijkstra();
            AddColumnCollection(13);
        }

        private bool CanPerformBypass()
        {
            if (VertexNodes == null || MatrixAdjacency.Count == 0)
                return false;
            if (StartBVertex == 0 ||
                StartBVertex > SelectedVNumber ||
                FinishBVertex > SelectedVNumber &&
                (SelectedBypass == 2 || SelectedBypass == 3))
                return false;
            return true;
        }

        private void PerformBypass()
        {
            foreach (var node in VertexNodes)
            {
                node.VStatus = VStatEnum.NoViewed;
                node.Distance = 10000;
                node.ParentId = -1;
            }

            bool result = true;
            switch (SelectedBypass)
            {
                case 0:
                    result = BypassService.DepthBypass(VertexNodes, MatrixAdjacency, BypassCollection);
                    break;
                case 1:
                    result = BypassService.WidthBypass(VertexNodes, MatrixAdjacency, BypassCollection);
                    break;
                case 2:
                    result = BypassService.DijkstraBypass(VertexNodes, MatrixAdjacency, BypassCollection, StartBVertex);
                    break;
                case 3:
                    result = BypassService.AStarBypass(VertexNodes, MatrixAdjacency, BypassCollection, StartBVertex, FinishBVertex);
                    break;
            }

            if (!result)
            {
                new ModernDialog()
                {
                    Title = "Обход графа",
                    Content = "Не удалось дойти до целевой вершины графа (нельзя обойти все вершины)"
                }.ShowDialog();
            }

        }

        private bool CanRandom()
        {
            if (SelectedVNumber == 0 || RandomStep == 0)
                return false;
            return true;
        }

        private void RandomMatrix()
        {
            VertexNodes = new ObservableCollection<VertexNode>(VertexFactory.GetVertices(SelectedVNumber));
            MatrixAdjacency = VertexFactory.GetRandomMatrix(SelectedVNumber, RandomStep, VertexNodes);
        }

        private bool CanAddVertex(string par)
        {
            if (VertexNodes == null)
                return false;
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
        private ObservableCollection<ObservableCollection<int>> _matrixAdjaency;



        #endregion

        #region Properties

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

        public int RandomStep
        {
            get { return GetProperty(() => RandomStep); }
            set { SetProperty(() => RandomStep, value); }
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
