using System;

namespace DailyLocalCode
{
    // 一个自定义特性 BugFix 被赋给类及其成员
    [AttributeUsage(AttributeTargets.Class |
    AttributeTargets.Constructor |
    AttributeTargets.Field |
    AttributeTargets.Method |
    AttributeTargets.Property,
    AllowMultiple = true)]

    public class DeBugInfo : System.Attribute
    {
        private int bugNo;
        private string developer;
        private string lastReview;
        public string message;

        public DeBugInfo(int bg, string dev, string d)
        {
            this.bugNo = bg;
            this.developer = dev;
            this.lastReview = d;
        }

        public int BugNo
        {
            get
            {
                return bugNo;
            }
        }
        public string Developer
        {
            get
            {
                return developer;
            }
        }
        public string LastReview
        {
            get
            {
                return lastReview;
            }
        }
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }
    }


    [DeBugInfo(45,"Zara Ali","12/8/2012",
        Message = "Return type mismatch")]
    [DeBugInfo(49,"Nuha Ali","10/10/2012",
        message = "Unused variable")]
    class Rectangle
    {
        protected double length;
        protected double width;
        public Rectangle(double l,double w)
        {
            length = l;
            width = w;
        }

        [DeBugInfo(45, "Zara Ali", "12/8/2012",
            Message = "Return type mismatch")]
        public double GetArea()
        {
            return length * width;
        }

        [DeBugInfo(56,"Zara Ali","19/10/2012")]
        public void Display()
        {
            Console.WriteLine("Length:{0}",length);
            Console.WriteLine("Width:{0}", width);
            Console.WriteLine("Area:{0}",GetArea());
        }
    }


    class ExecuteRectangle
    {
        static void Main4(string[] args)
        {
            Rectangle rectangle = new Rectangle(4.5,7.5);
            rectangle.Display();
            var type = typeof(Rectangle);
            foreach (var attributes in type.GetCustomAttributes(true))
            {
                DeBugInfo deBugInfo = (DeBugInfo)attributes;
                if (deBugInfo != null)
                {
                    Console.WriteLine("Bug no:{0}",deBugInfo.BugNo);
                    Console.WriteLine("Developer:{0}", deBugInfo.Developer);
                    Console.WriteLine("LastReview:{0}", deBugInfo.LastReview);
                    Console.WriteLine("Remarks:{0}",deBugInfo.Message);
                }
            }
        }
    }

}
