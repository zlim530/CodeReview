using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpSenior {
    class ItCast {

        static void Main() {

            #region 将字符串"   hello      world,你  好 世界   !     "两端的空格去掉，并且将其中的所有其他空格都替换成一个空格，输出结果为："hello world,你 好 世界 !"

            //string msg = "   hello      world,你  好 世界   !     ";
            //msg = msg.Trim();
            //string[] words = msg.Split(new char[] { ' '},StringSplitOptions.RemoveEmptyEntries);
            //msg = string.Join(" ",words);
            //Console.WriteLine("============" + msg + "============");

            #endregion
            #region 输入姓名小程序

            //string name = string.Empty;
            //int count = 0;
            //List<string> list = new List<string>();
            //do {
            //    Console.WriteLine("请输入姓名:");
            //    name = Console.ReadLine();
            //    if (name.IndexOf('王') == 0) {
            //        count++;
            //    }
            //    list.Add(name);
            //} while (name.ToLower() != "quit");

            //list.RemoveAt(list.Count - 1);
            //Console.WriteLine($"共输入学生个数：{list.Count}");
            //Console.WriteLine("分别为：");
            //for (int i = 0; i < list.Count; i++) {
            //    Console.WriteLine(list[i]);
            //}
            //Console.WriteLine($"姓王的人个数为:{count}");

            #endregion

            #region 请将字符串数组{"中国","美国","巴西","澳大利亚","加拿大"}中的内容反转，然后输出反转后的数组，不能使用数组的 Reverse() 方法

            //string[] msg = { "中国", "美国", "巴西", "澳大利亚", "加拿大" };

            //MyReverse(msg);

            //for (int i = 0; i < msg.Length; i++) {
            //    Console.WriteLine(msg[i]);
            //}
            //Console.ReadKey();

            #endregion

            
        }

        private static void MyReverse(string[] msg) {

            for (int i = 0; i < msg.Length / 2; i++) {
                string temp = msg[i];
                msg[i] = msg[msg.Length - 1 - i];
                msg[msg.Length - 1 - i] = temp;
            }
        
        }

        class TestIndexr {
            public int Count {
                get {
                    return _names.Length;      
                }
             }

            private string[] _names = { "Trm","Tum","Tam","Tim","Tom"};
            
            // 索引器其实就是一个属性，是一个非常特殊的属性，常规情况下索引器其实都是一个名字叫做 Item 的属性，因此此时我们不能再声明一个 Item 的属性
            public string this[int index] {
                get {
                    if (index < 0 || index >= _names.Length ) {
                        throw new ArgumentException();
                    }
                    return _names[index];
                }

                set {
                    _names[index] = value;
                }
            }


            public string this[string useranme] {
                get {
                    
                    return "";
                }

                set {
                }
            }

        }

        static void BobbleSort(int[] array) {
            for (int i = 0; i < array.Length - 1; i++) {
                for (int j = array.Length - 1; j > i; j--) {
                    if (array[j] > array[j - 1] ) {
                        int temp = array[j];
                        array[j] = array[j - 1];
                        array[j - 1] = temp;
                    }
                }
            }
        }


    }

    
}
