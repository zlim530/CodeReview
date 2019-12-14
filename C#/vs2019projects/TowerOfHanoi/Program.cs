using System;
using System.Collections.Generic;


namespace TowerOfHanoi
{
    class Program
    {
        static void Main(string[] args)
        {
            Hanoi h = new Hanoi();
            int num;
            for (; ; )
            {
                Console.WriteLine("pls input Hanoi's tower num:");
                num = Convert.ToInt32(Console.ReadLine());
                h.SolveHanoi(num, 'A', 'B', 'C');
            }

        }
    }
    
    class Hanoi
    {
        public void SolveHanoi(int n, char 最初承重柱子, char 中转柱子, char 目标柱子)
        {
            if (n == 1)
            {
                Console.WriteLine("将盘子[{0}]从{1} ---> {2}", n, 最初承重柱子, 目标柱子);
            }
            else
            {
                SolveHanoi(n - 1, 最初承重柱子, 目标柱子, 中转柱子);//即当原始柱子上有n个盘子时  要先将前n-1个盘子移动到中转柱子上 
                //故此时的中转柱子为目标柱子 而目标柱子则成为了中转柱子
                Console.WriteLine("将盘子[{0}]从{1} ---> {2}",n, 最初承重柱子, 目标柱子);//当原始柱子又只剩一个盘子时 将此盘子从原始柱子移动到目标柱子即可
                SolveHanoi(n - 1, 中转柱子, 最初承重柱子, 目标柱子);
                //为了将中转柱子上的n-1个盘子移动到目标柱子上 将原始柱子作为中转柱子 此时中转柱子为原始柱子 目标柱子为目标柱子
            }

        }
    }
    
}
