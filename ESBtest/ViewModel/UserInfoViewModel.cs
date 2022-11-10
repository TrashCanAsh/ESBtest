using ESBtest.Common;
using ESBtest.Model;
using ESBtest.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ESBtest.ViewModel
{
    public class UserInfoViewModel : NotifyBase
    {
        private DBControl dBControl;

        public UserModel userModel { get; set; }
        private string oldPassword;
        public string OldPassword
        {
            get { return oldPassword; }
            set
            {
                oldPassword = value;
                RaisePropertyChanged();
            }
        }

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase MinWindowCommand { get; set; }

        public CommandBase UpdateUserInfoCommand { get; set; }
        public CommandBase UpdateUserPasswordCommand { get; set; }


        public UserInfoViewModel()
        {
            Initialization();

            SetCommand();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialization()
        {
            //
            this.dBControl = new DBControl();
            //
            this.userModel = GlobalValue.CurrentUser;
        }

        /// <summary>
        /// 命令合集
        /// </summary>
        private void SetCommand()
        {
            //创建命令实例
            this.CloseWindowCommand = new CommandBase();
            this.MinWindowCommand = new CommandBase();
            this.UpdateUserInfoCommand = new CommandBase();
            this.UpdateUserPasswordCommand = new CommandBase();

            //关闭窗口命令
            this.CloseWindowCommand.ExecuteAction = new Action<object>(GlobalFunc.CloseWindow);
            //最小化窗口命令
            this.MinWindowCommand.ExecuteAction = new Action<object>(GlobalFunc.MinWindow);
            //确认修改用户信息命令
            this.UpdateUserInfoCommand.ExecuteAction = new Action<object>(UpdateUserInfo);
            //确认修改用户密码命令
            this.UpdateUserPasswordCommand.ExecuteAction = new Action<object>(UpdateUserPassword);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="w"></param>
        private void UpdateUserInfo(object w)
        {
            //修改数据库内容
            if (dBControl.UpdateUserTable(GlobalValue.CurrentUser.UserID, userModel.Name, userModel.Institute, userModel.PhoneNumber) > 0)
            {
                MessageBox.Show((w as Window), "修改成功", "提示");
                (w as Window).DialogResult = true;
            }
            else
            {
                MessageBox.Show((w as Window), "修改失败", "提示");
            }
            (w as Window).Close();
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="w"></param>
        private void UpdateUserPassword(object w)
        {
            //MD5加密
            string pwd = GlobalFunc.MD5ToString(userModel.UserName + "@" + OldPassword);

            if (string.IsNullOrEmpty(userModel.Password))
            {
                MessageBox.Show((w as Window), "新密码不能为空", "提示");
            }
            else if (userModel.Password != userModel.PasswordCheck)
            {
                MessageBox.Show((w as Window), "新密码不一致", "提示");
            }
            else if (string.IsNullOrEmpty(OldPassword))
            {
                MessageBox.Show((w as Window), "旧密码不能为空", "注册提示");
            }
            else if(dBControl.IsUserNameAndPasswordMatch(userModel.UserName, pwd) == false)
            {
                MessageBox.Show((w as Window), "旧密码有误", "注册提示");
            }
            else
            {
                //MD5加密
                pwd = GlobalFunc.MD5ToString(userModel.UserName + "@" + userModel.Password);
                //修改数据库内容
                if (dBControl.UpdateUserTable(GlobalValue.CurrentUser.UserID, pwd) > 0)
                {
                    MessageBox.Show((w as Window), "修改成功", "提示");
                    (w as Window).DialogResult = true;
                }
                else
                {
                    MessageBox.Show((w as Window), "修改失败", "提示");
                }
                (w as Window).Close();
            }
        }
    }
}
