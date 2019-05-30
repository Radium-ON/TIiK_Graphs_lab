
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
            TestStartCommand = new DelegateCommand(PerformTests, CanPerformTests);
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
            var diikstraAvg = 0;
            var astarAvg = 0;
            int sumD = 0;
            int sumA = 0;
            for (int i = 0; i < TestsNum; i++)
            {
                var vertices = VertexFactory.GetVertices(TestVertexNum);
                var matrix = VertexFactory.GetRandomMatrix(TestVertexNum, 500, vertices);

                sumD += BypassService.DijkstraBypass(vertices, matrix, StartTestVertex);
                foreach (var node in vertices)
                {
                    node.VStatus = VStatEnum.NoViewed;
                    node.Distance = 10000;
                    node.ParentId = -1;
                }
                sumA += BypassService.AStarBypass(vertices, matrix, StartTestVertex, FinishTestVertex);
            }

            diikstraAvg = sumD / TestsNum;
            astarAvg = sumA / TestsNum;
            RelaxationScore.Add(new RelaxationStats(TestVertexNum, diikstraAvg, astarAvg));
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
        public DelegateCommand TestStartCommand { get; private set; }



        #endregion
    }
}
