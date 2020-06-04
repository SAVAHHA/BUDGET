using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BUGDETapp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string UserLogin { get; set; }
        public static int UserID { get; set; }
        public static string UserName { get; set; }
        public static string UserPassword { get; set; }
    }
}
