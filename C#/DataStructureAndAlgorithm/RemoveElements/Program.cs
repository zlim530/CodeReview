using System;

namespace RemoveElements
{
    class Program
    {
        static void Main()
        {
            ListNode head = new ListNode(1);
            ListNode list2 = new ListNode(2);
            head.next = list2;
            ListNode list3 = new ListNode(3);
            list2.next = list3;
            ListNode list4 = new ListNode(4);
            list3.next = list4;
            ListNode list5 = new ListNode(5);
            list4.next = list5;
            ListNode list6 = new ListNode(6);
            list5.next = list6;
            ListNode list7 = new ListNode(7);
            list6.next = list7;
            list7.next = null;
            head = MiddleNode(head);
            Report(head);
        }
        static void Main2()
        {
            ListNode head = new ListNode(1);
            ListNode list2 = new ListNode(1);
            head.next = list2;
            ListNode list3 = new ListNode(2);
            list2.next = list3;
            list3.next = null;
            DeleteDuplicates(head);
            Report(head);

        }

        static void Main1(string[] args)
        {
            ListNode head = new ListNode(1);
            ListNode list2 = new ListNode(2);
            head.next = list2;
            ListNode list3 = new ListNode(6);
            list2.next = list3;
            ListNode list4 = new ListNode(3);
            list3.next = list4;
            ListNode list5 = new ListNode(4);
            list4.next = list5;
            ListNode list6 = new ListNode(5);
            list5.next = list6;
            ListNode list7 = new ListNode(6);
            list6.next = list7;
            list7.next = null;
            RemoveElements(head, 6);
            Report(head);

        }

        // 删除链表中的指定元素
        public static ListNode RemoveElements(ListNode head, int val)
        {
            ListNode first = new ListNode(0);
            first.next = head;
            ListNode tmp = first;
            while (tmp.next != null)
            {
                if (tmp.next.val == val)
                {
                    if (tmp.next.next != null)
                    {
                        tmp.next.val = tmp.next.next.val;
                        tmp.next = tmp.next.next;
                    }
                    else
                    {
                        tmp.next = null;
                    }
                }
                else
                {
                    tmp = tmp.next;
                }
            }
            return first.next;
            /*
            ListNode newHead = new ListNode(0);
            ListNode previousNode = newHead;
            ListNode currentNode = head;

            while (currentNode != null)
            {
                if (currentNode.val != val)
                {
                    previousNode.next = currentNode;
                    previousNode = currentNode;
                }
                currentNode = currentNode.next;
            }
            previousNode.next = null;

            return newHead.next;
            */
        }

        // 删除链表中值重复的结点
        public static ListNode DeleteDuplicates(ListNode head)
        {
            ListNode first = head;
            while (first != null && first.next != null)
            {
                if (first.val == first.next.val)
                {
                    first.next = first.next.next;
                }
                else
                {
                    first = first.next;
                }
            }
            return head;
            /*
            if(head == null){return null;} 
            if(head.next == null){return head;}
            ListNode curd = head;
            while(curd != null && curd.next != null)
            {
                if(curd.val == curd.next.val)
                {
                    curd.next = curd.next.next;
                }
                else
                {
                    curd = curd.next;
                }
            }
            return head;
            */

        }

        // 找到链表的中间结点并将其作为新的头结点返回
        public static ListNode MiddleNode(ListNode head)
        {
            ListNode slow = head;
            ListNode fast = head;
            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }
            // head = slow;
            return slow;
            /*
            ListNode slow = head, fast = head;
            while (fast != null && fast.next != null) {
                slow = slow.next;
                fast = fast.next.next;
            }
            return slow;
            */
        }

        public static void Report(ListNode node)
        {
            System.Console.WriteLine("Report listnode:");
            while (node != null)
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
