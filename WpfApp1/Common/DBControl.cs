using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using MySql.Data.MySqlClient;
using WpfApp1.Model;

namespace WpfApp1.Common
{
    public class DBControl
    {
        //数据库连接实例
        private MySqlConnection giricon = null;

        //
        public DBControl()
        {
            Initialization();
        }

        //
        private void Initialization()
        {
            if(TryConnection())
            {

            }
            
        }

        /// <summary>
        /// 尝试连接数据库
        /// </summary>
        /// <returns>
        /// 成功连接返回true
        /// 失败返回false
        /// </returns>
        private bool TryConnection()
        {
            try
            {
                string str = "server=localhost;User Id=root;password=scr170410;Database=test01";//连接MySQL的字符串
                this.giricon = new MySqlConnection(str);//实例化链接
                this.giricon.Open();//开启连接
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("数据库连接异常！" + ex);
                return false;
            }
        }


        #region 增

        #endregion

        #region 删

        #endregion

        #region 改

        #endregion

        #region 查

        public bool isLoginSuccess(string username, string password)
        {
            string sqlcmd = "select * from user where username = '" + username + "' and password = '" + password + "'";
            MySqlCommand mycmd = new MySqlCommand(sqlcmd, giricon);
            MySqlDataReader reader = mycmd.ExecuteReader();
            if (reader.HasRows)
            {
                return true;
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();

            return false;
        }

        #endregion
    }
}
