using System;

namespace GenericAlogorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a1 = { 1,2,3,4,5};
            int[] a2 = { 1,2,3,4,5,6};
            double[] a3 = { 1.1,2.2,3.3,4.4,5.5};
            double[] a4 = { 1.1,2.2,3.3,4.4,5.5,6.6};
            // 现在的问题是：当前的 Zip 仅对 int 类型数组有效，无法合并两个 double 数组。
            // 解决办法使用泛型后：
            var restult = Zip(a1,a2);
            Console.WriteLine(string.Join(",",restult));
            var restult2 = Zip(a3,a4);
            Console.WriteLine(string.Join(",",restult2));
        }

        // 将普通方法改为泛型方法：将类型参数<>加在方法名后面
        static T[] Zip<T>(T[] a ,T[] b)
        {
            T[] zipped = new T[a.Length+b.Length];
            int ai = 0, bi = 0, zi = 0;
            do
            {
                if (ai<a.Length)
                {
                    zipped[zi++] = a[ai++];
                }
                if (bi < b.Length)
                {
                    zipped[zi++] = b[bi++];
                }
            } while (ai<a.Length || bi<b.Length);

            return zipped;
        }
    }
}
