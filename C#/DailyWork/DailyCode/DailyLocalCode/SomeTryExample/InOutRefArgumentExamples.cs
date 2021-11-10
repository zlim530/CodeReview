using System;

namespace DailyLocalCode.SomeTryExample
{
    /// <summary>
    /// 方法参数修饰符 in，out，ref
    /// </summary>
    public class InOutRefArgumentExamples
    {
        /// <summary>
        /// in 参数只能从外部把值传入方法内部使用：
        /// </summary>
        /// <param name="args"></param>
        static void MainIn(string[] args)
        {
            Console.WriteLine("in 参数只能从外部把值传入方法内部使用：");
            var i = 10;
            InMethod01(in i);
            //InMethod01(in 10);  // 错误，in 后不能是实际的数据
            Console.WriteLine($"Outer:{i}");
            // 可以直接传入数据
            InMethod01(10);

            // order 内的 OrderNo 传入方法内部可能被改掉，但 order 对象不可以被替换
            var order = new Order { OrderNo = "0_000_000" };
            InMethod02(in order);
            Console.WriteLine($"Outer:{order}");
            //InMethod02(in new Order { OrderNo = "0_000_000"});   // 错误，in 后不能是实际的数据
            // 可以直接传入数据
            InMethod02(new Order { OrderNo = "0_000_000" });

            //int i1;
            //Order order1;
            //InMethod03(in order1, in i1);// 错误，不能是没有赋值的变量
            int i1 = 1;
            Order order1 = new Order() { OrderNo = "0_000_000"};
            InMethod03(in order1, in i1);
            InMethod03(new Order() { OrderNo = "0_000_000" },20); ;
        }

        #region in 参数方法
        public static void InMethod01(in int i)
        {
            Console.WriteLine($"In:{i}");
        }

        public static void InMethod02(in Order order)
        {
            order.OrderNo = "000_0002";
            Console.WriteLine($"In:{order}");
        }

        public static void InMethod03(in Order order, in int i)
        {
            Console.WriteLine($"In:{i}");

            Console.WriteLine($"In:{order}");
        }
        #endregion


        /// <summary>
        /// out 参数，是从方法内部把数据带出来
        /// </summary>
        /// <param name="args"></param>
        static void MainOut(string[] args)
        {
            Console.WriteLine("out 参数，是从方法内部把数据带出来");
            // 定义调用一起
            OutMethod01(out int i1);
            Console.WriteLine($"Out:{i1}");
            // 定义调用分开
            int i2 = 20;    // 即使赋值，方法内部接收不到
            OutMethod01(out i2);
            Console.WriteLine($"Out:{i2}");

            OutMethod02(out Order order1);
            Console.WriteLine($"Out:{order1}");
            Order order2;
            OutMethod02(out order2);
            Console.WriteLine($"Out:{order2}");

            // 多个 out 同时存在
            OutMethod03(out Order order3, out int i3);
            Console.WriteLine($"Out:{order3}");
            Console.WriteLine($"Out:{i3}");

            Order order4;
            int i4 = 20;    // 这里的20是可以传入 OutMethod03 中的，但在方法内部，i 在赋值前不能使用
            OutMethod03(out order4, out i4);
            Console.WriteLine($"Out:{order4}");
            Console.WriteLine($"Out:{i4}");
        }

        #region out 参数方法
        /// <summary>
        /// 带有 out 的参数，在方法内即使有值，也不能使用，只有赋值后才能使用
        /// </summary>
        /// <param name="i"></param>
        static void OutMethod01(out int i)
        {
            //Console.WriteLine(i);   // 错误：使用了未赋值的 out 参数"i"
            i = 10;
            Console.WriteLine(i);
        }

        static void OutMethod02(out Order order)
        {
            order = new Order { OrderNo = "0_000_001"};
            Console.WriteLine(order);
        }

        static void OutMethod03(out Order order, out int i)
        {
            i = 10;
            order = new Order { OrderNo = "0_000_001" };
            Console.WriteLine(i);
            Console.WriteLine(order);
        }

        #endregion


        /// <summary>
        /// ref 参数，即能把外部的数据传入，也能把方法里的参数变化值传出，这里更多的是把参数转成一个引用，穿透方法内外共享
        /// </summary>
        /// <param name="args"></param>
        static void MainRef(string[] args)
        {
            // RefMethod01(ref 10);    // 错误，只能传入一个变量，不能是具体的数据：ref 或 out 值必须是可以赋值的变量
            
            // int i1; 
            // RefMethod01(ref i1);// 错误，ref 要求传入必须有值
            int i1 = 1;
            RefMethod01(ref i1);
            Console.WriteLine($"Out:{i1}");

            var order1 = new Order { OrderNo = "0_000_000" };
            RefMethod02(ref order1);
            Console.WriteLine($"Out:{order1}");

            Order order2 = new Order { OrderNo = "0_000_000"};
            int i2 = 1;
            RefMethod03(ref order2, ref i2);
            Console.WriteLine($"Out:{order2}");
            Console.WriteLine($"Out:{i2}");
            
        }

        #region ref 参数方法
        static void RefMethod01(ref int i)
        {
            Console.WriteLine($"Before:{i}");
            i = 10;
            Console.WriteLine($"After:{i}");
        }

        static void RefMethod02(ref Order order)
        {
            Console.WriteLine($"Before:{order}");
            order = new Order { OrderNo = "0_00000_1" };
            Console.WriteLine($"After:{order}");
        }

        static void RefMethod03(ref Order order, ref int i)
        {
            Console.WriteLine($"Before:{i}");
            Console.WriteLine($"Before:{order}");
            i = 10;
            order = new Order { OrderNo = "0_00000_1" };
            Console.WriteLine($"After:{i}");
            Console.WriteLine($"After:{order}");
        }

        #endregion

    }

    /// <summary>
    /// 定义一个引用类型 Order
    /// </summary>
    public class Order
    {
        public string OrderNo { get; set; }
        public override string ToString()
        {
            return OrderNo;
        }
    }
}
