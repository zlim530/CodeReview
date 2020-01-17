using System;

namespace LinkedList
{
    public class CircleLinkedList<E>:AbstractList<E>
    {
        const int ELEMENT_NOT_FOUND = -1;

        public Node<E> first;

        public Node<E> last;

        public Node<E> current;

        // 成员类：也即嵌套类
        public class Node<E>
        {
            // 链表结点中的数据域：也即存放数据的地方
            public E element;

            public Node<E> prev;
            public Node<E> next;

            public Node(Node<E> prev,E element,Node<E> next)
            {
                this.prev = prev;
                this.element = element;
                this.next = next;
            }


        }


        public void Reset()
        {
            current = first;
        }

        public E Next()
        {
            if ( current == null)
            {
                return default(E);
            }
            current = current.next;
            return current.element;
        }

        public E Remove()
        {
            if ( current == null)
            {
                return default(E);
            }
            Node<E> next = current.next;
            E element = Remove(current);
            if ( size == 0)
            {
                current = null;
            }else
            {
                current = next;
            }
            return element;

        }

        private E Remove(Node<E> node)
        {
            if (size == 1 )
            {
                first = null;
                last  = null;
            }else
            {
                Node<E> prev = node.prev;
                Node<E> next = node.next;
                prev.next = next;
                next.prev = prev;

                if ( node == first)
                {
                    first = next;
                }

                if ( node == last)
                {
                    last = prev;
                }
            }

            size--;
            return node.element;
        }

        public override bool IsContains(E element)
        {
            return GetindexOf(element) != ELEMENT_NOT_FOUND;
        }
        public override void Clear()
        {
            size = 0;
            first = null;
            last = null;
        }

        public override E Get(int index)
        {
            return FindNode(index).element;
        }

        // 获取index位置处的结点对象
        private Node<E> FindNode(int index)
        {
            rangeCheck(index);

            if ( index < (size >> 1))
            {
                Node<E> node  = first;
                for (int i = 0; i < index; i++)
                {
                    node = node.next;
                }
                return node;
            }else
            {
                Node<E> node = last;
                for (int i = size -1; i > index; i--)
                {
                    node = node.prev;
                }
                return node;
            }
        }

        public override E Set(int index,E element)
        {
            Node<E> node = FindNode(index);
            E old = node.element;
            node.element = element;
            return old;
        }

        public override void Add(int index,E element)
        {
            rangeCheckForAdd(index);

            if ( index == size)
            {
                Node<E> oldLast = last;
                last = new Node<E>(oldLast,element,first);
                if (oldLast == null)
                {
                    first = last;
                    first.prev = first;
                    first.next = first;
                } else
                {
                    oldLast.next = last;
                    first.prev = last;
                }
            } else
            {
                Node<E> next = FindNode(index);
                Node<E> prev = next.prev;
                Node<E> node  = new Node<E>(prev,element,next);
                next.prev = node;
                prev.next = node;

                if ( next == first)
                {
                    first = node;
                }
            }
            size ++;
        }

        public override E Remove(int index)
        {
            rangeCheck(index);
            return Remove(FindNode(index));
        }

        public override int GetindexOf(E element)
        {
            if ( element == null)
            {
                Node<E> node = first;
                for (int i = 0; i < size; i++)
                {
                    if (node.element == null)
                    {
                        return i;
                    }
                    node = node.next;
                }
            }else
            {
                Node<E> node = first;
                for (int i = 0; i < size; i++)
                {
                    if ( element.Equals(node.element) )
                    {
                        return i;
                    }
                    node = node.next;
                }
            }
            return ELEMENT_NOT_FOUND;
        }


    }
}