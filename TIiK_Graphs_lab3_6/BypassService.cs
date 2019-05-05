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
    public static class BypassService
    {
        private static Stack stack = new Stack();
        private static Queue queue = new Queue();

        enum VStatEnum
        {
            NoVisited = 1,
            Visited = 2,
            Closed = 3
        }

        public static void DepthBypass(ObservableCollection<VertexNode> list, ObservableCollection<ObservableCollection<int>> matrix, ref ObservableCollection<VertexNode> path)
        {
            stack.Push(list[0]);
            list[0].VStatus = (int)VStatEnum.Visited;
            while (stack.Count>0)
            {
                var vertex = stack.Pop() as VertexNode;
                path.Add(vertex);
                for (int i = 0; i < list.Count; i++)
                {
                    if (matrix[vertex.VertexId-1][i]>0 && list[i].VStatus==(int)VStatEnum.NoVisited)
                    {
                        stack.Push(list[i]);
                        list[i].VStatus = (int)VStatEnum.Visited;
                    }
                }
            }
        }
    }
}
