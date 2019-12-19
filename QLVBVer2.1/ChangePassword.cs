using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLVBVer2._1
{
    public partial class ChangePassword : Form
    {
        private QLVBEntities1 db = new QLVBEntities1();
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            try
            {
                string userId = ConfigurationManager.AppSettings["UserLogin"];
                var user = db.tblLogins.Where(m => m.id.ToString() == userId).FirstOrDefault();
                if (user != null)
                {
                    //var tempMD5 = CreateMD5(txtOldPassword.Text).ToLower();
                    if (user.passWord == CreateMD5(txtOldPassword.Text).ToLower())
                    {
                        if (String.IsNullOrEmpty(txtNewPassword.Text) || String.IsNullOrEmpty(txtConfirm.Text))
                        {
                            lblError.Visible = true;
                            lblError.Text = "Confirm & new password must not null";
                        }
                        else
                        {
                            if (txtNewPassword.Text == txtConfirm.Text)
                            {
                                user.passWord = CreateMD5(txtNewPassword.Text).ToLower();
                                db.SaveChanges();
                                lblError.Visible = false;
                                MessageBox.Show("Update thành công.", "Success!!!");
                                txtOldPassword.Text = txtOldPassword.Text = txtConfirm.Text = String.Empty;
                            }
                            else
                            {
                                lblError.Visible = true;
                                lblError.Text = "Confirm & new password must be the same";
                            }
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "Incorrect Password";
                    }
                }
            }
            catch
            {
                lblError.Visible = true;
                lblError.Text = "Cannot update password";
            }
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOldPassword.Text = txtNewPassword.Text = txtConfirm.Text = "";
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        public static string CreateMD5(string input)
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

    }
}
