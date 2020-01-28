public class BulidTree {

    public TreeNdoe buildTree(int[] inorder, int[] postorder) {
        TreeNdoe root = new TreeNdoe(0);
        int size = postorder.length;
        int cen = inorder[size];
        root.val = cen;
        while (true) {
            if (root.left == null) {
                root = root.right;
            } else {
                while (root.left != null) {
                    root = root.left;
                }
                root.val = inorder[0];
                root = root.right;
            }

        }
    }

    // LeetCode：105.从前序和中序遍历序列构造二叉树
    // https://leetcode-cn.com/problems/construct-binary-tree-from-preorder-and-inorder-traversal/
    public TreeNdoe buildTree105(int[] preorder, int[] inorder) {
        return buildTree105Helper(preorder,0,preorder.length, inorder,0,inorder.length);
    }

    private TreeNdoe buildTree105Helper(int[] preorder, int p_start, int p_size, int[] inorder, int i_start, int i_size) {
        if ( p_start == p_size) {
            return null;
        }
        int root_val = preorder[p_start];
        TreeNdoe root = new TreeNdoe(root_val);
        int i_root_index = 0;
        for (int i = i_start; i < i_size; i++) {
            if ( inorder[i] == root_val) {
                i_root_index = i;
                break;
            }
        }
        int leftSize = i_root_index - i_start;
        root.left = buildTree105Helper(preorder, p_start+1, p_start + leftSize + 1, inorder, i_start, i_root_index);
        root.right = buildTree105Helper(preorder, p_start + leftSize + 1, p_size, inorder, i_root_index+1, i_size);
        return null;
    }

    public class TreeNdoe {

        int val;

        TreeNdoe left;

        TreeNdoe right;

        public TreeNdoe(int x) {
            val = x;
        }
    }
}