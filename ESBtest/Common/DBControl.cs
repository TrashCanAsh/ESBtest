using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//
using MySql.Data.MySqlClient;
using ESBtest.Model;
using System.Collections.ObjectModel;

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
        #endregion 私人变量

        #region 公共变量
        /// <summary>
        /// 数据库状态
        /// 0 -> 未连接
        /// 1 -> 连接成功
        /// </summary>
        public int sqlStatus = 0;
        #endregion 公共变量


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
        #endregion 基础操作



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
                string num = (NumOfLastID("user") + 1).ToString();
                TryConnection();
                string sqlcmd = "insert into user (iduser, name, username, password, LOA) values (" + num + ", '" + name + "', '"
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
        /// <summary>
        /// 向样品表中添加数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="category"></param>
        /// <param name="samplingTime"></param>
        /// <param name="lo"></param>
        /// <param name="la"></param>
        /// <returns></returns>
        public int InsertIntoSampleTable(string name, string category, string samplingTime, string lo, string la, int state, string comment)
        {
            try
            {
                string num = (NumOfLastID("samples") + 1).ToString();
                TryConnection();
                string sqlcmd = "INSERT INTO samples (idsamples, name, category, samplingtime, longitude, latitude, state, comment) VALUES (" + num + ",'" + name + "','"
                    + category + "','" + samplingTime + "'," + lo + "," + la + "," + state + ",'" + comment + "')";
                Console.WriteLine(sqlcmd);
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
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
        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="sList"></param>
        /// <returns></returns>
        public int InsertIntoSampleTable(List<SampleModel> sList)
        {
            try
            {
                int num = (NumOfLastID("samples") + 1);
                TryConnection();
                string sqlcmd = "";
                foreach (SampleModel sample in sList)
                {
                    sqlcmd += "insert into samples (idsamples, name, category, samplingtime, longitude, latitude, state, comment) values (" + num++ + ",'" + sample.SampleName + "','"
                    + sample.Category + "','" + sample.SamplingDate + "'," + sample.Longitude + "," + sample.Latitude + "," + sample.State + ",'" + sample.Comment + "');\n";
                }
                Console.WriteLine(sqlcmd);
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
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
        /// <summary>
        /// 向用户收藏表中添加数据
        /// </summary>
        /// <param name="iduser"></param>
        /// <param name="idsamples"></param>
        /// <returns></returns>
        public int InsertIntoFavoriteTable(int iduser, string idsamples)
        {
            try
            {
                string num = (NumOfLastID("userfavorites") + 1).ToString();
                TryConnection();
                string sqlcmd = "insert into userfavorites (iduserfavorites, iduser, idsamples) values (" + num + ", " + iduser + ", " + idsamples + ")";
                Console.WriteLine(sqlcmd);
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
        /// <summary>
        /// 向用户收藏表中批量添加数据
        /// </summary>
        /// <param name="iduser"></param>
        /// <param name="iList"></param>
        /// <returns></returns>
        public int InsertIntoFavoriteTable(int iduser, List<int> iList)
        {
            try
            {
                int num = (NumOfLastID("userfavorites") + 1);
                TryConnection();
                string sqlcmd = "";
                foreach (int i in iList)
                {
                    sqlcmd += "INSERT INTO userfavorites (iduserfavorites, iduser, idsamples) VALUES (" + num++ + "," + iduser + "," + i + ");\n";
                }
                Console.WriteLine(sqlcmd);
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
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
        /// <summary>
        /// 向用户购物车表中添加数据
        /// </summary>
        /// <param name="iduser"></param>
        /// <param name="idsamples"></param>
        /// <returns></returns>
        public int InsertIntoCartTable(int iduser, string idsamples)
        {
            try
            {
                string num = (NumOfLastID("usercarts") + 1).ToString();
                TryConnection();
                string sqlcmd = "insert into usercarts (idusercarts, iduser, idsamples) values (" + num + ", " + iduser + ", " + idsamples + ")";
                Console.WriteLine(sqlcmd);
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
        /// <summary>
        /// 向用户购物车表中批量添加数据
        /// </summary>
        /// <param name="iduser"></param>
        /// <param name="iList"></param>
        /// <returns></returns>
        public int InsertIntoCartTable(int iduser, List<int> iList)
        {
            try
            {
                int num = (NumOfLastID("usercarts") + 1);
                TryConnection();
                string sqlcmd = "";
                foreach (int i in iList)
                {
                    sqlcmd += "INSERT INTO usercarts (idusercarts, iduser, idsamples) VALUES (" + num++ + "," + iduser + "," + i + ");\n";
                }
                Console.WriteLine(sqlcmd);
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
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
        /// <summary>
        /// 向样品借出申请表中添加数据
        /// </summary>
        /// <param name="sList">样品ID列表</param>
        /// <param name="requestDate">申请日期</param>
        /// <param name="requestcomment">申请备注</param>
        /// <param name="userID">申请用户ID</param>
        /// <param name="state">申请状态</param>
        /// <returns></returns>
        public int InsertIntoSampleRecordTable(List<int> sList, string requestDate, string requestcomment, int userID, int state)
        {
            try
            {
                int num = (NumOfLastID("samplesrecord") + 1);
                int idrecord = SearchLastSampleRecordId(userID) + 1;
                TryConnection();
                string sqlcmd = "";
                foreach (int s in sList)
                {
                    sqlcmd += "INSERT INTO samplesrecord (idsamplesrecord, idrecord, iduser, idsamples, requestdate, requestcomment, state) VALUES (" + num++ + "," + idrecord + "," 
                        + userID + "," + s + ",'" + requestDate + "','" + requestcomment + "'," + state + ");\n";
                }
                Console.WriteLine(sqlcmd);
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
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
        #endregion 增

        #region 删
        /// <summary>
        /// 根据选中的样品ID来删除对应样品数据
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public int DeleteSampleTable(List<int> idList)
        {
            try
            {
                TryConnection();
                //DELETE FROM table_name WHERE (situation)
                string sqlcmd = "DELETE FROM samples WHERE (";
                bool isFirst = true;
                foreach (int i in idList)
                {
                    if(isFirst)
                    {
                        sqlcmd += "idsamples = " + i;
                        isFirst = false;
                    }
                    else
                    {
                        sqlcmd += " or idsamples = " + i;
                    }
                }
                sqlcmd += ")";
                Console.WriteLine(sqlcmd);
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
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
        /// <summary>
        /// 根据选中的样品ID和当前用户ID来删除对应收藏夹中数据
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int DeleteFavoriteTable(List<int> idList, int userID)
        {
            try
            {
                TryConnection();
                //DELETE FROM table_name WHERE (situation)
                string sqlcmd = "DELETE FROM userfavorites WHERE ( iduser = " + userID + " AND ( ";
                bool isFirst = true;
                foreach (int i in idList)
                {
                    if (isFirst)
                    {
                        sqlcmd += "idsamples = " + i;
                        isFirst = false;
                    }
                    else
                    {
                        sqlcmd += " or idsamples = " + i;
                    }
                }
                sqlcmd += "))";
                Console.WriteLine(sqlcmd);
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
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
        /// <summary>
        /// 根据用户ID和样品ID删除收藏夹对应信息
        /// </summary>
        /// <param name="iduser"></param>
        /// <param name="idsamples"></param>
        /// <returns></returns>
        public int DeleteFavoriteTable(int iduser, string idsamples)
        {
            try
            {
                TryConnection();
                //DELETE FROM table_name WHERE (situation)
                string sqlcmd = "DELETE FROM userfavorites WHERE ( iduser = " + iduser + " AND idsamples = " + idsamples + " )";
                Console.WriteLine(sqlcmd);
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
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
        /// <summary>
        /// 根据选中的样品ID和当前用户ID来删除对应购物车中数据
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int DeleteCartTable(List<int> idList, int userID)
        {
            try
            {
                TryConnection();
                //DELETE FROM table_name WHERE (situation)
                string sqlcmd = "DELETE FROM usercarts WHERE ( iduser = " + userID + " AND ( ";
                bool isFirst = true;
                foreach (int i in idList)
                {
                    if (isFirst)
                    {
                        sqlcmd += "idsamples = " + i;
                        isFirst = false;
                    }
                    else
                    {
                        sqlcmd += " or idsamples = " + i;
                    }
                }
                sqlcmd += "))";
                Console.WriteLine(sqlcmd);
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
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
        /// <summary>
        /// 根据用户ID和样品ID删除购物车对应信息
        /// </summary>
        /// <param name="iduser"></param>
        /// <param name="idsamples"></param>
        /// <returns></returns>
        public int DeleteCartTable(int iduser, string idsamples)
        {
            try
            {
                TryConnection();
                //DELETE FROM table_name WHERE (situation)
                string sqlcmd = "DELETE FROM usercarts WHERE ( iduser = " + iduser + " AND idsamples = " + idsamples + " )";
                Console.WriteLine(sqlcmd);
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
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
        /// <summary>
        /// 根据用户ID来删除购物车中所有对应信息
        /// </summary>
        /// <param name="iduser"></param>
        /// <returns></returns>
        public int DeleteCartTable(int iduser)
        {
            try
            {
                TryConnection();
                //DELETE FROM table_name WHERE (situation)
                string sqlcmd = "DELETE FROM usercarts WHERE ( iduser = " + iduser + " )";
                Console.WriteLine(sqlcmd);
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
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
        #endregion 删

        #region 改
        /// <summary>
        /// 修改某一样品信息
        /// </summary>
        /// <param name="sampleID"></param>
        /// <param name="sampleName"></param>
        /// <param name="category"></param>
        /// <param name="samplingTime"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int UpdateSampleTable(string sampleID, string sampleName, string category, string samplingTime, string longitude, string latitude, int state, string comment)
        {
            try
            {
                TryConnection();
                //UPDATE table_name SET column_name1 = new_value, column_name2 = new_value, ... WHERE ( situation);
                string sqlcmd = "UPDATE samples SET name = '" + sampleName + "', category = '" + category + "', samplingtime = '" + samplingTime + 
                    "', longitude = " + longitude + ", latitude = " + latitude + ", state = " + state + ", comment = '" + comment + "' WHERE idsamples = " + sampleID;
                Console.WriteLine(sqlcmd);
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
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
        /// <summary>
        /// 修改样品借出表中内容
        /// </summary>
        /// <param name="idRecord"></param>
        /// <param name="idUser"></param>
        /// <param name="approvalDate"></param>
        /// <param name="outDate"></param>
        /// <param name="inDate"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public int UpdateRecordTable(int idRecord, int idUser, string approvalDate, int idadmin, string approvalcomment, int State)
        {
            //todo
            try
            {
                TryConnection();
                //UPDATE table_name SET column_name1 = new_value, column_name2 = new_value, ... WHERE ( situation);
                bool isFirst = true;
                //string sqlcmd = "UPDATE samplesrecord SET approvaldate = '" + approvalDate + "', outdate = '" + outDate 
                //    + "', indate = '" + inDate + "', state = " + State + " WHERE (idrecord = " + idRecord + " AND iduser = " + idUser + ")";
                string sqlcmd = "UPDATE samplesrecord SET ";
                //approvalDate
                if (!string.IsNullOrEmpty(approvalDate))
                {
                    sqlcmd += "approvaldate = '" + approvalDate + "'";
                    isFirst = false;
                }
                //approvalcomment
                if(!string.IsNullOrEmpty(approvalcomment))
                {
                    if(isFirst)
                    {
                        sqlcmd += "approvalcomment = '" + approvalcomment + "'";
                        isFirst = false;
                    }
                    else
                    {
                        sqlcmd += ", approvalcomment = '" + approvalcomment + "'";
                    }
                }
                //idadmin
                if(isFirst)
                {
                    sqlcmd += "idadmin = " + idadmin;
                    isFirst = false;
                }
                else
                {
                    sqlcmd += ", idadmin = " + idadmin;
                }
                //state
                if (isFirst)
                {
                    sqlcmd += "state = " + State;
                    isFirst = false;
                }
                else
                {
                    sqlcmd += ", state = " + State;
                }
                sqlcmd += " WHERE (idrecord = " + idRecord + " AND iduser = " + idUser + ")";
                Console.WriteLine(sqlcmd);
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
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
        #endregion 改

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
        /// 根据用户名查询用户信息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public UserModel GetUserInformation(string username)
        {
            try
            {
                TryConnection();
                string sqlcmd = "select * from user where username = '" + username + "'";
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                
                if (reader.HasRows)
                {
                    reader.Read();
                    UserModel user = new UserModel()
                    {
                        UserID = reader.GetInt32(0),
                        Name = reader.GetValue(1).ToString(),
                        UserName = reader.GetValue(2).ToString()
                    };
                    return user;
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
            return null;
        }
        /// <summary>
        /// （管理员）查询用户名是否已存在
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsAdminNameExist(string adminname)
        {
            try
            {
                TryConnection();
                string sqlcmd = "select * from admin where adminname = '" + adminname + "'";
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                bool flag = reader.HasRows;
                reader.Close();
                if (flag)
                {
                    return true;
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
        /// （管理员）根据用户名和密码查找是否匹配
        /// </summary>
        /// <param name="adminname"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsAdminNameAndPasswordMatch(string adminname, string password)
        {
            try
            {
                TryConnection();
                string sqlcmd = "select * from admin where adminname = '" + adminname + "' and password = '" + password + "'";
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                bool flag = reader.HasRows;
                reader.Close();
                if (flag)
                {
                    return true;
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
        /// （管理员）根据用户名查询用户信息
        /// </summary>
        /// <param name="adminname"></param>
        /// <returns></returns>
        public UserModel GetAdminInformation(string adminname)
        {
            try
            {
                TryConnection();
                string sqlcmd = "select * from admin where adminname = '" + adminname + "'";
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    UserModel user = new UserModel()
                    {
                        UserID = reader.GetInt32(0),
                        Name = reader.GetValue(1).ToString(),
                        UserName = reader.GetValue(2).ToString()
                    };
                    return user;
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
            return null;
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
        /// <summary>
        /// 查找表中最后一个数据的ID值
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public int NumOfLastID(string tablename)
        {
            try
            {
                TryConnection();
                //SELECT * FROM table_name ORDER BY column_name DESC LIMIT 1;
                string sqlcmd = "select * from " + tablename + " order by id" + tablename + " desc limit 1";
                Console.WriteLine(sqlcmd);
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                if(reader.HasRows)
                {
                    //默认位置 SqlDataReader 位于第一条记录之前。 因此，必须调用 Read 才能开始访问任何数据。
                    reader.Read();
                    int n = reader.GetInt32(0);
                    reader.Close();
                    return n;
                }
                else
                {
                    return 0;
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
            return -1;
        }
        /// <summary>
        /// 搜索全部内容
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<SampleModel> SearchSample()
        {
            ObservableCollection<SampleModel> sList = new ObservableCollection<SampleModel>();
            try
            {
                TryConnection();
                string sqlcmd = "select * from samples";
                
                Console.WriteLine(sqlcmd);

                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    sList = GetSampleList(reader);
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
        /// <summary>
        /// 根据各种参数进行搜索
        /// </summary>
        /// <param name="sampleName">样品名称</param>
        /// <param name="category">样品种类</param>
        /// <param name="start">采样起始时间</param>
        /// <param name="end">采样结束时间</param>
        /// <param name="NW">左上角坐标</param>
        /// <param name="SE">右下角坐标</param>
        /// <returns></returns>
        public ObservableCollection<SampleModel> SearchSample(string sampleName, string category, string start, string end, Location NW, Location SE)
        {
            ObservableCollection<SampleModel> sList = new ObservableCollection<SampleModel>();
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
                if (!string.IsNullOrEmpty(start))
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        sqlcmd += "samplingtime >= '" + start + "' ";
                    }
                    else
                    {
                        sqlcmd += "and samplingtime >= '" + start + "' ";
                    }
                }
                if (!string.IsNullOrEmpty(end))
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        sqlcmd += "samplingtime <= '" + end + "' ";
                    }
                    else
                    {
                        sqlcmd += "and samplingtime <= '" + end + "' ";
                    }
                }
                if (NW != null)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        sqlcmd += "longitude >= '" + NW.longitude.ToString() + "' and latitude >= '" + NW.latitude.ToString() + "' ";
                    }
                    else
                    {
                        sqlcmd += "and longitude >= '" + NW.longitude.ToString() + "' and latitude >= '" + NW.latitude.ToString() + "' ";
                    }
                }
                if (SE != null)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        sqlcmd += "longitude <= '" + SE.longitude.ToString() + "' and latitude <= '" + SE.latitude.ToString() + "' ";
                    }
                    else
                    {
                        sqlcmd += "and longitude <= '" + SE.longitude.ToString() + "' and latitude <= '" + SE.latitude.ToString() + "' ";
                    }
                }

                if(isFirst)
                {
                    sqlcmd = "select * from samples";
                }
                

                Console.WriteLine(sqlcmd);

                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    sList = GetSampleList(reader);
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
        /// <summary>
        /// 根据选中的样品ID进行搜索
        /// </summary>
        /// <param name="idList">样品ID列表</param>
        /// <returns></returns>
        public ObservableCollection<SampleModel> SearchSample(List<int> idList)
        {
            ObservableCollection<SampleModel> sList = new ObservableCollection<SampleModel>();
            try
            {
                TryConnection();
                string sqlcmd = "SELECT * FROM samples WHERE (";
                if(idList.Count>0)
                {
                    bool isFirst = true;
                    foreach (int i in idList)
                    {
                        if (isFirst)
                        {
                            sqlcmd += "idsamples = " + i;
                            isFirst = false;
                        }
                        else
                        {
                            sqlcmd += " or idsamples = " + i;
                        }
                    }
                    sqlcmd += ")";
                }
                else
                {
                    return null;
                }
                
                Console.WriteLine(sqlcmd);

                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    sList = GetSampleList(reader);
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
        /// <summary>
        /// 根据用户ID来查询收藏样品信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>对应用户的收藏夹中的样品ID列表</returns>
        public List<int> SearchFavorited(int userID)
        {
            List<int> fList = new List<int>();
            try
            {
                TryConnection();
                string sqlcmd = "select * from userfavorites where iduser = " + userID;

                Console.WriteLine(sqlcmd);

                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fList.Add(reader.GetInt32(2));
                    }
                }
                reader.Close();
                return fList;
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
        /// <summary>
        /// 根据用户ID来查询购物车中样品信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>对应用户购物车中的样品ID列表</returns>
        public List<int> SearchInCart(int userID)
        {
            List<int> cList = new List<int>();
            try
            {
                TryConnection();
                string sqlcmd = "select * from usercarts where iduser = " + userID;

                Console.WriteLine(sqlcmd);

                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cList.Add(reader.GetInt32(2));
                    }
                }
                reader.Close();
                return cList;
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
        /// <summary>
        /// 根据用户ID来查询借出申请表中最后一个申请的ID值
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int SearchLastSampleRecordId(int userID)
        {
            try
            {
                TryConnection();
                //SELECT * FROM table_name ORDER BY column_name DESC LIMIT 1;
                string sqlcmd = "select * from samplesrecord where iduser = " + userID + " order by idrecord desc limit 1 ";
                Console.WriteLine(sqlcmd);
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    //默认位置 SqlDataReader 位于第一条记录之前。 因此，必须调用 Read 才能开始访问任何数据。
                    reader.Read();
                    int n = reader.GetInt32(1);//读取idrecord信息
                    reader.Close();
                    return n;
                }
                else
                {
                    return 0;
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
            return -1;
        }
        //todotodo
        public ObservableCollection<SampleRecordModel> SearchRecord()
        {
            ObservableCollection<SampleRecordModel> srList = new ObservableCollection<SampleRecordModel>();
            try
            {
                TryConnection();
                //select distinct username, idrecord, requestdate, state from samplesrecord, user where samplesrecord.iduser = user.iduser and samplesrecord.state = 1;
                string sqlcmd = "SELECT DISTINCT name, idrecord, user.iduser, requestdate, state FROM samplesrecord, user WHERE samplesrecord.iduser = user.iduser AND samplesrecord.state = ";

                Console.WriteLine(sqlcmd);

                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SampleRecordModel sr = new SampleRecordModel()
                        {
                            UserName = reader.GetValue(0).ToString(),
                            IdRecord = reader.GetInt32(1),
                            IdUser = reader.GetInt32(2),
                            RequestDate = reader.GetDateTime(3),
                            State = reader.GetInt32(4)
                        };
                        sr.StateStr = GlobalValue.RecordState[sr.State];
                        srList.Add(sr);
                    }
                }
                return srList;
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
        /// <summary>
        /// 根据申请状态查询对应的申请记录
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<SampleRecordModel> SearchRecord(string state)
        {
            ObservableCollection<SampleRecordModel> srList = new ObservableCollection<SampleRecordModel>();
            try
            {
                TryConnection();
                //select distinct username, idrecord, requestdate, state from samplesrecord, user where samplesrecord.iduser = user.iduser and samplesrecord.state = 1;
                //选中所有“待审批”状态（state = 1）的记录
                string sqlcmd = "SELECT DISTINCT name, idrecord, user.iduser, requestdate, state FROM samplesrecord, user WHERE samplesrecord.iduser = user.iduser AND samplesrecord.state = " + state;

                Console.WriteLine(sqlcmd);

                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SampleRecordModel sr = new SampleRecordModel()
                        {
                            UserName = reader.GetValue(0).ToString(),
                            IdRecord = reader.GetInt32(1),
                            IdUser = reader.GetInt32(2),
                            RequestDate = reader.GetDateTime(3),
                            State = reader.GetInt32(4)
                        };
                        sr.StateStr = GlobalValue.RecordState[sr.State];
                        srList.Add(sr);
                    }
                }
                return srList;
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
        /// <summary>
        /// 根据用户ID来查询样品申请表中对应用户的样品申请记录（记录编号，申请日期，申请状态）
        /// </summary>
        /// <param name="iduser"></param>
        /// <returns></returns>
        public ObservableCollection<SampleRecordModel> SearchRecord(int iduser)
        {
            ObservableCollection<SampleRecordModel> srList = new ObservableCollection<SampleRecordModel>();
            try
            {
                TryConnection();
                string sqlcmd = "SELECT DISTINCT idrecord, requestdate, state FROM samplesrecord WHERE iduser = " + iduser;

                Console.WriteLine(sqlcmd);

                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SampleRecordModel sr = new SampleRecordModel()
                        {
                            IdUser = iduser,
                            IdRecord = reader.GetInt32(0),
                            RequestDate = reader.GetDateTime(1).Date,
                            State = reader.GetInt32(2)
                        };
                        if (sr.State < 10)
                        {
                            sr.StateStr = GlobalValue.RecordState[sr.State];
                            srList.Add(sr);
                        }
                    }
                }
                return srList;
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
        /// <summary>
        /// 根据用户ID和样品申请记录ID来查询对应的申请记录信息（申请日期，申请人姓名）
        /// </summary>
        /// <param name="idrecord"></param>
        /// <param name="iduser"></param>
        /// <returns></returns>
        public SampleRecordModel SearchRecord(int idrecord, int iduser)
        {
            try
            {
                TryConnection();
                //select distinct username, idrecord, requestdate, state from samplesrecord, user where user.iduser = 1 and samplesrecord.idrecord = 1;
                string sqlcmd = "SELECT DISTINCT name, requestdate, state FROM samplesrecord, user WHERE user.iduser = " + iduser + " AND samplesrecord.idrecord = " + idrecord;

                Console.WriteLine(sqlcmd);
                SampleRecordModel sr = null;
                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    sr = new SampleRecordModel()
                    {
                        IdRecord = idrecord,
                        IdUser = iduser,
                        UserName = reader.GetValue(0).ToString(),
                        RequestDate = reader.GetDateTime(1),
                        State = reader.GetInt32(2),
                    };
                    sr.StateStr = GlobalValue.RecordState[sr.State];
                }
                return sr;
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
        /// <summary>
        /// 根据用户ID和记录ID来查询某一样品申请的具体内容
        /// </summary>
        /// <param name="idrecord"></param>
        /// <param name="iduser"></param>
        /// <returns></returns>
        public List<int> SearchSampleRecord(int idrecord, int iduser)
        {
            List<int> iList = new List<int>();
            try
            {
                TryConnection();
                string sqlcmd = "SELECT idsamples FROM samplesrecord WHERE iduser = " + iduser + " AND idrecord = " + idrecord;

                Console.WriteLine(sqlcmd);

                mysqlCmd = new MySqlCommand(sqlcmd, mysqlConn);
                MySqlDataReader reader = mysqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        iList.Add(reader.GetInt32(0));
                    }
                }
                return iList;
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
        #endregion 查

        #region 辅助函数
        /// <summary>
        /// 通过已有的MySqlDataReader来将数据存储到SampleModel的List中并返回
        /// </summary>
        /// <param name="reader">MySqlDataReader</param>
        /// <returns></returns>
        private ObservableCollection<SampleModel> GetSampleList(MySqlDataReader reader)
        {
            ObservableCollection<SampleModel> sList = new ObservableCollection<SampleModel>();
            while (reader.Read())
            {
                SampleModel s = new SampleModel
                {
                    SampleID = reader.GetValue(0).ToString(),
                    SampleName = reader.GetValue(1).ToString(),
                    Category = reader.GetValue(2).ToString(),
                    SamplingDate = ((DateTime)reader.GetValue(3)).ToShortDateString(),
                    SamplingDateTime = (DateTime)reader.GetValue(3),
                    SamplingLocation = reader.GetValue(4).ToString() + ", " + reader.GetValue(5).ToString(),
                    Longitude = reader.GetValue(4).ToString(),
                    Latitude = reader.GetValue(5).ToString(),
                    State = reader.GetInt32(6),
                    Comment = reader.GetValue(7).ToString()
                };
                switch (s.Category)
                {
                    case "solid":
                        s.CategoryIndex = 1;
                        break;
                    case "liquid":
                        s.CategoryIndex = 2;
                        break;
                    case "gas":
                        s.CategoryIndex = 3;
                        break;
                    case "bio":
                        s.CategoryIndex = 4;
                        break;
                    default:
                        break;
                }
                switch (s.State)
                {
                    case 0:
                        s.StateStr = "unknown";
                        break;
                    case 1:
                        s.StateStr = "in stock";
                        break;
                    case 2:
                        s.StateStr = "locked";
                        break;
                    case 3:
                        s.StateStr = "out on loan";
                        break;
                    default:
                        break;
                }
                sList.Add(s);
            }
            reader.Close();
            return SearchFC(sList, GlobalValue.CurrentUser.UserID);
        }
        /// <summary>
        /// 通过已有的MySqlDataReader来将数据存储到SampleRecordModel的List中并返回
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private ObservableCollection<SampleRecordModel> GetSampleRecordList(MySqlDataReader reader)
        {
            //todo
            ObservableCollection<SampleRecordModel> srList = new ObservableCollection<SampleRecordModel>();
            while (reader.Read())
            {
                SampleRecordModel sr = new SampleRecordModel
                {
                };
                srList.Add(sr);
            }
            reader.Close();
            return srList;
        }
        /// <summary>
        /// 向SampleModel列表中添加收藏夹和购物车信息
        /// </summary>
        /// <param name="sList"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public ObservableCollection<SampleModel> SearchFC(ObservableCollection<SampleModel> sList, int UserID)
        {
            if (UserID > 0)
            {
                List<int> fList = SearchFavorited(UserID);
                List<int> cList = SearchInCart(UserID);
                foreach (SampleModel sample in sList)
                {
                    foreach (int f in fList)
                    {
                        if (sample.SampleID == f.ToString())
                        {
                            sample.IsFavorited = true;
                            fList.Remove(f);
                            break;
                        }
                    }
                    foreach (int c in cList)
                    {
                        if (sample.SampleID == c.ToString())
                        {
                            sample.IsInCart = true;
                            cList.Remove(c);
                            break;
                        }
                    }
                    if (fList.Count == 0 && cList.Count == 0)
                        break;
                }
            }
            return sList;
        }

        #endregion 辅助函数

    }
}
