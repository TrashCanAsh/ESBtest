using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ESBtest.Common;
using ESBtest.Model;
using ESBtest.View;
using ESBtest.ViewModel.Base;

namespace ESBtest.ViewModel
{
    public class SigninViewModel : NotifyBase
    {
        private DBControl dBControl;

        public UserModel userModel { get; set; }

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase MinWindowCommand { get; set; }
        public CommandBase SigninCommand { get; set; }
        public CommandBase CancelCommand { get; set; }

        public SigninViewModel()
        {
            //创建数据库操作实例
            this.dBControl = new DBControl();
            //创建用户数据实例
            this.userModel = new UserModel();
            //创建命令实例
            this.CloseWindowCommand = new CommandBase();
            this.MinWindowCommand = new CommandBase();
            this.SigninCommand = new CommandBase();
            this.CancelCommand = new CommandBase();

            //关闭窗口命令
            this.CloseWindowCommand.ExecuteAction = new Action<object>((w) =>
            {
                (w as Window).Close();
            });
            //最小化窗口命令
            this.MinWindowCommand.ExecuteAction = new Action<object>((w) =>
            {
                (w as Window).WindowState = WindowState.Minimized;
            });
            //注册命令
            this.SigninCommand.ExecuteAction = new Action<object>(SigninFunc);
            //取消返回命令->返回登录界面
            this.CancelCommand.ExecuteAction = new Action<object>((w) =>
            {
                LoginView loginWindow = new LoginView();
                loginWindow.Show();
                (w as Window).Close();
            });

        }

        private void SigninFunc(object w)
        {
            if (string.IsNullOrEmpty(userModel.UserName))
            {
                MessageBox.Show((w as Window), "用户名不能为空", "注册提示");
            }
            else if (string.IsNullOrEmpty(userModel.Password))
            {
                MessageBox.Show((w as Window), "密码不能为空", "注册提示");
            }
            else if (dBControl.IsUserNameExist(userModel.UserName))
            {
                MessageBox.Show((w as Window), "用户名已存在", "注册提示");
            }
            else if (userModel.Password != userModel.PasswordCheck)
            {
                MessageBox.Show((w as Window), "密码不一致", "注册提示");
            }
            else
            {
                if (dBControl.InsertIntoUserTable(userModel.UserName, userModel.UserName, userModel.Password) > 0)
                {
                    MessageBox.Show((w as Window), "注册成功\n正在返回登录界面...", "注册提示");
                    //注册成功后返回登录界面
                    LoginView loginWindow = new LoginView();
                    loginWindow.Show();
                    (w as Window).Close();
                }
                else
                {
                    MessageBox.Show((w as Window), "注册失败：未知错误", "注册提示");
                }
            }
        }
    }
}
