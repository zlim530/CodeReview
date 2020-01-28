// LeetCode：114.二叉树展开为链表
// https://leetcode-cn.com/problems/flatten-binary-tree-to-linked-list/solution/xiang-xi-tong-su-de-si-lu-fen-xi-duo-jie-fa-by--26/

public class Flatten {

    // 将左子树插入到右子树的地方
    // 将原来的右子树接到左子树的最右边节点
    // 考虑新的右子树的根节点，一直重复上边的过程，直到新的右子树为 null
    public void flatten(TreeNdoe root) {
        while ( root != null) {
            if ( root.left == null) {
                root = root.right;
            } else {
                TreeNdoe node = root.left;
                while ( node.right != null) {
                    node = node.right;
                }
                node.right = root.right;
                root.right = root.left;
                root.left = null;
                root = root.right;
            }
        }
    }

    /**
     * TreeNdoe
     */
    public class TreeNdoe {
    
        int val;

        TreeNdoe left;

        TreeNdoe right;

        public TreeNdoe(int x) {
            val = x;
        }
    }
}