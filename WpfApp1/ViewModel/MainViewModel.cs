using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ESBtest.Common;
using ESBtest.Model;
using ESBtest.ViewModel.Base;

namespace ESBtest.ViewModel
{
    public class MainViewModel : NotifyBase
    {
        private DBControl dBControl;

        public UserModel userModel { get; set; }

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase MinWindowCommand { get; set; }
        public CommandBase MaxWindowCommand { get; set; }

        public MainViewModel()
        {
            //创建数据库操作实例
            this.dBControl = new DBControl();
            //创建用户数据实例
            this.userModel = new UserModel();
            //创建命令实例
            this.CloseWindowCommand = new CommandBase();
            this.MinWindowCommand = new CommandBase();
            this.MaxWindowCommand = new CommandBase();

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
            //最大化窗口命令
            this.MaxWindowCommand.ExecuteAction = new Action<object>((w) =>
            {
                if ((w as Window).WindowState == WindowState.Maximized)
                {
                    (w as Window).WindowState = WindowState.Normal;
                }
                else
                {
                    (w as Window).WindowState = WindowState.Maximized;
                }
            });
        }
    }
}
