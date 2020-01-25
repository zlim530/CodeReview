import java.util.LinkedList;
import java.util.List;
import java.util.Queue;
import java.util.Stack;

import javax.management.Query;

// import HelloWorld.TreeNode;

public class LevelOrderBottom {

    public static void main(String[] args) {
        System.out.println("我的脾气太暴躁了。");
        // LinkedList<TreeNode> queue = new LinkedList<TreeNode>();
        // List<List<Integer>> list = new LinkedList<List<Integer>>();
        // queue.addFirst(e);
    }

    public List<List<Integer>> levelOrderBottom(TreeNode root) {
        Queue<TreeNode> q = new LinkedList<>();
        LinkedList<List<Integer>> list = new LinkedList<>();

        if (root == null) {
            return list;
        }

        q.offer(root);

        while (!q.isEmpty()) {
            int levelSize = q.size();
            LinkedList<Integer> sublist = new LinkedList<>();
            for (int i = 0; i < levelSize; i++) {
                TreeNode node = q.poll();

                sublist.add(node.val);
                if (node.left != null) {
                    q.offer(node.left);
                }
                if (node.right != null) {
                    q.offer(node.right);
                }

                
            }

            if (sublist.size() > 0) {
                list.addFirst(sublist);
            }
        }

        return list;
    }

    /**
     * TreeNode
     */
    public class TreeNode {

        int val;

        TreeNode left;

        TreeNode right;

        public TreeNode(int x) {
            val = x;
        }
    }

}