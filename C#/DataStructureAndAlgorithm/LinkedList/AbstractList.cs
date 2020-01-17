using System;

namespace LinkedList
{
    public abstract class AbstractList<E>:IList<E>
    {
        protected int size;

        public int GetSize()
        {
            return size;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }



        public void Add(E element)
        {
            Add(size,element);
        }

        protected void rangeCheck(int index)
        {
            if (index < 0 || index >= size) 
            {
                OutOfBounds(index);
            }
	    }

        private void OutOfBounds(int index)
        {
            throw new NotImplementedException("Index:" + index + ", Size:" + size);
        }

        protected void rangeCheckForAdd(int index) 
        {
            if (index < 0 || index > size) 
            {
                OutOfBounds(index);
            }
        }

        public abstract void Clear();

        public abstract void Add(int index,E element);

        public abstract E Get(int index);

        public abstract E Set(int index, E element);

        public abstract E Remove(int index);

        public abstract int GetindexOf(E element);

        public abstract bool IsContains(E element);


    }
}