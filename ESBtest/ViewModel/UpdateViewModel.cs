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
    public class UpdateViewModel: NotifyBase
    {
        private DBControl dBControl;

        public ObservableCollection<string> comboBoxCategory { get; set; }

        public SampleModel sampleUpdated { get; set; }

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase MinWindowCommand { get; set; }
        public CommandBase UpdateSampleCommand { get; set; }
        public CommandBase CancelCommand { get; set; }


        public UpdateViewModel()
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
            //创建样品信息实例
            this.sampleUpdated = new SampleModel();
            //下拉框内容
            comboBoxCategory = new ObservableCollection<string>() { "null", "solid", "liquid", "gas", "bio" };
        }
        /// <summary>
        /// 命令合集
        /// </summary>
        private void SetCommand()
        {
            //创建命令实例
            this.CloseWindowCommand = new CommandBase();
            this.MinWindowCommand = new CommandBase();
            this.UpdateSampleCommand = new CommandBase();
            this.CancelCommand = new CommandBase();

            //关闭窗口命令
            this.CloseWindowCommand.ExecuteAction = new Action<object>(GlobalFunc.CloseWindow);
            //最小化窗口命令
            this.MinWindowCommand.ExecuteAction = new Action<object>(GlobalFunc.MinWindow);
            //确认修改信息命令
            this.UpdateSampleCommand.ExecuteAction = new Action<object>(UpdateSample);
            //取消修改命令
            this.CancelCommand.ExecuteAction = new Action<object>(GlobalFunc.CloseWindow);
        }
        /// <summary>
        /// 对样品信息进行修改
        /// </summary>
        /// <param name="w"></param>
        private void UpdateSample(object w)
        {
            //修改数据库内容
            if (dBControl.UpdateSampleTable(sampleUpdated.SampleID, sampleUpdated.SampleName, comboBoxCategory[sampleUpdated.CategoryIndex] , sampleUpdated.SamplingDateTime.ToShortDateString(), sampleUpdated.Longitude, sampleUpdated.Latitude) > 0)
            {
                MessageBox.Show((w  as Window), "修改成功", "提示");
                (w as Window).DialogResult = true;
            }
            else
            {
                MessageBox.Show((w as Window), "修改失败", "提示");
            }
            (w as Window).Close();
        }
    }
}
