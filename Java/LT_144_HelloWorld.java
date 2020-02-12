// package com.zlim;

import java.util.LinkedList;
import java.util.List;


// LeetCode：144.二叉树的前序遍历
// https://leetcode-cn.com/problems/binary-tree-preorder-traversal/
public class LT_144_HelloWorld {

    public static void main(String[] args) {
        System.out.println("Hello World!");
        System.out.println("你好,世界!");
    }

    // 递归实现
    public List<Integer> preorderTraversal(TreeNode root) {
        LinkedList<Integer> list = new LinkedList<>();
        preorder(root, list);
        return list;
        // if ( root == null) {
        //     return null;
        // }

        // list.add(root.val);

        // preorderTraversal(root.left);
        // preorderTraversal(root.right);

        // return list;
    }

    private static void preorder(TreeNode root,LinkedList<Integer> list) {
        // if ( root == null) {
        //     return ;
        // } else {
        //     list.add(root.val);
        //     preorder(root.left, list);
        //     preorder(root.right, list);
        // }
        if(root != null){
            // list.push(root.val);
            list.add(root.val);
            preorder(root.left, list);
            preorder(root.right, list);
        }
    }

    // 迭代实现
    public List<Integer> preordertraversal(TreeNode root) {
        LinkedList<TreeNode> stack = new LinkedList<>();
        LinkedList<Integer> output = new LinkedList<>();

        if ( root == null) {
            return output;
        }

        stack.add(root);

        while ( !stack.isEmpty()) {
            // 相当于Queue.deQueue()队列出队：即弹出队头元素：也即弹出最先加入的元素
            TreeNode node = stack.pollLast();
            // 添加元素到尾部：模拟入栈操作
            output.add(node.val);
            if ( node.right != null) {
                stack.add(node.right);
            }

            if ( node.left != null) {
                stack.add(node.left);
            }

        }

        return output;

    }

    public class TreeNode {
        int val;
        TreeNode left;
        TreeNode right;
        public TreeNode(int x) {
            val = x;
        }
    }

}


