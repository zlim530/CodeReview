using System;
using System.Collections.Generic;

// leetcode:232.用栈来实现队列
//https://leetcode-cn.com/problems/implement-queue-using-stacks/
namespace Leetcode
{
    public class MyQueue
    {
        private Stack<int> inStack;
        private Stack<int> outStack;

        /** Initialize your data structure here. */
        public MyQueue() 
        {
            inStack = new Stack<int>();
            outStack = new Stack<int>();
        }
        
        /** Push element x to the back of queue. */
        public void Push(int x) 
        {
            inStack.Push(x);
        }
        
        /** Removes the element from in front of queue and returns that element. */
        public int Pop() 
        {
            CheckOutStack();
            return outStack.Pop();
            /*
            
            */
        }
        
        /** Get the front element. */
        public int Peek() 
        {
            CheckOutStack();
            return outStack.Peek();
        }
        
        /** Returns whether the queue is empty. */
        public bool Empty() 
        {
            return inStack.Count == 0 && outStack.Count == 0;
        }

        private void CheckOutStack()
        {
            if ( outStack.Count == 0)
            {
                while ( inStack.Count != 0)
                {
                    outStack.Push(inStack.Pop());
                }
            }
        } 
    }
    
    // public class MyQueue {
    //     private Stack<int> data;
    //     private Stack<int> outport;
    //     /** Initialize your data structure here. */
    //     public MyQueue() {
    //         data = new Stack<int>();
    //         outport = new Stack<int>();
    //     }
        
    //     /** Push element x to the back of queue. */
    //     public void Push(int x) {
    //         data.Push(x);
    //     }
        
    //     /** Removes the element from in front of queue and returns that element. */
    //     public int Pop() {
    //         if (outport.Count == 0) {
    //             while (data.Count != 0) {
    //                 outport.Push(data.Pop());
    //             }
    //         }
    //         return outport.Pop();
    //     }
        
    //     /** Get the front element. */
    //     public int Peek() {
    //         if (outport.Count == 0) {
    //             while (data.Count != 0) {
    //                 outport.Push(data.Pop());
    //             }
    //         }
    //         return outport.Peek();
    //     }
        
    //     /** Returns whether the queue is empty. */
    //     public bool Empty() {
    //         return outport.Count == 0 && data.Count == 0;
    //     }
    // }

    /**
    * Your MyQueue object will be instantiated and called as such:
    * MyQueue obj = new MyQueue();
    * obj.Push(x);
    * int param_2 = obj.Pop();
    * int param_3 = obj.Peek();
    * bool param_4 = obj.Empty();
    */
}