import java.util.ArrayList;
import java.util.Stack;

// 给定一个二叉搜索树，编写一个函数 kthSmallest 来查找其中第 k 个最小的元素。

// 说明：
// 你可以假设 k 总是有效的，1 ≤ k ≤ 二叉搜索树元素个数。

// 示例 1:

// 输入: root = [3,1,4,null,2], k = 1
//    3
//   / \
//  1   4
//   \
//    2
// 输出: 1
// 示例 2:

// 输入: root = [5,3,6,2,4,null,null,1], k = 3
//        5
//       / \
//      3   6
//     / \
//    2   4
//   /
//  1
// 输出: 3


// LeetCode：230.二叉搜索中第K小的元素
// https://leetcode-cn.com/problems/kth-smallest-element-in-a-bst/submissions/

public class _230_KthSmallest {

    // inorder
    public static int kthSmallest(TreeNode root, int k) {
        if (root == null) {
            return 0;
        }
        Stack<TreeNode> stack = new Stack<>();
        ArrayList<Integer> array = new ArrayList<>();
        TreeNode node = root;
        while (true) {
            if (node != null) {
                stack.push(node);
                node = node.left;
            } else if (stack.isEmpty()) {
                break;
            } else {
                node = stack.pop();
                array.add(node.val);
                node = node.right;
            }
        }
        
        return array.get(k - 1);
    }
}