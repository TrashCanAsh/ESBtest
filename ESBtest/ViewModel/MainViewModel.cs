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

        public ObservableCollection<string> comboBoxCategory { get; set; }

        public UserModel userModel { get; set; }
        public SearchModel searchModel { get; set; }

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase MinWindowCommand { get; set; }
        public CommandBase MaxWindowCommand { get; set; }
        public CommandBase SearchCommand { get; set; }
        public CommandBase SearchResetCommand { get; set; }
        public CommandBase OpenInsertFileDialogCommand { get; set; }

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
            this.userModel = new UserModel();
            //创建搜索条件实例
            this.searchModel = new SearchModel();
            //下拉框内容
            comboBoxCategory = new ObservableCollection<string>() { "null", "solid", "liquid", "gas", "bio" };
        }

        private void SetCommand()
        {
            //创建命令实例
            this.CloseWindowCommand = new CommandBase();
            this.MinWindowCommand = new CommandBase();
            this.MaxWindowCommand = new CommandBase();
            this.SearchCommand = new CommandBase();
            this.SearchResetCommand = new CommandBase();
            this.OpenInsertFileDialogCommand = new CommandBase();

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
            //
            this.OpenInsertFileDialogCommand.ExecuteAction = new Action<object>(InsertFileDialog);
        }
        /// <summary>
        /// 根据搜索条件对样品数据进行搜索
        /// </summary>
        /// <param name="w"></param>
        private void SearchSample(object w)
        {
            //*可能要做分割*
            string keyword = searchModel.KeyWord;
            //
            string category = "";
            if(searchModel.IsCategoryChecked)
            {
                switch (searchModel.Category)
                {
                    case 1:
                        category = "solid";
                        break;
                    case 2:
                        category = "liquid";
                        break;
                    case 3:
                        category = "gas";
                        break;
                    case 4:
                        category = "bio";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                category = null;
            }
            
            string start = searchModel.StartDate > new DateTime(2022, 1, 1) && searchModel.IsSamplingTimeChecked ? searchModel.StartDate.ToShortDateString().ToString() : null;
            string end = searchModel.EndDate > new DateTime(2022, 1, 1) && searchModel.IsSamplingTimeChecked ? searchModel.EndDate.ToShortDateString().ToString() : null;

            Location nw = (searchModel.NW.longitude >= 100 && searchModel.NW.latitude >= 25) && searchModel.IsSamplingLocationChecked ? searchModel.NW : null;
            Location se = (searchModel.SE.longitude >= 100 && searchModel.SE.latitude >= 25) && searchModel.IsSamplingLocationChecked ? searchModel.SE : null;

            //Console.WriteLine("keyword:" + keyword + "//category:" + category + "//startDate:" + start + "//endDate:" + end +
            //    "//NW:" + searchModel.NW.longitude + ", " + searchModel.NW.latitude + "//SE:" + searchModel.SE.longitude + ", " + searchModel.SE.latitude);

            List<SampleModel> sList = dBControl.SearchSample(keyword, category, start, end, nw, se);
            (w as DataGrid).ItemsSource = sList;
        }

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

        private void InsertFileDialog(object w)
        {
            OpenFileDialog ofp = new OpenFileDialog() { Filter = "Txt files(*.txt)|*.txt|Csv files(*.csv)|*.csv|Excel files(*.xlsx, *.xls)|*.xlsx;*.xls|All files(*.*)|*.*" };
            if(ofp.ShowDialog() == true)
            {
                (w as MainView).TextBoxFilePath.Text = ofp.FileName;
            }
        }
    }
}
