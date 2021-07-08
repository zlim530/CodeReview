using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyLocalCode.DataStructures.List
{
    public class SignlyLinkedList<T> where T : class
    {
        private int _count;
        private SignlyLinkedListNode<T> _firstNode { get; set; }

        public int Count { get => _count; }
        public T First { get => _firstNode== null ? default(T) : _firstNode.Data;}

        public SignlyLinkedList()
        {
            _firstNode = null;
            _count = 0;
        }

        public bool IsEmpty { get => Count == 0; }

        public void Clear()
        {
            _firstNode = null;
            _count = 0;
        }

        private void CheckRange(int index)
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException($"Index is {index}, But Count is {_count}");
        }

        private void CheckRangeForAdd(int index)
        {
            if (index < 0 || index > _count)
                throw new IndexOutOfRangeException($"Index is {index}, But Count is {_count}");
        }

        public T GetAt(int index)
        {
            if (index == 0)
            {
                return First;
            }
            return node(index).Data;
        }

        public T SetAt(int index, T dataItem)
        {
            var newNode = new SignlyLinkedListNode<T>(dataItem);
            var oldNode = node(index);
            var old = oldNode.Next;
            oldNode.Next = newNode;
            newNode.Next = old;
            _count++;
            return oldNode.Data;
        }

        public void RemoveAt(int index)
        {
            CheckRange(index);
            // 删除第一个结点
            if (index == 0)
            {
                _firstNode = _firstNode.Next;
            }
            else
            {
                var currentNode = _firstNode;
                var prevNode = node(index - 1);
                currentNode = prevNode.Next;
                prevNode.Next = currentNode.Next;
            }
            _count--;
        }

        private SignlyLinkedListNode<T> node(int index)
        {
            CheckRangeForAdd(index);
            var currentNode = _firstNode;
            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }
            return currentNode;
        }

        public void Append(T dataItem)
        {
            SignlyLinkedListNode<T> newNode = new SignlyLinkedListNode<T>(dataItem);
            if (_firstNode == null)
            {
                _firstNode = newNode;
            }
            else
            {
                var lastNode = node(_count - 1);
                if (lastNode == null)
                {
                    lastNode = newNode;
                }
                var currentNode = lastNode;
                currentNode.Next = newNode;
            }
            _count++;
        }

        public void Prepend(T dataItem)
        {
            var newNode = new SignlyLinkedListNode<T>(dataItem);
            if (_firstNode == null)
            {
                _firstNode = newNode;
            }
            else
            {
                var currentNode = _firstNode;
                newNode.Next = currentNode;
                _firstNode = newNode;
            }
            _count++;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Size=").Append(_count).Append(", [");
            SignlyLinkedListNode<T> node = _firstNode;
            for (int i = 0; i < _count; i++)
            {
                if (i != 0)
                {
                    builder.Append(", ");
                }
                builder.Append(node.Data);
                node = node.Next;
            }
            builder.Append("]");
            return builder.ToString();
        }

    }


    public class Test
    {
        static void Main(string[] args)
        {
            SignlyLinkedList<string> list = new SignlyLinkedList<string>();
            list.Prepend("Sequence:");
            list.Append("ZLim");
            list.Append("HelloWorld");
            list.Append("!");
            Console.WriteLine(list);
            Console.WriteLine(list.GetAt(0));
            Console.WriteLine(list.SetAt(0, "Here is"));
            Console.WriteLine(list);
            list.RemoveAt(4);
            Console.WriteLine(list);
            list.RemoveAt(2);
            Console.WriteLine(list);
            Console.ReadLine();
        }
    }
}
