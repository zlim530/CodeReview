using System;
using System.Collections.Generic;
using System.Collections;

namespace StatementsExample
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] intArray = new int[] { 1,2,3,4,5,6,7,8};
            IEnumerator enumerator = intArray.GetEnumerator();
        }
    }
}
