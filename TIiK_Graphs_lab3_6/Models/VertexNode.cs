using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace TIiK_Graphs_lab3_6.Models
{
    public class VertexNode : BindableBase
    {
        public int VertexId { get; set; }

        public int ParentId { get; set; }

        public int Distance { get; set; }

        public string Name { get; set; }

        public int VStatus { get; set; }

        public VertexNode(int vertexId, string name)//for Dijkstra
        {
            VertexId = vertexId;
            Distance = -1;
            Name = name;
            VStatus = (int) VertexStatusEnum.NoVisited;
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