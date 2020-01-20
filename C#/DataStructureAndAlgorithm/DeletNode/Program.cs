using System;

// LeetCode：237.删除链表中的结点
// https://leetcode-cn.com/problems/delete-node-in-a-linked-list/
namespace DeleteNode
{
    class Program
    {
        static void Main(string[] args)
        {
            ListNode head = new ListNode(4);
            ListNode sed = new ListNode(5);
            head.next = sed;
            ListNode tid = new ListNode(1);
            sed.next = tid;
            ListNode frh = new ListNode(9);
            tid.next = frh;
            frh.next = null;
            Report(head);
            DeleteNode(sed);
            Report(head);
        }

        public static void DeleteNode(ListNode node)
        {
            node.val = node.next.val;
            node.next = node.next.next;
        }

        public static void Report(ListNode node)
        {
            System.Console.WriteLine("Report listnode:");
            for (int i = 0; i < 4; i++)
            {
                System.Console.WriteLine(node.val);
                node = node.next;
            }
        }
    }
    
}
