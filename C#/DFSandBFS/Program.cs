using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFSandBFS
{
    class Program
    {
        static void Main(string[] args)
        {
            var values = Enumerable.Range(0, 10).ToArray();
            var binarySearchTree = GetTree(values,0,values.Length-1);
            DFS(binarySearchTree);
            Console.WriteLine("=============================");
            BFS(binarySearchTree);
        }

        private static Node GetTree(int[] values, int lowIndex, int highIndex)
        {
            if (lowIndex > highIndex)
            {
                return null;
            }
            var middleIndex = lowIndex + (highIndex - lowIndex) / 2;
            var node = new Node(values[middleIndex]);
            node.Left = GetTree(values,lowIndex,middleIndex-1);
            node.Right = GetTree(values,middleIndex+1,highIndex);
            return node;

        }

        // DFS:depth first search 深度优先搜索
        static void DFS(Node node)
        {
            if ( node == null)
            {
                return;
            }
            DFS(node.Left);
            Console.WriteLine(node.Value);
            DFS(node.Right);
        }

        // BFS:breadth first search 广度优先搜索
        static void BFS(Node root)
        {
            var q = new Queue<Node>();
            q.Enqueue(root);//Enqueue 将对象添加至Queue<T>结尾处
            while (q.Count > 0)
            {
                var node = q.Dequeue();// 移除并返回Queue<T>开始处的对象
                Console.WriteLine(node.Value);
                if (node.Left != null)
                {
                    q.Enqueue(node.Left);
                }
                if ( node.Right != null)
                {
                    q.Enqueue(node.Right);
                }
            }
        }
    }

    class Node
    {
        public int Value { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public Node(int value)
        {
            Value = value;
        }
    }
}
