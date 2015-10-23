using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Web;
using System.Drawing.Imaging;

namespace ThrFrd.GameTrans.Admin.WebLogic
{
    public class VerifyCodeImage
    {
        private Random rnd = new Random(unchecked((int)DateTime.Now.Ticks));

        #region 属性变量
        int fontSize = 30;  //验证码大小
        int padding = 2;    //边框
        bool chaos = true;  //是否输出燥点(默认输出)
        Color chaosColor = Color.LightGray;   //噪点颜色
        int chaoCount = 100;//噪点数量

        Color backgroundColor = Color.White;  //背景颜色
        //颜色数组
        Color[] colors = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        //字体数组
        string[] fonts = { "Arial", "Georgia", "宋体", "黑体" };
        #endregion

        #region 属性设置

        #region 验证码字体大小(为了显示扭曲效果，默认40像素，可以自行修改)
        /// <summary>
        /// 验证码字体大小(默认为40默认40像素)
        /// </summary>
        public int FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }
        #endregion

        #region 边框像素
        /// <summary>
        /// 边框像素(默认为2)
        /// </summary>
        public int Padding
        {
            get { return padding; }
            set { padding = value; }
        }
        #endregion

        #region 是否输出燥点(默认输出)
        /// <summary>
        /// 是否输出燥点（默认为是）
        /// </summary>
        public bool Chaos
        {
            get { return chaos; }
            set { chaos = value; }
        }
        #endregion

        #region 输出燥点的颜色(默认灰色)
        /// <summary>
        /// 输出燥点的颜色(默认Color.LightGray)
        /// </summary>
        public Color ChaosColor
        {
            get { return chaosColor; }
            set { chaosColor = value; }
        }
        #endregion

        #region 燥点数量
        /// <summary>
        /// 燥点数量（默认为100）
        /// </summary>
        public int ChaoCount
        {
            get { return chaoCount; }
            set { chaoCount = value; }
        }
        #endregion

        #region 自定义背景色(默认白色)
        /// <summary>
        /// 背景颜色，默认为白色
        /// </summary>
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }
        #endregion

        #region 自定义随机颜色数组
        /// <summary>
        /// 随机颜色数组Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple
        /// </summary>
        public Color[] Colors
        {
            get { return colors; }
            set { colors = value; }
        }
        #endregion

        #region 自定义字体数组
        /// <summary>
        /// 字体数组"Arial", "Georgia", "宋体", "黑体"
        /// </summary>
        public string[] Fonts
        {
            get { return fonts; }
            set { fonts = value; }
        }
        #endregion

        #endregion

        #region 产生波形滤镜效果并画一个边框

        // <summary>
        // 正弦曲线Wave扭曲图片（Edit By 51aspx.com）
        // </summary>
        // <param name="srcBmp">图片路径</param>
        // <param name="bXDir">如果扭曲则选择为True</param>
        // <param name="nMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        // <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        // <returns></returns>
        private Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            //const double PI = 3.1415926535897932384626433832795;
            const double PI2 = 6.283185307179586476925286766559;
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // 将位图背景填充为白色
            Graphics g = System.Drawing.Graphics.FromImage(destBmp);
            g.FillRectangle(new SolidBrush(System.Drawing.Color.White), 0, 0, destBmp.Width, destBmp.Height);


            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;

            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI2 * (double)j) / dBaseAxisLen : (PI2 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    System.Drawing.Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            //渐变效果
            //g.FillRectangle(new LinearGradientBrush(new Point(0, 0), new Point(destBmp.Width, destBmp.Height), Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 255, 255, 255)), 0, 0, destBmp.Width, destBmp.Height);
            //画边框
            g.DrawRectangle(new Pen(Color.Gainsboro, 0), 0, 0, destBmp.Width - 1, destBmp.Height - 1);
            g.Dispose();
            return destBmp;
        }



        #endregion

        #region 生成验证码码图片
        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="code">验证码</param>
        /// <returns>返回Bitmap</returns>
        private Bitmap CreateImageCode(string code)
        {
            int fSize = FontSize + Padding;  //字体大小
            int imageWidth = (int)(code.Length * fSize) + 10 + Padding * 2;//图片宽度
            int imageHeight = FontSize * 2 + Padding;             //图片高度
            Bitmap image = new Bitmap(imageWidth, imageHeight);   //创建图像                         
            Graphics g = Graphics.FromImage(image);
            g.Clear(BackgroundColor);                             // 填充背景颜色

            #region 画图片的背景噪音线
            for (int i = 0; i < 25; i++)
            {
                int x1 = rnd.Next(image.Width);
                int x2 = rnd.Next(image.Width);
                int y1 = rnd.Next(image.Height);
                int y2 = rnd.Next(image.Height);
                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }
            #endregion

            #region 写入每一个字符
            //定义位移（左右，上下)
            int left = 0, top = 0, top1 = 1, top2 = 1;

            int n1 = (imageHeight - FontSize - Padding * 2);
            int n2 = n1 / 5;
            top1 = n2;
            top2 = n2 * 2;

            Font f;
            Brush b;
            int cindex, findex;//颜色和字体


            for (int i = 0; i < code.Length; i++)
            {
                cindex = rnd.Next(Colors.Length - 1);//从颜色数组中随机取出一个
                findex = rnd.Next(Fonts.Length - 1); //从字体数组中随机取出一个

                f = new Font(Fonts[findex], fSize, FontStyle.Bold);
                b = new SolidBrush(Colors[cindex]);

                if (i % 2 == 1)
                {
                    top = top2;
                }
                else
                {
                    top = top1;
                }
                //产生、随机移位
                int sub;
                sub = rnd.Next(4, 8);
                if (sub > 5)
                {
                    sub = -sub;
                }
                //---------
                if (i != 0)   //第一个字不位移
                {
                    left = i * (FontSize + Padding) + sub;
                    top += sub;
                }
                //----------
                g.DrawString(code.Substring(i, 1), f, b, left, top);
            }
            #endregion

            #region 生成随机噪点
            if (chaos)
            {
                Pen pen = new Pen(ChaosColor, 0);
                int c = chaoCount;

                for (int i = 0; i < c; i++)
                {
                    int x = rnd.Next(image.Width);
                    int y = rnd.Next(image.Height);

                    g.DrawRectangle(pen, x, y, 1, 1);
                }
            }
            #endregion
            //g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1); 
            //画边框(因为后面还要做个波行效果，所以先不画边框

            g.Dispose();

            #region 产生波形
            //随机生成后三个参数(true or false,5~8，(0~2PI),)
            int _int1 = rnd.Next(4, 10);
            int _int2 = rnd.Next(1, 6);
            bool _bool = _int2 > 3 ? true : false;
            image = TwistImage(image, _bool, _int1, _int2);
            #endregion
            return image;
        }
        #endregion

        #region 将创建好的图片输出到页面
        /// <summary>
        /// 把字符图片输出到页面
        /// </summary>
        /// <param name="code">字符</param>
        /// <param name="context">页面</param>
        public void CreateImageOnPage(string code, HttpContext context)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            Bitmap image = this.CreateImageCode(code);

            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            context.Response.ClearContent();
            context.Response.ContentType = "image/Jpeg";
            context.Response.BinaryWrite(ms.GetBuffer());

            ms.Close();
            ms = null;
            image.Dispose();
            image = null;
        }
        #endregion
    }
}
