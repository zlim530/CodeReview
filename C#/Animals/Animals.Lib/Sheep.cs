using System;
using System.Collections.Generic;
using System.Text;
using BabyStroller.SDK;

namespace Animals.Lib
{
    [Unfinished]
    public class Sheep:IAnimal
    {
        public void Voice(int times)
        {
            for (int i = 0; i < times; i++)
            {
                Console.WriteLine("Baa ...");
            }
        }
    }
}
