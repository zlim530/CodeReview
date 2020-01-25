import java.util.LinkedList;
import java.util.List;

public class N_aryTree {

    public static void main(String[] args) {
        System.out.println("SEX EADUCATION.");
    }

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