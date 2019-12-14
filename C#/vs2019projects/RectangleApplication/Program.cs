using System;

namespace RectangleApplication
{
    class Rectangle
    {
        //在Rectangle类中定义了两个成员变量 
        //成员变量是类的属性或数据成员 用于存储数据
        double length;
        double width;
        public void Acceptdetails()
        {
            length = 4.5;
            width = 3.6;
        }

        public double GetArea()
        {
            return length * width;
        }

        public void Display()
        {
            Console.WriteLine("length = {0}", length);
            Console.WriteLine("width = {0}", width);
            Console.WriteLine("area = {0}", GetArea());
        }
    }
    class ExcuteRectangle
    {
        static void Main(string[] args)
        {
            //实例化了一个Rectangle类对象r
            Rectangle r = new Rectangle();
            //Rectangle类实例化出来的对象r具有Rectangle类中所定义的方法
            r.Acceptdetails();
            r.Display();
            Console.ReadLine();
        }
    }
}
