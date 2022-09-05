using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Common
{
    public class PasswordBoxHelper
    {
        static bool isUpdating = false;

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password",typeof(string),typeof(PasswordBoxHelper),
                new FrameworkPropertyMetadata("", new PropertyChangedCallback(OnPropertyChanged)));

        public static string GetPassword(DependencyObject d)
        {
            return d.GetValue(PasswordProperty).ToString();
        }

        public static void SetPassword(DependencyObject d, string value)
        {
            d.SetValue(PasswordProperty, value);
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox pb = d as PasswordBox;
            if (pb != null)
            {
                //禁用密码改变事件
                pb.PasswordChanged -= Password_PasswordChanged;

            }
            if (e.NewValue != null)
            {
                if (!isUpdating)
                {
                    pb.Password = e.NewValue.ToString();
                }
            }
            else
            {
                pb.Password = string.Empty;
            }
            // 启用密码改变事件
            pb.PasswordChanged += Password_PasswordChanged; 
        }

        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PasswordBoxHelper),
                new FrameworkPropertyMetadata(default(bool), new PropertyChangedCallback(OnAttached)));

        

        public static bool GetAttach(DependencyObject d)
        {
            return (bool)d.GetValue(AttachProperty);
        }

        public static void SetAttach(DependencyObject d, bool value)
        {
            d.SetValue(AttachProperty, value);
        }

        private static void OnAttached(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox pb = d as PasswordBox;
            pb.PasswordChanged += Password_PasswordChanged;
        }

        private static void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = sender as PasswordBox;
            isUpdating = true;
            SetPassword(pb, pb.Password);
            isUpdating = false;
        }
    }
}
