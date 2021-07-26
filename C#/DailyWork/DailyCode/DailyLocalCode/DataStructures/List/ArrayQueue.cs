using System;
using System.Text;

namespace DailyLocalCode.DataStructures.ArrayQueue
{
    /// <summary>
    /// The Queue FirstInFirstOut Data Structure
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ArrayQueue<T> where T : class
    {
        private int _size { get; set; }
        private int _headPointer { get; set; }
        private int _tailPointer { get; set; }

        private T[] _collection { get; set; }
        private const int _defaultCapacity = 8;

        public ArrayQueue() : this(_defaultCapacity)
        {
        }

        public ArrayQueue(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();
            _size = capacity;
            _headPointer = 0;
            _tailPointer = 0;
            _collection = new T[capacity];
        }

        public bool Enqueue(T item)
        {
            if (_tailPointer == _size)
                return false;
            _collection[_tailPointer] = item;
            _tailPointer++;
            return true;
        }

        public T Dequeue()
        {
            if (_headPointer == _tailPointer)
                return null;
            T result = _collection[_headPointer];
            _headPointer++;
            return result;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Size = ").Append(_size).Append(", [");
            for (int i = 0; i < _size; i++)
            {
                if (i != 0)
                    sb.Append(", ");
                sb.Append(_collection[i]);
            }
            sb.Append("]");
            return sb.ToString();
        }

    }


    public static class test
    {
        static void Main(string[] args)
        {
            var queue = new ArrayQueue<string>();
            queue.Enqueue("A");
            queue.Enqueue("B");
            queue.Enqueue("C");
            queue.Enqueue("D");
            queue.Enqueue("E");
            Console.WriteLine(queue);
        }
    }
}
