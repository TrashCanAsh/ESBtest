
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;
using ESBtest.ViewModel;

namespace ESBtest.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        private DispatcherTimer ShowTimer;
        //构造函数
        public MainView()
        {
            //初始化组件
            InitializeComponent();
            //关联ViewModel
            this.DataContext = new MainViewModel();
            //Timer
            ShowCurrentTime();
        }
        //鼠标拖动窗口事件
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        //状态栏显示当前时间
        private void ShowCurrentTime()
        {
            this.CurrentTime.Text = DateTime.Now.ToString();
            ShowTimer = new System.Windows.Threading.DispatcherTimer();
            ShowTimer.Tick += new EventHandler((s,e)=> 
            {
                this.CurrentTime.Text = DateTime.Now.ToString();
            });
            ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            ShowTimer.Start();
        }
    }
}
