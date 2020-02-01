// 给定一个排序链表，删除所有重复的元素，使得每个元素只出现一次。

// 示例 1:

// 输入: 1->1->2
// 输出: 1->2
// 示例 2:

// 输入: 1->1->2->3->3
// 输出: 1->2->3

/**
 * DeleteDupllicates
 */
// LeetCode：83.删除排序链表中的重复元素
// https://leetcode-cn.com/problems/remove-duplicates-from-sorted-list/
public class DeleteDupllicates {

    public ListNode deleteDuplicates(ListNode head) {
        ListNode tmp = head;
        while ( tmp != null && tmp.next != null) {
            if ( tmp.val == tmp.next.val) {
                tmp.next = tmp.next.next;
            }else{
                tmp = tmp.next;
            }
        }
        return head;
    }   
    
    public class ListNode {
        int val;
        ListNode next;

        ListNode(int x) {
            val = x;
        }
    }
}