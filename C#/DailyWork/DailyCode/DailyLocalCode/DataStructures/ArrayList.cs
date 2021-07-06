using System;
using System.Text;

namespace DailyLocalCode.DataStructures
{
    public class ArrayList<T> where T : class
    {
        /// <summary>
        /// 数组元素个数
        /// </summary>
        private int _size { get; set; }

        /// <summary>
        /// 数组本身
        /// </summary>
        private T[] _array;

        /// <summary>
        /// 初始容量
        /// </summary>
        private const int _defaultCapacity = 8;

        public ArrayList(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (capacity == 0)
            {
                capacity = _defaultCapacity;
            }
            _array = new T[capacity];
            _size = 0;
        }

        public ArrayList() : this(_defaultCapacity)
        {
        }

        public void Clear()
        {
            // 将数组中所有的元素置为 null
            for (int i = 0; i < _size; i++)
            {
                _array[i] = null;
            }
            // 而后 size 置为0
            _size = 0;
        }

        public int Count
        {
            get
            {
                return _size;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return Count == 0;
            }
        }

        public bool Contains(T dataItem)
        {
            if (dataItem == null)
            {
                for (int i = 0; i < _size; i++)
                {
                    if (_array[i] == null)
                        return true;
                }
            }
            else
            {
                for (int i = 0; i < _size; i++)
                {
                    // 以防出现 NullReferenceException，用 dataItem.Equals(_array[i]) 而不是 _array[i].Equals(dataItem)
                    if (dataItem.Equals(_array[i]))
                        return true;
                }
            }
            return false;
        }

        private void CheckRange(int index)
        {
            if (index < 0 || index >= _size)
                throw new IndexOutOfRangeException($"Index is {index}, But Size is {_size}");
        }

        private void CheckRangeForAdd(int index)
        {
            // 第一次添加元素时，_size 为0，index 也为0
            if (index < 0 || index > _size)
                throw new IndexOutOfRangeException($"Index is {index}, But Size is {_size}");
        }

        /// <summary>
        /// 索引器：实现 T t = _array[index]; 与 _array[index] = t;
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                CheckRange(index);
                return _array[index];
            }

            set
            {
                CheckRange(index);
                _array[index] = value;
            }
        }

        public void Add(T dataItem)
        {
            Add(_size,dataItem);
        }

        public void Add(int index, T dataItem)
        {
            CheckRangeForAdd(index);
            _ensureCapacity(_size + 1);

            for (int i = _size - 1; i > index; i--)
            {
                _array[i] = _array[i - 1];
            }
            _array[index] = dataItem;
            _size++;
        }

        public T RemoveAt(int index)
        {
            CheckRange(index);
            T old = _array[index];
            for (int i = index + 1; i < _size; i++)
            {
                _array[i - 1] = _array[i];
            }
            _size--;
            _array[_size] = null;
            return old;
        }

        private void _ensureCapacity(int capacity)
        {
            int oldCapacity = _array.Length;
            if (oldCapacity >= capacity)
                return;

            // oldCapacity << 1表示左移一位其效果等效于 oldCapacity * 2 :位运算的效率远远大于浮点数运算的效率
            int newCapacity = oldCapacity << 2;
            T[] newArray = new T[newCapacity];
            for (int i = 0; i < _size; i++)
            {
                newArray[i] = _array[i];
            }
            _array = newArray;
        }

        public override string ToString()
        {
            // size=3, [a, b, c]
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Size=").Append(_size).Append(", [");
            for (int i = 0; i < _size; i++)
            {
                if (i != 0)
                {
                    stringBuilder.Append(", ");
                }
                stringBuilder.Append(_array[i]);
            }
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

    }


    public class Test
    {
        /// <summary>
        /// Array.Clear(Array array, int index, int length)
        /// </summary>
        /// <param name="args"></param>
        static void Main0(string[] args)
        {
            Console.WriteLine("One dimension (Rank=1):");
            int[] numbers = { 9,8,7,6,5,4,3,2,1};
            
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine($"{numbers[i]}");
            }
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Array.Clear(numbers,2,5)");
            // 容量不变，元素置零
            Array.Clear(numbers,2,5);
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine($"{numbers[i]}");
            }
            Console.WriteLine();
            Console.WriteLine();
        }


        static void Main(string[] args)
        {
            var array = new ArrayList<string>(8);
            array.Add(0, "a");
            array.Add(1, "b");
            array.Add(2, "c");
            array.Add(3, "d");
            array.Add(4, "HelloWorld");
            Console.WriteLine(array);
            Console.WriteLine(array.RemoveAt(2));
            array.Add(10, "ZLim");
            Console.WriteLine(array);
            Console.ReadLine();

            //ArrayList<Person> persons = new ArrayList<Person>();
            //persons.Add(new Person(10,"Jack"));
            //persons.Add(new Person(12,"James"));
            //persons.Add(new Person(15,"Rose"));
            ////persons.Clear();
            //persons.Add(new Person(22, "abc"));
            //Console.WriteLine(persons);
            //Console.ReadLine();
        }
    }


    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }

        public Person(int age, string name)
        {
            Age = age;
            Name = name;
        }

        public override string ToString()
        {
            return $"Person [age={Age},name={Name}]";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is Person)
            {
                Person person = (Person)obj;
                return this.Age == person.Age;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Age.GetHashCode() + Name.GetHashCode();
        }
    }
}
