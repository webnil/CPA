
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CheckIn
{
    /// <summary>
    /// Summary description for GenerateCaptcha
    /// </summary>
    public class GenerateCaptcha : IHttpHandler,IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            MemoryStream memStream = new MemoryStream();
            string phrase = Convert.ToString(context.Session["captcha"]);

            Bitmap imgCapthca = GenerateImage(220,70,phrase);
            imgCapthca.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imgBytes = memStream.GetBuffer();

            imgCapthca.Dispose();
            memStream.Close();

            context.Response.ContentType = "image/jpeg";
            context.Response.BinaryWrite(imgBytes);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public Bitmap GenerateImage(int Width, int Height, string Phrase)
        {

            Bitmap CaptchaImg = new Bitmap(Width, Height);
            Random Randomizer = new Random();
            Graphics Graphics = Graphics.FromImage(CaptchaImg);
            Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            Graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, Width, Height);

            Graphics.RotateTransform(-3);
            Graphics.DrawString(Phrase, new Font("Verdana", 30), new SolidBrush(Color.White), 15, 15);
            Graphics.Flush();

            return CaptchaImg;

        }
    }
}