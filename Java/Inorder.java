
import java.util.LinkedList;
import java.util.List;
import java.util.Stack;

// LeetCode：94.二叉树的中序遍历
// https://leetcode-cn.com/problems/binary-tree-inorder-traversal/
public class Inorder {

    public static void main(String[] args) {
        System.out.println("加油！");
    }

    public List<Integer> InorderTraversal(TreeNode root){
        List<Integer> output = new LinkedList<>();
        Stack<TreeNode> stack = new Stack<>();

        // stack.peek();

        if ( root == null) {
            return output;
        }

        TreeNode node = root;

        while ( node != null  ||  !stack.isEmpty() ) {
            // 结点不为空就一直压栈
            while ( node != null) {
                stack.push(node);
                // 考虑左子树
                node = node.left;
            }
            // 结点为空，就出栈并进行结点元素的访问
            node = stack.pop();
            output.add(node.val);
            // 考虑右子树
            node = node.right;
        }

        return output;
    }

    public List<Integer> inorderTraversal(TreeNode root){
        LinkedList<Integer> list = new LinkedList<>();

        inorder(root,list);

        return list;
    }

    private void inorder(TreeNode root, LinkedList<Integer> list){
        if ( root != null) {
            inorder(root.left, list);
            list.add(root.val); 
            inorder(root.right, list);
            
        }
    }

    class TreeNode {
        int val;
        TreeNode left;
        TreeNode right;
        public TreeNode(int x) {
            val = x;
        }
    }

}