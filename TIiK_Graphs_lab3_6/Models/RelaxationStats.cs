using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace TIiK_Graphs_lab3_6.Models
{
    public class RelaxationStats : ViewModelBase
    {
        public int VertexNumber
        {
            get => GetProperty(() => VertexNumber);
            set => SetProperty(()=>VertexNumber,value);
        } //Число вершин
        
        public int EdgesNumber
        {
            get => GetProperty(() => EdgesNumber);
            set => SetProperty(()=>EdgesNumber,value);
        } // Число рёбер

        public int Theory
        {
            get => VertexNumber * VertexNumber + EdgesNumber;
            set { SetProperty(() => Theory, value); }
        } //Теор. значение

        public int DijkstraRelax { get; set; }//Релакс. Дейкстры

        public int StarRelax { get; set; }//Релакс. A-Star

        public RelaxationStats(int vtx, int e, int d, int ast)
        {
            VertexNumber = vtx;
            EdgesNumber = e;
            DijkstraRelax = d;
            StarRelax = ast;
        }

        public RelaxationStats(int vtx, int e, int d)
        {
            VertexNumber = vtx;
            EdgesNumber = e;
            DijkstraRelax = d;
        }

    }
}
