using ESBtest.Common;
using ESBtest.Model;
using ESBtest.View;
using ESBtest.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ESBtest.ViewModel
{
    public class CartViewModel : NotifyBase
    {
        private DBControl dBControl;

        public ObservableCollection<SampleModel> SampleModelList { get; set; }

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase MinWindowCommand { get; set; }
        public CommandBase MaxWindowCommand { get; set; }

        public CommandBase CheckAllCommand { get; set; }
        public CommandBase DeleteCartCommand { get; set; }
        public CommandBase SampleRequestCommand { get; set; }


        public CartViewModel()
        {
            Initialization();

            SetCommand();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialization()
        {
            //创建数据库操作实例
            this.dBControl = new DBControl();
            //创建表格数据源实例,赋值：当前用户的购物车内容
            this.SampleModelList = dBControl.SearchSample(dBControl.SearchInCart(GlobalValue.CurrentUser.UserID));
        }
        /// <summary>
        /// 命令合集
        /// </summary>
        private void SetCommand()
        {
            #region 窗口命令
            //创建命令实例
            this.CloseWindowCommand = new CommandBase();
            this.MinWindowCommand = new CommandBase();
            this.MaxWindowCommand = new CommandBase();

            //关闭窗口命令
            this.CloseWindowCommand.ExecuteAction = new Action<object>(GlobalFunc.CloseWindow);
            //最小化窗口命令
            this.MinWindowCommand.ExecuteAction = new Action<object>(GlobalFunc.MinWindow);
            //最大化窗口命令
            this.MaxWindowCommand.ExecuteAction = new Action<object>(GlobalFunc.MaxWindow);
            #endregion 窗口命令

            #region 功能命令
            //创建命令实例
            this.CheckAllCommand = new CommandBase();
            this.DeleteCartCommand = new CommandBase();
            this.SampleRequestCommand = new CommandBase();

            //表格全选or全不选命令
            this.CheckAllCommand.ExecuteAction = new Action<object>(CheckAll);
            //删除购物车内选中样品命令
            this.DeleteCartCommand.ExecuteAction = new Action<object>(DeleteCart);
            //申请购物车中样品命令
            this.SampleRequestCommand.ExecuteAction = new Action<object>(SampleRequest);
            #endregion 功能命令
        }

        #region 功能命令实现
        /// <summary>
        /// 刷新DataGrid
        /// </summary>
        /// <param name="dg"></param>
        private void RefreshDataGrid(DataGrid dg)
        {
            dg.ItemsSource = null;
            SampleModelList = dBControl.SearchSample(dBControl.SearchInCart(GlobalValue.CurrentUser.UserID));
            dg.ItemsSource = SampleModelList;
        }
        /// <summary>
        /// 获取DataGrid中所有被选中的行的ID值
        /// </summary>
        /// <param name="dg"></param>
        /// <returns></returns>
        private List<int> GetSelectedSamples(DataGrid dg)
        {
            List<int> iList = new List<int>();
            ObservableCollection<SampleModel> sList = (ObservableCollection<SampleModel>)dg.ItemsSource;
            if (sList != null)
            {
                foreach (SampleModel s in sList)
                {
                    if (s.IsSelected)
                    {
                        iList.Add(int.Parse(s.SampleID));
                    }
                }
            }
            return iList;
        }
        /// <summary>
        /// 表格表头全选或全不选
        /// </summary>
        /// <param name="w">isChecked</param>
        private void CheckAll(object w)
        {
            foreach (SampleModel sample in SampleModelList)
            {
                sample.IsSelected = (bool)w;
            }
        }
        /// <summary>
        /// 删除购物车内选中样品
        /// </summary>
        /// <param name="w">CartView</param>
        private void DeleteCart(object w)
        {
            List<int> iList = GetSelectedSamples((w as CartView).CartDataGrid);
            if (iList.Count > 0)
            {
                if (dBControl.DeleteCartTable(iList, GlobalValue.CurrentUser.UserID) > 0)
                {
                    MessageBox.Show((w as Window), "成功移除", "提示");
                    RefreshDataGrid((w as CartView).CartDataGrid);
                }
            }
        }
        /// <summary>
        /// 申请借出购物车中样品
        /// </summary>
        /// <param name="w">CartView</param>
        private void SampleRequest(object w)
        {
            SampleRequestView SampleRequestWindow = new SampleRequestView();
            if (!GlobalFunc.AdminRight())
            {
                SampleRequestWindow.ToolAdminApproval.Visibility = Visibility.Hidden;
            }
            (w as Window).Close();
            SampleRequestWindow.ShowDialog();
        }
        #endregion 功能命令实现
    }
}
