// 反转一个单链表。

// 示例:

// 输入: 1->2->3->4->5->NULL
// 输出: 5->4->3->2->1->NULL

// LeetCode：206.反转链表
// https://leetcode-cn.com/problems/reverse-linked-list/
class Solution {
    public ListNode reverseList(ListNode head) {
        if (head == null) {
            return null;    
        }
        ListNode pre = null;
        ListNode next;
        while ( head != null) {
            next = head.next;//保留head的next，以防取下head 后丢失原链表中的数据
            head.next = pre;// 将head从原链表中取出，并添加到新链表上
            pre = head;// 让pre右移
            head = next;// 让head右移
        }
        return pre;
    }

    public class ListNode {
        int val;
        ListNode next;
        ListNode(int x) { val = x; }
    }
}