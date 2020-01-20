using System;
using System.Collections.Generic;

// LeetCode：225.用队列实现栈
// https://leetcode-cn.com/problems/implement-stack-using-queues/
namespace Leetcode
{
    public class MyStack {

        private Queue<int> queue;

        /** Initialize your data structure here. */
        public MyStack() {
            queue = new Queue<int>();
        }
        
        /** Push element x onto stack. */
        public void Push(int x) {
            queue.Enqueue(x);
            int count = queue.Count;
            while ( count > 1)
            {
                queue.Enqueue(queue.Dequeue());
                count --;
            }
            // for (int i = 0; i < queue.Count() - 1; i++)
            // queue.Enqueue(queue.Dequeue());
        }
        
        /** Removes the element on top of the stack and returns that element. */
        public int Pop() {
            return queue.Dequeue();
        }
        
        /** Get the top element. */
        public int Top() {
            return queue.Peek();
        }
        
        /** Returns whether the stack is empty. */
        public bool Empty() {
            return queue.Count == 0;
        }
    }

/**
 * Your MyStack object will be instantiated and called as such:
 * MyStack obj = new MyStack();
 * obj.Push(x);
 * int param_2 = obj.Pop();
 * int param_3 = obj.Top();
 * bool param_4 = obj.Empty();
 */
}