using ESBtest.Common;
using ESBtest.Model;
using ESBtest.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ESBtest.ViewModel
{
    public class SampleRecordDetailViewModel
    {
        private DBControl dBControl;

        public ObservableCollection<SampleModel> SampleModelList { get; set; }
        public SampleRecordModel SampleRecordModel { get; set; }

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase MinWindowCommand { get; set; }
        public CommandBase MaxWindowCommand { get; set; }

        public CommandBase RequsetApprovalCommand { get; set; }
        public CommandBase RequestRejectCommand { get; set; }

        public CommandBase CancelRequestCommand { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SampleRecordDetailViewModel()
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
            this.RequsetApprovalCommand = new CommandBase();
            this.RequestRejectCommand = new CommandBase();
            this.CancelRequestCommand = new CommandBase();

            //确认申请命令
            this.RequsetApprovalCommand.ExecuteAction = new Action<object>(RequsetApproval);
            //拒绝申请命令
            this.RequestRejectCommand.ExecuteAction = new Action<object>(RequestReject);
            //
            this.CancelRequestCommand.ExecuteAction = new Action<object>(CancelRequest);

            #endregion 功能命令
        }

        #region 功能命令实现

        private void RequsetApproval(object w)
        {
            if(MessageBox.Show("确认同意此申请？","审批提示",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if(dBControl.UpdateRecordTable(SampleRecordModel.IdRecord, SampleRecordModel.IdUser, DateTime.Now.ToShortDateString(), null, null, 2) > 0)
                {
                    MessageBox.Show("已同意此申请\n即将关闭此界面...", "审批提示");
                    (w as Window).Close();
                }
                else
                {
                    MessageBox.Show("操作失败：未知错误", "审批提示");
                }
            }
        }

        private void RequestReject(object w)
        {
            if(MessageBox.Show("确认拒绝此申请？", "审批提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (dBControl.UpdateRecordTable(SampleRecordModel.IdRecord, SampleRecordModel.IdUser, DateTime.Now.ToShortDateString(), null, null, 9) > 0)
                {
                    MessageBox.Show("已拒绝此申请\n即将关闭此界面...", "审批提示");
                    (w as Window).Close();
                }
                else
                {
                    MessageBox.Show("操作失败：未知错误", "审批提示");
                }
            }
        }

        /// <summary>
        /// 取消选中的样品申请
        /// </summary>
        /// <param name="w">SampleRecordDetailView</param>
        private void CancelRequest(object w)
        {
            if (MessageBox.Show("确认取消此条申请记录?", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (dBControl.UpdateRecordTable(SampleRecordModel.IdRecord, SampleRecordModel.IdUser, null, null, null, 10) > 0)
                {
                    MessageBox.Show("取消成功\n正在返回上一界面...", "提示");
                    (w as Window).Close();
                }
                else
                {
                    MessageBox.Show("取消失败：未知错误", "提示");
                }
            }
        }

        #endregion 功能命令实现
    }
}
