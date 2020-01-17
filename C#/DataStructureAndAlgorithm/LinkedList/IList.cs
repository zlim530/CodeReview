namespace LinkedList
{
    public interface IList<E>
    {
        const int ELEMENT_NOT_FOUND = -1;

        void Clear();

        int GetSize();

        bool IsEmpty();

        bool IsContains(E element);

        void Add(E element);

        E Get(int index);

        E Set(int index, E element);

        void Add(int index, E element);

        E Remove(int index);

        // 查看元素所对应的索引
        int GetindexOf(E element);
    }
}