using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
namespace QLVBVer2._1
{
    public partial class Dangnhap : Form
    {
        private QLVBEntities1 db = new QLVBEntities1();
        public Dangnhap()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            var username = txtUsername.Text;
            var password = CreateMD5(txtPassword.Text);
            var user = db.tblLogins.Where(m => m.userName == username && m.passWord == password).FirstOrDefault();

            if (user != null)
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["Role"].Value = user.RoleId.ToString();
                configuration.AppSettings.Settings["UserLogin"].Value = user.id.ToString();
                configuration.Save();
                ConfigurationManager.RefreshSection("appSettings");
                this.Hide();
                Form1 form = new Form1();
                form.ShowDialog();
            }
            else
            {
                lblError.Visible = true;
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void bntClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = txtPassword.Text = "";
        }

        private string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private void Dangnhap_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        private void Dangnhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
    }
}
