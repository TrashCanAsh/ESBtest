using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ESBtest.Common;
using ESBtest.Model;
using ESBtest.View;
using ESBtest.ViewModel.Base;
using Microsoft.Win32;

namespace ESBtest.ViewModel
{
    /// <summary>
    /// 主界面的逻辑实现
    /// </summary>
    public class MainViewModel : NotifyBase
    {
        private DBControl dBControl;

        public ObservableCollection<string> ComboBoxCategory { get; set; }

        public UserModel UserModel { get; set; }
        public SampleModel SampleModel { get; set; }
        public SearchModel SearchModel { get; set; }

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase MinWindowCommand { get; set; }
        public CommandBase MaxWindowCommand { get; set; }
        public CommandBase SearchCommand { get; set; }
        public CommandBase SearchResetCommand { get; set; }
        public CommandBase SearchClearCommand { get; set; }
        public CommandBase InsertSampleInfoCommand { get; set; }
        public CommandBase OpenInsertFileDialogCommand { get; set; }
        public CommandBase InsertFileDataCommand { get; set; }
        public CommandBase DataGridDoubleClickCommand { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MainViewModel()
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
            //创建用户数据实例
            this.UserModel = new UserModel();
            //创建样品信息实例
            this.SampleModel = new SampleModel();
            //创建搜索条件实例
            this.SearchModel = new SearchModel();
            //下拉框内容
            ComboBoxCategory = new ObservableCollection<string>() { "null", "solid", "liquid", "gas", "bio" };
        }
        /// <summary>
        /// 命令合集
        /// </summary>
        private void SetCommand()
        {
            //创建命令实例
            this.CloseWindowCommand = new CommandBase();
            this.MinWindowCommand = new CommandBase();
            this.MaxWindowCommand = new CommandBase();
            this.SearchCommand = new CommandBase();
            this.SearchResetCommand = new CommandBase();
            this.SearchClearCommand = new CommandBase();
            this.InsertSampleInfoCommand = new CommandBase();
            this.OpenInsertFileDialogCommand = new CommandBase();
            this.InsertFileDataCommand = new CommandBase();
            this.DataGridDoubleClickCommand = new CommandBase();

            //关闭窗口命令
            this.CloseWindowCommand.ExecuteAction = new Action<object>(GlobalFunc.CloseWindow);
            //最小化窗口命令
            this.MinWindowCommand.ExecuteAction = new Action<object>(GlobalFunc.MinWindow);
            //最大化窗口命令
            this.MaxWindowCommand.ExecuteAction = new Action<object>(GlobalFunc.MaxWindow);
            //搜索样品信息命令
            this.SearchCommand.ExecuteAction = new Action<object>(SearchSample);
            //重置搜索条件命令
            this.SearchResetCommand.ExecuteAction = new Action<object>(SearchReset);
            //重置搜索结果命令
            this.SearchClearCommand.ExecuteAction = new Action<object>(SearchClear);
            //手动逐条添加样品命令
            this.InsertSampleInfoCommand.ExecuteAction = new Action<object>(InsertSampleInfo);
            //打开信息导入文件窗口命令
            this.OpenInsertFileDialogCommand.ExecuteAction = new Action<object>(InsertFileDialog);
            //批量导入样品命令
            this.InsertFileDataCommand.ExecuteAction = new Action<object>(InsertFileData);
            //DataGrid双击事件
            this.DataGridDoubleClickCommand.ExecuteAction = new Action<object>(DataGridDoubleClick);
        }

        private void RefreshDataGrid(DataGrid dg)
        {
            dg.ItemsSource = null;
            List<SampleModel> sList = dBControl.SearchSample();
            dg.ItemsSource = sList;
        }
        /// <summary>
        /// 根据搜索条件对样品数据进行搜索
        /// </summary>
        /// <param name="w"></param>
        private void SearchSample(object w)
        {
            //*字符串分割/模糊搜索*
            string keyword = SearchModel.KeyWord;
            //
            string category = null;
            if(SearchModel.IsCategoryChecked)
            {
                category = ComboBoxCategory[SearchModel.CategoryIndex];
            }
            else
            {
                category = null;
            }
            
            string start = SearchModel.StartDate > new DateTime(2022, 1, 1) && SearchModel.IsSamplingTimeChecked ? SearchModel.StartDate.ToShortDateString() : null;
            string end = SearchModel.EndDate > new DateTime(2022, 1, 1) && SearchModel.IsSamplingTimeChecked ? SearchModel.EndDate.ToShortDateString() : null;
            //待实现：范围选取设定
            Location nw = (SearchModel.NW.longitude >= 100 && SearchModel.NW.latitude >= 25) && SearchModel.IsSamplingLocationChecked ? SearchModel.NW : null;
            Location se = (SearchModel.SE.longitude >= 100 && SearchModel.SE.latitude >= 25) && SearchModel.IsSamplingLocationChecked ? SearchModel.SE : null;

            //Console.WriteLine("keyword:" + keyword + "//categoryIndex:" + categoryIndex + "//startDate:" + start + "//endDate:" + end +
            //    "//NW:" + searchModel.NW.longitude + ", " + searchModel.NW.latitude + "//SE:" + searchModel.SE.longitude + ", " + searchModel.SE.latitude);

            List<SampleModel> sList = dBControl.SearchSample(keyword, category, start, end, nw, se);
            (w as DataGrid).ItemsSource = sList;
        }
        /// <summary>
        /// 重新设置搜索条件
        /// </summary>
        /// <param name="w"></param>
        private void SearchReset(object w)
        {
            (w as MainView).TextBoxKeyWord.Text = "";
            (w as MainView).ComboBoxCategory.SelectedIndex = 0;
            (w as MainView).DatePikerStartDate.SelectedDate = DateTime.Now;
            (w as MainView).DatePikerEndDate.SelectedDate = DateTime.Now;
            (w as MainView).TextBoxNW_x.Text = "0.00";
            (w as MainView).TextBoxNW_y.Text = "0.00";
            (w as MainView).TextBoxSE_x.Text = "0.00";
            (w as MainView).TextBoxSE_y.Text = "0.00";
            (w as MainView).CheckBoxCategory.IsChecked = false;
            (w as MainView).CheckBoxSamplingTime.IsChecked = false;
            (w as MainView).CheckBoxSamplingLocation.IsChecked = false;
        }
        /// <summary>
        /// 清除搜索结果
        /// </summary>
        /// <param name="w"></param>
        private void SearchClear(object w)
        {
            (w as DataGrid).ItemsSource = null;
        }
        /// <summary>
        /// 逐条插入样品数据
        /// </summary>
        /// <param name="w"></param>
        private void InsertSampleInfo(object w)
        {
            if(string.IsNullOrEmpty(SampleModel.SampleName))
            {
                MessageBox.Show((w as Window), "样品名称不能为空", "提示");
            }
            else if (SampleModel.CategoryIndex <= 0)
            {
                MessageBox.Show((w as Window), "样品种类不能为空", "提示");
            }
            else if(SampleModel.SamplingDateTime == null)
            {
                MessageBox.Show((w as Window), "样品采样时间不能为空", "提示");
            }
            else if(string.IsNullOrEmpty(SampleModel.Longitude)|| string.IsNullOrEmpty(SampleModel.Latitude))
            {
                MessageBox.Show((w as Window), "样品采样地点不能为空", "提示");
            }
            else
            {
                string name = SampleModel.SampleName;
                string category = ComboBoxCategory[SampleModel.CategoryIndex];
                string time = SampleModel.SamplingDateTime.ToShortDateString();
                string longitude = SampleModel.Longitude;
                string latitude = SampleModel.Latitude;
                if (dBControl.InsertIntoSampleTable(name, category, time, longitude, latitude) > 0)
                {
                    MessageBox.Show((w as Window), "导入成功", "提示");
                    //刷新表中内容
                    RefreshDataGrid((w as MainView).SampleDataGrid);
                }
                else
                {
                    MessageBox.Show((w as Window), "导入失败：未知错误", "提示");
                }
            }
        }
        /// <summary>
        /// 选择样品数据文件
        /// </summary>
        /// <param name="w"></param>
        private void InsertFileDialog(object w)
        {
            OpenFileDialog ofp = new OpenFileDialog() { Title = "打开样品数据导入文件", Filter = "Txt files(*.txt)|*.txt|Csv files(*.csv)|*.csv|Excel files(*.xlsx, *.xls)|*.xlsx;*.xls|All files(*.*)|*.*" };
            if (ofp.ShowDialog() == true)
            {
                (w as MainView).TextBoxFilePath.Text = ofp.FileName;
            }
        }
        /// <summary>
        /// 批量导入样品数据
        /// </summary>
        /// <param name="w"></param>
        private void InsertFileData(object w)
        {
            string filepath = (w as MainView).TextBoxFilePath.Text;
            if (dBControl.InsertIntoSampleTable(FileControl.ReadFile(filepath)) > 0)
            {
                MessageBox.Show((w as Window), "批量导入成功", "提示");
                //刷新表中内容
                RefreshDataGrid((w as MainView).SampleDataGrid);
            }
            else
            {
                MessageBox.Show((w as Window), "批量导入失败", "提示");
            }
        }
        /// <summary>
        /// DataGrid双击事件->打开样品数据修改界面
        /// </summary>
        /// <param name="w"></param>
        private void DataGridDoubleClick(object w)
        {
            SampleModel sample = (SampleModel)(w as DataGrid).SelectedItem;
            UpdateView updateView = new UpdateView();
            (updateView.DataContext as UpdateViewModel).sampleUpdated = sample;
            if (updateView.ShowDialog().Value)
            {
                //刷新表中内容
                RefreshDataGrid(w as DataGrid);
            }

        }
    }
}
