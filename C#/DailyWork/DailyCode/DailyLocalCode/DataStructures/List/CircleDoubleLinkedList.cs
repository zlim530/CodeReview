using System;
using System.Text;

namespace DailyLocalCode.DataStructures.CircleDoubleLinkedList
{
    public class CircleDoubleLinkedList<T> where T : class
    {
        private int _count;
        private CircleDoubleLinkedNode<T> _firstNode { get; set; }
        private CircleDoubleLinkedNode<T> _lastNode { get; set; }
        private readonly int ELEMENT_NOT_FOUND = -1;
        
        public virtual int Count
        {
            get => _count;
        }

        public void Clear()
        {
            _count = 0;
            _firstNode = _lastNode = null;
        }

        public virtual bool IsEmpty()
        {
            return Count == 0;
        }

        protected virtual CircleDoubleLinkedNode<T> _getElementAt(int index)
        {
            if (IsEmpty() || index < 0 || index >= Count)
                throw new IndexOutOfRangeException("List is empty.");

            if (index == 0)
            {
                return _firstNode;
            }
            else if (index == (Count - 1))
            {
                return _lastNode;
            }

            CircleDoubleLinkedNode<T> currentNode = null;
            if (index > (Count >> 1))
            {
                currentNode = _lastNode;
                for (int i = Count - 1; i > index; i --)
                {
                    currentNode = currentNode.Previous;
                }
            }
            else
            {
                currentNode = _firstNode;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.Next;
                }
            }
            return currentNode;
        }

        public virtual T this[int index]
        {
            get => _getElementAt(index).Data;
            set => _setElementAt(index, value);
        }

        protected virtual void _setElementAt(int index, T value)
        {
            if (IsEmpty() || index < 0 || index >= Count)
                throw new IndexOutOfRangeException("List is empty.");

            var currentNode = _getElementAt(index);
            currentNode.Data = value;

        }

        public virtual void AppendAt(int index, T dataItem)
        {
            if (index < 0 || index > Count)
                throw new IndexOutOfRangeException("List is empty.");

            if (index == Count)
            {
                var oldLast = _lastNode;
                _lastNode = new CircleDoubleLinkedNode<T>(dataItem,_firstNode,oldLast);
                // if index == Count == 0
                if (oldLast == null)
                {
                    _firstNode = _lastNode;
                    _firstNode.Next = _firstNode;
                    _firstNode.Previous = _firstNode;
                }
                else
                {
                    _firstNode.Previous = _lastNode;
                    oldLast.Next = _lastNode;
                }
            }
            else
            {
                var nextNode = _getElementAt(index);
                var prevNode = nextNode.Previous;
                var currentNode = new CircleDoubleLinkedNode<T>(dataItem, nextNode, prevNode);
                prevNode.Next = currentNode;
                nextNode.Previous = currentNode;
                // if index ==  0 and Count == 1
                if (nextNode == _firstNode)
                {
                    _firstNode = currentNode;
                }
            }

            _count++;
        }

        public virtual void RemoveAt(int index)
        {
            if (IsEmpty() || index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            var currentNode = _getElementAt(index);
            if (Count == 1)
            {
                _firstNode = _lastNode = null;
            }
            else
            {
                var prev = currentNode.Previous;
                var next = currentNode.Next;
                prev.Next = next;
                next.Previous = prev;

                if (currentNode == _firstNode)
                {
                    _firstNode = next;
                }
                else if (currentNode == _lastNode)
                {
                    _lastNode = prev;
                }
            }
            _count--;
        }

        public virtual int IndexOf(T dataItem)
        {
            if (dataItem == null)
            {
                var currentNode = _firstNode;
                for (int i = 0; i < Count; i++)
                {
                    if (currentNode.Data == null)
                        return i;
                    currentNode = currentNode.Next;
                }
            }
            else
            {
                var currentNode = _firstNode;
                for (int i = 0; i < Count; i++)
                {
                    if (dataItem.Equals(currentNode.Data))
                        return i;
                    currentNode = currentNode.Next;
                }
            }
            return ELEMENT_NOT_FOUND;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            var currentNode = _firstNode;
            for (int i = 0; i < Count; i++)
            {
                if (i != 0)
                    sb.Append(", ");
                sb.Append(currentNode);
                currentNode = currentNode.Next;
            }
            sb.Append("]");
            return sb.ToString();

            /*var listAsString = string.Empty;
            var currentNode = _firstNode;
            for (int i = 0; i < Count; i++)
            {
                listAsString = string.Format($"{listAsString}[{i}] => {currentNode.Data}\r\n");
                currentNode = currentNode.Next;
            }

            return listAsString;*/
        }
    }

    public static class Test
    {
        static void Main(string[] args)
        {
            var circleList = new CircleDoubleLinkedList<string>();
            Console.WriteLine($"circleList.IsEmpty => {circleList.IsEmpty()}");
            Console.WriteLine($"circleList.Count => {circleList.Count}");
            circleList.AppendAt(0,"11");
            circleList.AppendAt(1,"22");
            circleList.AppendAt(2,"33");
            circleList.AppendAt(3,"44");
            circleList.AppendAt(4,"55");
            Console.WriteLine(circleList);
            circleList.RemoveAt(1);
            Console.WriteLine("After circleList.RemoveAt(1)");
            Console.WriteLine($"circleList.IndexOf(\"22\") => {circleList.IndexOf("22")}");
            Console.WriteLine($"circleList.Count => {circleList.Count}");
            Console.WriteLine($"circleList.IsEmpty => {circleList.IsEmpty()}");
            Console.WriteLine(circleList);
        }
    }

    public class CircleDoubleLinkedNode<T> where T : class
    {
        private T _date;
        public virtual T Data
        {
            get { return _date; }
            set { _date = value; }
        }

        private CircleDoubleLinkedNode<T> _previous;
        public CircleDoubleLinkedNode<T> Previous
        {
            get { return _previous; }
            set { _previous = value; }
        }

        private CircleDoubleLinkedNode<T> _next;
        public CircleDoubleLinkedNode<T> Next
        {
            get { return _next; }
            set { _next = value; }
        }

        public CircleDoubleLinkedNode() : this(default(T)) { }

        public CircleDoubleLinkedNode(T dataItem) : this(dataItem, null, null) { }

        public CircleDoubleLinkedNode(T dataItem, CircleDoubleLinkedNode<T> next, CircleDoubleLinkedNode<T> previous)
        {
            Data = dataItem;
            Next = next;
            Previous = previous;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (_previous != null)
            {
                sb.Append(_previous.Data);
            }
            else
            {
                sb.Append("null");
            }

            sb.Append("_").Append(Data).Append("_");

            if (_next != null)
            {
                sb.Append(_next.Data);
            }
            else
            {
                sb.Append("null");
            }

            return sb.ToString();
        }

    }
}
