using System;

class Node
{
    public int val;
    public Node next;
}

class Program
{
    static void Main(string[] args)
    {
        var list1 = MakeList(1, 3, 5, 7, 7, 9);
        var list2 = MakeList(2, 6, 7, 8);
        PrintList(list1);
        PrintList(list2);
        var result = Zip(list1, list2);
        PrintList(result);
    }

    static Node Zip(Node p, Node q)
    {
        Node head = new Node(), zipper = head; // dummy head
        while (p != null && q != null)
        {
            if (p.val <= q.val)
            {
                zipper.next = p;
                p = p.next;
            }
            else
            {
                zipper.next = q;
                q = q.next;
            }

            zipper = zipper.next;
        }

        zipper.next = p == null ? q : p;
        return head.next;

        /*while (true)
        {
            if (p == null)
            {
                zipper.next = q;
                break;
            }

            if (q == null)
            {
                zipper.next = p;
                break;
            }
            
            if (p.val <= q.val)
            {
                zipper.next = p;
                p = p.next;
            }
            else
            {
                zipper.next = q;
                q = q.next;
            }

            zipper = zipper.next;
            
        }

        return head.next;*/
    }

    static Node MakeList(params int[] a)
    {
        Node head = null;
        for (int i = a.Length - 1; i >= 0; i--)
            head = new Node{val = a[i], next = head};
        return head;
    }

    static void PrintList(Node list)
    {
        while (list != null)
        {
            Console.Write($"{list.val}->");
            list = list.next;
        }

        Console.WriteLine("null");
    }
}