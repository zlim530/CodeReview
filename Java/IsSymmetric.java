/* 给定一个二叉树，检查它是否是镜像对称的。

例如，二叉树 [1,2,2,3,4,4,3] 是对称的。

    1
   / \
  2   2
 / \ / \
3  4 4  3
但是下面这个 [1,2,2,null,3,null,3] 则不是镜像对称的:

    1
   / \
  2   2
   \   \
   3    3
 */

/**
 * IsSymmetric
 */

// LeetCode：101.堆成二叉树
// https://leetcode-cn.com/problems/symmetric-tree/submissions/
public class IsSymmetric {

    public boolean isSymmetric(TreeNode root) {
        if ( root == null) {
            return true;
        }
        return isSymmetricHelper(root.left,root.right);
    }

    private boolean isSymmetricHelper(TreeNode left, TreeNode right) {
        if ( left == null && right != null || left != null && right == null) {  // 存在度为1的结点一定是不平衡的
            return false;
        }
        if ( left != null && right != null) {
            if ( left.val != right.val) {
                return false;
            }
            return isSymmetricHelper(left.left, right.right) && isSymmetricHelper(left.right, right.left);
        }
        return true;
    }

    public class TreeNode {
        int val;
        TreeNode left;
        TreeNode right;
        TreeNode(int x) { val = x; }
    }
}