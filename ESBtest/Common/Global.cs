using ESBtest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ESBtest.Common
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public static class GlobalValue
    {
        public static DateTime FirstSamplingTime = new DateTime(2022, 08, 10);
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
    public class GlobalFunc
    {
        public static void CloseWindow(object w)
        {
            (w as Window).Close();
        }

        public static void MinWindow(object w)
        {
            (w as Window).WindowState = WindowState.Minimized;
        }

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
    }
    
}
