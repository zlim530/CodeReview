import java.util.Comparator;
import java.util.PriorityQueue;
import java.util.Queue;

/* 合并 k 个排序链表，返回合并后的排序链表。请分析和描述算法的复杂度。

示例:

输入:
[
  1->4->5,
  1->3->4,
  2->6
]
输出: 1->1->2->3->4->4->5->6
 */

// LeetCode：23.合并K个有序链表
// https://leetcode-cn.com/problems/merge-k-sorted-lists/submissions/
// 详细讲解：https://zhuanlan.zhihu.com/p/56694331
public class MergeKLists {

    public ListNode mergeKLists(ListNode[] lists) {
        if (lists == null || lists.length == 0) {
            return null;
        }
        // Java中的优先级队列默认为小顶堆
        // 优先级队列的底层实现就是使用二叉堆实现的
        // 并且堆中元素的数量就是链表数组中链表的条数
        Queue<ListNode> q = new PriorityQueue<ListNode>(new Comparator<ListNode>() {
            @Override
            public int compare(ListNode o1, ListNode o2) {
                // TODO Auto-generated method stub
                return o1.val - o2.val;
            }
        });
        // 将链表数组中每条链表的头结点添加到优先级队列中
        for (ListNode l : lists) {
            if (l != null) {
                q.add(l);
            }
        }
        ListNode head = new ListNode(0);
        ListNode ans = head;
        // 不断删除堆顶元素并且把堆顶元素的next添加到堆中
        while (!q.isEmpty()) {
            ListNode node = q.poll();
            ans = ans.next = node;
            if (node.next != null) {
                q.offer(node.next);
            }
        }
        return head.next;
    }

    public class ListNode {
        int val;
        ListNode next;

        ListNode(int x) {
            val = x;
        }
    }
}