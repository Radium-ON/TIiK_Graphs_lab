using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIiK_Graphs_lab3_6.Models
{
    public static class VertexFactory
    {
        public static ObservableCollection<VertexNode> GetVertexDijkstra()
        {
            var coll = new ObservableCollection<VertexNode>
            {
                new VertexNode(1, "Новосибирск",55.030199, 82.920430),
                new VertexNode(2, "Прага",50.080293, 14.428983),
                new VertexNode(3, "Москва",55.755814, 37.617635),
                new VertexNode(4, "Санкт-Петербург",59.939095, 30.315868),
                new VertexNode(5, "Рим",41.902689, 12.496176),
                new VertexNode(6, "Милан",45.469436, 9.180621),
                new VertexNode(7, "Цюрих",47.386966, 8.534139),
                new VertexNode(8, "Лондон",51.507351, -0.127660),
                new VertexNode(9, "Неаполь",40.853197, 14.249544),
                new VertexNode(10, "Верона",45.441586, 10.973442),
                new VertexNode(11, "Франкфурт",50.106045, 8.679486),
                new VertexNode(12, "Вена",48.206481, 16.363451),
                new VertexNode(13, "Катания",37.525338, 15.066203),
            };
            return coll;
        }

        public static ObservableCollection<VertexNode> GetVertexes(int num)
        {
            var coll = new ObservableCollection<VertexNode>();
            for (int i = 1; i <= num; i++)
            {
                coll.Add(new VertexNode(i, $"rand {i}"));
            }
            return coll;
        }

        public static ObservableCollection<ObservableCollection<int>> GetWeightMatrix(int vertexNum)
        {
            var coll = new ObservableCollection<ObservableCollection<int>>();
            for (int i = 0; i < vertexNum; i++)
            {
                coll.Add(new ObservableCollection<int>());
                for (int j = 0; j < vertexNum; j++)
                {
                    coll[i].Add(0);
                }
            }
            return coll;
        }

        public static ObservableCollection<ObservableCollection<int>> GetRandomMatrix(int vertexNum)
        {
            var random = new Random();
            var coll = new ObservableCollection<ObservableCollection<int>>();
            for (int i = 0; i < vertexNum; i++)
            {
                coll.Add(new ObservableCollection<int>());
                for (int j = 0; j < vertexNum; j++)
                {
                    if (i == j)
                    {
                        coll[i].Add(0);
                    }
                    else
                    {
                        coll[i].Add(random.Next(0, 2));
                    }
                }
            }
            return coll;
        }

        public static ObservableCollection<ObservableCollection<int>> GetWeightMatrixCost()
        {
            var coll = new ObservableCollection<ObservableCollection<int>>
            {
                new ObservableCollection<int>{0,126,54,129,0,0,0,0,0,0,0,0,0},     //1
                new ObservableCollection<int>{126,0,0,0,190,0,0,0,0,0,0,0,0},      //2
                new ObservableCollection<int>{54,0,0,69,139,0,0,0,0,0,0,0,0},      //3
                new ObservableCollection<int>{129,0,69,0,0,53,0,0,0,0,0,0,0},      //4
                new ObservableCollection<int>{0,190,139,0,0,154,174,229,0,0,0,0,0},//5
                new ObservableCollection<int>{0,0,0,53,154,0,0,0,56,0,0,0,0},      //6
                new ObservableCollection<int>{0,0,0,0,174,0,0,0,0,0,0,0,348},      //7
                new ObservableCollection<int>{0,0,0,0,229,0,0,0,0,190,0,0,0},      //8
                new ObservableCollection<int>{0,0,0,0,0,56,0,0,0,57,0,0,0},        //9
                new ObservableCollection<int>{0,0,0,0,0,0,0,190,57,0,295,0,138},   //10
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,0,0,356,238},      //11
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,0,356,0,145},      //12
                new ObservableCollection<int>{0,0,0,0,0,0,348,0,0,138,238,145,0}   //13
            };
            return coll;
        }

        public static ObservableCollection<ObservableCollection<int>> GetWeightMatrixQuality()
        {
            var coll = new ObservableCollection<ObservableCollection<int>>
            {
                new ObservableCollection<int>{0,180,100,100,0,0,0,0,0,0,0,0,0},    //1
                new ObservableCollection<int>{180,0,0,0,200,0,0,0,0,0,0,0,0},      //2
                new ObservableCollection<int>{100,0,0,120,100,0,0,0,0,0,0,0,0},    //3
                new ObservableCollection<int>{100,0,120,0,0,100,0,0,0,0,0,0,0},    //4
                new ObservableCollection<int>{0,200,100,0,0,180,100,200,0,0,0,0,0},//5
                new ObservableCollection<int>{0,0,0,100,180,0,0,0,100,0,0,0,0},    //6
                new ObservableCollection<int>{0,0,0,0,100,0,0,0,0,0,0,0,200},      //7
                new ObservableCollection<int>{0,0,0,0,200,0,0,0,0,180,0,0,0},      //8
                new ObservableCollection<int>{0,0,0,0,0,100,0,0,0,120,0,0,0},      //9
                new ObservableCollection<int>{0,0,0,0,0,0,0,180,120,0,180,0,200},  //10
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,180,0,200,200},    //11
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,0,200,0,180},      //12
                new ObservableCollection<int>{0,0,0,0,0,0,200,0,0,200,200,180,0},  //13
            };
            return coll;
        }

    }
}
