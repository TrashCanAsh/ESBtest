using ESBtest.Common;
using ESBtest.Model;
using ESBtest.View;
using ESBtest.ViewModel.Base;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ZXing;

namespace ESBtest.ViewModel
{
    public class SampleManageViewModel : NotifyBase
    {
        private DBControl dBControl;

        public ObservableCollection<SampleModel> SampleModelList { get; set; }
        public ObservableCollection<SampleRecordModel> SampleRecordModelList { get; set; }
        private string _QRCodePath;
        public string QRCodePath
        {
            get { return _QRCodePath; }
            set
            {
                _QRCodePath = value;
                RaisePropertyChanged();
            }
        }
        private BitmapImage _QRCodeImage;
        public BitmapImage QRCodeImage
        {
            get { return _QRCodeImage; }
            set
            {
                _QRCodeImage = value;
                RaisePropertyChanged();
            }
        }

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase MinWindowCommand { get; set; }
        public CommandBase MaxWindowCommand { get; set; }

        public CommandBase ManageCommand { get; set; }
        public CommandBase OpenQRCodeImageCommand { get; set; }
        public CommandBase CheckOutCommand { get; set; }
        public CommandBase PutInStroageCommand { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SampleManageViewModel()
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
            this.SampleModelList = null;
            //创建记录表格数据源实例
            this.SampleRecordModelList = dBControl.SearchRecord(">", "1");
            //初始化二维码图像路径
            this.QRCodePath = "";
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
            this.ManageCommand = new CommandBase();
            this.OpenQRCodeImageCommand = new CommandBase();
            this.CheckOutCommand = new CommandBase();
            this.PutInStroageCommand = new CommandBase();

            //跳转下一界面（出入库界面）
            this.ManageCommand.ExecuteAction = new Action<object>(Manange);
            //打开二维码图像路径命令
            this.OpenQRCodeImageCommand.ExecuteAction = new Action<object>(OpenQRCodeImage);
            //出库命令
            this.CheckOutCommand.ExecuteAction = new Action<object>(CheckOut);
            //入库命令
            this.PutInStroageCommand.ExecuteAction = new Action<object>(PutInStorage);
            #endregion 功能命令

        }

        #region 功能命令实现
        /// <summary>
        /// 根据当前选中的申请记录转到出入库界面
        /// </summary>
        /// <param name="w">SampleManageWindow</param>
        private void Manange(object w)
        {
            SampleRecordModel srm = (SampleRecordModel)(w as SampleManageView).RecordDataGrid.SelectedItem;
            List<int> iList = dBControl.SearchSampleRecord(srm.IdRecord, srm.IdUser);
            SampleModelList = dBControl.SearchSample(iList);
            (w as SampleManageView).SampleDataGrid.ItemsSource = SampleModelList;
            (w as SampleManageView).TabControlRecord.SelectedIndex = 1;
        }
        /// <summary>
        /// 打开对应路径的二维码文件
        /// </summary>
        /// <param name="w"></param>
        private void OpenQRCodeImage(object w)
        {
            OpenFileDialog MyOpenFileDialog = new OpenFileDialog();
            if (MyOpenFileDialog.ShowDialog() == true)
            {
                this.QRCodePath = MyOpenFileDialog.FileName;
                QRCodeImage = new BitmapImage(new Uri(MyOpenFileDialog.FileName));
            }
            BarcodeReader codeReader = new BarcodeReader();
            var result = codeReader.Decode(QRCodeImage);
            Console.WriteLine(result);
            //todo, 获取样品信息后在表中对应样品中打勾，否则报错，全部完成以后一起出库or入库，设置一下出入库的权限，同一时间出现一个
        }
        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="w"></param>
        private void CheckOut(object w)
        {

        }
        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="w"></param>
        private void PutInStorage(object w)
        {

        }
        #endregion 功能命令实现
    }
}
