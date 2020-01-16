using System;

namespace HasCycle
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
            var restult = HasCycle(head);
            System.Console.WriteLine(restult);
        }

        static Boolean HasCycle(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return false;
            }
            // 快慢指针
            ListNode slow = head;
            ListNode fast = head.next;
            // 注意判断条件：两个都不能省略
            // 并且每执行一次while循环，fast与slow之间的距离就会缩小一个单位的距离：也即缩小一个结点的距离
            while ( fast != null && fast.next != null )
            {
                slow = slow.next;
                fast = fast.next.next;
                if ( fast == slow )
                {
                    return true;
                }
            }
            // 如果是从while循环中不满足循环条件而退出while循环执行到这里，则表示fast == null 或者 fast.next == null
            // 这样一定没有环
            return false;
            /*
            if (head == null || head.next == null) { return false; }

            ListNode prev = head;
            ListNode curt = head.next;
            while (prev != curt)
            {
                if (curt == null || curt.next == null)
                {
                    return false;
                }
                prev = prev.next;
                curt = curt.next.next;
            }
            return true;
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

    class ListNode
    {
        public int val;

        public ListNode next;
        public ListNode(int x)
        {
            val = x;
        }
    }
}
