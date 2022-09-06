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
    /// <summary>
    /// 数据库操作类
    /// 内置各种数据库操作（增删改查）
    /// </summary>
    public class DBControl
    {
        #region 私人变量
        //数据库连接实例
        private MySqlConnection giricon = null;
        #endregion

        #region 公共变量
        /// <summary>
        /// 数据库状态
        /// 0 -> 未连接
        /// 1 -> 连接成功
        /// </summary>
        public int sqlStatus = 0;
        #endregion
        //
        public DBControl()
        {
            Initialization();
        }

        /// <summary>
        /// 数据库初始化函数
        /// </summary>
        private void Initialization()
        {
            if(TryConnection())
            {
                this.sqlStatus = 1;
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
        public int InsertIntoUserTable(string name, string username, string password)
        {
            string sqlcmd = "insert into user (iduser, name, username, password, LOA) values (" + (NumOfTableRow("user") + 1).ToString() + ", '" + name + "', '"
                    + username + "', '" + password + "', 'normal_user')";
            MySqlCommand mycmd = new MySqlCommand(sqlcmd, giricon);
            //For UPDATE, INSERT, and DELETE statements
            //返回值为受影响的列数，如果为-1则为操作失败
            return mycmd.ExecuteNonQuery();
        }
        #endregion

        #region 删

        #endregion

        #region 改

        #endregion

        #region 查
        /// <summary>
        /// 查询用户名是否已存在
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsUserNameExist(string username)
        {
            string sqlcmd = "select * from user where username = '" + username + "'";
            MySqlCommand mycmd = new MySqlCommand(sqlcmd, giricon);
            MySqlDataReader reader = mycmd.ExecuteReader();
            bool flag = reader.HasRows;
            reader.Close();
            if (flag)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据用户名和密码查找是否匹配
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsUserNameAndPasswordMatch(string username, string password)
        {
            string sqlcmd = "select * from user where username = '" + username + "' and password = '" + password + "'";
            MySqlCommand mycmd = new MySqlCommand(sqlcmd, giricon);
            MySqlDataReader reader = mycmd.ExecuteReader();
            bool flag = reader.HasRows;
            reader.Close();
            if (flag)
            {
                return true;
            }
            else
            {
                Console.WriteLine("No rows found: username & password not match");
            }

            return false;
        }
        /// <summary>
        /// 查找某表的列数
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public int NumOfTableRow(string tablename)
        {
            string sqlcmd = "select count(*) from " + tablename;
            MySqlCommand mycmd = new MySqlCommand(sqlcmd, giricon);
            MySqlDataReader reader = mycmd.ExecuteReader();
            //默认位置 SqlDataReader 位于第一条记录之前。 因此，必须调用 Read 才能开始访问任何数据。
            reader.Read();
            int n = reader.GetInt32(0);
            reader.Close();
            return n;
        }

        #endregion
    }
}
