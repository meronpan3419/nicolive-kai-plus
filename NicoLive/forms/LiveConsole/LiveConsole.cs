using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NicoLive
{
    public partial class LiveConsole : Form
    {

        string mLv;
        public LiveConsole(string iLv)
        {
            mLv = iLv;
            InitializeComponent();
        }

        private void LiveConsole_Load(object sender, EventArgs e)
        {
            LoadMovie(mLv);
        }

        public void LoadMovie(string iLv)
        {
            string url = "http://live.nicovideo.jp/nicolivebroadcaster.swf?dicfilename=NicoliveBroadcasterDictionaryJAJP.swf&v=" + iLv;
            mFlash.LoadMovie(0, url);
        }

        public IntPtr getFlashHandle()
        {
            return this.mFlash.Handle;
        }

    }
}
