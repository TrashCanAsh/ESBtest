using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//
using MySql.Data.MySqlClient;
using ESBtest.Model;

namespace ESBtest.Common
{
    /// <summary>
    /// 数据库操作类
    /// 内置各种数据库操作（增删改查）
    /// </summary>
    public class DBControl
    {
        #region 私人变量
        //数据库连接实例
        private MySqlConnection mysqlConn = null;
        //数据库命令实例
        private MySqlCommand mysqlCmd = null;
        #endregion

        #region 公共变量
        /// <summary>
        /// 数据库状态
        /// 0 -> 未连接
        /// 1 -> 连接成功
        /// </summary>
        public int sqlStatus = 0;

        #endregion


        /// <summary>
        /// 构造函数
        /// </summary>
        public DBControl() { }

        #region 基础操作
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
                this.mysqlConn = new MySqlConnection(str);//实例化链接
                this.mysqlConn.Open();//开启连接
                this.sqlStatus = 1;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("数据库连接异常！" + ex);
                this.sqlStatus = 0;
                return false;
            }
        }

        /// <summary>
        /// 结束命令和连接
        /// </summary>
        private void sqlDispose()
        {
            if (mysqlCmd != null)
            {
                mysqlCmd.Dispose();
                mysqlCmd = null;
            }
            if (mysqlConn != null)
            {
                mysqlConn.Close();
                mysqlConn.Dispose();
                mysqlConn = null;
            }
        }
        #endregion



        #region 增
        /// <summary>
        /// 向用户表中添加数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int InsertIntoUserTable(string name, string username, string password)
        {
            try
            {
                TryConnection();
                string sqlcmd = "insert into user (iduser, name, username, password, LOA) values (" + (NumOfTableRow("user") + 1).ToString() + ", '" + name + "', '"
                    + username + "', '" + password + "', 'normal_user')";
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                //For UPDATE, INSERT, and DELETE statements
                //返回值为受影响的列数，如果为-1则为操作失败
                return mysqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                sqlDispose();
            }
            return -1;
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
            try
            {
                TryConnection();
                string sqlcmd = "select * from user where username = '" + username + "'";
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                bool flag = reader.HasRows;
                reader.Close();
                if (flag)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("No rows found: username not exist");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                sqlDispose();
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
            try
            {
                TryConnection();
                string sqlcmd = "select * from user where username = '" + username + "' and password = '" + password + "'";
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                sqlDispose();
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
            try
            {
                TryConnection();
                string sqlcmd = "select count(*) from " + tablename;
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                //默认位置 SqlDataReader 位于第一条记录之前。 因此，必须调用 Read 才能开始访问任何数据。
                reader.Read();
                int n = reader.GetInt32(0);
                reader.Close();
                return n;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                sqlDispose();
            }
            return -1;
        }

        public List<SampleModel> SearchSample(string sampleName)
        {
            List<SampleModel> sList = new List<SampleModel>();
            try
            {
                TryConnection();
                string sqlcmd;
                if (string.IsNullOrEmpty(sampleName))
                {
                    sqlcmd = "select * from samples ";
                }
                else
                {
                    sqlcmd = "select * from samples where name = '" + sampleName + "'";
                }
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    SampleModel s = new SampleModel();
                    s.SampleID = reader.GetValue(0).ToString();
                    s.SampleName = reader.GetValue(1).ToString();
                    s.Category = reader.GetValue(2).ToString();
                    s.SamplingTime = ((DateTime)reader.GetValue(3)).ToShortDateString().ToString();
                    s.SamplingLocation = reader.GetValue(4).ToString() + ", " + reader.GetValue(5).ToString();
                    sList.Add(s);
                }

                return sList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                sqlDispose();
            }
            return null;
        }


        public List<SampleModel> SearchSample(string sampleName, string category, DateTime start, DateTime end, Location NW, Location SE)
        {
            List<SampleModel> sList = new List<SampleModel>();
            try
            {
                TryConnection();
                bool isFirst = true;
                string sqlcmd = "select * from samples where ";
                if (!string.IsNullOrEmpty(sampleName))
                {
                    isFirst = false;
                    sqlcmd += "name = '" + sampleName + "' ";
                }
                if (!string.IsNullOrEmpty(category))
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        sqlcmd += "category = '" + category + "' ";
                    }
                    else
                    {
                        sqlcmd += "and category = '" + category + "' ";
                    }
                }
                if (start != null)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        sqlcmd += "samplingtime >= '" + start.ToShortDateString().ToString() + "' ";
                    }
                    else
                    {
                        sqlcmd += "and samplingtime >= '" + start.ToShortDateString().ToString() + "' ";
                    }
                }
                if (end != null)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        sqlcmd += "samplingtime <= '" + end.ToShortDateString().ToString() + "' ";
                    }
                    else
                    {
                        sqlcmd += "and samplingtime <= '" + end.ToShortDateString().ToString() + "' ";
                    }
                }
                if (NW != null)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        sqlcmd += "longitude >= '" + NW.longitude.ToString() + "' and latitude >= '" + NW.latitude.ToString() + ", ";
                    }
                    else
                    {
                        sqlcmd += "and longitude >= '" + NW.longitude.ToString() + "' and latitude >= '" + NW.latitude.ToString() + ", ";
                    }
                }
                if (SE != null)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        sqlcmd += "longitude <= '" + SE.longitude.ToString() + "' and latitude <= '" + SE.latitude.ToString() + ", ";
                    }
                    else
                    {
                        sqlcmd += "and longitude <= '" + SE.longitude.ToString() + "' and latitude <= '" + SE.latitude.ToString() + ", ";
                    }
                }

                if(isFirst)
                {
                    sqlcmd = "select * from samples";
                }

                Console.WriteLine(sqlcmd);

                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    SampleModel s = new SampleModel();
                    s.SampleID = reader.GetValue(0).ToString();
                    s.SampleName = reader.GetValue(1).ToString();
                    s.Category = reader.GetValue(2).ToString();
                    s.SamplingTime = ((DateTime)reader.GetValue(3)).ToShortDateString().ToString();
                    s.SamplingLocation = reader.GetValue(4).ToString() + ", " + reader.GetValue(5).ToString();
                    sList.Add(s);
                }

                return sList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                sqlDispose();
            }
            return null;
        }

        #endregion
    }
}
