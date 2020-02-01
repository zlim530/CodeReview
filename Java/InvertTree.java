/* 翻转一棵二叉树。

示例：

输入：

     4
   /   \
  2     7
 / \   / \
1   3 6   9
输出：

     4
   /   \
  7     2
 / \   / \
9   6 3   1
备注:
这个问题是受到 Max Howell 的 原问题 启发的 ：

谷歌：我们90％的工程师使用您编写的软件(Homebrew)，但是您却无法在面试时在白板上写出翻转二叉树这道题，这太糟糕了。
 */

/**
 * InvertTree
 */
// LeetCode：226.反转二叉树：核心是遍历二叉树并且拿到当前结点时执行左右结点交换的动作即可
// https://leetcode-cn.com/problems/invert-binary-tree/
public class InvertTree {

    public TreeNode invertTree(TreeNode root) {
        if ( root == null) {
            return null;
        }
        TreeNode node = root.right;
        root.right = invertTree(root.left);
        root.left = invertTree(node);
        return root;
    }

    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }
}