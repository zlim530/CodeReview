using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArcSoftFace
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]                                          //单一线程单元线程
        static void Main1()
        {
            Application.EnableVisualStyles();               //启用可视化样式
            Application.SetCompatibleTextRenderingDefault(false);  //控件（包括窗体）显示出来，false使用GDI方式显示文本.
            Application.Run(new FaceForm());            //在当前线程上开始运行标准应用程序消息循环
        }

        static void Main() {
            Application.EnableVisualStyles();               //启用可视化样式
            Application.SetCompatibleTextRenderingDefault(false);  //控件（包括窗体）显示出来，false使用GDI方式显示文本.
            Application.Run(new StuInfoManage());
        }
    }
}
