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
    /// SigninWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SigninView : Window
    {
        //私人变量
        //数据库连接实例
        private MySqlConnection giricon;
        //用户名已存在标识符
        private bool isUsernameSigned = true;
        //密码不一致标识符
        private bool isPasswordDifferent = true;

        //构造函数
        public SigninView()
        {
            InitializeComponent();
        }
        //
        public SigninView(MySqlConnection giricon)
        {
            InitializeComponent();
            this.giricon = giricon;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonSignin_Click(object sender, RoutedEventArgs e)
        {
            string SigninUsername = TextBoxUsername.Text;
            string SigninPassword = PasswordBoxPassword.Password;
            string SigninPasswordCheck = PasswordBoxPasswordCheck.Password;
            //Console.WriteLine(SigninUsername + SigninPassword + SigninPasswordCheck);
            if(SigninUsername == "")
            {
                MessageBox.Show(this, "用户名不能为空", "注册提示");
            }
            else if(SigninPassword == "")
            {
                MessageBox.Show(this, "密码不能为空", "注册提示");
            }
            else if(isUsernameSigned)
            {
                MessageBox.Show(this, "用户名已存在！", "注册提示");
            }
            else if(isPasswordDifferent)
            {
                MessageBox.Show(this, "密码不一致！", "注册提示");
            }
            else
            {
                string cmd = "select count(*) from user";
                MySqlCommand mycmd = new MySqlCommand(cmd, giricon);
                MySqlDataReader reader = mycmd.ExecuteReader();
                //默认位置 SqlDataReader 位于第一条记录之前。 因此，必须调用 Read 才能开始访问任何数据。
                reader.Read();
                int n = reader.GetInt32(0);
                reader.Close();
                //INSERT INTO table (atttributes ) VALUES (values );
                cmd = "insert into user (iduser, name, username, password, LOA) values (" + (n + 1).ToString() + ", '" + TextBoxUsername.Text + "', '"
                    + TextBoxUsername.Text + "', '" + PasswordBoxPassword.Password + "', 'normal_user')";
                mycmd = new MySqlCommand(cmd, giricon);
                mycmd.ExecuteNonQuery();
                MessageBox.Show(this, "注册成功", "注册提示");
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginWindow = new LoginView(this.giricon);
            loginWindow.Show();
            this.Close();
        }

        private void TextBoxUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            string cmd = "select * from user where username = '" + TextBoxUsername.Text + "'";
            MySqlCommand mycmd = new MySqlCommand(cmd, giricon);
            MySqlDataReader reader = mycmd.ExecuteReader();

            if (reader.HasRows)
            {
                LabelInfoUsername.Content = "用户名已存在！";
                isUsernameSigned = true;
            }
            else
            {
                LabelInfoUsername.Content = "";
                isUsernameSigned = false;
            }
            reader.Close();
        }

        private void PasswordBoxPasswordCheck_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBoxPassword.Password == PasswordBoxPasswordCheck.Password)
            {
                LabelInfoPasswordCheck.Content = "";
                isPasswordDifferent = false;
            }
            else
            {
                LabelInfoPasswordCheck.Content = "密码不一致！";
                isPasswordDifferent = true;
            }
        }
    }
}
