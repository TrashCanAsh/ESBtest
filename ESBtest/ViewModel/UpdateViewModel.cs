using ESBtest.Common;
using ESBtest.Model;
using ESBtest.View;
using ESBtest.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace ESBtest.ViewModel
{
    public class UpdateViewModel: NotifyBase
    {
        private DBControl dBControl;

        public ObservableCollection<string> ComboBoxCategory { get; set; }
        public ObservableCollection<string> ComboBoxState { get; set; }

        public SampleModel SampleUpdated { get; set; }
        public ImageSource QRcode { get; set; }

        public CommandBase CloseWindowCommand { get; set; }
        public CommandBase MinWindowCommand { get; set; }
        public CommandBase UpdateSampleCommand { get; set; }
        public CommandBase CancelCommand { get; set; }
        public CommandBase GenerateQRcodeCommand { get; set; }


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
            this.SampleUpdated = new SampleModel();
            //二维码相关实例
            this.QRcode = null;
            //下拉框内容
            this.ComboBoxCategory = new ObservableCollection<string>() { "null", "solid", "liquid", "gas", "bio" };
            this.ComboBoxState = new ObservableCollection<string>() { "unknown", "in stock", "locked", "out on loan" };
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
            this.GenerateQRcodeCommand = new CommandBase();

            //关闭窗口命令
            this.CloseWindowCommand.ExecuteAction = new Action<object>(GlobalFunc.CloseWindow);
            //最小化窗口命令
            this.MinWindowCommand.ExecuteAction = new Action<object>(GlobalFunc.MinWindow);
            //确认修改信息命令 权限：管理员
            this.UpdateSampleCommand.ExecuteAction = new Action<object>(UpdateSample);
            this.UpdateSampleCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.AdminRight);
            //取消修改命令 权限：管理员
            this.CancelCommand.ExecuteAction = new Action<object>(GlobalFunc.CloseWindow);
            this.CancelCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.AdminRight);
            //生成二维码命令 权限：管理员
            this.GenerateQRcodeCommand.ExecuteAction = new Action<object>(GenerateQRcode);
            this.GenerateQRcodeCommand.CanExecuteFunc = new Func<object, bool>(GlobalFunc.AdminRight);

        }
        /// <summary>
        /// 对样品信息进行修改
        /// </summary>
        /// <param name="w"></param>
        private void UpdateSample(object w)
        {
            //修改数据库内容
            if (dBControl.UpdateSampleTable(SampleUpdated.SampleID, SampleUpdated.SampleName, ComboBoxCategory[SampleUpdated.CategoryIndex] , SampleUpdated.SamplingDateTime.ToShortDateString(), SampleUpdated.Longitude, SampleUpdated.Latitude, SampleUpdated.State, SampleUpdated.Comment) > 0)
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
        /// <summary>
        /// 根据选中的样品信息来生成二维码
        /// </summary>
        /// <param name="w"></param>
        private void GenerateQRcode(object w)
        {
            string msg = SampleUpdated.SampleID + ", " + SampleUpdated.SampleName + ", " + SampleUpdated.Category + ", " + SampleUpdated.SamplingTime 
                + ", " + SampleUpdated.Longitude + ", " + SampleUpdated.Latitude + ", " + SampleUpdated.StateStr + ", " + SampleUpdated.Comment;
            QRcode = CreateQRCode(msg, 200, 200);
            (w as UpdateView).QRcodeImage.Source = QRcode;
        }
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="content">二维码包含的内容</param>
        /// <param name="width">二维码图像的宽度</param>
        /// <param name="height">二维码图像的高度</param>
        /// <returns></returns>
        private ImageSource CreateQRCode(String content, int width, int height)
        {
            EncodingOptions options;//包含一些编码、大小等的设置
            BarcodeWriter write = null;//用来生成二维码，对应的BarcodeReader用来解码
            options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = width,
                Height = height,
                Margin = 0
            };
            write = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = options
            };
            Bitmap bitmap = write.Write(content);
            IntPtr ip = bitmap.GetHbitmap();
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                ip, IntPtr.Zero, Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(ip);
            return bitmapSource;
        }

        // 注销对象方法API
        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);
    }
}
