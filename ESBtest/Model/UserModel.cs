using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESBtest.ViewModel.Base;

namespace ESBtest.Model
{
    /// <summary>
    /// 用户数据模型
    /// </summary>
    public class UserModel:NotifyBase
    {
        #region 私人变量
        private string userName;
        private string password;
        private string passwordCheck;
        private string name;
        private int userRight;

        #endregion

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged();
            }
        }


        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                this.RaisePropertyChanged();
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                this.RaisePropertyChanged();
            }
        }

        public string PasswordCheck
        {
            get { return passwordCheck; }
            set
            {
                passwordCheck = value;
                RaisePropertyChanged();
            }
        }


        public int UserRight
        {
            get { return userRight; }
            set
            {
                userRight = value;
                RaisePropertyChanged();
            }
        }


    }
}
