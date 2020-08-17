using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QLVBVer2._1
{
    public partial class Form1 : Form
    {
        private QLVBEntities1 db = new QLVBEntities1();
        public Form1()
        {
            InitializeComponent();
            var outdate = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["date"]);
            var today = DateTime.Today.Date;
            var currentDay = DateTime.Today.AddDays(outdate).Date;
            var listQuahan = db.tblCVdens.Where(m => m.Daxuly == false && m.ngayhethan != null && ((m.ngayhethan >= today && m.ngayhethan.Value <= currentDay) || (m.ngayhethan < today))).ToList();
            if (listQuahan != null && listQuahan.Count > 0)
            {
                lblExpiredCount.Text = listQuahan.Count.ToString();
            }
            else
                lblExpiredCount.Text = "0";
        }

        private void btnCVDen_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            Congvanden cvDen = new Congvanden(this);
            cvDen.Show();
            Thread.Sleep(1500);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            
        }

        private void btnCVDi_Click(object sender, EventArgs e)
        {
            //Congvandi cvDen = new Congvandi();
            //cvDen.Show();
            Danhmuccuatoi cat = new Danhmuccuatoi();
            cat.Show();
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm newForm = new ConfigForm();
            newForm.Show();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                    Application.OpenForms[i].Hide();
            }
            Dangnhap dn = new Dangnhap();
            dn.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword newForm = new ChangePassword();
            newForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
