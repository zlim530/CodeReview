using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CSharpSenior {
    class _004_0x01委托与事件 {
        /// <summary>
        /// 从一个有趣的需求入手。有三个角色，猫，老鼠和主人，当猫叫的时候，老鼠开始逃跑，主人则从睡梦中惊醒。
        /// </summary>
        /// <param name="args"></param>
        static void Main1(string[] args) {
            Cat cat = new Cat("猫");
            Mouse mouse = new Mouse("老鼠", cat);
            Master master = new Master("张三", cat);

            // 猫叫 -> 内部触发事件 -> 通知所有的事件订阅者
            cat.CatCry();

        }

        // 使用委托实现
        public delegate void Del1();

        /// <summary>
        /// 总结：事件是基于委托实现的
        /// 联系：
        ///     1.事件是基于委托实现的，可以通俗地理解为：事件是一种特殊的委托，特殊的地方在于它定义的是一个有两个参数（事件源和事件参数）没有返回值的委托；并且事件只能出现在 += 、-= 操作符左边
        ///     2.当事件的订阅者订阅事件时，本质上是将事件的处理方法加入到委托链中，当事件触发时，委托链中的所有事件处理方法都会被调用
        /// 区别：
        ///     委托的本质是一种自定义类型（class），而事件的本质是一个特殊的委托实例（对象）
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            Del1 del1 = () => Console.WriteLine("猫叫了");
            del1 += () => Console.WriteLine("老鼠逃走了：我勒个去，赶紧跑啊！");
            del1 += () => Console.WriteLine("主人醒了：我勒个去，叫个锤子啊！");

            del1();

        }

    }

    #region 猫
    /// <summary>
    /// 事件拥有者/源：sender / source
    /// </summary>
    public class Cat {
        private string name;

        public event EventHandler<CatCryEventArgs> CatCryEvent;

        public Cat(string name) {
            this.name = name;
        }

        public void CatCry() {
            CatCryEventArgs args = new CatCryEventArgs(name);
            Console.WriteLine(args);

            // 在内部触发事件
            CatCryEvent(this,args);
        }
    }

    /// <summary>
    /// 事件参数
    /// </summary>
    public class CatCryEventArgs : EventArgs {
        private string catName;

        public CatCryEventArgs(string catName):base() {
            this.catName = catName;
        }

        public override string ToString() {
            return string.Format("{0}叫了",catName);
        }
    }

    #endregion

    #region 老鼠
    /// <summary>
    /// 事件处理者/事件订阅者
    /// </summary>
    public class Mouse {
        private string name;

        public Mouse(string name,Cat cat) {
            this.name = name;
            cat.CatCryEvent += CatCryEventHandler;// 本质上就是往CatCryEvent所对应的底层委托链中添加一个方法
        }

        /// <summary>
        /// 事件处理器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CatCryEventHandler(object sender,CatCryEventArgs e) {
            Console.WriteLine("{0}逃走了：我勒个去，赶紧跑啊！",name);
        }
    }

    #endregion

    #region 主人
    /// <summary>
    /// 事件订阅/响应者
    /// </summary>
    public class Master {
        private string name;
        public Master(string name,Cat cat) {
            this.name = name;
            cat.CatCryEvent += CatCryEventHandler;// 本质上就是往CatCryEvent所对应的底层委托链中添加一个方法
        }

        private void CatCryEventHandler(object sender,CatCryEventArgs e) {
            Console.WriteLine("{0}醒了：我勒个去，叫个锤子！",name);
        }
    }

    #endregion

}
