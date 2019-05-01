﻿using System;
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

        public VertexNode(int vertexId, int distance, string name, int vStatus)//for Dijkstra
        {
            VertexId = vertexId;
            Distance = distance;
            Name = name;
            VStatus = vStatus;
        }

        public VertexNode(int vertexId, int vStatus)//for width & depth
        {
            VertexId = vertexId;
            VStatus = vStatus;
        }

        public enum VertexStatusEnum
        {
            noVisited,
            visited,
            closed
        }
    }
}