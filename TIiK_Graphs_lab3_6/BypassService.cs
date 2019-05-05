using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIiK_Graphs_lab3_6.Models;

namespace TIiK_Graphs_lab3_6
{
    public enum VStatEnum
    {
        NoVisited = 1,
        Visited = 2,
        Closed = 3
    }
    public static class BypassService
    {
        private static Stack stack = new Stack();
        private static Queue queue = new Queue();

        /// <summary>
        /// Исправить обход графов с циклами (проверять посещение всех вершин)
        /// добавить дейкстры
        /// </summary>



        public static void DepthBypass(ObservableCollection<VertexNode> list, 
            ObservableCollection<ObservableCollection<int>> matrix, 
            ObservableCollection<VertexNode> path)
        {
            path.Clear();
            stack.Push(list[0]);
            list[0].VStatus = VStatEnum.Visited;
            while (stack.Count > 0)
            {
                var vertex = stack.Pop() as VertexNode;
                path.Add(vertex);
                for (int i = 0; i < list.Count; i++)
                {
                    if (matrix[vertex.VertexId - 1][i] > 0 && list[i].VStatus == VStatEnum.NoVisited)
                    {
                        stack.Push(list[i]);
                        list[i].VStatus = VStatEnum.Visited;
                    }
                }
            }
        }

        public static void WidthBypass(ObservableCollection<VertexNode> list, 
            ObservableCollection<ObservableCollection<int>> matrix, 
            ObservableCollection<VertexNode> path)
        {
            path.Clear();
            queue.Enqueue(list[0]);
            list[0].VStatus = VStatEnum.Visited;
            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue() as VertexNode;
                path.Add(vertex);
                for (int i = 0; i < list.Count; i++)
                {
                    if (matrix[vertex.VertexId - 1][i] > 0 && list[i].VStatus == VStatEnum.NoVisited)
                    {
                        queue.Enqueue(list[i]);
                        list[i].VStatus = VStatEnum.Visited;
                    }
                }
            }
        }

    }
}
