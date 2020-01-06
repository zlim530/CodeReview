using System;
using System.Collections;

namespace IspExample2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums1 = { 1,2,3,4,5};
            ArrayList nums2 = new ArrayList { 1,2,3,4,5};

            Console.WriteLine(Sum(nums1));
            Console.WriteLine(Sum(nums2));

            var nums3 = new ReadOnlyCollection(nums1);
            foreach (var n in nums3)
            {
                Console.WriteLine(n);
            }
            
            Console.WriteLine(Sum(nums3));
        }

        // 如果将Sum的形参设置为实现了ICollection的则无法接口只实现了IEnumerable接口的类
        // 实际上Sum方法也只需要形参实现了IEnumerable即可 这里给ICollection实际上是传了一个大接口
        // static int Sum(ICollection nums)
        // 调用者绝不多要
        static int Sum(IEnumerable nums)
        {
            int sum = 0;
            foreach (var n in nums)
            {
                sum += (int)n;
            }
            return sum;
        }

        static double Avg(ICollection nums)
        {
            int sum = 0;
            double count = 0;
            foreach (var n in nums)
            {
                sum += (int)n;
                count++;
            }
            return sum / count;
        }

    }

    // IEnumerable接口中只有一个返回值为IEnumerator接口类型的GetEnumerator方法
    class ReadOnlyCollection : IEnumerable
    {
        private int[] _array;

        public ReadOnlyCollection(int[] array)
        {
            _array = array;
        }

        // 而IEnumerator接口中的有一个object类型的Current字段
        // 与一个返回值为bool类型的MoveNext方法与一个没有返回值的Reset方法
        // 我们在ReadOnlyCollection类中实现GetEnumerator方法来实现IEnumerable接口
        // 而GetEnumerator方法则在类成员（或者说嵌套类）Enumerator中实现：通过实现IEnumerator接口来实现GetEnumerator方法方法
        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        // 为了名称空间的污染：这里首次使用了成员类：即类Enumerator是类ReadOnlyCollection的一个成员
        class Enumerator :IEnumerator
        {
            private ReadOnlyCollection _collection;

            private int _head;

            public Enumerator(ReadOnlyCollection collection)
            {
                _collection = collection;
                _head = -1;
            }

            public object Current
            {
                get
                {
                    // 因为类Enumerator在类ReadOnlyCollection的内部，故可以访问类ReadOnlyCollection的私有字段_array
                    object o = _collection._array[_head];
                    return o;
                }
            }

            public bool MoveNext()
            {
                if ( ++ _head < _collection._array.Length )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Reset()
            {
                _head = -1;
            }
        }
    }
}
