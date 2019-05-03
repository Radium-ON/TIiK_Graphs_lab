using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIiK_Graphs_lab3_6.Models
{
    public class VertexNode : BindableBase
    {
        private int _vertexId;

        public int VertexId
        {
            get => _vertexId;
            set { SetProperty(ref _vertexId, value); }
        }

        public int ParentId { get; set; }

        public int Distance { get; set; }

        private string _name;

        public string Name
        {
            get => _name;
            set { SetProperty(ref _name, value); }
        }

        public int VStatus { get; set; }

        public VertexNode(int vertexId, string name)//for Dijkstra
        {
            VertexId = vertexId;
            Distance = -1;
            Name = name;
            VStatus = (int)VertexStatusEnum.NoVisited;
        }

        public VertexNode(int vertexId, int vStatus)//for width & depth
        {
            VertexId = vertexId;
            VStatus = vStatus;
        }

        public enum VertexStatusEnum
        {
            NoVisited = 1,
            Visited = 2,
            Closed = 3
        }
    }
}