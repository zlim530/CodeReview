/* 二叉搜索树中的两个节点被错误地交换。

请在不改变其结构的情况下，恢复这棵树。

示例 1:

输入: [1,3,null,null,2]

   1
  /
 3
  \
   2

输出: [3,1,null,null,2]

   3
  /
 1
  \
   2
示例 2:

输入: [3,1,4,null,null,2]

  3
 / \
1   4
   /
  2

输出: [2,1,4,null,null,3]

  2
 / \
1   4
   /
  3
 */

// LeetCode：99.恢复二叉搜索树
// https://leetcode-cn.com/problems/recover-binary-search-tree/submissions/
// 解题思路：https://zhuanlan.zhihu.com/p/73093380

public class RecoverTree {

    public static void recoverTree(TreeNode root) {
        if ( root == null) {
          return;
        }
        TreeNode leftMax = getMaxOfBST(root.left);
        TreeNode rightMin = getMinOfBST(root.right);
        if ( leftMax != null && rightMin != null) {
            if ( leftMax.val > root.val && rightMin.val < root.val) {
                int tmp = leftMax.val;
                leftMax.val = rightMin.val;
                rightMin.val = tmp;
            }
        }

        if ( leftMax != null) {
            if ( leftMax.val > root.val) {
                int tmp = leftMax.val;
                leftMax.val = root.val;
                root.val = tmp;
            }
        }

        if ( rightMin != null) {
            if ( rightMin.val < root.val) {
                int tmp = rightMin.val;
                rightMin.val = root.val;
                root.val = tmp;
            }
        }

        recoverTree(root.left);
        recoverTree(root.right);
    }

    private static TreeNode getMinOfBST(TreeNode root) {
        if ( root == null) {
            return null;
        }
        TreeNode leftMin = getMinOfBST(root.left);
        TreeNode rightMin = getMinOfBST(root.right);
        TreeNode min = root;
        if ( leftMin != null && min.val > leftMin.val) {
            min = leftMin;
        }
        if ( rightMin != null && min.val > rightMin.val) {
            min = rightMin;
        }

        return min;
    }

    private static TreeNode getMaxOfBST(TreeNode root) {
        if ( root == null) {
            return null;
        }
        TreeNode leftMax = getMaxOfBST(root.left);
        TreeNode rightMax = getMaxOfBST(root.right);
        TreeNode max = root;
        if ( leftMax != null && max.val < leftMax.val ) {
            max = leftMax;
        }
        if ( rightMax != null && max.val < rightMax.val) {
            max = rightMax;
        }
        return max;
    }
}