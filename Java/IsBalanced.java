
// LeetCode：110.平衡二叉树
// https://leetcode-cn.com/problems/balanced-binary-tree/
public class IsBalanced {

    public static void main(String[] args) {
        System.out.println("Hello,world!");
    }

    public boolean isBalanced(TreeNode root){
        // boolean res = true;
        return isBalancedHelper(root) != -1;
        // return res;
    }

    private int isBalancedHelper(TreeNode root) {   
        if ( root == null) {
            return 0;
        }
        int leftHeight = isBalancedHelper(root.left) ;
        if ( leftHeight == -1) {
            return -1;
        }
        int rightHeight = isBalancedHelper(root.right) ;
        if ( rightHeight == -1) {
            return -1;
        }
        if ( Math.abs(leftHeight - rightHeight) > 1) {
            return  -1;
        }
        return Math.max(leftHeight, rightHeight) + 1;
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