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
        #region property
        /// <summary>
        /// 真实姓名
        /// </summary>
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                this.RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 密码
        /// </summary>
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                this.RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 重复输入密码
        /// 注册相关
        /// </summary>
        private string passwordCheck;
        public string PasswordCheck
        {
            get { return passwordCheck; }
            set
            {
                passwordCheck = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 用户权限
        /// 0 - 游客
        /// 1 - 普通用户
        /// 2 - 管理员
        /// </summary>
        private int userRight;
        public int UserRight
        {
            get { return userRight; }
            set
            {
                userRight = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        private int userID;
        public int UserID
        {
            get { return userID; }
            set
            {
                userID = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 用户所在机构
        /// </summary>
        private string institute;
        public string Institute
        {
            get { return institute; }
            set
            {
                institute = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 用户手机号
        /// </summary>
        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        //构造函数
        public UserModel()
        {

        }

    }
}
