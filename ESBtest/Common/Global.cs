using ESBtest.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ESBtest.Common
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class GlobalValue
    {
        public static DateTime FirstSamplingTime = new DateTime(2022, 08, 10);
        public static UserModel CurrentUser;
        public static ObservableCollection<string> SampleCategory = new ObservableCollection<string>() { "null", "solid", "liquid", "gas", "bio" };
        public static ObservableCollection<string> SampleState = new ObservableCollection<string>() { "未知", "在库", "锁定", "已借出" };
        public static ObservableCollection<string> RecordState = new ObservableCollection<string>() { "未知", "审批中", "审批通过", "已借出", "已返还", "5", "6", "7", "8", "审批未通过", "用户取消" };
    }
    /// <summary>
    /// 地点类
    /// longitude代表经度
    /// latitude代表维度
    /// </summary>
    public class Location
    {
        public double longitude { get; set; }
        public double latitude { get; set; }

        public Location()
        {
            this.longitude = 0.0;
            this.latitude = 0.0;
        }
    }
    /// <summary>
    /// 全局函数
    /// </summary>
    public class GlobalFunc
    {
        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        /// <param name="w">View</param>
        public static void CloseWindow(object w)
        {
            (w as Window).Close();
        }
        /// <summary>
        /// 最小化当前窗口
        /// </summary>
        /// <param name="w">View</param>
        public static void MinWindow(object w)
        {
            (w as Window).WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// 最大化当前窗口，如果已经最大化，则还原窗口
        /// </summary>
        /// <param name="w">View</param>
        public static void MaxWindow(object w)
        {
            if ((w as Window).WindowState == WindowState.Maximized)
            {
                (w as Window).WindowState = WindowState.Normal;
            }
            else
            {
                (w as Window).WindowState = WindowState.Maximized;
            }
        }
        /// <summary>
        /// 是否为管理员及以上权限
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public static bool AdminRight()
        {
            return GlobalValue.CurrentUser.UserRight > 1 ? true : false;
        }
        public static bool AdminRight(object w)
        {
            return GlobalValue.CurrentUser.UserRight > 1 ? true : false;
        }
        /// <summary>
        /// 是否为普通用户及以上权限
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public static bool NormalUserRight()
        {
            return GlobalValue.CurrentUser.UserRight > 0 ? true : false;
        }
        public static bool NormalUserRight(object w)
        {
            return GlobalValue.CurrentUser.UserRight > 0 ? true : false;
        }
        /// <summary>
        /// 对目标字符串进行MD5加密
        /// </summary>
        /// <param name="argString"></param>
        /// <returns></returns>
        public static string MD5ToString(String argString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(argString);
            byte[] result = md5.ComputeHash(data);
            String strReturn = String.Empty;
            for (int i = 0; i < result.Length; i++)
                strReturn += result[i].ToString("x").PadLeft(2, '0');
            return strReturn;
        }
    }
    
}
