import java.util.LinkedList;
import java.util.Queue;

public class MaxDepth {
    public static void main(String[] args) {
        System.out.println("Hello,World!");
    }

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