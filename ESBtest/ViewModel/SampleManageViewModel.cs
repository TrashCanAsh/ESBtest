using ESBtest.Common;
using ESBtest.Model;
using ESBtest.View;
using ESBtest.ViewModel.Base;
using Microsoft.Win32;
using System;
using System.Windows;
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

        public int checknum { get; set; }
        public int listnum { get; set; }
        public SampleRecordModel SampleRecordModel { get; set; }
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
        public CommandBase PutInStorageCommand { get; set; }

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
            //
            this.checknum = 0;
            this.listnum = 0;
            //创建样品实例
            this.SampleRecordModel = new SampleRecordModel();
            //创建表格数据源实例
            this.SampleModelList = null;
            //创建记录表格数据源实例
            this.SampleRecordModelList = dBControl.SearchRecord("2", "3");
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
            this.PutInStorageCommand = new CommandBase();

            //跳转下一界面（出入库界面）
            this.ManageCommand.ExecuteAction = new Action<object>(Manange);
            //打开二维码图像路径命令
            this.OpenQRCodeImageCommand.ExecuteAction = new Action<object>(OpenQRCodeImage);
            //出库命令
            this.CheckOutCommand.ExecuteAction = new Action<object>(CheckOut);
            //入库命令
            this.PutInStorageCommand.ExecuteAction = new Action<object>(PutInStorage);
            #endregion 功能命令

        }

        #region 功能命令实现
        /// <summary>
        /// 根据当前选中的申请记录转到出入库界面
        /// </summary>
        /// <param name="w">SampleManageWindow</param>
        private void Manange(object w)
        {
            this.SampleRecordModel = (SampleRecordModel)(w as SampleManageView).RecordDataGrid.SelectedItem;
            List<int> iList = dBControl.SearchSampleRecord(SampleRecordModel.IdRecord, SampleRecordModel.IdUser);
            SampleModelList = dBControl.SearchSample(iList);
            (w as SampleManageView).SampleDataGrid.ItemsSource = SampleModelList;
            listnum = SampleModelList.Count();
            if (SampleRecordModel.State == 2)
            {
                (w as SampleManageView).ButtonOut.Visibility = Visibility.Visible;
            }
            else if (SampleRecordModel.State == 3)
            {
                (w as SampleManageView).ButtonIn.Visibility = Visibility.Visible;
            }
            (w as SampleManageView).TabControlRecord.SelectedIndex = 1;
        }
        /// <summary>
        /// 打开对应路径的二维码文件
        /// </summary>
        /// <param name="w">SampleManageWindow</param>
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
            string[] strsplit = result.ToString().Split(',');
            foreach (SampleModel sm in SampleModelList)
            {
                if (sm.SampleID == strsplit[0])
                {
                    sm.IsSelected = true;
                    checknum++;
                    if (checknum == listnum)
                    {
                        (w as SampleManageView).ButtonOut.IsEnabled = true;
                        (w as SampleManageView).ButtonIn.IsEnabled = true;
                    }
                    return;
                }
            }
        }
        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="w">SampleManageWindow</param>
        private void CheckOut(object w)
        {
            //改变样品信息表中样品状态
            foreach (SampleModel sm in SampleModelList)
            {
                if (dBControl.UpdateSampleTable(sm.SampleID, 3) < 0)
                {
                    return;
                }
            }
            //改变样品申请表中申请状态，添加出库日期
            if (dBControl.UpdateRecordTable(SampleRecordModel.IdRecord, SampleRecordModel.IdUser, 3, DateTime.Now.ToShortDateString()) < 0)
            {
                return;
            }
            //回到上一界面，初始化部分变量
            MessageBox.Show("出库成功", "提示");
            (w as SampleManageView).TabControlRecord.SelectedIndex = 0;
            this.SampleRecordModelList = dBControl.SearchRecord("2", "3");
            (w as SampleManageView).RecordDataGrid.ItemsSource = this.SampleRecordModelList;
            checknum = 0;
        }
        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="w">SampleManageWindow</param>
        private void PutInStorage(object w)
        {
            //改变样品信息表中样品状态
            foreach (SampleModel sm in SampleModelList)
            {
                if (dBControl.UpdateSampleTable(sm.SampleID, 1) < 0)
                {
                    return;
                }
            }
            //改变样品申请表中申请状态，添加入库日期
            if (dBControl.UpdateRecordTable(SampleRecordModel.IdRecord, SampleRecordModel.IdUser, 4, DateTime.Now.ToShortDateString()) < 0)
            {
                return;
            }
            //回到上一界面，初始化部分变量
            MessageBox.Show("入库成功", "提示");
            (w as SampleManageView).TabControlRecord.SelectedIndex = 0;
            this.SampleRecordModelList = dBControl.SearchRecord("2", "3");
            (w as SampleManageView).RecordDataGrid.ItemsSource = this.SampleRecordModelList;
            checknum = 0;
        }
        #endregion 功能命令实现
    }
}
