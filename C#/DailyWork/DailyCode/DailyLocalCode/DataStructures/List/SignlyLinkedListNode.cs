namespace DailyLocalCode.DataStructures.List
{
    public class SignlyLinkedListNode<T> where T : class
    {
        private T _data;
        private SignlyLinkedListNode<T> _next;

        public T Data { get => _data; set => _data = value; }
        public SignlyLinkedListNode<T> Next { get => _next ; set => _next = value; }

        public SignlyLinkedListNode()
        {
            Next = null;
            Data = default(T);
        }

        public SignlyLinkedListNode(T dataItem)
        {
            Next = null;
            Data = dataItem;
        }

    }
}
