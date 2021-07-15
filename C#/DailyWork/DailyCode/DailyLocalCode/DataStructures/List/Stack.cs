using System;

namespace DailyLocalCode.DataStructures.Stack
{
    public class Stack<T> where T : class
    {
        private ArrayList<T> _collection { get; set; }
        public int Count { get => _collection.Count; }

        public Stack()
        {
            _collection = new ArrayList<T>();
        }

        public Stack(int initialCapacity)
        {
            if (initialCapacity < 0)
                throw new ArgumentOutOfRangeException("InitalCapacity cannot be lower than zero.");

            _collection = new ArrayList<T>(initialCapacity);
        }

        public bool IsEmpty { get => _collection.IsEmpty; }


        public T Pop()
        {
            if (Count > 0)
            {
                var top = _collection[_collection.Count - 1];
                _collection.RemoveAt(_collection.Count - 1);
                return top;
            }

            throw new Exception("The Stack is Empty.");
        }
        

        public void Push(T dataItem)
        {
            _collection.Add(dataItem);
        }

        public override string ToString()
        {
            return _collection.ToString();
        }

    }


    public static class Test
    {
        static void Main0(string[] args)
        {
            Stack<string> stack = new Stack<string>();
            stack.Push("A");
            stack.Push("B");
            stack.Push("C");
            stack.Push("D");
            stack.Push("E");
            Console.WriteLine(stack);
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
        }
    }

}
