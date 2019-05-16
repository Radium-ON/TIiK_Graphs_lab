using DevExpress.Mvvm;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TIiK_Graphs_lab3_6.Services;
using BindableBase = DevExpress.Mvvm.BindableBase;

namespace TIiK_Graphs_lab3_6.Models
{
    public class VertexNode : BindableBase
    {
        public int VertexId
        {
            get { return GetProperty(() => VertexId); }
            set
            {
                if (value == 0)
                    SetProperty(() => VertexId, 1);
                else
                    SetProperty(() => VertexId, value);
            }
        }

        public int ParentId { get; set; }

        public int Distance { get; set; }

        public Point Position { get; set; }

        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }

        public VStatEnum VStatus { get; set; }

        public int[] Edges { get; set; }//размер - число ребер, значения - номера смежных вершин

        public VertexNode(int vertexId, string name)//for Dijkstra
        {
            VertexId = vertexId;
            Distance = 10000;
            Name = name;
            VStatus = VStatEnum.NoViewed;
            ParentId = -1;
        }

        public VertexNode(int vertexId, string name, double x, double y)//for A*
        {
            VertexId = vertexId;
            Distance = 10000;
            Name = name;
            Position = new Point(x, y);
        }

        public VertexNode(int vertexId, string name, int[] edges, double x, double y)//for A*
        {
            VertexId = vertexId;
            Distance = 10000;
            Name = name;
            Position = new Point(x, y);
            Edges = edges;
        }
        #region Overrides of Object

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}