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
        public bool isNormalUser { get; set; }
        public bool isAdmin { get; set; }

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase MinWindowCommand { get; set; }
        public CommandBase LoginCommand { get; set; }
        public CommandBase SigninCommand { get; set; }
        public CommandBase GuestLoginCommand { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
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
            //设置初始用户身份选择
            this.isNormalUser = true;
            this.isAdmin = false;
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
            //游客登录命令
            this.GuestLoginCommand.ExecuteAction = new Action<object>(GuestLoginFunc);
        }
        /// <summary>
        /// 登录命令
        /// </summary>
        /// <param name="w"></param>
        private void LoginFunc(object w)
        {
            //普通用户登录入口
            if(isNormalUser)
            {
                //（用户表）查找用户名是否存在
                if (dBControl.IsUserNameExist(userModel.UserName))
                {
                    //（用户表）查找用户名和密码是否能对应
                    //MD5加密
                    string pwd = GlobalFunc.MD5ToString(userModel.UserName + "@" + userModel.Password);
                    if (dBControl.IsUserNameAndPasswordMatch(userModel.UserName, pwd))
                    {
                        MessageBox.Show((w as Window), "登录成功", "登录提示");
                        //获取用户信息
                        GlobalValue.CurrentUser = dBControl.GetUserInformation(userModel.UserName);
                        //权限设置
                        GlobalValue.CurrentUser.UserRight = 1;
                    }
                    else
                    {
                        MessageBox.Show((w as Window), "登录失败：密码错误", "登录提示");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show((w as Window), "登录失败：用户名不存在", "登录提示");
                    return;
                }
            }
            //管理员登录入口
            else if(isAdmin)
            {
                //（用户表）查找用户名是否存在
                if (dBControl.IsAdminNameExist(userModel.UserName))
                {
                    //（用户表）查找用户名和密码是否能对应
                    //MD5加密
                    string pwd = GlobalFunc.MD5ToString(userModel.UserName + "@" + userModel.Password);
                    if (dBControl.IsAdminNameAndPasswordMatch(userModel.UserName, pwd))
                    {
                        MessageBox.Show((w as Window), "管理员登录成功", "登录提示");
                        //获取用户信息
                        GlobalValue.CurrentUser = dBControl.GetAdminInformation(userModel.UserName);
                        //权限设置
                        GlobalValue.CurrentUser.UserRight = 2;
                    }
                    else
                    {
                        MessageBox.Show((w as Window), "管理员登录失败：密码错误", "登录提示");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show((w as Window), "管理员登录失败：用户名不存在", "登录提示");
                    return;
                }
            }
            else
            {
                MessageBox.Show((w as Window), "登录失败：未知错误", "登录提示");
                return;
            }
            //创建主界面窗口
            MainView mainWindow = new MainView();
            //管理员权限以下，隐藏部分控件
            if (GlobalValue.CurrentUser.UserRight < 2)
            {
                mainWindow.MenuItemInsert.Visibility = Visibility.Collapsed;
                mainWindow.AdminDeleteButton.Visibility = Visibility.Collapsed;
            }
            //窗口显示
            mainWindow.Show();
            (w as Window).Close();
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
            mainWindow.SampleDataGrid.IsHitTestVisible = false;
            (w as Window).Close();
        }
    }
}
