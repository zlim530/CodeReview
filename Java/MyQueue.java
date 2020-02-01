import java.util.Stack;

// 使用栈实现队列的下列操作：

// push(x) -- 将一个元素放入队列的尾部。
// pop() -- 从队列首部移除元素。
// peek() -- 返回队列首部的元素。
// empty() -- 返回队列是否为空。
// 示例:

// MyQueue queue = new MyQueue();

// queue.push(1);
// queue.push(2);  
// queue.peek();  // 返回 1
// queue.pop();   // 返回 1
// queue.empty(); // 返回 false
// 说明:

// 你只能使用标准的栈操作 -- 也就是只有 push to top, peek/pop from top, size, 和 is empty 操作是合法的。
// 你所使用的语言也许不支持栈。你可以使用 list 或者 deque（双端队列）来模拟一个栈，只要是标准的栈操作即可。
// 假设所有操作都是有效的 （例如，一个空的队列不会调用 pop 或者 peek 操作）。


/**
 * MyQueue
 */
// leetcode:232.用栈来实现队列
//https://leetcode-cn.com/problems/implement-queue-using-stacks/
public class MyQueue {
//     1、使用两个栈，一个栈（stackPush）用于元素进栈，一个栈（stackPop）用于元素出栈；

// 2、pop() 或者 peek() 的时候：

// （1）如果 stackPop 里面有元素，直接从 stackPop 里弹出或者 peek 元素；

// （2）如果 stackPop 里面没有元素，一次性将 stackPush 里面的所有元素倒入 stackPop。

// 为此，可以写一个 shift 辅助方法，一次性将 stackPush 里的元素倒入 stackPop。

// 注意：

// 一定要保证 stackPop 为空的时候，才能把元素从 stackPush 里拿到 stackPop 中。

    private Stack<Integer> stackPush;
    private Stack<Integer> stackPop;
    /** Initialize your data structure here. */
    public MyQueue() {
        stackPush = new Stack<>();
        stackPop = new Stack<>();
    }
    
    /** Push element x to the back of queue. */
    public void push(int x) {
        stackPush.push(x);
    }
    
    /** Removes the element from in front of queue and returns that element. */
    public int pop() {
        shift();
        return stackPop.pop();
    }
    
        /**
     * 辅助方法：一次性将 stackPush 里的所有元素倒入 stackPop
     * 注意：1、该操作只在 stackPop 里为空的时候才操作，否则会破坏出队入队的顺序
     * 2、在 peek 和 pop 操作之前调用该方法
     */
    private void shift() {
        if ( stackPop.size() == 0) {
            while ( !stackPush.isEmpty()) {
                stackPop.push(stackPush.pop());
            }
        }
    }

    /** Get the front element. */
    public int peek() {
        shift();
        return stackPop.peek();
    }
    
    /** Returns whether the queue is empty. */
    public boolean empty() {
        return stackPush.isEmpty() && stackPop.isEmpty();
    }
}