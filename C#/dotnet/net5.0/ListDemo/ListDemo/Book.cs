using System;
using System.Text.Json;

namespace ListDemo
{
    // IComparable<Book>：泛型 Book 类，表示 Book 永远只跟其他的 Book 比较排序
    public class Book : IComparable<Book>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public override bool Equals(object obj)
        {
            // 第一次非空判断：看传入的对象是不是null值
            if (obj == null) return false; 
            Book other = obj as Book;
            // 第二次非空判断：看传入的对象进行类型转换之后会不会是null值
            if (other == null) return false;
            if (other.Id == Id && other.Name == Name && other.Price == Price) return true;
            return false;
        }

        /// <summary>
        /// 比较方法
        /// </summary>
        /// <param name="other"></param>
        /// <returns>大于0表示当前对象比传入对象相比大，等于0表示当前对象与传入对象相比一样，小于0表示当前对象比传入对象小</returns>
        public int CompareTo(Book other)
        {
            if (other == null) return 1;
            return Id - other.Id;
        }
    }
}
