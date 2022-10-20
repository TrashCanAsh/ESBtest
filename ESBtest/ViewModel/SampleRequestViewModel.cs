using ESBtest.Common;
using ESBtest.Model;
using ESBtest.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESBtest.ViewModel
{
    public class SampleRequestViewModel : NotifyBase
    {
        private DBControl dBControl;

        public SampleRecord SampleRecord { get; set; }

        public ObservableCollection<SampleModel> SampleModelList { get; set; }

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase MinWindowCommand { get; set; }
        public CommandBase MaxWindowCommand { get; set; }

        public CommandBase UploadRequestCommand { get; set; }



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
            //创建表格数据源实例
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
            //
            this.UploadRequestCommand = new CommandBase();

            //
            this.UploadRequestCommand.ExecuteAction = new Action<object>(UploadRequest);
            #endregion
        }

        #region 功能命令实现
        private void UploadRequest(object w)
        {
            List<int> iList = dBControl.SearchInCart(GlobalValue.CurrentUser.UserID);
            string requestDate = SampleRecord.RequestDate.ToShortDateString().ToString();

        }
        #endregion

    }
}
