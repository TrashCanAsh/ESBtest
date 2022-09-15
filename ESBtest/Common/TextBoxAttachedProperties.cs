using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ESBtest.Common
{
    /// <summary>
    /// 文本框添加新property以实现禁止输入数字以外的字符
    /// </summary>
    public class TextBoxAttachedProperties
    {
        public static bool GetMyIsOnlyNumber(DependencyObject obj)
        {
            return (bool)obj.GetValue(MyIsOnlyNumberProperty);
        }
        public static void SetMyIsOnlyNumber(DependencyObject obj, bool value)
        {
            obj.SetValue(MyIsOnlyNumberProperty, value);
        }
        public static readonly DependencyProperty MyIsOnlyNumberProperty =
            DependencyProperty.RegisterAttached("MyIsOnlyNumber", typeof(bool), typeof(TextBox), new PropertyMetadata(false, (s, e) =>
                 {
                     if (s is TextBox textBox)
                     {
                         textBox.SetValue(InputMethod.IsInputMethodEnabledProperty, !(bool)e.NewValue);
                         textBox.PreviewTextInput -= TxtInput;
                         if ((bool)e.NewValue)
                         {
                             textBox.PreviewTextInput += TxtInput;
                         }
                     }
                 }));
        private static void TxtInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

    }
}
