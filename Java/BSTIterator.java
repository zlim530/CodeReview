import java.util.LinkedList;
import java.util.List;
import java.util.Queue;
import java.util.Stack;

// 实现一个二叉搜索树迭代器。你将使用二叉搜索树的根节点初始化迭代器。

// 调用 next() 将返回二叉搜索树中的下一个最小的数。

// 输入: [7,3,15,null,null,9,20]

// BSTIterator iterator = new BSTIterator(root);
// iterator.next();    // 返回 3
// iterator.next();    // 返回 7
// iterator.hasNext(); // 返回 true
// iterator.next();    // 返回 9
// iterator.hasNext(); // 返回 true
// iterator.next();    // 返回 15
// iterator.hasNext(); // 返回 true
// iterator.next();    // 返回 20
// iterator.hasNext(); // 返回 false
//  

// 提示：

// next() 和 hasNext() 操作的时间复杂度是 O(1)，并使用 O(h) 内存，其中 h 是树的高度。
// 你可以假设 next() 调用总是有效的，也就是说，当调用 next() 时，BST 中至少存在一个下一个最小的数。

// LeetCode：173.二叉搜索树迭代器
// https://leetcode-cn.com/problems/binary-search-tree-iterator/submissions/

public class BSTIterator {

    TreeNode node = null;
    // Queue<Integer> list;
    Stack<TreeNode> stack =  new Stack<>();

    public BSTIterator(TreeNode root) {
        this.node = root;
    }

    /** @return the next smallest number */
    public int next() {
        int res = -1;
        while (true) {
            if (node != null) {
                stack.push(node);
                node = node.left;
            } else if ( stack.isEmpty()) {
                break;
            }else{
                node = stack.pop();
                // list.offer(node.val);
                res = node.val;
                node = node.right;
                break;
            }
        }
        return res;
    }

    /** @return whether we have a next smallest number */
    public boolean hasNext() {
        return node != null || !stack.isEmpty();
    }
}
/**
 * Your BSTIterator object will be instantiated and called as such: BSTIterator
 * obj = new BSTIterator(root); int param_1 = obj.next(); boolean param_2 =
 * obj.hasNext();
 */