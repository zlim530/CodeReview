using System;
using System.Collections.Generic;

namespace Queue
{
    public class Queue<T>
    {
        List<T> list = new List<T>();
        //private T[] data;

        public int GetSize()
        {
            //return data.Length;
        }

        public bool IsEmpty()
        {
            //if (data.Length == 0)
            //{
            //    return true;
            //} else
            //{
            //    return false;
            //}
        }

        public void Clear()
        {
            //list.Clear();
            //data.Initialize();
            //Array.Clear(data,0,data.Length);
        }

        public void EnQueue(T element)
        {
            //list.Add(element);
            //Array.Fill(data,element);
        }

        //public T DeQueue()
        //{
        //    data.GetValue(0);
        //    return 
        //}

        public T GetFront()
        {
            //return (T)data.GetValue(0);
        }

    }
}