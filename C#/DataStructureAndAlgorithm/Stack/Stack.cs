namespace Stack
{
    public class Stack<E>
    {
       public ArrayList<E> list = new ArrayList<E>(10);

       public void Clear()
       {
           list.Clear();
       }

        public int GetSize()
        {
            return list.GetSize();
        }

        public bool IsEmpty()
        {
            return list.IsEmpty();
        }

        public void Push(E element)
        {
            list.Add(element);
        }

        public E Pop()
        {
            return list.Remove(list.GetSize() - 1);
        }

        public E GetTop()
        {
            return list.Get(list.GetSize() - 1);
        }


    }
}