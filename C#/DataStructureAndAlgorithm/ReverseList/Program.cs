using System;

namespace ReverseList
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
            ReverseListVersion2(head);
            Report(frh);
        }

        // 递归实现 反转链表
        static ListNode ReverseList(ListNode head)
        {
            if ( head == null || head.next == null )
            {
                return head;
            }
            ListNode newNode = ReverseList(head.next);
            head.next.next = head;
            head.next = null;
            return newNode;
            
        }

        // 非递归：迭代实现反转链表
        static ListNode ReverseListVersion2(ListNode head)
        {
            
            ListNode newNode = null;
            while ( head != null)
            {
                ListNode tmpNode = head.next;
                head.next = newNode;
                newNode = head;
                head = tmpNode;
            }
            return newNode;
            /*
                ListNode prev = null;
                ListNode curr = head;
                while (curr != null)
                {
                    ListNode temp=curr.next;
                    curr.next = prev;
                    prev = curr;
                    curr = temp;
                }
                return prev;
            */
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
