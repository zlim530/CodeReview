import java.util.Stack;

/* 给定一个所有节点为非负值的二叉搜索树，求树中任意两节点的差的绝对值的最小值。

示例 :

输入:

   1
    \
     3
    /
   2

输出:
1

解释:
最小绝对差为1，其中 2 和 1 的差的绝对值为 1（或者 2 和 3）。
注意: 树中至少有2个节点。
 */

//  LeetCode：530.二叉搜素树的最小绝对差
//  https://leetcode-cn.com/problems/minimum-absolute-difference-in-bst/submissions/

public class GetMinimumDifference {

    public static int getMinmumDifference(TreeNode root) {
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