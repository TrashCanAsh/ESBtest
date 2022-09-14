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
using ESBtest.ViewModel.Base;

namespace ESBtest.ViewModel
{
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

            //关闭窗口命令
            this.CloseWindowCommand.ExecuteAction = new Action<object>((w) =>
            {
                (w as Window).Close();
            });
            //最小化窗口命令
            this.MinWindowCommand.ExecuteAction = new Action<object>((w) =>
            {
                (w as Window).WindowState = WindowState.Minimized;
            });
            //最大化窗口命令
            this.MaxWindowCommand.ExecuteAction = new Action<object>((w) =>
            {
                if ((w as Window).WindowState == WindowState.Maximized)
                {
                    (w as Window).WindowState = WindowState.Normal;
                }
                else
                {
                    (w as Window).WindowState = WindowState.Maximized;
                }
            });
            //
            this.SearchCommand.ExecuteAction = new Action<object>(SearchSample);
        }

        private void SearchSample(object w)
        {
            string keyword = searchModel.KeyWord;
            string category = "";
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
            string start = searchModel.StartDate > new DateTime(2022, 1, 1) ? searchModel.StartDate.ToShortDateString().ToString() : null;
            string end = searchModel.EndDate > new DateTime(2022, 1, 1) ? searchModel.EndDate.ToShortDateString().ToString() : null;

            Console.WriteLine("keyword:" + keyword + "//category:" + category + "//startDate:" + start + "//endDate:" + end +
                "//NW:" + searchModel.NW.longitude + ", " + searchModel.NW.latitude + "//SE:" + searchModel.SE.longitude + ", " + searchModel.SE.latitude);
            //List<SampleModel> sList = dBControl.SearchSample(searchModel.KeyWord);
            //(w as DataGrid).ItemsSource = sList;
        }
    }
}
