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

        public string Name
        {
            get { return GetProperty(() => Name); }
            set { SetProperty(()=>Name, value); }
        }       

        public VertexNode(int vertexId, string name)//for Dijkstra
        {
            VertexId = vertexId;
            Distance = 10000;
            Name = name;
            VStatus = VStatEnum.NoViewed;
        }

        public VStatEnum VStatus { get; set; }

        public VertexNode(int vertexId, VStatEnum stat)//for width & depth
        {
            VertexId = vertexId;
            VStatus = stat;
        }

        #region Overrides of Object

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}