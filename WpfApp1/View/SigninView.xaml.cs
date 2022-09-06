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
using System.Windows.Shapes;
//
using WpfApp1.ViewModel;

namespace WpfApp1.View
{
    /// <summary>
    /// SigninWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SigninView : Window
    {
        //构造函数
        public SigninView()
        {
            //初始化组件
            InitializeComponent();
            //关联ViewModel
            this.DataContext = new SigninViewModel();
        }
        //鼠标拖动窗口事件
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
