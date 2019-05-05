﻿using DevExpress.Mvvm.Native;
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
        /// добавить дейкстры
        /// </summary>



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

        public static void DijkstraBypass(ObservableCollection<VertexNode> list,
            ObservableCollection<ObservableCollection<int>> matrix,
            ObservableCollection<VertexNode> path, int start)
        {
            path.Clear();
            var sortedList = list.Where(node => node.VStatus == VStatEnum.Open).OrderBy(node => node.Distance);
            var vertex = list.Single(x => x.VertexId == start);
            vertex.Distance = 0;
            vertex.ParentId = start;
            vertex.VStatus = VStatEnum.Open;
            while (list.Any((node => node.VStatus != VStatEnum.Closed)))
            {
                var u = sortedList.First();//node with min distance
                path.Add(u);
                for (int i = 0; i < list.Count; i++)
                {
                    if (matrix[u.VertexId - 1][i] > 0 && list[i].VStatus == VStatEnum.NoViewed)
                    {
                        list[i].Distance = matrix[u.VertexId - 1][i] + u.Distance;
                        list[i].VStatus = VStatEnum.Open;
                    }
                }
                list.ElementAt(u.VertexId - 1).VStatus = VStatEnum.Closed;
            }
        }
    }
}
