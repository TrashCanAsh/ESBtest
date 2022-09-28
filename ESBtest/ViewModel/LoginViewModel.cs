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
    public class LoginViewModel : NotifyBase
    {
        private DBControl dBControl;

        public UserModel userModel { get; set; }

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase MinWindowCommand { get; set; }
        public CommandBase LoginCommand { get; set; }
        public CommandBase SigninCommand { get; set; }
        public CommandBase GuestLoginCommand { get; set; }

        public LoginViewModel()
        {
            Initialization();

            SetCommand();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialization()
        {
            //创建数据库操作实例
            this.dBControl = new DBControl();
            //创建用户数据实例
            this.userModel = new UserModel();
        }
        /// <summary>
        /// 命令合集
        /// </summary>
        private void SetCommand()
        {
            //创建命令实例
            this.CloseWindowCommand = new CommandBase();
            this.MinWindowCommand = new CommandBase();
            this.LoginCommand = new CommandBase();
            this.SigninCommand = new CommandBase();
            this.GuestLoginCommand = new CommandBase();

            //关闭窗口命令
            this.CloseWindowCommand.ExecuteAction = new Action<object>(GlobalFunc.CloseWindow);
            //最小化窗口命令
            this.MinWindowCommand.ExecuteAction = new Action<object>(GlobalFunc.MinWindow);
            //登录命令
            this.LoginCommand.ExecuteAction = new Action<object>(LoginFunc);
            //注册命令
            this.SigninCommand.ExecuteAction = new Action<object>(SigninFunc);
            //
            this.GuestLoginCommand.ExecuteAction = new Action<object>(GuestLoginFunc);
        }
        /// <summary>
        /// 登录命令
        /// </summary>
        /// <param name="w"></param>
        private void LoginFunc(object w)
        {
            //Console.WriteLine(userModel.UserName + userModel.Password);
            if (dBControl.IsUserNameExist(userModel.UserName))
            {
                if (dBControl.IsUserNameAndPasswordMatch(userModel.UserName, userModel.Password))
                {
                    MessageBox.Show((w as Window), "登录成功", "登录提示");
                    GlobalValue.CurrentUser = dBControl.GetUserInformation(userModel.UserName);
                    MainView mainWindow = new MainView();
                    mainWindow.Show();
                    (w as Window).Close();
                }
                else
                {
                    MessageBox.Show((w as Window), "密码错误", "登录提示");
                }
            }
            else
            {
                MessageBox.Show((w as Window), "用户名不存在", "登录提示");
            }
        }
        /// <summary>
        /// 注册命令->打开注册界面
        /// </summary>
        /// <param name="w"></param>
        private void SigninFunc(object w)
        {
            SigninView signinWindow = new SigninView();
            signinWindow.Show();
            (w as Window).Close();
        }
        /// <summary>
        /// 游客登录
        /// </summary>
        /// <param name="w"></param>
        private void GuestLoginFunc(object w)
        {
            MessageBox.Show((w as Window), "游客登录成功", "登录提示");
            GlobalValue.CurrentUser = new UserModel() {
                Name = "Guest",
                UserRight = 0
            };

            MainView mainWindow = new MainView();
            mainWindow.Show();
            (w as Window).Close();
        }
    }
}
