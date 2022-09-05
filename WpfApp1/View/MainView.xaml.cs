
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

namespace WpfApp1.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            

        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonMini_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonMax_Click(object sender, RoutedEventArgs e)
        {
            if(this.WindowState == WindowState.Maximized)
            {
                //var myResourceDictionary = new ResourceDictionary
                //{
                //    Source = new Uri("../Style/MyCustomControl.xaml", UriKind.RelativeOrAbsolute) 
                //};
                //var myButtonStyle = myResourceDictionary["MaximizeWindowButtonStyle1"] as Style;
                //ButtonMax.Style = myButtonStyle;
                this.WindowState = WindowState.Normal;
            }
            else
            {
                //var myResourceDictionary = new ResourceDictionary
                //{
                //    Source = new Uri("../Style/MyCustomControl.xaml", UriKind.RelativeOrAbsolute) 
                //};
                //var myButtonStyle = myResourceDictionary["MaximizeWindowButtonStyle2"] as Style;
                //ButtonMax.Style = myButtonStyle;
                this.WindowState = WindowState.Maximized;
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
