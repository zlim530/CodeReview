using System.Windows;

namespace Convert
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            //double x = System.Convert.ToDouble(tb1.Text);
            //double y = System.Convert.ToDouble(tb2.Text);
            double x = double.Parse(this.tb1.Text);
            // 或者使用double类型的Parse方法进行转换 但Parse仅能转换数据格式正确的数据
            double y = double.Parse(this.tb2.Text);
            double result = x + y;
            //this.tb3.Text = System.Convert.ToString(result);
            this.tb3.Text = result.ToString();
            // object o;
            // 在C#中 规定所有类型的基类均为object 即C#中的所有类型均有object演变而来
            // 而object类型具有Equals GetHashCode GetType 以及 Tostring这四个方法
            // 故所有的类型都具有上述这四个方法


        }
    }
}
