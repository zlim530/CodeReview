
import java.util.LinkedList;
import java.util.List;
import java.util.Stack;

// LeetCode：145.二叉树的后序遍历
// https://leetcode-cn.com/problems/binary-tree-postorder-traversal/
public class Postorder {

    public static void main(String[] args) {
        System.out.println("加油，世界！");
    }

    public List<Integer> postorderTraversal(TreeNode root){
        Stack<TreeNode> stack = new Stack<>();
        LinkedList<Integer> output = new LinkedList<>();

        TreeNode node = root;
        TreeNode prenode = null;

        while ( node != null || !stack.isEmpty()) {
            if ( node != null) {
                stack.push(node);
                node = node.left;
            } else { // node == null => stack.isEmpty == false
                TreeNode tmp = stack.peek();
                if ( tmp.right == null || tmp.right == prenode ) {
                    output.add(tmp.val);
                    prenode = tmp;
                    stack.pop();
                } else{
                    node = tmp.right;
                }
            }
        }
        return output;

    }

    public List<Integer> postorderTraversal2(TreeNode root){
        LinkedList<TreeNode> stack = new LinkedList<>();
        LinkedList<Integer> output = new LinkedList<>();

        if ( root == null) {
            return output;
        }

        stack.add(root);

        while ( !stack.isEmpty()) {
            TreeNode node = stack.pollLast();
            output.addFirst(node.val);

            if ( node.left != null ) {
                stack.add(node.left);
            }

            if ( node.right != null) {
                stack.add(node.right);
            }
        }

        return output;

    } 


    public List<Integer> postorderTraversal3(TreeNode root){
        LinkedList<Integer> list = new LinkedList<>();
        if ( root == null) {
            return list;
        }
        postorder(root,list);
        return list;
    }

    private void postorder(TreeNode root, LinkedList<Integer> list) {
        if ( root != null) {
            postorder(root.left, list);
            postorder(root.right, list);
            list.add(root.val);
        }

    }

    public class TreeNode {
        int val;
        TreeNode left;
        TreeNode right;
        TreeNode(int x) { val = x; }
    }

}