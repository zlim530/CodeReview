import java.util.LinkedList;
import java.util.List;
import java.util.Queue;

public class MaxDepth {
    public static void main(String[] args) {
        System.out.println("Hello,World!");
    }

    // LeetCode：559. N叉树的最大高度
    // https://leetcode-cn.com/problems/maximum-depth-of-n-ary-tree/solution/559-ncha-shu-de-zui-da-shen-du-by-en-zhao/
    public int MaxDepth3(Node root) {
        int level = 0;
        Queue<Node> q = new LinkedList<>();
        if (root == null) {
            return 0;
        }
        q.add(root);
        while (!q.isEmpty()) {
            int levelSize = q.size();
            for (int i = 0; i < levelSize; i++) {
                Node node = q.poll();
                for (Node item : node.children) {
                    q.add(item);
                }
            }
            level++;
        }

        return level;
    }

    class Node {
        public int val;
        public List<Node> children;
    
        public Node() {}
    
        public Node(int _val) {
            val = _val;
        }
    
        public Node(int _val, List<Node> _children) {
            val = _val;
            children = _children;
        }
    }

    // LeetCode：104.二叉树的最大深度
    // https://leetcode-cn.com/problems/maximum-depth-of-binary-tree/
    public int MaxDepth2(TreeNode root) {
        int level = 0;
        Queue<TreeNode> q = new LinkedList<>();
        if (root == null) {
            return 0;
        }
        q.add(root);
        while (!q.isEmpty()) {
            int levelSize = q.size();
            for (int i = 0; i < levelSize; i++) {
                TreeNode node = q.poll();
                if (node != null) {
                    if (node.left != null) {
                        q.add(node.left);
                    }
                    if (node.right != null) {
                        q.add(node.right);
                    }
                }
            }
            level++;
        }

        return level;
    }

    class Solution {
        public int maxDepth(TreeNode root) {
            if (null == root) {
                return 0;
            }
            int maxLeft = maxDepth(root.left);
            int maxRight = maxDepth(root.right);
            return Math.max(maxLeft, maxRight) + 1;
        }
    }

    public int maxDepth(TreeNode root) {
        int hight = 0;

        if (root == null) {
            return hight;
        }

        // 根结点在第一层
        int levelSize = 1;

        Queue<TreeNode> q = new LinkedList<>();
        q.offer(root);

        while (!q.isEmpty()) {
            TreeNode node = q.poll();

            levelSize--;

            if (node.left != null) {
                q.offer(node.left);
            }

            if (node.right != null) {
                q.offer(node.right);
            }

            if (levelSize == 0) {
                levelSize = q.size();
                hight++;
            }
        }

        return hight;
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