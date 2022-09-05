using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Common;
using WpfApp1.Model;
using WpfApp1.View;
using WpfApp1.ViewModel.Base;

namespace WpfApp1.ViewModel
{
    public class LoginViewModel:NotifyBase
    {
        private DBControl dBControl;

        public UserModel userModel { get; set; }

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase MinWindowCommand { get; set; }
        public CommandBase LoginCommand { get; set; }
        public CommandBase SigninCommand { get; set; }

        public LoginViewModel()
        {
            //
            this.dBControl = new DBControl();
            //
            this.userModel = new UserModel();
            
            //
            this.CloseWindowCommand = new CommandBase();
            this.MinWindowCommand = new CommandBase();
            this.LoginCommand = new CommandBase();
            this.SigninCommand = new CommandBase();

            this.CloseWindowCommand.ExecuteAction = new Action<object>((w) => 
            {
                (w as Window).Close();
            });

            this.MinWindowCommand.ExecuteAction = new Action<object>((w) =>
            {
                (w as Window).WindowState = WindowState.Minimized;
            });

            this.LoginCommand.ExecuteAction = new Action<object>((w) =>
            {
                //Console.WriteLine(userModel.UserName + userModel.Password);
                if(dBControl.isLoginSuccess(userModel.UserName, userModel.Password))
                {
                    MessageBox.Show((w as Window), "登录成功", "登录提示");
                    MainView mainWindow = new MainView();
                    mainWindow.Show();
                    (w as Window).Close();
                }
                else
                {
                    MessageBox.Show((w as Window), "用户名或密码错误", "登录提示");
                }
            });

            this.SigninCommand.ExecuteAction = new Action<object>((w) =>
            {
                SigninView signinWindow = new SigninView();
                signinWindow.Show();
                (w as Window).Close();
            });


        }
    }
}
