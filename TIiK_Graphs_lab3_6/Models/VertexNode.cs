using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace TIiK_Graphs_lab3_6.Models
{
    public class VertexNode : ViewModelBase
    {
        private int _vertexId;

        public int VertexId
        {
            get { return GetProperty(() => VertexId); }
            set
            {
                if(value==0)
                    SetProperty(()=> VertexId, 1);
                else
                    SetProperty(()=> VertexId, value);
            }
        }

        public int ParentId { get; set; }

        public int Distance { get; set; }

        private string _name;

        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(()=>Name, value); }
        }

        public int VStatus { get; set; }

        public VertexNode(int vertexId, string name)//for Dijkstra
        {
            VertexId = vertexId;
            Distance = -1;
            Name = name;
            VStatus = 1;
        }

        public VertexNode(int vertexId, int vStatus)//for width & depth
        {
            VertexId = vertexId;
            VStatus = vStatus;
        }       
    }
}