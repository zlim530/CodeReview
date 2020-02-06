// 给定一个二叉搜索树的根节点 root 和一个值 key，删除二叉搜索树中的 key 对应的节点，
// 并保证二叉搜索树的性质不变。返回二叉搜索树（有可能被更新）的根节点的引用。

// 一般来说，删除节点可分为两个步骤：

// 首先找到需要删除的节点；
// 如果找到了，删除它。
// 说明： 要求算法时间复杂度为 O(h)，h 为树的高度。

// 示例:

// root = [5,3,6,2,4,null,7]
// key = 3

//     5
//    / \
//   3   6
//  / \   \
// 2   4   7

// 给定需要删除的节点值是 3，所以我们首先找到 3 这个节点，然后删除它。

// 一个正确的答案是 [5,4,6,2,null,null,7], 如下图所示。

//     5
//    / \
//   4   6
//  /     \
// 2       7

// 另一个正确答案是 [5,2,6,null,4,null,7]。

//     5
//    / \
//   2   6
//    \   \
//     4   7

// 删除结点的三种情况：
// 1.待删除结点的度为2：找到此结点的前驱或者后继结点，将其前驱或者后继结点的值覆盖当前结点，而后删除其前驱或者后继结点；
// 2.待删除结点的度为1：直接删除，并让其子结点代替其即可；
// 3.待删除结点的度为0：直接删除

// LeetCode：450.删除二叉搜索树中的节点
// https://leetcode-cn.com/problems/delete-node-in-a-bst/submissions/
public class DeletNode{

	public TreeNode deletNode(TreeNode root,int key){
		if ( root == null) {
			return null;
		}
		if ( root.val < key ) {	// 如果key的值大于root，则在root的右子树查找
			root.right = deletNode(root.right,key);
			return root;
		} else if ( root.val > key) {	// 如果key的值小于root，则在root的左子树查找
			root.left = deletNode(root.left, key);
			return root;
		} else {	// root.val == key
			// root的度为1：
			if ( root.left == null) {
				TreeNode rightNode = root.right;
				root.right = null;
				return rightNode;
			}
			if ( root.right == null) {
				TreeNode leftNode = root.left;
				root.left = null;
				return leftNode;
			}
			// root的度为2：
			// 找后继结点
			TreeNode successor = minNode(root.right);
			// 将后继结点的值覆盖根结点的值
			root.val = successor.val;
			// 而后在右子树中删除后继结点：后继结点在右子树中是值最小的结点
			root.right = removeMin(root.right);
			return root;
		}
	}


	private TreeNode removeMin(TreeNode root) {
		if ( root.left == null) {
			TreeNode rightNode = root.right;
			root.right = null;
			return rightNode;
		}
		root.left = removeMin(root.left);
		return root;
	}

	private TreeNode minNode(TreeNode root) {
		while ( root.left!= null) {
			root = root.left;
		}
		return root;
	}

	public class TreeNode {
		int val;
		TreeNode left;
		TreeNode right;
		TreeNode(int x) { val = x; }
	}

}