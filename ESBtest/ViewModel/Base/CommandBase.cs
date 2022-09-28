using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ESBtest.ViewModel.Base
{
    /// <summary>
    /// 命令基础类
    /// </summary>
    public class CommandBase : ICommand
    {
        public Action<object> ExecuteAction { get; set; }
        public Func<object, bool> CanExecuteFunc { get; set; }

        public event EventHandler CanExecuteChanged = (sender, e) => { };
        /// <summary>
        /// 命令是否可以被执行
        /// </summary>
        /// <param name="parameter">额外参数</param>
        /// <returns>
        /// true -> 可以被执行
        /// false -> 不可以被执行，对应按钮等控件变成disable状态（变灰、无法选中等）
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc?.Invoke(parameter) != false;
        }
        /// <summary>
        /// 命令的具体执行内容，挂载在ExecuteAction 上
        /// </summary>
        /// <param name="parameter">额外参数</param>
        public void Execute(object parameter)
        {
            ExecuteAction?.Invoke(parameter);
        }

    }
}
