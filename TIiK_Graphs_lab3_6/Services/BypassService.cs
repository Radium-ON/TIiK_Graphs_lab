using DevExpress.Mvvm.Native;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TIiK_Graphs_lab3_6.Models;

namespace TIiK_Graphs_lab3_6.Services
{
    public enum VStatEnum
    {
        NoViewed,
        Open,
        Closed
    }
    public static class BypassService
    {
        private static Stack<VertexNode> stack = new Stack<VertexNode>();
        private static Queue<VertexNode> queue = new Queue<VertexNode>();

        /// <summary>
        /// Исправить обход графов с циклами (проверять посещение всех вершин)
        /// 
        /// </summary>

        private static int GetHeuristicPathLength(Point from, Point to)
        {
            return (int)(Math.Abs(@from.X - to.X) + Math.Abs(@from.Y - to.Y));
        }

        public static void DepthBypass(ObservableCollection<VertexNode> list,
            ObservableCollection<ObservableCollection<int>> matrix,
            ObservableCollection<VertexNode> path)
        {
            path.Clear();
            stack.Push(list[0]);
            list[0].VStatus = VStatEnum.Open;
            while (stack.Count > 0)
            {
                var vertex = stack.Pop() as VertexNode;
                path.Add(vertex);
                for (int i = 0; i < list.Count; i++)
                {
                    if (matrix[vertex.VertexId - 1][i] > 0 && list[i].VStatus == VStatEnum.NoViewed)
                    {
                        stack.Push(list[i]);
                        list[i].VStatus = VStatEnum.Open;
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
            list[0].VStatus = VStatEnum.Open;
            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue() as VertexNode;
                path.Add(vertex);
                for (int i = 0; i < list.Count; i++)
                {
                    if (matrix[vertex.VertexId - 1][i] > 0 && list[i].VStatus == VStatEnum.NoViewed)
                    {
                        queue.Enqueue(list[i]);
                        list[i].VStatus = VStatEnum.Open;
                    }
                }
            }
        }

        public static bool DijkstraBypass(ObservableCollection<VertexNode> list,
            ObservableCollection<ObservableCollection<int>> matrix,
            ObservableCollection<VertexNode> path, int start, int finish)
        {
            path.Clear();
            int relax = 0;
            int newD;
            var sortedList = list.Where(node => node.VStatus == VStatEnum.Open).OrderBy(node => node.Distance);
            var index = list.IndexOf(x => x.VertexId == start);
            list[index].Distance = 0;
            list[index].ParentId = start;
            list[index].VStatus = VStatEnum.Open;
            while (list.Any(node => node.VStatus == VStatEnum.Open))
            {
                var u = sortedList.FirstOrDefault();//node with min distance
                if (u == null)
                    return false;
                path.Add(u);

                if (u.VertexId == finish) return true;

                for (int i = 0; i < list.Count; i++)
                {
                    if (matrix[u.VertexId - 1][i] > 0 && list[i].VStatus == VStatEnum.NoViewed)
                    {
                        newD = matrix[u.VertexId - 1][i] + u.Distance;
                        if (newD < list[i].Distance)
                        {
                            list[i].Distance = newD;
                            relax++;
                        }

                        list[i].VStatus = VStatEnum.Open;
                        list[i].ParentId = u.VertexId;


                    }
                }
                list.ElementAt(u.VertexId - 1).VStatus = VStatEnum.Closed;
            }
            return true;
        }

        public static bool AStarBypass(ObservableCollection<VertexNode> list,
            ObservableCollection<ObservableCollection<int>> matrix,
            ObservableCollection<VertexNode> path, int start, int finish)
        {
            path.Clear();
            int relax = 0;
            int newD;
            var sortedList = list.Where(node => node.VStatus == VStatEnum.Open).OrderBy(node => node.Distance);
            var index = list.IndexOf(x => x.VertexId == start);
            list[index].Distance = GetHeuristicPathLength(list[index].Position, list[finish - 1].Position);
            list[index].ParentId = start;
            list[index].VStatus = VStatEnum.Open;
            while (list.Any(node => node.VStatus == VStatEnum.Open))
            {
                var u = sortedList.FirstOrDefault();//node with min distance
                path.Add(u);

                if (u.VertexId == finish) return true;

                for (int i = 0; i < list.Count; i++)
                {
                    if (matrix[u.VertexId - 1][i] > 0 && list[i].VStatus == VStatEnum.NoViewed)
                    {
                        newD = matrix[u.VertexId - 1][i] + u.Distance + GetHeuristicPathLength(list[i].Position, list[finish - 1].Position);
                        if (newD < list[i].Distance)
                        {
                            list[i].Distance = newD;
                            relax++;
                        }

                        list[i].VStatus = VStatEnum.Open;
                        list[i].ParentId = u.VertexId;


                    }
                }
                list.ElementAt(u.VertexId - 1).VStatus = VStatEnum.Closed;
            }

            return list[finish - 1].ParentId != -1;
        }
    }
}
