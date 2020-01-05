using System;
using System.Collections.Generic;
using System.Text;
using BabyStroller.SDK;

namespace Animals.Lib2
{
    // 在开放过程中假设Cow这个类没有开发完，使用[Unfinished]进行标记一下即可
    // 则后续用户在使用此插进接入主体程序时不会使用到还没有开发完全的类
    [Unfinished]
    public class Cow:IAnimal
    {
        public void Voice(int times)
        {
            Console.WriteLine("Moo!");
        }
    }
}
