/* 将两个有序链表合并为一个新的有序链表并返回。新链表是通过拼接给定的两个链表的所有节点组成的。 

示例：

输入：1->2->4, 1->3->4
输出：1->1->2->3->4->4
 */

// LeetCode：21.合并两个有序链表
// https://leetcode-cn.com/problems/merge-two-sorted-lists/submissions/
// 详细讲解：https://zhuanlan.zhihu.com/p/56637501
public class MergeTwoLists {

    public ListNode mergeTwoLists(ListNode l1, ListNode l2) {
        if (l1 == null || l2 == null) {
            return null;
        }
        // 创建一个虚拟头结点
        ListNode h = new ListNode(0);
        ListNode ans = h;
        while (l1 != null && l2 != null) {
            if (l1.val < l2.val) {
                h.next = l1;
                l1 = l1.next;
                h = h.next;
            } else {
                h.next = l2;
                l2 = l2.next;
                h = h.next;
            }
        }
        if ( l1 == null) {
            h.next = l2;
        } else{
            h.next = l1;
        }
        return ans.next;
    }

    public class ListNode {
        int val;
        ListNode next;

        ListNode(int x) {
            val = x;
        }
    }
}