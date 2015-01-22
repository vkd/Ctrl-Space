using Microsoft.Xna.Framework;
using System;
using System.Configuration;
using System.IO;

namespace Ctrl_Space.Helpers
{
    class Config
    {
        private string _filename = "config.cfg";

        public bool IsFullScreen { get; set; }
        public Point Resolution { get; set; }

        public Config(string filename = null)
        {
            IsFullScreen = ConfigurationManager.AppSettings["IsFullScreen"].ToLower() == "true";
            Resolution = ParseResolution(ConfigurationManager.AppSettings["Resolution"]);

            if (!string.IsNullOrEmpty(filename))
                _filename = filename;

            Load();

            if (!File.Exists(_filename))
            {
                Save();
            }
        }

        public void Load()
        {
            if (File.Exists(_filename))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(_filename))
                    {
                        while (!sr.EndOfStream)
                        {
                            try
                            {
                                var config = sr.ReadLine().Split('=');
                                if (config.Length != 2)
                                    continue;

                                switch (config[0].Trim())
                                {
                                    case "IsFullScreen":
                                        IsFullScreen = config[1].Trim().ToLower() == "true";
                                        break;
                                    case "Resolution":
                                        Resolution = ParseResolution(config[1].Trim());
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                Game.DebugConsole.AppendLine("Error load config: " + ex.Message);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Game.DebugConsole.AppendLine("Error load config: " + ex.Message);
                }
            }
        }

        public void Save()
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(_filename, false))
                {
                    sr.WriteLine("IsFullScreen=" + IsFullScreen.ToString().ToLower());
                    sr.WriteLine("Resolution=" + Resolution);
                }
            }
            catch (Exception ex)
            {
                Game.DebugConsole.AppendLine("Error load config: " + ex.Message);
            }
        }

        private Point ParseResolution(string value)
        {
            try
            {
                var t = value.Split('x');
                return new Point(int.Parse(t[0]), int.Parse(t[1]));
            }
            catch (Exception ex)
            {
                throw new Exception("Error parsing resolution from config.", ex);
            }
        }
    }
}
