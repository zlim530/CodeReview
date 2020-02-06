import java.util.Stack;

/* 给定一个二叉搜索树的根结点 root, 返回树中任意两节点的差的最小值。

示例：

输入: root = [4,2,6,1,3,null,null]
输出: 1
解释:
注意，root是树结点对象(TreeNode object)，而不是数组。

给定的树 [4,2,6,1,3,null,null] 可表示为下图:

          4
        /   \
      2      6
     / \    
    1   3  

最小的差值是 1, 它是节点1和节点2的差值, 也是节点3和节点2的差值。
注意：

二叉树的大小范围在 2 到 100。
二叉树总是有效的，每个节点的值都是整数，且不重复。
 */

// LeetCode：783.二叉搜索树结点最小距离
// https://leetcode-cn.com/problems/minimum-distance-between-bst-nodes/
public class MinDiffInBST {

    public static int minDiffInBST(TreeNode root) {
        if (root == null) return 0;
        Stack<TreeNode> stack = new Stack<>();
        TreeNode pre = null;
        int res = 0;
        if(root.left != null ){
            res = root.val - root.left.val;
        } else if(root.right != null){
            res = root.right.val - root.val;
        }
        
        while (root != null || !stack.isEmpty()) {
            while (root != null) {
                stack.push(root);
                root = root.left;
            }
            root = stack.pop();
            
            if(pre != null && res > root.val - pre.val) {
                res = root.val - pre.val;
            };
            pre = root;
            root = root.right;
        }
        return res;
    }

    public class TreeNode {
		int val;
		TreeNode left;
		TreeNode right;
		TreeNode(int x) { val = x; }
	}
}