using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace MidoriDesktop
{
    class Temp : IDisposable
    {
        string temp;
        int files = 0;
        public Temp()
        {
            StringBuilder sb = new StringBuilder();

            string temppath = System.IO.Path.GetTempPath();
            sb.Append(temppath);
            if (!temppath.EndsWith(@"\")) sb.Append(@"\");
            sb.Append("MidoriDesktop/");

            string temp2 = sb.ToString();
            if (!Directory.Exists(temp2)) Directory.CreateDirectory(temp2);

            long seconds = (long)Math.Round((DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
            sb.Append(seconds + @"\");
            temp = sb.ToString();

            if (!Directory.Exists(temp)) Directory.CreateDirectory(temp);
        }

        public void AddImage(Image img)
        {
            FileStream stream = File.Create(temp + files + ".bmp");

            img.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

            stream.Close();
            stream.Dispose();
            files++;
        }

        public void Dispose()
        {
            if (Directory.Exists(temp)) Directory.Delete(temp);
        }
    }
}
