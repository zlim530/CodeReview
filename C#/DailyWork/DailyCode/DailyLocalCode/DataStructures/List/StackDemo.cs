using System;
using System.Collections.Generic;

namespace DataStructures.Lists
{
    /// <summary>
    /// The Stack (LIFO) Data Structure.
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public class Stack<T> : IEnumerable<T> where T : IComparable<T>
    {
        /// <summary>
        /// Instance variables.
        /// _collection: Array-Based List.
        /// Count: Public Getter for returning the number of elements.
        /// </summary>
        private ArrayList<T> _collection { get; set; }
        public int Count { get { return _collection.Count; } }


        /// <summary>
        /// CONSTRUCTORS
        /// </summary>
        public Stack()
        {
            // The internal collection is implemented as an array-based list.
            // See the ArrayList.cs for the list implementation.
            _collection = new ArrayList<T>();
        }


        public Stack(int initialCapacity)
        {
            if (initialCapacity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            // The internal collection is implemented as an array-based list.
            // See the ArrayList.cs for the list implementation.
            _collection = new ArrayList<T>(initialCapacity);
        }


        /// <summary>
        /// Checks whether the stack is empty.
        /// </summary>
        /// <returns>True if stack is empty, false otherwise.</returns>
        public bool IsEmpty
        {
            get
            {
                return _collection.IsEmpty;
            }
        }


        /// <summary>
        /// Returns the top element in the stack.
        /// </summary>
        public T Top
        {
            get
            {
                try
                {
                    return _collection[_collection.Count - 1];
                }
                catch (Exception)
                {
                    throw new Exception("Stack is empty.");
                }
            }
        }


        /// <summary>
        /// Inserts an element at the top of the stack.
        /// </summary>
        /// <param name="dataItem">Element to be inserted.</param>
        public void Push(T dataItem)
        {
            _collection.Add(dataItem);
        }


        /// <summary>
        /// Removes the top element from stack.
        /// </summary>
        public T Pop()
        {
            if (Count > 0)
            {
                var top = Top;
                _collection.RemoveAt(_collection.Count - 1);
                return top;
            }

            throw new Exception("Stack is empty.");
        }

        /// <summary>
        /// Returns an array version of this stack.
        /// </summary>
        /// <returns>System.Array.</returns>
        public T[] ToArray()
        {
            return _collection.ToArray();
        }


        /// <summary>
        /// Returns a human-readable, multi-line, print-out (string) of this stack.
        /// </summary>
        /// <returns>String.</returns>
        public string ToHumanReadable()
        {
            return _collection.ToHumanReadable();
        }


        /********************************************************************************/


        public IEnumerator<T> GetEnumerator()
        {
            for (int i = _collection.Count - 1; i >= 0; --i)
                yield return _collection[i];
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

    }


    public static class Solution
    {
        static void Main0(string[] args)
        {
            EvalRPN(new string[] { "3", "+", "5", "*", "8", "-", "6" });
        }

        public static int EvalRPN(string[] tokens)
        {
            var operand = new Stack<string>();
            var operatorS = new Stack<string>();
            for (int i = 0; i < tokens.Length - 1; i++)
            {
                if ("+".Equals(tokens[i]) || "-".Equals(tokens[i]))
                {
                    if (!operand.IsEmpty && !operatorS.IsEmpty)
                    {
                        var top = operatorS.Top;
                        if (("+".Equals(top) || "-".Equals(top)))
                        {
                            var fir = operand.Pop();
                            var sed = "0";
                            if (!operand.IsEmpty)
                            { 
                                sed = operand.Pop();
                            }
                            var res = (int.Parse(fir) + int.Parse(sed)).ToString();
                            operand.Push(res);
                        }
                    }
                    operatorS.Push(tokens[i]);
                }
                else if ("*".Equals(tokens[i]))
                {
                    operatorS.Push(tokens[i]);
                    if (!operand.IsEmpty)
                    {
                        var fir = operand.Pop();
                        var sed = "0";
                        if (!operand.IsEmpty)
                        {
                            sed = operand.Pop();
                        }
                        var res = (int.Parse(fir) * int.Parse(sed)).ToString();
                        operand.Push(res);
                    }
                }
                else if ("/".Equals(tokens[i]))
                {
                    operatorS.Push(tokens[i]);
                    if (!operand.IsEmpty)
                    {
                        var fir = operand.Pop();
                        var sed = "0";
                        if (!operand.IsEmpty)
                        {
                            sed = operand.Pop();
                        }
                        var res = (int.Parse(fir) / int.Parse(sed)).ToString();
                        operand.Push(res);
                    }
                }
                else
                {
                    operand.Push(tokens[i]);
                }
            }
            Console.WriteLine(operand.ToString());
            Console.WriteLine(operatorS.ToString());

            return int.Parse(operand.Pop());
        }


        public enum OperatorLevel
        {
            Plus = 1,
            Minus = 1,
            Multiply = 2,
            Divide = 2
        }
    }
}
