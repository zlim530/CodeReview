using System;
using System.Collections.Generic;

namespace TreeTraversal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        
        static Stack<int> PreorderTraversal(TreeNode root){
            Stack<TreeNode> stack = new Stack<TreeNode>();
            Stack<int> output = new Stack<int>();

            if ( root == null)
            {
                return output;
            }

            while ( stack.Count != 0)
            {
                // TreeNode node = stack.FindLast();
                // stack.RemoveLast();
                // output.AddLast(node.val);
                TreeNode node = stack.Pop();
                output.Push(node.val);
                if ( node.right != null)
                {
                    // stack.AddLast(node.left);
                    stack.Push(node.right);
                }

                if ( node.left != null)
                {
                    // stack.AddLast(node.right);
                    stack.Push(node.left);
                }
            }
            return output;

        }

    }

    public class TreeNode
    {
        public int val;

        public TreeNode left;

        public TreeNode right;

        public TreeNode(int x)
        {
            val = x;
        }
    }
}
