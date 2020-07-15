using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace CSharpSenior{
    
    class _006_表达式主体定义{
        
        static void Main0(string[] args){
            Person p = new Person("Mandy","Dejesus");
            System.Console.WriteLine(p);// Mandy Dejesus
            p.DisplayName();// Mandy Dejesus
        }

        static void Main1(string[] args){
            LocationGet lg = new LocationGet("Beijing");
            System.Console.WriteLine(lg.Name);// Beijing
        }

        static void Main2(string[] args){
            Sports sports = new Sports();
            var str0 = sports[0];
            System.Console.WriteLine(str0);// Baseball
        }

    }

    /*
    方法
    expression-bodied 方法包含单个表达式，它返回的值的类型与方法的返回类型匹配；或者，对于返回 void 的方法，其表达式则执行某些操作。 例如，替代 ToString 方法的类型通常包含单个表达式，该表达式返回当前对象的字符串表示形式。

    下面的示例定义 Person 类，该类通过表达式主体定义替代 ToString。 它还定义向控制台显示名称的 DisplayName 方法。 请注意，ToString 表达式主体定义中未使用 return 关键字。

    对于有返回值的方法，表达式主体也不用写 return 关键字。
    */
    public class Person{
        private string fname;
        private string lname;

        public Person(string firstName,string lastName){
            this.fname = firstName;
            this.lname = lastName;
        }

        public Person(){
            
        }

        public override string ToString() => $"{fname} {lname}".Trim();

        public void DisplayName() => System.Console.WriteLine(ToString());
    }

    /*
    构造函数
    构造函数的表达式主体定义通常包含单个赋值表达式或一个方法调用，该方法调用可处理构造函数的参数，也可初始化实例状态。

    以下示例定义 Location 类，其构造函数具有一个名为“name”的字符串参数。 表达式主体定义向 Name 属性分配参数。
    */

    public class Location{
        private string locationName;

        public Location(string name) => Name = name;
        /*
        上述构造函数等价于：
            public Location(string name){
                this.Name = name;
            }
        */

        public Location(){
            
        }

        public string Name{
            get{
                return Name;
            }
            set {
                Name = value;
            }
        }
    }

    /*
    属性 Get 语句
    如果选择自行实现属性 Get 访问器，可以对只返回属性值的单个表达式使用表达式主体定义。 请注意，未使用 return 语句。

    下面的示例定义 Location.Name 属性，其属性 Get 访问器返回支持该属性的私有 locationName 字段的值。
    */
    public class LocationGet{
        private string locationName;

        public LocationGet(string name) => locationName = name;

        public LocationGet(){
            
        }

        // 属性名为 Name，对应内部字段为 locationName
        public string Name => locationName;
        /*
        上述属性 Get 语句等价于：
            public string Name{
                get{
                    return Name;
                }
            }
        */
    }

    /*
    属性 Set 语句
    如果选择自行实现属性 Set 访问器，可以对单行表达式使用表达式主体定义，该单行表达式用于对支持该属性的字段赋值。

    下面的示例定义 Location.Name 属性，其属性 Set 语句将其输入参数赋给支持该属性的私有 locationName 字段。
    */
    public class LocationSet{
        private string locationName;

        public LocationSet(string name) => locationName = name;

        public LocationSet(){
            
        }

        // 属性名为 Name，对应内部字段为 locationName
        public string Name{
            get => locationName;
            set => locationName = value;
        }
        /*
        上述 Name 属性语句等价于：
            public string Name{
                get{
                    return Name;
                }
                set {
                    Name = value;
                }
            }
        */
    }


    /*
    索引器
    与属性一样，如果索引器的 Get 访问器包含单个返回值的语句或其 Set 访问器执行简单的赋值，则 Get 和 Set 访问器包含表达式主体定义。

    下面的示例定义名为 Sports 的类，其中包含一个内部 String 数组，该数组包含大量体育运动的名称。索引器的 Get 和 Set 访问器都以表达式主体定义的形式实现。
    */
    public class Sports{
        private string[] types = {"Baseball","BasketBall","Football","Hockey","Soccer","Tennis","Volleyball","PingPong"};

        public Sports(){

        }

        public string this[int i]{
            get => types[i];
            set => types[i] = value;
        }
    }

}