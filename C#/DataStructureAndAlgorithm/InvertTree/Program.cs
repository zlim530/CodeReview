using System;
using System.Collections.Generic;

// LeetCode：226.反转二叉树：核心是遍历二叉树并且拿到当前结点时执行左右结点交换的动作即可
// https://leetcode-cn.com/problems/invert-binary-tree/
namespace InvertTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        // 递归实现
        // Preorder traversal
        // 104mS
        public static TreeNode InvertTree(TreeNode root) {
            if ( root == null)
            {
                return root;
            }

            // 使用前序遍历二叉树
            TreeNode tmp = root.left;
            root.left = root.right;
            root.right = tmp;

            InvertTree(root.left);
            InvertTree(root.right);

            return root;
        }

        // 104mS
        // Inorder traversal
        public static TreeNode InvertTreeInOrder(TreeNode root) {
            if ( root == null)
            {
                return root;
            }
            
            InvertTreeInOrder(root.left);

            TreeNode tmp = root.left;
            root.left = root.right;
            root.right = tmp;

            
            InvertTreeInOrder(root.left);

            return root;
        }

        // Postorder traversal
        // 100mS
        public static TreeNode InvertTreePostOrder(TreeNode root) {
            if ( root == null)
            {
                return root;
            }
            
            InvertTreePostOrder(root.left);
            InvertTreePostOrder(root.right);

            TreeNode tmp = root.left;
            root.left = root.right;
            root.right = tmp;

            return root;
        }
        

        public static TreeNode InvertTree100mS(TreeNode root){
            if ( root != null )
            {
                TreeNode node = root.right;
                root.right = InvertTree100mS(root.left);
                root.left = InvertTree100mS(node);
            }
            return root;
        }

        // 层序遍历迭代实现：112mS
        static TreeNode InvertTreeLevelTraversal(TreeNode root){
            if ( root == null)
            {
                return root;
            }

            Queue<TreeNode> queue = new Queue<TreeNode>();
            // TreeNode node = root;
            queue.Enqueue(root);
            while ( queue.Count != 0 )
            {
                TreeNode node = queue.Dequeue();
                TreeNode tmp = node.left;
                node.left = node.right;
                node.right = tmp;

                if ( node.left != null)
                {
                    queue.Enqueue(node.left);
                }

                if ( node.right != null)
                {
                    queue.Enqueue(node.right);
                }
            }

            return root;
        }


    }

    // 96mS case
    public class Solution {
        public TreeNode InvertTree(TreeNode root) {
            if (root == null) return null;
                Helper(root);
                return root;
        }

        // preorder traversal
        public void Helper(TreeNode root){
            if (root == null) return;
            var temp = root.left;
            root.left = root.right;
            root.right = temp;
            Helper(root.left);
            Helper(root.right);
        }
    }

    // Definition for a binary tree node.
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
