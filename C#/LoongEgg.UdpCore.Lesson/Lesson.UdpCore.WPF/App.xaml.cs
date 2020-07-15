using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LoongEgg.DependencyInjection;

namespace Lesson.UdpCore.WPF {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            IoC = new Container();
            IoC.AddOrUpdate(new AltViewModel { Name = "HotName", Value = 9966, Unit = "km/h"});
        }

        public static Container IoC { get; set; }
    }
}
