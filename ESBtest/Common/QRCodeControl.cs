using System;
using System.Collections.Generic;
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

namespace ESBtest.Common
{
    public class QRCodeControl
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="content">二维码包含的内容</param>
        /// <param name="width">二维码图像的宽度</param>
        /// <param name="height">二维码图像的高度</param>
        /// <returns></returns>
        public static ImageSource CreateQRCode(String content, int width, int height)
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

        /// <summary>
        /// 二维码解码
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static string ReadQRCode(BitmapImage bitmap)
        {
            BarcodeReader codeReader = new BarcodeReader();
            var result = codeReader.Decode(bitmap);
            return result.Text;
        }

        // 注销对象方法API
        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);

    }
}
