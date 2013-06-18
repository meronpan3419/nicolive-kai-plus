using System;
using System.Collections.Generic;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Threading;


namespace NicoLive
{
    public class Launcher
    {
        public struct Command
        {
            public bool mEnabled;
            public string mName;
            public string mPath;
            public string mArgs;
        };

        private static Launcher mInstance = null;
        private List<Command> mLuncher = null;


        //-------------------------------------------------------------------------
        // コンストラクタ
        //-------------------------------------------------------------------------
        private Launcher()
        {
            mLuncher = new List<Command>();
        }

        //-------------------------------------------------------------------------
        // シングルトン用
        //-------------------------------------------------------------------------
        public static Launcher Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new Launcher();
                }
                return mInstance;
            }
        }

        //-------------------------------------------------------------------------
        // リロード
        //-------------------------------------------------------------------------
        public void Reload()
        {
            mLuncher.Clear();
            LoadLauncher();
        }

        //-------------------------------------------------------------------------
        // 返信メッセージ読み込み
        //-------------------------------------------------------------------------
        public void LoadLauncher()
        {
            const string file = "Launcher.xml";

            using (XmlTextReader xml = new XmlTextReader(file))
            {
                if (xml == null)
                {
                    Utils.WriteLog(file + "が見つかりません");
                    return;
                }


                bool enabled = false;
                string name = "";
                string path = "";
                string args = "";

                try
                {
                    while (xml.Read())
                    {
                        if (xml.NodeType == XmlNodeType.Element)
                        {
                            if (xml.LocalName.Equals("command"))
                            {
                                for (int i = 0; i < xml.AttributeCount; i++)
                                {
                                    xml.MoveToAttribute(i);
                                    if (xml.Name == "enabled")
                                    {
                                        enabled = xml.Value.ToString().Equals("True");
                                    }
                                    else
                                        if (xml.Name == "name")
                                        {
                                            name = xml.Value;
                                        }
                                        else
                                            if (xml.Name == "path")
                                            {
                                                path = xml.Value;
                                            }
                                            else
                                                if (xml.Name == "args")
                                                {
                                                    args = xml.Value;
                                                }
                                }


                                if (enabled)
                                {
                                    Command cmd = new Command();
                                    cmd.mEnabled = enabled;
                                    cmd.mName = name;
                                    cmd.mPath = path;
                                    cmd.mArgs = args;

                                    mLuncher.Add(cmd);
                                }

                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Utils.WriteLog("LoadLauncher:" + e.Message);
                }
            }
        }

        public void Exec(string lv)
        {

            foreach (Command cmd in mLuncher)
            {
                if (!cmd.mEnabled) continue;


                Thread th = new Thread(delegate()
                {
                    try
                    {
                        string args = cmd.mArgs.Replace("@LV", lv);
                        ProcessStartInfo psInfo = new ProcessStartInfo();
                        psInfo.FileName = cmd.mPath;
                        psInfo.CreateNoWindow = false;
                        psInfo.UseShellExecute = true;
                        psInfo.Arguments = args;
                        Process.Start(psInfo);
                    }
                    catch (Exception e)
                    {
                        Utils.WriteLog("Launcher.Exec():" + e.Message);
                    }
                });
                th.Name = "Launcher.Exec(): " + cmd.mName;
                th.Start();

            }

        }


    }
}

