// 将一个按照升序排列的有序数组，转换为一棵高度平衡二叉搜索树。

// 本题中，一个高度平衡二叉树是指一个二叉树每个节点 的左右两个子树的高度差的绝对值不超过 1。

// 示例:

// 给定有序数组: [-10,-3,0,5,9],

// 一个可能的答案是：[0,-3,9,-10,null,5]，它可以表示下面这个高度平衡二叉搜索树：

//       0
//      / \
//    -3   9
//    /   /
//  -10  5

// LeetCode：108.将有序数组转换为二叉搜索树
// https://leetcode-cn.com/problems/convert-sorted-array-to-binary-search-tree/submissions/

public class SortedArrayToBST{

	public TreeNode sortedArrayToBST(int[] nums){
		return sortedArrayToBST(nums,0,nums.length);
	}

	public TreeNode sortedArrayToBST(int[] nums,int begin,int end){
		if ( begin == end) {
			return null;
		}
		int mid = (begin + end) >>> 1;
		TreeNode root = new TreeNode(nums[mid]);
		root.left = sortedArrayToBST(nums, begin, mid);
		root.right = sortedArrayToBST(nums, mid+1, end);
		return root;
	}

}