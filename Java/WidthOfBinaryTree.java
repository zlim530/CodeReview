import java.util.LinkedList;
import java.util.Queue;

// leetcode：662：二叉树的最大宽度
// https://leetcode-cn.com/problems/maximum-width-of-binary-tree/

public class WidthOfBinaryTree {

    public static void main(String[] args) {
        System.out.println("Never learn how to reconcile.");
    }

    /*
    树的宽度其实就是每一层两个端结点之间的结点数量（包括null结点）
    故我们可以使用满二叉树的性质：满二叉树是最后一层结点的度都为0，其他结点的度都为2
    如果按从上到下，从左到右的层次遍历满二叉树并对每个结点编号（索引），假设根结点的索引为1：那么索引为i的结点的左子结点的索引值为2 * i；右子结点的索引值为 2 * i + 1；
    因此我们只需要求出每一层左右端结点的索引值并想减而后加1，然后比较每一层的宽度比出最大值即可求出这颗二叉树的最大宽度
          1 
         /  
        3    
       / \       
      5   3  
      
           1
         /   \
        3     2
       / \     \  
      5   3     9

          1
         / \
        3   2 
       /        
      5  

          1
         / \
        3   2
       /     \  
      5       9 
     /         \
    6           7

     */

    public int widthOfBinaryTree(TreeNode root) {
        Queue<TreeNode> q = new LinkedList<>();
        LinkedList<Integer> indexList = new LinkedList<>();
        int width = 0;
        if (root == null) {
            return 0;
        }
        q.offer(root);
        indexList.add(1);

        while ( !q.isEmpty()){
            int levelSize = q.size();
            int first = indexList.peek();
            int index = 0;
            for (int i = 0; i < levelSize; i++) {
                TreeNode node = q.poll();
                index = indexList.poll();
                if (node.left != null) {
                    q.offer(node.left);
                    indexList.add(2 * index);
                }
                if ( node.right != null) {
                    q.offer(node.right);
                    indexList.add(2 * index +1);
                }
            }
            width = Math.max(width, 1 + index - first);
        }
        return width;

    }

    // public int widthOfBinaryTreeX(TreeNode root) {
    //     Queue<TreeNode> q = new LinkedList<>();
    //     LinkedList<Integer> indexList = new LinkedList<>();
    //     double height = 0;
    //     double width = 0;
    //     if (root == null) {
    //         return (int)width;
    //     }
    //     q.offer(root);
    //     indexList.add(1);
    //     while (!q.isEmpty()) {
    //         int levelSize = q.size();
    //         for (int i = 0; i < levelSize; i++) {
    //             TreeNode node = q.poll();
    //             int index = indexList.removeFirst();
    //             if (node.left != null) {
    //                 q.offer(node.left);
    //                 indexList.add(2 * index);
    //             }
    //             if ( node.right != null) {
    //                 q.offer(node.right);
    //                 indexList.add(2 * index +1);
    //             }
    //         }
    //         height++;
    //         if ( levelSize == 0) {
    //             if ( indexList.size() >= 2) {
    //                 return indexList.size();
    //             }
    //         }
    //     }
    //     double i = height - 1;
    //     width = Math.pow(2,i);
    //     return (int)width;
    // }

    public class TreeNode {

        int val;

        TreeNode left;

        TreeNode right;

        public TreeNode(int x) {
            val = x;
        }
    }

}