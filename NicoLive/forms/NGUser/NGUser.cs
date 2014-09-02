using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace NicoLive
{
    public partial class NGUser : Form
    {

        const string NG_API = "http://watch.live.nicovideo.jp/api/configurengword?";
        string mLv;
        
        public NGUser(string iLv)
        {
            mLv = iLv;
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            update_nguser(mLv);        
        }

        private void update_nguser(string iLv)
        {
            //Nico nico = Nico.Instance;
            //if (!nico.IsLogin) return;



            //Dictionary<string, string> ng_user = nico.GetNGUser(iLv);
            
            //foreach (string id in ng_user.Keys)
            //{
            //    Utils.WriteLog("token: " + ng_user[id]);
            //    Utils.WriteLog("source: " + id);
            //    lbNGUser.Items.Add(id);
            //}
            lbNGUser.Items.Clear();
            UserID user_id = UserID.Instance;
            List<string> ng_user_list = user_id.GetNGUserList();
            foreach (string ng_user in ng_user_list)
            {
                lbNGUser.Items.Add(ng_user);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbNGUser.SelectedIndex == -1) return;
            UserID user_id = UserID.Instance;
            user_id.delNGUser(lbNGUser.SelectedItem.ToString());
            lbNGUser.Items.Remove(lbNGUser.SelectedItem);

        }

        private void NGUser_Load(object sender, EventArgs e)
        {
            update_nguser(mLv);  
        }
    }
}
