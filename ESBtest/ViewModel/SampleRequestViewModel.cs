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
    public class SampleRequestViewModel : NotifyBase
    {
        private DBControl dBControl;

        public SampleRecordModel SampleRecord { get; set; }

        public ObservableCollection<SampleModel> SampleModelList { get; set; }
        public ObservableCollection<SampleRecordModel> SampleRecordModelList { get; set; }

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase MinWindowCommand { get; set; }
        public CommandBase MaxWindowCommand { get; set; }

        public CommandBase ToolSampleRequestTab { get; set; }
        public CommandBase ToolProgressTab { get; set; }
        public CommandBase ToolHistoryTab { get; set; }
        public CommandBase ToolApprovalTab { get; set; }

        public CommandBase UploadRequestCommand { get; set; }

        public CommandBase CheckDetailsCommand { get; set; }
        public CommandBase CancelRequestCommand { get; set; }
        public CommandBase AdminCheckDetailsCommand { get; set; }



        /// <summary>
        /// 构造函数
        /// </summary>
        public SampleRequestViewModel()
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
            //创建样品申请记录实例
            this.SampleRecord = new SampleRecordModel();
            //创建表格数据源实例
            this.SampleModelList = dBControl.SearchSample(dBControl.SearchInCart(GlobalValue.CurrentUser.UserID));
            //创建样品申请记录列表实例
            this.SampleRecordModelList = null;
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
            this.ToolSampleRequestTab = new CommandBase();
            this.ToolProgressTab = new CommandBase();
            this.ToolHistoryTab = new CommandBase();
            this.ToolApprovalTab = new CommandBase();
            this.UploadRequestCommand = new CommandBase();
            this.CheckDetailsCommand = new CommandBase();
            this.CancelRequestCommand = new CommandBase();
            this.AdminCheckDetailsCommand = new CommandBase();

            //转到样品申请界面命令
            this.ToolSampleRequestTab.ExecuteAction = new Action<object>(SampleRequsetTab);
            //转到进度查询界面命令
            this.ToolProgressTab.ExecuteAction = new Action<object>(ProgressTab);
            //转到历史记录界面命令
            this.ToolHistoryTab.ExecuteAction = new Action<object>(HistoryTab);
            //转到审批界面命令
            this.ToolApprovalTab.ExecuteAction = new Action<object>(ApprovalTab);
            //提交申请命令
            this.UploadRequestCommand.ExecuteAction = new Action<object>(UploadRequest);
            //查看申请细节命令
            this.CheckDetailsCommand.ExecuteAction = new Action<object>(CheckDetails);
            //取消申请命令
            this.CancelRequestCommand.ExecuteAction = new Action<object>(CancelRequest);
            //管理员审批查看详情命令
            this.AdminCheckDetailsCommand.ExecuteAction = new Action<object>(AdminCheckDetails);
            #endregion
        }

        #region 功能命令实现
        /// <summary>
        /// 刷新样品申请记录
        /// </summary>
        /// <param name="dg"></param>
        private void RefreshSampleRecordDataGrid(DataGrid dg, int flag)
        {
            dg.ItemsSource = null;
            if(flag == 1)
            {
                this.SampleRecordModelList = dBControl.SearchRecord(GlobalValue.CurrentUser.UserID);
            }
            else if (flag == 2)
            {
                this.SampleRecordModelList = dBControl.SearchRecord();
            }
            dg.ItemsSource = this.SampleRecordModelList;
        }
        /// <summary>
        /// 切换至样品申请界面
        /// </summary>
        /// <param name="w">SampleRequestView</param>
        private void SampleRequsetTab(object w)
        {
            (w as SampleRequestView).TabControlFunction.SelectedIndex = 0;
        }
        /// <summary>
        /// 切换至进度查询界面
        /// </summary>
        /// <param name="w">SampleRequestView</param>
        private void ProgressTab(object w)
        {
            (w as SampleRequestView).TabControlFunction.SelectedIndex = 1;
            RefreshSampleRecordDataGrid((w as SampleRequestView).ProgressDataGrid, 1);
        }
        /// <summary>
        /// 切换至历史记录界面
        /// </summary>
        /// <param name="w">SampleRequestView</param>
        private void HistoryTab(object w)
        {
            (w as SampleRequestView).TabControlFunction.SelectedIndex = 2;
        }
        /// <summary>
        /// 切换到审批界面
        /// </summary>
        /// <param name="w"></param>
        private void ApprovalTab(object w)
        {
            (w as SampleRequestView).TabControlFunction.SelectedIndex = 3;
            RefreshSampleRecordDataGrid((w as SampleRequestView).ApprovalDataGrid, 2);
        }
        /// <summary>
        /// 提交样品申请
        /// </summary>
        /// <param name="w">SampleRequestView</param>
        private void UploadRequest(object w)
        {
            List<int> iList = dBControl.SearchInCart(GlobalValue.CurrentUser.UserID);
            if (iList.Count <= 0)
            {
                MessageBox.Show((w as Window), "当前购物车中没有样品", "提示");
                return;
            }
            string requestDate = SampleRecord.RequestDate.ToShortDateString().ToString();
            if (dBControl.InsertIntoSampleRecordTable(iList, requestDate, GlobalValue.CurrentUser.UserID, 1) > 0)
            {
                if (dBControl.DeleteCartTable(GlobalValue.CurrentUser.UserID) > 0)
                {
                    MessageBox.Show((w as Window), "申请提交成功", "提示");
                }
                else
                {
                    MessageBox.Show((w as Window), "清空购物车失败", "提示");
                }
            }
            else
            {
                MessageBox.Show((w as Window), "申请提交失败", "提示");
            }
        }
        /// <summary>
        /// 查看选中的样品申请的详情
        /// </summary>
        /// <param name="w"></param>
        private void CheckDetails(object w)
        {
            SampleRecordDetailView srdv = new SampleRecordDetailView();
            srdv.ControllerGrid.Visibility = Visibility.Collapsed;
            List<int> iList = dBControl.SearchSampleRecord((w as SampleRecordModel).IdRecord, GlobalValue.CurrentUser.UserID);
            (srdv.DataContext as SampleRecordDetailViewModel).SampleModelList = dBControl.SearchSample(iList);
            (srdv.DataContext as SampleRecordDetailViewModel).SampleRecordModel = dBControl.SearchRecord((w as SampleRecordModel).IdRecord, GlobalValue.CurrentUser.UserID);
            srdv.ShowDialog();
        }
        /// <summary>
        /// 取消选中的样品申请
        /// </summary>
        /// <param name="w"></param>
        private void CancelRequest(object w)
        {
            SampleRecordModel sr = (SampleRecordModel)(w as DataGrid).SelectedItem;
            if(MessageBox.Show("确认取消此条申请记录?","提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if(dBControl.UpdateRecordTable(sr.IdRecord, GlobalValue.CurrentUser.UserID, DateTime.Now, DateTime.Now, DateTime.Now, 10) > 0)
                {
                    MessageBox.Show("取消成功", "提示");
                    RefreshSampleRecordDataGrid((w as SampleRequestView).ProgressDataGrid, 1);
                }
            }
        }
        /// <summary>
        /// 管理员审批界面查看选中的样品申请的详情，并进行审批
        /// </summary>
        /// <param name="w"></param>
        private void AdminCheckDetails(object w)
        {
            SampleRecordDetailView srdv = new SampleRecordDetailView();
            List<int> iList = dBControl.SearchSampleRecord((w as SampleRecordModel).IdRecord, GlobalValue.CurrentUser.UserID);
            (srdv.DataContext as SampleRecordDetailViewModel).SampleModelList = dBControl.SearchSample(iList);
            (srdv.DataContext as SampleRecordDetailViewModel).SampleRecordModel = dBControl.SearchRecord((w as SampleRecordModel).IdRecord, GlobalValue.CurrentUser.UserID);
            srdv.ShowDialog();
        }
        #endregion

    }
}
