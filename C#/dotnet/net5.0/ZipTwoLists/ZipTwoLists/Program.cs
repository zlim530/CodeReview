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