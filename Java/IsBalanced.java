/* 
给定一个二叉树，判断它是否是高度平衡的二叉树。

本题中，一棵高度平衡二叉树定义为：

一个二叉树每个节点 的左右两个子树的高度差的绝对值不超过1。

示例 1:

给定二叉树 [3,9,20,null,null,15,7]

    3
   / \
  9  20
    /  \
   15   7
返回 true 。

示例 2:

给定二叉树 [1,2,2,3,3,null,null,4,4]

       1
      / \
     2   2
    / \
   3   3
  / \
 4   4
返回 false 。
 */

// LeetCode：110.平衡二叉树
// https://leetcode-cn.com/problems/balanced-binary-tree/
public class IsBalanced {

    public static void main(String[] args) {
        System.out.println("Hello,world!");
    }

    public boolean isBalanced(TreeNode root){
        return isBalancedHelper(root) != -1;
    }

    private int isBalancedHelper(TreeNode root) {   
        if ( root == null) {
            return 0;   // 空树的高度为0
        }
        int leftHeight = isBalancedHelper(root.left) ;
        if ( leftHeight == -1) {
            return -1;
        }
        int rightHeight = isBalancedHelper(root.right) ;
        if ( rightHeight == -1) {
            return -1;
        }

        // 左右子树高度差的绝对值不超过1
        if ( Math.abs(leftHeight - rightHeight) > 1) {
            return  -1;
        }
        return Math.max(leftHeight, rightHeight) + 1;   // 整颗树的高度 = 左/右子树高度 + 1
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