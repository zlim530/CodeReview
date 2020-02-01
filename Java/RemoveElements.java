// 删除链表中等于给定值 val 的所有节点。

// 示例:

// 输入: 1->2->6->3->4->5->6, val = 6
// 输出: 1->2->3->4->5

/**
 * RemoveElements
 */

// LeetCode：203.移除链表元素
// https://leetcode-cn.com/problems/remove-linked-list-elements/
public class RemoveElements {

    public ListNode removeElements(ListNode head, int val) {
        // dummy是头指针始终指向head首结点
        // 头指针不存储数据仅用来指向头结点（首结点）
        ListNode dummy = new ListNode(0);
        dummy.next = head;
        ListNode nextHead = dummy;
        // nextHead始终指向当前结点的前一个位置（结点）
        while (nextHead.next != null) {
            ListNode next = nextHead.next;
            if (next.val == val) {
                nextHead.next = next.next;
            } else {
                nextHead = nextHead.next;
            }
        }
        return dummy.next;
    }

    public class ListNode {
        int val;
        ListNode next;

        ListNode(int x) {
            val = x;
        }
    }
}