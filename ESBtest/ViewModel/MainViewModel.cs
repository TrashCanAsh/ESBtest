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

        public SampleModel SampleModel { get; set; }
        public SearchModel SearchModel { get; set; }

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase MinWindowCommand { get; set; }
        public CommandBase MaxWindowCommand { get; set; }

        public CommandBase MenuSearchSampleCommand { get; set; }
        public CommandBase MenuInsertDataCommand { get; set; }
        public CommandBase MenuOutputDataCommand { get; set; }

        public CommandBase SearchCommand { get; set; }
        public CommandBase SearchResetCommand { get; set; }
        public CommandBase SearchClearCommand { get; set; }
        public CommandBase InsertSampleInfoCommand { get; set; }
        public CommandBase OpenInsertFileDialogCommand { get; set; }
        public CommandBase InsertFileDataCommand { get; set; }
        public CommandBase DataGridDoubleClickCommand { get; set; }
        public CommandBase DeleteSelectedSampleCommand { get; set; }
        public CommandBase OpenOutputFileDialogCommand { get; set; }
        public CommandBase OutputFileDataCommand { get; set; }

        public CommandBase FavoritesCommand { get; set; }
        public CommandBase CartCommand { get; set; }

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
            //创建样品信息实例
            this.SampleModel = new SampleModel();
            //创建搜索条件实例
            this.SearchModel = new SearchModel();
            //下拉框内容
            this.ComboBoxCategory = new ObservableCollection<string>() { "null", "solid", "liquid", "gas", "bio" };
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

            #region 菜单栏命令
            //创建命令实例
            this.MenuSearchSampleCommand = new CommandBase();
            this.MenuInsertDataCommand = new CommandBase();
            this.MenuOutputDataCommand = new CommandBase();

            //菜单栏选择样品信息搜索命令
            this.MenuSearchSampleCommand.ExecuteAction = new Action<object>(MenuSearchSample);
            //菜单栏选择样品信息导入命令
            this.MenuInsertDataCommand.ExecuteAction = new Action<object>(MenuInsertData);
            //菜单栏选择样品信息导出命令
            this.MenuOutputDataCommand.ExecuteAction = new Action<object>(MenuOutputData);
            #endregion 菜单栏命令

            #region 功能命令
            //创建命令实例
            this.SearchCommand = new CommandBase();
            this.SearchResetCommand = new CommandBase();
            this.SearchClearCommand = new CommandBase();
            this.InsertSampleInfoCommand = new CommandBase();
            this.OpenInsertFileDialogCommand = new CommandBase();
            this.InsertFileDataCommand = new CommandBase();
            this.DataGridDoubleClickCommand = new CommandBase();
            this.DeleteSelectedSampleCommand = new CommandBase();
            this.OpenOutputFileDialogCommand = new CommandBase();
            this.OutputFileDataCommand = new CommandBase();
            this.FavoritesCommand = new CommandBase();
            this.CartCommand = new CommandBase();


            //搜索样品信息命令
            this.SearchCommand.ExecuteAction = new Action<object>(SearchSample);
            //重置搜索条件命令
            this.SearchResetCommand.ExecuteAction = new Action<object>(SearchReset);
            //重置搜索结果命令
            this.SearchClearCommand.ExecuteAction = new Action<object>(SearchClear);
            //手动逐条添加样品命令
            this.InsertSampleInfoCommand.ExecuteAction = new Action<object>(InsertSampleInfo);
            //打开选择导入数据文件路径窗口命令
            this.OpenInsertFileDialogCommand.ExecuteAction = new Action<object>(InsertFileDialog);
            //批量导入样品命令
            this.InsertFileDataCommand.ExecuteAction = new Action<object>(InsertFileData);
            //DataGrid双击事件
            this.DataGridDoubleClickCommand.ExecuteAction = new Action<object>(DataGridDoubleClick);
            //删除选中样品命令
            this.DeleteSelectedSampleCommand.ExecuteAction = new Action<object>(DeleteSample);
            //打开选择导出数据文件路径窗口命令
            this.OpenOutputFileDialogCommand.ExecuteAction = new Action<object>(OutputFileDialog);
            //导出被选中的样品数据命令
            this.OutputFileDataCommand.ExecuteAction = new Action<object>(OutputSample);
            //
            this.FavoritesCommand.ExecuteAction = new Action<object>(Favorites);
            //
            this.CartCommand.ExecuteAction = new Action<object>(Cart);
            #endregion 功能命令

        }

        #region 菜单栏命令实现
        /// <summary>
        /// 将标签页设置到第一页（样品数据搜索）
        /// </summary>
        /// <param name="w">TabControl</param>
        private void MenuSearchSample(object w)
        {
            (w as MainView).TabControlFunction.SelectedIndex = 0;
            (w as MainView).MenuItemSearch.IsChecked = true;
            (w as MainView).MenuItemInsert.IsChecked = false;
            (w as MainView).MenuItemOutput.IsChecked = false;
        }
        /// <summary>
        /// 将标签页设置到第二页（数据导入）
        /// </summary>
        /// <param name="w">TabControl</param>
        private void MenuInsertData(object w)
        {
            (w as MainView).TabControlFunction.SelectedIndex = 1;
            (w as MainView).MenuItemSearch.IsChecked = false;
            (w as MainView).MenuItemInsert.IsChecked = true;
            (w as MainView).MenuItemOutput.IsChecked = false;
        }
        /// <summary>
        /// 将标签页设置到第三页（数据导出）
        /// </summary>
        /// <param name="w">TabControl</param>
        private void MenuOutputData(object w)
        {
            (w as MainView).TabControlFunction.SelectedIndex = 2;
            (w as MainView).MenuItemSearch.IsChecked = false;
            (w as MainView).MenuItemInsert.IsChecked = false;
            (w as MainView).MenuItemOutput.IsChecked = true;
        }
        #endregion 菜单栏命令实现

        #region 功能命令实现
        /// <summary>
        /// 刷新DataGrid
        /// </summary>
        /// <param name="dg"></param>
        private void RefreshDataGrid(DataGrid dg)
        {
            dg.ItemsSource = null;
            dg.ItemsSource = dBControl.SearchSample();
        }
        /// <summary>
        /// 根据搜索条件对样品数据进行搜索
        /// </summary>
        /// <param name="w">DataGrid</param>
        private void SearchSample(object w)
        {
            //*字符串分割/模糊搜索*
            string keyword = SearchModel.KeyWord;
            //
            string category = null;
            if (SearchModel.IsCategoryChecked)
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
            (w as DataGrid).ItemsSource = dBControl.SearchSample(keyword, category, start, end, nw, se);
        }
        /// <summary>
        /// 重新设置搜索条件
        /// </summary>
        /// <param name="w">MainView</param>
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
        /// <param name="w">DataGrid</param>
        private void SearchClear(object w)
        {
            (w as DataGrid).ItemsSource = null;
        }
        /// <summary>
        /// 逐条插入样品数据
        /// </summary>
        /// <param name="w">MainView</param>
        private void InsertSampleInfo(object w)
        {
            if (string.IsNullOrEmpty(SampleModel.SampleName))
            {
                MessageBox.Show((w as Window), "样品名称不能为空", "提示");
            }
            else if (SampleModel.CategoryIndex <= 0)
            {
                MessageBox.Show((w as Window), "样品种类不能为空", "提示");
            }
            else if (SampleModel.SamplingDateTime == null)
            {
                MessageBox.Show((w as Window), "样品采样时间不能为空", "提示");
            }
            else if (string.IsNullOrEmpty(SampleModel.Longitude) || string.IsNullOrEmpty(SampleModel.Latitude))
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
        /// 选择批量导入样品数据文件路径
        /// </summary>
        /// <param name="w">MainView</param>
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
        /// <param name="w">MainView</param>
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
        /// <param name="w">DataGrid</param>
        private void DataGridDoubleClick(object w)
        {
            SampleModel sample = (SampleModel)(w as DataGrid).SelectedItem;
            if (sample != null)
            {
                UpdateView updateView = new UpdateView();
                (updateView.DataContext as UpdateViewModel).sampleUpdated = sample;
                if (updateView.ShowDialog().Value)
                {
                    //刷新表中内容
                    RefreshDataGrid((w as DataGrid));
                }
            }
        }
        /// <summary>
        /// 获取DataGrid中所有被选中的行的ID值
        /// </summary>
        /// <param name="dg"></param>
        /// <returns></returns>
        private List<int> GetSelectedSamples(DataGrid dg)
        {
            List<int> iList = new List<int>();
            List<SampleModel> sList = (List<SampleModel>)dg.ItemsSource;
            if(sList != null)
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
        /// 删除选中的样品信息
        /// </summary>
        /// <param name="w">MainWindow</param>
        private void DeleteSample(object w)
        {
            List<int> iList = GetSelectedSamples((w as MainView).SampleDataGrid);
            if (iList.Count > 0)
            {
                string str = "";
                foreach (int i in iList)
                {
                    str += i + ";";
                }
                if (MessageBox.Show((w as Window), "正在删除：" + str, "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (dBControl.DeleteSampleTable(iList) > 0)
                    {
                        MessageBox.Show((w as Window), "成功删除：" + str, "提示");
                    }
                    RefreshDataGrid((w as MainView).SampleDataGrid);
                }
            }
            else
            {
                MessageBox.Show((w as Window), "请先选择样品！", "提示");
            }
        }
        /// <summary>
        /// 选择批量导出样品数据文件路径
        /// </summary>
        /// <param name="w">MainView</param>
        private void OutputFileDialog(object w)
        {
            SaveFileDialog sfp = new SaveFileDialog() { Title = "选择保存路径", Filter = "Txt files(*.txt)|*.txt|Csv files(*.csv)|*.csv|Excel files(*.xlsx, *.xls)|*.xlsx;*.xls|All files(*.*)|*.*" };
            if (sfp.ShowDialog() == true)
            {
                (w as MainView).TextBoxOutputFilePath.Text = sfp.FileName;
            }
        }
        /// <summary>
        /// 批量导出样品数据
        /// </summary>
        /// <param name="w">MainView</param>
        private void OutputSample(object w)
        {
            List<int> iList = GetSelectedSamples((w as MainView).SampleDataGrid);
            if (iList.Count > 0)
            {
                if(FileControl.WriteFile((w as MainView).TextBoxOutputFilePath.Text, dBControl.SearchSample(iList)))
                {
                    MessageBox.Show((w as Window), "导出成功", "提示");
                }
                else
                {
                    MessageBox.Show((w as Window), "导出失败", "提示");
                }
            }
            else
            {
                MessageBox.Show((w as Window), "请先选择样品！", "提示");
            }
        }
        //
        private void Favorites(object w)
        {
            if((w as SampleModel).IsFavorited)
            {
                dBControl.InsertIntoFavoriteTable(GlobalValue.CurrentUser.UserID, (w as SampleModel).SampleID);
            }
            else
            {
                dBControl.DeleteFavoriteTable(GlobalValue.CurrentUser.UserID, (w as SampleModel).SampleID);
            }
        }
        //
        private void Cart(object w)
        {
            if ((w as SampleModel).IsInCart)
            {
                dBControl.InsertIntoCartTable(GlobalValue.CurrentUser.UserID, (w as SampleModel).SampleID);
            }
            else
            {
                dBControl.DeleteCartTable(GlobalValue.CurrentUser.UserID, (w as SampleModel).SampleID);
            }
        }
        #endregion 功能命令实现
    }
}
