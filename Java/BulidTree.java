public class BulidTree {

    /*
     * 根据一棵树的前序遍历与中序遍历构造二叉树。
     * 
     * 注意: 你可以假设树中没有重复的元素。
     * 
     * 例如，给出
     * 
     * 前序遍历 preorder = [3,9,20,15,7] 中序遍历 inorder = [9,3,15,20,7] 返回如下的二叉树：
     * 
     * 3 / \ 9 20 / \ 15 7
     */
    // LeetCode：105.从前序和中序遍历序列构造二叉树
    // https://leetcode-cn.com/problems/construct-binary-tree-from-preorder-and-inorder-traversal/
    public TreeNdoe buildTree105(int[] preorder, int[] inorder) {
        return buildTree105Helper(preorder, 0, preorder.length, inorder, 0, inorder.length);
    }

    private TreeNdoe buildTree105Helper(int[] preorder, int p_start, int p_size, int[] inorder, int i_start,
            int i_size) {
        if (p_start == p_size || i_start == i_size) {
            return null;
        }
        int root_val = preorder[p_start];
        TreeNdoe root = new TreeNdoe(root_val);
        int i_root_index = 0;
        for (int i = i_start; i < i_size; i++) {
            if (inorder[i] == root_val) {
                i_root_index = i;
                break;
            }
        }
        int leftSize = i_root_index - i_start; // 左子树的长度
        int preEdLeft = p_start + leftSize + 1; // == preStRight
        int preStLeft = p_start + 1; // 因为对于前序遍历而言，第一个一定是根结点，故代码每次执行到这里就表明已经处理完根结点了
        root.left = buildTree105Helper(preorder, preStLeft, preEdLeft, inorder, i_start, i_root_index);
        root.right = buildTree105Helper(preorder, preEdLeft, p_size, inorder, i_root_index + 1, i_size);
        return root;
    }

    public static void main(String[] args) {
        System.out.println("----------------------------------------------------------------------------");
    }

    /*
     * 根据一棵树的中序遍历与后序遍历构造二叉树。
     * 
     * 注意: 你可以假设树中没有重复的元素。
     * 
     * 例如，给出
     * 
     * 中序遍历 inorder = [9,3,15,20,7] 后序遍历 postorder = [9,15,7,20,3] 返回如下的二叉树：
     * 
     * 3 / \ 9 20 / \ 15 7
     */
    // LeetCode：106.从后序和中序遍历序列构造二叉树
    // https://leetcode-cn.com/problems/construct-binary-tree-from-inorder-and-postorder-traversal/

    public TreeNdoe buildTree106(int[] inorder, int[] postorder) {
        return buildTree106Helper(postorder, 0, postorder.length, inorder, 0, inorder.length);
    }

    private TreeNdoe buildTree106Helper(int[] postorder, int p_start, int p_size, int[] inorder, int i_start,
            int i_size) {
        if (p_start == p_size || i_start == i_size) {
            return null;
        }
        int root_val = postorder[p_size - 1];
        TreeNdoe root = new TreeNdoe(root_val);
        int i_root_index = 0;
        for (int i = i_start; i < i_size; i++) {
            if (inorder[i] == root_val) {
                i_root_index = i;
                break;
            }
        }
        int leftSize = i_root_index - i_start;
        int postStLeft = p_start + leftSize - i_start;
        root.left = buildTree106Helper(postorder, p_start, postStLeft, inorder, i_start, i_root_index);
        root.right = buildTree106Helper(postorder, postStLeft, p_size - 1, inorder, i_root_index + 1, i_size);
        return root;
    }

    private int preIndex = 0;
    private int postIndex = 0;

    public TreeNdoe constructFromPrePost(int[] pre, int[] post) {
        TreeNdoe root = new TreeNdoe(pre[preIndex++]);
        if ( root.val != post[postIndex]) {
            root.left = constructFromPrePost(pre, post);
        }
        if ( root.val != post[postIndex]) {
            root.right = constructFromPrePost(pre, post);
        }
        postIndex++;
        return root;
    }

    public TreeNdoe buildTree889(int[] pre, int[] post) {
        return buildTree889Helper(pre, 0, pre.length, post, 0, post.length);
    }

    private TreeNdoe buildTree889Helper(int[] pre, int pre_start, int pre_size, int[] post, int post_start,
            int post_size) {
        if (pre_start > pre_size || post_start > post_size) {
            return null;
        }
        int root_val = pre[pre_start];
        TreeNdoe root = new TreeNdoe(root_val);
        int right_root_val = post[post_size - 2];
        int right_root_index = 0;
        for (int i = pre_start; i < pre_size; i++) {
            if ( pre[i] == right_root_val) {
                right_root_index = i;
                break;
            }
        }
        int leftSize = right_root_index - pre_start - 1;
        // int postStRight = post_start + leftSize;
        root.left = buildTree889Helper(pre, pre_start+1, right_root_index, post, post_start, post_start+leftSize);
        root.right = buildTree889Helper(pre, right_root_index, pre_size, post, post_start+leftSize, post_size - 1);
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