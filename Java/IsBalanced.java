

public class IsBalanced {

    public static void main(String[] args) {
        
    }

    public boolean isBalanced(TreeNode root){
        boolean res = true;
        isBalancedHelper(root,res);
        return res;
    }

    private int isBalancedHelper(TreeNode root, boolean res) {
        if ( root == null) {
            return 0;
        }
        int leftHeight = isBalancedHelper(root.left,res) +1;
        int rightHeight = isBalancedHelper(root.right,res) + 1;
        if ( Math.abs(leftHeight - rightHeight) > 1) {
            res = false;
        }
        return Math.max(leftHeight, rightHeight);
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