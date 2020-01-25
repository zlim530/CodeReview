
import java.util.LinkedList;
import java.util.List;
import java.util.Queue;


// leetcode：102.二叉树的层次遍历
// https://leetcode-cn.com/problems/binary-tree-level-order-traversal/

public class LevelOrder {

    public static void main(String[] args) {
        System.out.println("加油，世界。");
    }

    public List<List<Integer>> levelOrderTraversal(TreeNode root) {
        List<List<Integer>> list = new LinkedList<List<Integer>>();

        levelorder(root, 0, list);

        return list;
    }

    private void levelorder(TreeNode root, int levelSize, List<List<Integer>> list) {
        // 递归结束条件
        if (root == null) {
            return;
        }

        // 当前层数还没有元素，先new一个空的列表：为了后面存储结点中的元素
        if (levelSize >= list.size()) {
            list.add(new LinkedList<Integer>());
        }
        // List.get(int index):List<Integer>
        // List.add(Integer e):boolean
        // 表示加入当前值
        list.get(levelSize).add(root.val);
        levelorder(root.left, levelSize + 1, list);
        levelorder(root.right, levelSize + 1, list);
    }

    public List<List<Integer>> levelOrder(TreeNode root) {
        Queue<TreeNode> queue = new LinkedList<TreeNode>();
        List<List<Integer>> list = new LinkedList<List<Integer>>();
        if (root == null) {
            return list;
        }
        queue.offer(root);
        while (!queue.isEmpty()) {
            int levelSize = queue.size();
            List<Integer> sublist = new LinkedList<>();
            for (int i = 0; i < levelSize; i++) {
                TreeNode node = queue.poll();
                if (node != null) {
                    sublist.add(node.val);
                    queue.offer(node.left);
                    queue.offer(node.right);
                }
            }
            if (sublist.size() > 0) {
                list.add(sublist);
            }

        }

        return list;
    }

    public class TreeNode {
        int val;
        TreeNode left;
        TreeNode right;

        TreeNode(int x) {
            val = x;
        }
    }

}