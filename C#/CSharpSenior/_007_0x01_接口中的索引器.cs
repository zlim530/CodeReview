using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpSenior {
    class _007_0x01_接口中的索引器 {

        static void Main(string[] args){
            IndexerClass test = new IndexerClass();
            Random rand = new Random();
            for(int i = 0;i < 10;i++){
                test[i] = rand.Next();
            }
            for(int i = 0;i < 10;i++){
                System.Console.WriteLine($"Element #{i} = {test[i]}");
            }
            /*
            Element #0 = 973030016
            Element #1 = 612694049
            Element #2 = 1395366370
            Element #3 = 1692165609
            Element #4 = 1795320553
            Element #5 = 1438486969
            Element #6 = 2065672868
            Element #7 = 1631839875
            Element #8 = 1374131970
            Element #9 = 903447799
            */
        }

    }

    /*
    可以在接口上声明索引器。 接口索引器的访问器与类索引器的访问器有所不同，差异如下：
    接口访问器不使用修饰符。
    接口访问器通常没有正文。
    访问器的用途是指示索引器为读写、只读还是只写。 可以为接口中定义的索引器提供实现，但这种情况非常少。 索引器通常定义 API 来访问数据字段，而数据字段无法在接口中定义。
    索引器的签名必须不同于同一接口中声明的所有其他索引器的签名。
    */

    public interface ISomeInterface{
        string this[int index]{
            get;
            set;
        }
    }

    public interface IIndexInterface{
        int this[int index]{
            get;set;
        }
    }

    class IndexerClass:IIndexInterface{
        private int[] arr = new int[10];
        public int this[int index]{
            get => arr[index];
            set => arr[index] = value;
        }
        
        public IndexerClass(){

        }
    }

    
}
