using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace MidoriDesktop
{
    public class Midori
    {
        public delegate void UploadStreamHandler(Stream str);
        public delegate void UploadCallback(string str);
        public static void Upload(UploadStreamHandler handler, string type, string ext, UploadCallback callback = null)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://midori.moe/upload.php?apikey=" + Settings.APIKey);

                req.Method = WebRequestMethods.Http.Post;

                req.UserAgent = "Midori-Desktop v1.0";

                req.Expect = "";
                req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                req.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
                req.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                req.Headers.Add(HttpRequestHeader.Pragma, "no-cache");
                req.Headers.Add(HttpRequestHeader.CacheControl, "no-cache");

                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", System.Globalization.NumberFormatInfo.InvariantInfo);
                req.ContentType = "multipart/form-data; boundary=" + boundary;
                boundary = "--" + boundary;

                Stream str = req.GetRequestStream();

                StringBuilder file = new StringBuilder();

                byte[] buffer = Encoding.ASCII.GetBytes(String.Format("{0}\r\nContent-Disposition: form-data; name=\"files[]\"; filename=\"midoridesktop.{1}\"\r\nContent-Type: {2}\r\n\r\n", boundary, ext, type));
                str.Write(buffer, 0, buffer.Length);

                handler(str);
                //img.Save(str, System.Drawing.Imaging.ImageFormat.Png);

                buffer = Encoding.ASCII.GetBytes("\r\n" + boundary + "--\r\n");
                str.Write(buffer, 0, buffer.Length);
                str.Close();
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                string response;
                using (Stream s = resp.GetResponseStream())
                using (GZipStream inflate = new GZipStream(s, CompressionMode.Decompress))
                using (StreamReader reader = new StreamReader(inflate))
                    response = reader.ReadToEnd();

                resp.Close();
                str.Dispose();

                if (response.EndsWith("." + ext))
                {
                    callback("https://i.midori.moe/" + response);
                    /*Program.STAThread.Send(delegate
                    {
                        Clipboard.SetText("https://i.midori.moe/" + response);
                        ico.ShowBalloonTip(3, "Info", "Link copied to clipboard", ToolTipIcon.Info);
                    }, null);*/
                }
            }
            catch (Exception ex)
            {
                Error error = new Error(ex.ToString());
            }
        }
    }
}
