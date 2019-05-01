using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIiK_Graphs_lab3_6.Models
{
    public class RelaxationStats
    {
        public int VertexNumber { get; set; }//Число вершин      
    
        public int Theory { get; set; }//Теор. значение

        public int DijkstraRelax { get; set; }//Релакс. Дейкстры

        public int StarRelax { get; set; }//Релакс. A-Star

        public RelaxationStats(int vtx, int th, int d, int ast)
        {
            VertexNumber = vtx;
            Theory = th;
            DijkstraRelax = d;
            StarRelax = ast;
        }
    }
}
