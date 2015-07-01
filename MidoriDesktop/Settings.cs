using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MidoriDesktop
{
    class Settings
    {
        public static bool
        HotkeyImageCtrl = false,
        HotkeyImageAlt = false,
        HotkeyImageShift = false,

        HotkeyVideoCtrl = false,
        HotkeyVideoAlt = false,
        HotkeyVideoShift = false;

        public static int HotkeyImage = 44, HotkeyVideo = 19, PostCapture = 0;

        public static string APIKey = "";

        static string file;
        public static void Initialize()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Midori\";
            file = folder + "settings";

            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            if (!File.Exists(file))
            {
                FileStream stream = File.Create(file);

                byte[] buffer = Encoding.ASCII.GetBytes("HotkeyImage=0:0:0:44\nHotkeyVideo=0:0:0:19\nPostCapture=0\nAPIKey=");
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();
                stream.Dispose();
            }
            else
            {
                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    string[] spl = line.Split('=');

                    switch (spl[0])
                    {
                        case "HotkeyImage":
                            string[] spl2 = spl[1].Split(':');
                            if (spl2.Length != 4) break;

                            HotkeyImageCtrl = (spl2[0] == "1");
                            HotkeyImageAlt = (spl2[1] == "1");
                            HotkeyImageShift = (spl2[2] == "1");
                            HotkeyImage = Int32.Parse(spl2[3]);
                            break;
                        case "HotkeyVideo":
                            string[] spl2v = spl[1].Split(':');
                            if (spl2v.Length != 4) break;

                            HotkeyVideoCtrl = (spl2v[0] == "1");
                            HotkeyVideoAlt = (spl2v[1] == "1");
                            HotkeyVideoShift = (spl2v[2] == "1");
                            HotkeyVideo = Int32.Parse(spl2v[3]);
                            break;
                        case "PostCapture":
                            PostCapture = Int32.Parse(spl[1]);
                            break;
                        case "APIKey":
                            APIKey = spl[1];
                            break;
                        default:
                            throw new Exception("Unknown setting key '" + spl[0] + "'");
                    }
                }
            }
        }

        public static void Save()
        {
            FileStream stream = (File.Exists(file) ? File.OpenWrite(file) : File.Create(file));

            byte[] buffer = Encoding.ASCII.GetBytes(
                String.Format("HotkeyImage={0}:{1}:{2}:{3}\nHotkeyVideo={4}:{5}:{6}:{7}\nPostCapture={8}\nAPIKey={9}",
                (HotkeyImageCtrl ? 1 : 0),
                (HotkeyImageAlt ? 1 : 0),
                (HotkeyImageShift ? 1 : 0),
                HotkeyImage,
                (HotkeyVideoCtrl ? 1 : 0),
                (HotkeyVideoAlt ? 1 : 0),
                (HotkeyVideoShift ? 1 : 0),
                HotkeyVideo,
                PostCapture,
                APIKey)
            );
            stream.Write(buffer, 0, buffer.Length);

            stream.Close();
            stream.Dispose();
        }
    }
}
