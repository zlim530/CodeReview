using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicArray
{
    public class ArrayList <E>
    {
        private int size;

        private E[] elements;

        private const int DEFAULT_CAPACITY = 10;
        private const int ELEMENT_NOT_FOUND = -1;

        public ArrayList(int capacity)
        {
            capacity = (capacity > DEFAULT_CAPACITY) ? capacity : DEFAULT_CAPACITY;
            elements = new E[capacity];
        }

        public ArrayList()
        {
            elements = new E[DEFAULT_CAPACITY];
        }

        public void Clear()
        {
            for (int i = 0; i < size; i++)
            {
                
                elements[i] = default(E);
            }
            size = 0;
        }

        public int GetSize()
        {
            return size;
        }

        public Boolean IsEmpty()
        {
            return size == 0;
        }

        public Boolean IsContains(E element)
        {
            return GetIndexOf(element) != ELEMENT_NOT_FOUND;
        }

        public int GetIndexOf(E element)
        {
            for (int i = 0; i < size; i++)
            {
                if (elements[i].Equals(element) )
                {
                    return i;
                }
            }
            return ELEMENT_NOT_FOUND;
        }

        public void Add(E element)
        {
            Add(size,element);
        }

        public void Add(int index, E element)
        {
            CheckRangeForAdd(index);

            EnsureCapacity(size + 1);

            for (int i = size; i > index; i--)
            {
                elements[i] = elements[i - 1];
            }
            elements[index] = element;
            size++;
        }

        public E Remove(int index)
        {
            CheckRange(index);
            E old = elements[index];
            for (int i = index + 1; i < size; i++)
            {
                elements[i - 1] = elements[i];
            }
            
            elements[--size] = default(E);
            return old;
        }

        public E Get(int index)
        {
            CheckRange(index);
            return elements[index];
        }

        public E Set(int index, E element)
        {
            CheckRange(index);
            E old = elements[index];
            elements[index] = element;
            return old;
        }

        private void CheckRange(int index)
        {
            if (index < 0 || index >= size)
            {
                OutOfBounds(index);
            }
        }

        private void EnsureCapacity(int capacity)
        {

            int oldCapacity = elements.Length;
            if ( oldCapacity > capacity)
            {
                return;
            }
            int newCapacity = oldCapacity + (oldCapacity >> 1);
            E[] newElements = new E[newCapacity];
            for (int i = 0; i < size; i++)
            {
                newElements[i] = elements[i];
            }
            elements = newElements;
        }

        private void CheckRangeForAdd(int index)
        {
            if ( index < 0 || index > size)
            {
                OutOfBounds(index);
            }
        }

        private void OutOfBounds(int index)
        {
            throw new NotImplementedException("Index:" + index + ",Size:" + size);
            //throw new NotImplementedException("Out of Bounds.");
        }

        public void Report()
        {
            Console.WriteLine("Report Elements:");
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine(elements[i]);
            }
        }

    }
}
