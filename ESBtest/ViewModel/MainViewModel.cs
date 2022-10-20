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
        public ObservableCollection<string> ComboBoxState { get; set; }
        public ObservableCollection<SampleModel> SampleModelList { get; set; }

        public SampleModel SampleModel { get; set; }
        public SearchModel SearchModel { get; set; }

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase MinWindowCommand { get; set; }
        public CommandBase MaxWindowCommand { get; set; }

        public CommandBase MenuSearchSampleCommand { get; set; }
        public CommandBase MenuInsertDataCommand { get; set; }
        public CommandBase MenuOutputDataCommand { get; set; }
        public CommandBase MenuFavoriteCommand { get; set; }
        public CommandBase MenuCartCommand { get; set; }
        public CommandBase MenuSampleLendCommand { get; set; }
        public CommandBase MenuLogOutCommand { get; set; }

        public CommandBase SearchCommand { get; set; }
        public CommandBase SearchResetCommand { get; set; }
        public CommandBase SearchClearCommand { get; set; }
        public CommandBase InsertSampleInfoCommand { get; set; }
        public CommandBase OpenInsertFileDialogCommand { get; set; }
        public CommandBase InsertFileDataCommand { get; set; }
        public CommandBase DataGridDoubleClickCommand { get; set; }
        public CommandBase AddFavoriteCommand { get; set; }
        public CommandBase AddIntoCartCommand { get; set; }
        public CommandBase DeleteSelectedSampleCommand { get; set; }
        public CommandBase OpenOutputFileDialogCommand { get; set; }
        public CommandBase OutputFileDataCommand { get; set; }
        public CommandBase UserInfoCommand { get; set; }

        public CommandBase CheckAllCommand { get; set; }
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
            this.ComboBoxState = new ObservableCollection<string>() { "unknown", "in stock", "locked", "out on loan" };
            //创建表格数据源实例
            this.SampleModelList = new ObservableCollection<SampleModel>() ;
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
            this.MenuFavoriteCommand = new CommandBase();
            this.MenuCartCommand = new CommandBase();
            this.MenuSampleLendCommand = new CommandBase();
            this.MenuLogOutCommand = new CommandBase();

            //菜单栏选择样品信息搜索命令 权限：游客、普通用户、管理员
            this.MenuSearchSampleCommand.ExecuteAction = new Action<object>(MenuSearchSample);
            //菜单栏选择样品信息导入命令 权限：管理员
            this.MenuInsertDataCommand.ExecuteAction = new Action<object>(MenuInsertData);
            this.MenuInsertDataCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.AdminRight);
            //菜单栏选择样品信息导出命令 权限：普通用户、管理员
            this.MenuOutputDataCommand.ExecuteAction = new Action<object>(MenuOutputData);
            this.MenuOutputDataCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.NormalUserRight);
            //菜单栏打开当前用户的收藏夹 权限：普通用户、管理员
            this.MenuFavoriteCommand.ExecuteAction = new Action<object>(MenuFavorite);
            this.MenuFavoriteCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.NormalUserRight);
            //菜单栏打开当前用户的购物车 权限：普通用户、管理员
            this.MenuCartCommand.ExecuteAction = new Action<object>(MenuCart);
            this.MenuCartCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.NormalUserRight);
            //菜单栏打开样品借出申请界面 权限：普通用户、管理员
            this.MenuSampleLendCommand.ExecuteAction = new Action<object>(MenuSampleLend);
            this.MenuSampleLendCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.NormalUserRight);
            //菜单栏登出当前用户并返回登录界面 权限：游客、普通用户、管理员
            this.MenuLogOutCommand.ExecuteAction = new Action<object>(MenuLogOut);
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
            this.AddFavoriteCommand = new CommandBase();
            this.AddIntoCartCommand = new CommandBase();
            this.DeleteSelectedSampleCommand = new CommandBase();
            this.OpenOutputFileDialogCommand = new CommandBase();
            this.OutputFileDataCommand = new CommandBase();
            this.CheckAllCommand = new CommandBase();
            this.FavoritesCommand = new CommandBase();
            this.CartCommand = new CommandBase();
            this.UserInfoCommand = new CommandBase();


            //搜索样品信息命令 权限：游客、普通用户、管理员
            this.SearchCommand.ExecuteAction = new Action<object>(SearchSample);
            //重置搜索条件命令 权限：游客、普通用户、管理员
            this.SearchResetCommand.ExecuteAction = new Action<object>(SearchReset);
            //重置搜索结果命令 权限：游客、普通用户、管理员
            this.SearchClearCommand.ExecuteAction = new Action<object>(SearchClear);
            //手动逐条添加样品命令 权限：管理员
            this.InsertSampleInfoCommand.ExecuteAction = new Action<object>(InsertSampleInfo);
            this.InsertSampleInfoCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.AdminRight);
            //打开选择导入数据文件路径窗口命令 权限：管理员
            this.OpenInsertFileDialogCommand.ExecuteAction = new Action<object>(InsertFileDialog);
            this.OpenInsertFileDialogCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.AdminRight);
            //批量导入样品命令 权限：管理员
            this.InsertFileDataCommand.ExecuteAction = new Action<object>(InsertFileData);
            this.InsertFileDataCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.AdminRight);
            //DataGrid双击事件 权限：游客、普通用户、管理员
            this.DataGridDoubleClickCommand.ExecuteAction = new Action<object>(DataGridDoubleClick);
            //批量添加收藏事件 权限：普通用户、管理员
            this.AddFavoriteCommand.ExecuteAction = new Action<object>(AddFavorite);
            this.AddFavoriteCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.NormalUserRight);
            //批量添加购物车事件 权限：普通用户、管理员
            this.AddIntoCartCommand.ExecuteAction = new Action<object>(AddIntoCart);
            this.AddIntoCartCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.NormalUserRight);
            //删除选中样品命令 权限：管理员
            this.DeleteSelectedSampleCommand.ExecuteAction = new Action<object>(DeleteSample);
            this.DeleteSelectedSampleCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.AdminRight);
            //打开选择导出数据文件路径窗口命令 权限：普通用户、管理员
            this.OpenOutputFileDialogCommand.ExecuteAction = new Action<object>(OutputFileDialog);
            this.OpenOutputFileDialogCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.NormalUserRight);
            //导出被选中的样品数据命令 权限：普通用户、管理员
            this.OutputFileDataCommand.ExecuteAction = new Action<object>(OutputSample);
            this.OutputFileDataCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.NormalUserRight);
            //表格全选or全不选命令 权限：普通用户、管理员
            this.CheckAllCommand.ExecuteAction = new Action<object>(CheckAll);
            this.CheckAllCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.NormalUserRight);
            //表格单击收藏样品命令 权限：普通用户、管理员
            this.FavoritesCommand.ExecuteAction = new Action<object>(Favorites);
            this.FavoritesCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.NormalUserRight);
            //表格单击加入购物车命令 权限：普通用户、管理员
            this.CartCommand.ExecuteAction = new Action<object>(Cart);
            this.CartCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.NormalUserRight);
            //获取当前用户信息 权限：游客、普通用户、管理员
            this.UserInfoCommand.ExecuteAction = new Action<object>(UserInfo);
            #endregion 功能命令

        }

        #region 菜单栏命令实现
        /// <summary>
        /// 将标签页设置到第一页（样品数据搜索）
        /// </summary>
        /// <param name="w">MainView</param>
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
        /// <param name="w">MainView</param>
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
        /// <param name="w">MainView</param>
        private void MenuOutputData(object w)
        {
            (w as MainView).TabControlFunction.SelectedIndex = 2;
            (w as MainView).MenuItemSearch.IsChecked = false;
            (w as MainView).MenuItemInsert.IsChecked = false;
            (w as MainView).MenuItemOutput.IsChecked = true;
        }
        /// <summary>
        /// 打开当前用户的收藏夹
        /// </summary>
        /// <param name="w"></param>
        private void MenuFavorite(object w)
        {
            FavoriteView favoriteView = new FavoriteView();
            favoriteView.ShowDialog();
            RefreshDataGrid((w as MainView).SampleDataGrid);
        }
        /// <summary>
        /// 打开当前用户的购物车
        /// </summary>
        /// <param name="w"></param>
        private void MenuCart(object w)
        {
            CartView cartView = new CartView();
            cartView.ShowDialog();
            RefreshDataGrid((w as MainView).SampleDataGrid);
        }
        /// <summary>
        /// 打开样品借出界面
        /// </summary>
        /// <param name="w"></param>
        private void MenuSampleLend(object w)
        {
            SampleRequestView SampleRequestWindow = new SampleRequestView();

            SampleRequestWindow.ShowDialog();
        }
        /// <summary>
        /// 登出当前用户
        /// </summary>
        /// <param name="w"></param>
        private void MenuLogOut(object w)
        {
            GlobalValue.CurrentUser = null;
            MessageBox.Show((w as Window), "当前用户已登出\n正在返回登录界面...", "提示");
            LoginView loginWindow = new LoginView();
            loginWindow.Show();
            (w as Window).Close();
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
            SampleModelList = dBControl.SearchSample();
            dg.ItemsSource = SampleModelList;
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
            else if (SampleModel.State <= 0)
            {
                MessageBox.Show((w as Window), "样品状态不能为空", "提示");
            }
            else
            {
                string name = SampleModel.SampleName;
                string category = ComboBoxCategory[SampleModel.CategoryIndex];
                string time = SampleModel.SamplingDateTime.ToShortDateString();
                string longitude = SampleModel.Longitude;
                string latitude = SampleModel.Latitude;
                int state = SampleModel.State;
                if (dBControl.InsertIntoSampleTable(name, category, time, longitude, latitude, state) > 0)
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
                (updateView.DataContext as UpdateViewModel).SampleUpdated = sample;
                if (updateView.ShowDialog().Value)
                {
                    //刷新表中内容
                    RefreshDataGrid((w as DataGrid));
                }
            }
        }
        /// <summary>
        /// 将选中的样品添加至当前用户的收藏夹中
        /// </summary>
        /// <param name="w"></param>
        private void AddFavorite(object w)
        {
            List<int> iList = GetSelectedSamples((w as MainView).SampleDataGrid);
            if(iList.Count>0)
            {
                if(dBControl.InsertIntoFavoriteTable(GlobalValue.CurrentUser.UserID, iList) > 0)
                {
                    MessageBox.Show((w as Window), "收藏成功", "提示");
                    RefreshDataGrid((w as MainView).SampleDataGrid);
                }
            }
        }
        /// <summary>
        /// 将选中的样品添加至当前用户的购物车中
        /// </summary>
        /// <param name="w"></param>
        private void AddIntoCart(object w)
        {
            List<int> iList = GetSelectedSamples((w as MainView).SampleDataGrid);
            if (iList.Count > 0)
            {
                if (dBControl.InsertIntoCartTable(GlobalValue.CurrentUser.UserID, iList) > 0)
                {
                    MessageBox.Show((w as Window), "加入购物车成功", "提示");
                    RefreshDataGrid((w as MainView).SampleDataGrid);
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
            ObservableCollection<SampleModel> sList = (ObservableCollection<SampleModel>)dg.ItemsSource;
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
        /// 添加或删除收藏样品
        /// </summary>
        /// <param name="w">DataGrid.SelectedItem</param>
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
        /// <summary>
        /// 添加或删除购物车中样品
        /// </summary>
        /// <param name="w">DataGrid.SelectedItem</param>
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
        /// <summary>
        /// 查看当前用户信息
        /// </summary>
        /// <param name="w"></param>
        private void UserInfo(object w)
        {
            string str = "用户名称：" + GlobalValue.CurrentUser.Name + "\n用户权限：" + GlobalValue.CurrentUser.UserRight;
            MessageBox.Show(str, "用户信息");
        }
        #endregion 功能命令实现
    }
}
