using System;

namespace HelloGeneric
{
    class Program
    {
        static void Main(string[] args)
        {
            var apple = new Apple() { Color = "Red"};
            //var box = new AppleBox() { Cargo = apple};
            //Console.WriteLine(box.Cargo.Color);
            var book = new Book() { Name = "New Book"};
            //var box2 = new BookBox() { Cargo = book };
            //Console.WriteLine(box2.Cargo.Name);
            //var box1 = new Box { Apple = apple};
            //var box2 = new Box() { Book = book};
            //Console.WriteLine(box1.Apple.Color);
            //Console.WriteLine(box2.Book.Name);
            var box1 = new Box<Apple>{ Cargo = apple};
            var box2 = new Box<Book> { Cargo = book};
            // 这样往盒子里放东西的时候很方便 但想查看盒子中的东西就很麻烦
            // (box1.Cargo as Apple)?.Color 表示如果box1.Cargo是Apple类型的就访问其属性成员Color 如果不是就什么都不操作
            //Console.WriteLine((box1.Cargo as Apple)?.Color);
            Console.WriteLine(box1.Cargo.Color);
            Console.WriteLine(box2.Cargo.Name);

        }
    }

    class Apple
    {
        public string Color { get; set; }
    }

    class Book
    {
        public string Name { get; set; }
    }

    class Box<TCargo>
    {
        public TCargo Cargo { get; set; }
    }

    //class Box
    //{
    //    public Object Cargo { get; set; }
    //}

    //class AppleBox
    //{
    //    public Apple Cargo { get; set; }
    //}

    //class BookBox
    //{
    //    public Book Cargo { get; set; }
    //}
}

