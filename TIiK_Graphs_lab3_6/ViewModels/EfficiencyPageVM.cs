
using DevExpress.Mvvm;
using FirstFloor.ModernUI.Windows.Controls;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIiK_Graphs_lab3_6.Models;
using TIiK_Graphs_lab3_6.Services;

namespace TIiK_Graphs_lab3_6.ViewModels
{
    public class EfficiencyPageVM : ViewModelBase
    {
        public EfficiencyPageVM()
        {
            RelaxationScore = new ObservableCollection<RelaxationStats>();
            BypassCommand = new DelegateCommand(PerformTests, CanPerformTests);
        }

        private bool CanPerformTests()
        {
            if (TestVertexNum == 0 || TestsNum == 0)
                return false;
            if (StartTestVertex == 0 || FinishTestVertex == 0)
                return false;
            return true;
        }

        private void PerformTests()
        {
            foreach (var node in VertexNodes)
            {
                node.VStatus = VStatEnum.NoViewed;
                node.Distance = 10000;
                node.ParentId = -1;
            }

            int result = 0;
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
                    result = BypassService.AStarBypass(new Collection<int>());
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


        #region Properties
        public ObservableCollection<RelaxationStats> RelaxationScore
        {
            get { return GetProperty(() => RelaxationScore); }
            set { SetProperty(() => RelaxationScore, value); }
        }

        public int StartTestVertex
        {
            get { return GetProperty(() => StartTestVertex); }
            set
            {
                if (value <= 0)
                    SetProperty(() => StartTestVertex, 1);
                else if (value < TestVertexNum)
                    SetProperty(() => StartTestVertex, value);
            }
        }

        public int FinishTestVertex
        {
            get { return GetProperty(() => FinishTestVertex); }
            set
            {
                if (value <= 0)
                    SetProperty(() => FinishTestVertex, 1);
                else if (value <= TestVertexNum)
                    SetProperty(() => FinishTestVertex, value);
            }
        }

        public int TestVertexNum
        {
            get { return GetProperty(() => TestVertexNum); }
            set
            {
                if (value <= 0)
                    SetProperty(() => TestVertexNum, 1);
                else if (value <= 100)
                    SetProperty(() => TestVertexNum, value);
            }
        }

        public int TestsNum
        {
            get { return GetProperty(() => TestsNum); }
            set
            {
                if (value <= 0)
                    SetProperty(() => TestsNum, 1);
                else if (value <= 10)
                    SetProperty(() => TestsNum, value);
            }
        }


        #endregion


        #region DelegateCommands
        public DelegateCommand BypassCommand { get; private set; }



        #endregion
    }
}
