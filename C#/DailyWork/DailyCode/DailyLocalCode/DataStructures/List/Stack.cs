using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyLocalCode.DataStructures.List
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

    }
}
