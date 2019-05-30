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

        private static Collection<VertexNode> GetNeighbours(ObservableCollection<ObservableCollection<int>> matrix,
            Collection<VertexNode> list, VertexNode node)
        {
            var result = new Collection<VertexNode>();
            for (var i = 0; i < list.Count; i++)
            {
                if (matrix[node.VertexId - 1][i] > 0)
                {
                    result.Add(list[i]);
                }
            }

            return result;
        }

        private static int GetHeuristicPath(Point from, Point to)
        {
            return (int)(Math.Abs(@from.X - to.X) + Math.Abs(@from.Y - to.Y));
        }

        public static bool DepthBypass(ObservableCollection<VertexNode> list,
            ObservableCollection<ObservableCollection<int>> matrix, ObservableCollection<VertexNode> path)
        {
            path.Clear();
            list[0].VStatus = VStatEnum.Open;
            stack.Push(list[0]);
            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                path.Add(currentNode);
                foreach (var node in GetNeighbours(matrix, list, currentNode))
                {
                    if (node.VStatus == VStatEnum.NoViewed)
                    {
                        node.VStatus = VStatEnum.Open;
                        stack.Push(node);
                    }
                }
            }

            return list.All(node => node.VStatus != VStatEnum.NoViewed);
        }

        public static bool WidthBypass(ObservableCollection<VertexNode> list,
            ObservableCollection<ObservableCollection<int>> matrix, ObservableCollection<VertexNode> path)
        {
            path.Clear();
            list[0].VStatus = VStatEnum.Open;
            queue.Enqueue(list[0]);
            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                path.Add(currentNode);
                foreach (var node in GetNeighbours(matrix, list, currentNode))
                {
                    if (node.VStatus == VStatEnum.NoViewed)
                    {
                        node.VStatus = VStatEnum.Open;
                        queue.Enqueue(node);
                    }
                }
            }

            return list.All(node => node.VStatus != VStatEnum.NoViewed);
        }

        public static bool DijkstraBypass(ObservableCollection<VertexNode> list,
            ObservableCollection<ObservableCollection<int>> matrix, ObservableCollection<VertexNode> path, int start)
        {
            path.Clear();
            int relax = 0;
            int newDistance;
            var sortedList = list.Where(node => node.VStatus != VStatEnum.Closed).OrderBy(node => node.Distance);
            var index = list.IndexOf(x => x.VertexId == start);
            list[index].Distance = 0;
            list[index].ParentId = start;
            list[index].VStatus = VStatEnum.Open;
            do
            {
                var currentNode = sortedList.FirstOrDefault(); //node with min distance
                if (currentNode == null)
                    return false;
                path.Add(currentNode);

                foreach (var node in GetNeighbours(matrix, list, currentNode))
                {
                    if (node.VStatus == VStatEnum.Closed) continue;

                    newDistance = matrix[currentNode.VertexId - 1][node.VertexId - 1] + currentNode.Distance;
                    if (node.VStatus == VStatEnum.NoViewed || newDistance < node.Distance)
                    {
                        node.Distance = newDistance;
                        node.ParentId = currentNode.VertexId;
                        if (node.VStatus != VStatEnum.Open) node.VStatus = VStatEnum.Open;
                        relax++;
                    }
                }
                currentNode.VStatus = VStatEnum.Closed;
            } while (list.Any(node => node.VStatus == VStatEnum.Open));
            return true;
        }

        public static bool AStarBypass(ObservableCollection<VertexNode> list,
            ObservableCollection<ObservableCollection<int>> matrix, ObservableCollection<VertexNode> path, int start,
            int finish)
        {
            path.Clear();
            int relax = 0;
            int newDistance;
            Point finishPoint = list[finish - 1].Position;
            foreach (var node in list)
            {
                node.HeuristicLength = GetHeuristicPath(node.Position, finishPoint);
            }
            var sortedList = list.Where(node => node.VStatus == VStatEnum.Open).OrderBy(node => node.Distance + node.HeuristicLength);
            var index = list.IndexOf(x => x.VertexId == start);
            list[index].Distance = 0;
            list[index].ParentId = start;
            list[index].VStatus = VStatEnum.Open;
            do
            {
                var currentNode = sortedList.FirstOrDefault(); //node with min distance
                if (currentNode == null)
                    return false;

                path.Add(currentNode);

                if (currentNode.VertexId == finish) return true;

                foreach (var node in GetNeighbours(matrix, list, currentNode))
                {
                    if (node.VStatus == VStatEnum.Closed) continue;
                    newDistance = matrix[currentNode.VertexId - 1][node.VertexId - 1] + currentNode.Distance +
                                  Math.Abs(node.HeuristicLength - currentNode.HeuristicLength);//опасно - эвристика below zero
                    if (node.VStatus == VStatEnum.NoViewed || newDistance < node.Distance)
                    {
                        node.Distance = newDistance;
                        if (node.VStatus != VStatEnum.Open) node.VStatus = VStatEnum.Open;
                        node.ParentId = currentNode.VertexId;
                        relax++;
                    }
                }
                currentNode.VStatus = VStatEnum.Closed;
            } while (list.Any(node => node.VStatus == VStatEnum.Open));

            return list[finish - 1].ParentId != -1; //не дошли до цели
        }

        //методы возвращают число! только для проверки релаксаций!

        public static int DijkstraBypass(Collection<VertexNode> list, ObservableCollection<ObservableCollection<int>> matrix, int start)
        {
            int relax = 0;
            int newDistance;
            var sortedList = list.Where(node => node.VStatus != VStatEnum.Closed).OrderBy(node => node.Distance);
            var index = list.IndexOf(x => x.VertexId == start);
            list[index].Distance = 0;
            list[index].ParentId = start;
            list[index].VStatus = VStatEnum.Open;
            do
            {
                var currentNode = sortedList.FirstOrDefault(); //node with min distance
                if (currentNode == null)
                    break;

                foreach (var node in GetNeighbours(matrix, list, currentNode))
                {
                    if (node.VStatus == VStatEnum.Closed) continue;

                    newDistance = matrix[currentNode.VertexId - 1][node.VertexId - 1] + currentNode.Distance;
                    if (node.VStatus == VStatEnum.NoViewed || newDistance < node.Distance)
                    {
                        node.Distance = newDistance;
                        node.ParentId = currentNode.VertexId;
                        if (node.VStatus != VStatEnum.Open) node.VStatus = VStatEnum.Open;
                        relax++;
                    }
                }
                currentNode.VStatus = VStatEnum.Closed;
            } while (list.Any(node => node.VStatus == VStatEnum.Open));
            return relax;
        }

        public static int AStarBypass(Collection<VertexNode> list, ObservableCollection<ObservableCollection<int>> matrix, int start, int finish)
        {
            int relax = 0;
            int newDistance;
            Point finishPoint = list[finish - 1].Position;
            foreach (var node in list)
            {
                node.HeuristicLength = GetHeuristicPath(node.Position, finishPoint);
            }
            var sortedList = list.Where(node => node.VStatus == VStatEnum.Open).OrderBy(node => node.Distance + node.HeuristicLength);
            var index = list.IndexOf(x => x.VertexId == start);
            list[index].Distance = 0;
            list[index].ParentId = start;
            list[index].VStatus = VStatEnum.Open;
            do
            {
                var currentNode = sortedList.FirstOrDefault(); //node with min distance
                if (currentNode == null)
                    break;

                if (currentNode.VertexId == finish) return relax;

                foreach (var node in GetNeighbours(matrix, list, currentNode))
                {
                    if (node.VStatus == VStatEnum.Closed) continue;
                    newDistance = matrix[currentNode.VertexId - 1][node.VertexId - 1] + currentNode.Distance +
                                  node.HeuristicLength - currentNode.HeuristicLength;//опасно - эвристика below zero
                    if (node.VStatus == VStatEnum.NoViewed || newDistance < node.Distance)
                    {
                        node.Distance = newDistance;
                        if (node.VStatus != VStatEnum.Open) node.VStatus = VStatEnum.Open;
                        node.ParentId = currentNode.VertexId;
                        relax++;
                    }
                }
                currentNode.VStatus = VStatEnum.Closed;
            } while (list.Any(node => node.VStatus == VStatEnum.Open));

            return relax;
        }
    }
}