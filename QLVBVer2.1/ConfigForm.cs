using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
namespace QLVBVer2._1
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["date"].Value = numericDate.Value.ToString();
                configuration.AppSettings.Settings["cbFlashColor"].Value = cbFlash.Checked.ToString();
                configuration.AppSettings.Settings["IPMain"].Value = txtIP.Text;
                configuration.Save();
                ConfigurationManager.RefreshSection("appSettings");
                MessageBox.Show(this, "Thêm mới thành công", "Success!");
            }
            catch
            {
                MessageBox.Show(this, "Lỗi", "Error!");
            }
            
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            try
            {
                int date = Convert.ToInt32(ConfigurationManager.AppSettings["date"]);
            bool cbFlashColor = Convert.ToBoolean(ConfigurationManager.AppSettings["cbFlashColor"]);
            numericDate.Value = date;
            cbFlash.Checked = cbFlashColor;
            txtIP.Text = ConfigurationManager.AppSettings["IPMain"].ToString();
            }
            catch
            {
                MessageBox.Show(this, "Lỗi", "Error!");
            }
        }
    }
}
