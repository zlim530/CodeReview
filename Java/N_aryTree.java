import java.util.LinkedList;
import java.util.List;

// LeetCode：589.N叉树的前序遍历
// https://leetcode-cn.com/problems/n-ary-tree-preorder-traversal/submissions/

public class N_aryTree {

    public static void main(String[] args) {
        System.out.println("SEX EADUCATION.");
    }

    /* 
        递归4部曲
    1、递归退出条件
    2、处理当前层
    3、处理下一层
    4、清理当前层

    结点：不仅可以存储数据，还需要具有指向子结点的功能

    二叉树：每个结点最多具有两个子结点，故树的结点结构为：
        class	TreeNode{
            int val;
            TreeNode left;
            TreeNode right;
            public TreeNode(int x){
                val = x;
            }
        }

    N叉树：每个结点都可以有N个结点，故树的结点结构变为：
        class Node {
            int val;
            List<Node> children;
            public Node(x,node){
                val = x;
                children = node;
            }
        }

    前序遍历：
        根结点最先遍历，而后依次遍历左子结点与右子结点
    */

    public List<Integer> pr(Node root) {
        LinkedList<Integer> list = new LinkedList<>();
        prhelper(root, list);

        return list;
    }

    private void prhelper(Node root, LinkedList<Integer> list) {
        if ( root == null) { // 递归终止条件
            return ;
        }
        list.add(root.val);
        for (Node node : root.children) {
            prhelper(node, list);
        }
    }

    public List<Integer> preorder(Node root) {
        LinkedList<Node> stack = new LinkedList<>();
        LinkedList<Integer> list = new LinkedList<>();

        if (root == null) {
            return list;
        }
        stack.add(root);
        while (!stack.isEmpty()) {
            Node node = stack.pollLast();
            list.add(node.val);
            List<Node> sublist = node.children; 
            int size = sublist.size();
            for (int i = size - 1; i >= 0; i--) {
                Node cur = sublist.get(i);
                stack.add(cur);
            }

        }

        return list;

    }

    // LeetCode：590.N叉树的后序遍历
    // https://leetcode-cn.com/problems/n-ary-tree-postorder-traversal/
    public List<Integer> postorder(Node root){
        LinkedList<Node> stack = new LinkedList<>();
        LinkedList<Integer> list = new LinkedList<>();
        if ( root == null ) {
            return list;
        }
        stack.add(root);
        // stack.addAll(c);
        while ( !stack.isEmpty()){
            Node node = stack.pollLast();
            list.addFirst(node.val);
            if ( node.children !=  null) {
                stack.addAll(node.children);
            }
            /* 
            for(Node item : node.children){
                if(item != null){
                    stack.add(item);
                }
            }
            */
        }
        return list;
    }

    public List<Integer> PostOrder(Node root){
        List<Integer> list = new LinkedList<>();
        postorderHelper(root,list);
        return list;
    }

    private void postorderHelper(Node root, List<Integer> list) {
        if ( root == null) {
            return ;
        }
        for (Node node : root.children) {
            postorderHelper(node, list);
        }
        list.add(root.val);
    }

    class Node {
        public int val;
        public List<Node> children;

        public Node() {
        }

        public Node(int _val) {
            val = _val;
        }

        public Node(int _val, List<Node> _children) {
            val = _val;
            children = _children;
        }
    }
}