using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DelegateSample
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // 现在常见的是把 delegate 当做委托关键字使用。 
            // delegate 也可以作为操作符使用，但由于 Lambda 表达式的流行，delegate 作为操作符的场景愈发少见（被 Lambda 替代，已经过时）。

            // 方法封装提高了复用性，但如果我这个方法在别的地方不太可能用到，我就可以使用匿名方法
            // 下面就是使用 delegate 来声明匿名方法
            //this.myButton.Click += delegate (object sender, RoutedEventArgs e)
            //{
            //    this.myTextBox.Text = "Hello,World!";
            //};

            // 现在推荐使用的是 Lambda 表达式
            this.myButton.Click += (sender, e) =>
            {
                this.myTextBox.Text = "Hello World! ";
            };
        }

        //private void MyButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.myTextBox.Text = "Hello,World!";
        //}
    }
}
