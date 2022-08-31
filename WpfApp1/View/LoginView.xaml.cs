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
using MySql.Data.MySqlClient;

namespace WpfApp1.View
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : Window
    {
        //私人变量
        //数据库连接实例
        private MySqlConnection giricon;
        //登录成功标识符
        private bool isLogin = false;
        //构造函数
        public LoginView()
        {
            //
            InitializeComponent();
            //
            try
            {
                string str = "server=localhost;User Id=root;password=scr170410;Database=test01";//连接MySQL的字符串
                giricon = new MySqlConnection(str);//实例化链接
                giricon.Open();//开启连接
                               // MySqlCommand mycmd = new MySqlCommand("insert into user(userId) VALUES('c#111')", giricon);
            }
            catch (Exception ex)
            {
                Console.WriteLine("数据库连接异常！" + ex);
            }
        }

        public LoginView(MySqlConnection giricon)
        {
            //
            InitializeComponent();
            //
            this.giricon = giricon;
        }




        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void ButtonSignin_Click(object sender, RoutedEventArgs e)
        {
            SigninView signinWindow = new SigninView(this.giricon);
            signinWindow.Show();
            this.Close();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PasswordBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Login();
        }

        private void Login()
        {
            string cmd = "select * from user where username = '" + TextBoxUsername.Text + "' and password = '" + PasswordBoxPassword.Password + "'";
            Console.WriteLine(cmd);
            MySqlCommand mycmd = new MySqlCommand(cmd, giricon);
            MySqlDataReader reader = mycmd.ExecuteReader();
            if (reader.HasRows)
            {
                isLogin = true;
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();

            if (isLogin)
            {
                MessageBox.Show(this, "登录成功", "登录提示");
                MainView mainWindow = new MainView();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show(this, "用户名或密码错误", "登录提示");
            }
        }
    }
}
