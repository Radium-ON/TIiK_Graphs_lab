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
                new VertexNode(1, "Новосибирск"),
                new VertexNode(2, "Прага"),
                new VertexNode(3, "Москва"),
                new VertexNode(4, "Санкт-Петербург"),
                new VertexNode(5, "Рим"),
                new VertexNode(6, "Милан"),
                new VertexNode(7, "Цюрих"),
                new VertexNode(8, "Лондон"),
                new VertexNode(9, "Неаполь"),
                new VertexNode(10, "Верона"),
                new VertexNode(11, "Франкфурт"),
                new VertexNode(12, "Вена"),
                new VertexNode(13, "Катания"),
            };
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

        public static ObservableCollection<ObservableCollection<int>> GetWeightMatrixCost()
        {
            var coll = new  ObservableCollection<ObservableCollection<int>>
            {
                new ObservableCollection<int>{0,126,54,129,0,0,0,0,0,0,0,0,0}, //1
                new ObservableCollection<int>{0,0,0,0,190,0,0,0,0,0,0,0,0},    //2
                new ObservableCollection<int>{0,0,0,69,139,0,0,0,0,0,0,0,0},   //3
                new ObservableCollection<int>{0,0,0,0,0,53,0,0,0,0,0,0,0},     //4
                new ObservableCollection<int>{0,0,0,0,0,154,174,229,0,0,0,0,0},//5
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,56,0,0,0,0},     //6
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,0,0,0,348},    //7
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,190,0,0,0},    //8
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,57,0,0,0},     //9
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,0,295,0,138},  //10
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,0,0,356,238},  //11
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,0,0,0,145},    //12
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,0,0,0,0},      //13
            };
            return coll;
        }

        public static ObservableCollection<ObservableCollection<int>> GetWeightMatrixQuality()
        {
            var coll = new  ObservableCollection<ObservableCollection<int>>
            {
                new ObservableCollection<int>{0,180,100,100,0,0,0,0,0,0,0,0,0},//1
                new ObservableCollection<int>{0,0,0,0,200,0,0,0,0,0,0,0,0},    //2
                new ObservableCollection<int>{0,0,0,120,100,0,0,0,0,0,0,0,0},  //3
                new ObservableCollection<int>{0,0,0,0,0,100,0,0,0,0,0,0,0},    //4
                new ObservableCollection<int>{0,0,0,0,0,180,100,200,0,0,0,0,0},//5
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,100,0,0,0,0},    //6
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,0,0,0,200},    //7
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,180,0,0,0},    //8
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,120,0,0,0},    //9
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,0,180,0,200},  //10
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,0,0,200,200},  //11
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,0,0,0,180},    //12
                new ObservableCollection<int>{0,0,0,0,0,0,0,0,0,0,0,0,0},      //13
            };
            return coll;
        }

    }
}
