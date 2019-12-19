using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLVBVer2._1
{
    public partial class Danhmuccuatoi : Form
    {
        private QLVBEntities1 db = new QLVBEntities1();
        public Danhmuccuatoi()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void Reload_Data()
        {
            var listCate = db.CategoryVBs.ToList();
            dataGridView1.DataSource = listCate;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCat.Text))
            {
                MessageBox.Show(this, "Tên danh mục không thể trống", "lỗi");
            }
            else
            {
                try
                {
                    var cate = new CategoryVB
                    {
                        NameCate = txtCat.Text,
                        Active = chkActive.Checked
                    };
                    var tempCat = db.CategoryVBs.Where(m => m.NameCate == txtCat.Text).FirstOrDefault();
                    if (tempCat != null)
                    {
                        MessageBox.Show(this, "Tên danh mục đã tồn tại", "LỖI!");
                    }
                    else
                    {
                        db.CategoryVBs.Add(cate);
                        db.SaveChanges();
                        MessageBox.Show(this, "Thêm mới thành công", "Success!");
                        Reload_Data();
                    }
                }
                catch
                {
                    MessageBox.Show(this, "Không thể thêm mới", "lỗi");
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtCat.Text))
                {

                    if (db.CategoryVBs.Where(m => m.NameCate == txtCat.Text).FirstOrDefault() != null)
                    {
                        MessageBox.Show(this, "Tên danh mục đã tồn tại", "LỖI!");
                    }
                    else
                    {
                        var tempId = Convert.ToInt32(lblId.Text);
                        var item = db.CategoryVBs.Where(m => m.Id == tempId).FirstOrDefault();
                        item.NameCate = txtCat.Text;
                        item.Active = chkActive.Checked;
                        db.CategoryVBs.AddOrUpdate(item);
                        db.SaveChanges();
                        MessageBox.Show(this, "Cập nhật thành công", "Success!");
                        Reload_Data();
                    }

                }

            }
            catch
            {
                MessageBox.Show(this, "Cập nhật lỗi", "lỗi");
            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var tempId = Convert.ToInt32(lblId.Text);
                var item = db.CategoryVBs.Where(m => m.Id == tempId).FirstOrDefault();
                if (item != null)
                {
                    db.CategoryVBs.Remove(item);
                    db.SaveChanges();
                    MessageBox.Show(this, "Xóa thành công", "Success!");
                    Reload_Data();
                }
                else
                {
                    MessageBox.Show(this, "Bản ghi không tồn tại", "LỖI!");
                }
            }
            catch
            {
                MessageBox.Show(this, "Không thể xóa", "LỖI!");
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            lblId.Text = "";
            txtCat.Text = "";
        }

        private void Danhmuccuatoi_Load(object sender, EventArgs e)
        {
            Reload_Data();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    var name = dataGridView1.Rows[e.RowIndex].Cells["NameCate"].Value.ToString();
                    var id = dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                    var check = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["Active"].Value);
                    lblId.Text = id;
                    txtCat.Text = name;
                    chkActive.Checked = check;
                }
            }
            catch
            {
                MessageBox.Show(this, "Unhandled Error!!!", "LỖI!");
            }
            
        }
    }
}
