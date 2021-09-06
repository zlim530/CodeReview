using System;
using System.Collections.Generic;
using System.Text;

namespace LINQAndXML
{
    public class MyList<T>
    {
        private int size;

        private T[] elements;

        private const int default_capacity = 10;
        private const int element_not_found = -1;


        public MyList(int capacity)
        {
            capacity = (capacity < default_capacity) ? default_capacity : capacity;
            elements =  new T[capacity];
        }

        public MyList():this(default_capacity)
        {
        }


        private void OutOfBounds(int index)
        {
            throw new IndexOutOfRangeException("Index:" + index + ", Size:" + size);
        }

        private void RangeCheck(int index)
        {
            if (index < 0 || index  >= size)
            {
                OutOfBounds(index);
            }
        }

        private void RangeCheckForAdd(int index)
        {
            if (index < 0 || index > size)
            {
                OutOfBounds(index);
            }
        }
    }
}
