using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace QLVBVer2._1
{
    public partial class Congvandi : Form
    {
        private QLVBEntities1 db = new QLVBEntities1();
        private List<tblCVdi> listCV;
        private int ItemPerPage = 2;
        private int CurrentPageIndex = 1;
        private int TotalPageIndex;
        private DBHelper helper = new DBHelper();
        private List<ParseObjectCVDi> listParseObject;
        private string ipmain = "";
        public Congvandi()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            ipmain = System.Configuration.ConfigurationManager.AppSettings["IPMain"].ToString();
        }
        private void setNull()
        {
            txtSoCV.Text = string.Empty;
            txtNoinhan.Text = string.Empty;
            dpNgaygui.Value = DateTime.Now;
            txtGhichu.Text = string.Empty;
            txtNoidung.Text = string.Empty;
            txtAnhScan.Text = "";
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSoCV.Text))
            {
                var cv = db.tblCVdis.Where(m => m.STT.ToString().ToLower() == txtSoCV.Text.ToLower()).FirstOrDefault();
                if (cv != null)
                {
                    MessageBox.Show(this, "Số công văn này đã tồn tại", "lỗi");
                }
                else
                {
                    try
                    {
                        var path = txtAnhScan.Text.Replace(@"T:", "");
                        var folderPath = @"\\SERVER-NC\doi 1$\DU LIEU SCAN" + path;
                        var newCV = new tblCVdi
                        {
                            socongvan = txtSoCV.Text,
                            ngaygui = dpNgaygui.Value,
                            anhscan = folderPath,
                            ghichu = txtGhichu.Text,
                            noidung = txtNoidung.Text,
                            noinhan = txtNoinhan.Text,
                        };
                        db.tblCVdis.Add(newCV);
                        db.SaveChanges();
                        MessageBox.Show(this, "Thêm mới thành công", "Success!");
                        ReloadData();
                        setNull();
                    }
                    catch
                    {
                        MessageBox.Show(this, "Không thể thêm mới", "lỗi");
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Không thể để trống số công văn", "lỗi");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lblID.Text))
            {
                var item = db.tblCVdis.Where(m => m.STT.ToString() == lblID.Text).FirstOrDefault();
                if (item != null)
                {
                    var path = txtAnhScan.Text.Replace(@"T:", "");
                    var folderPath = @"\\SERVER-NC\doi 1$\DU LIEU SCAN" + path;

                    item.anhscan = folderPath;
                    item.ngaygui = dpNgaygui.Value;
                    item.noidung = txtNoidung.Text;
                    item.ghichu = txtGhichu.Text;
                    item.socongvan = txtSoCV.Text;
                    item.noinhan = txtNoinhan.Text;
                    db.tblCVdis.AddOrUpdate(item);
                    db.SaveChanges();
                    MessageBox.Show(this, "Update thành công", "Success!");
                    ReloadData();
                }
            }
            else
            {
                MessageBox.Show(this, "Chưa chọn bản ghi nào", "lỗi");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lblID.Text))
            {
                var item = db.tblCVdis.Where(m => m.STT.ToString() == lblID.Text).FirstOrDefault();
                if (item != null)
                {
                    db.tblCVdis.Remove(item);
                    db.SaveChanges();
                    MessageBox.Show(this, "Xóa thành công", "Success!");
                    ReloadData();
                    setNull();
                }
            }
            else
            {
                MessageBox.Show(this, "Chưa chọn bản ghi nào", "lỗi");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            setNull();
            dataGridView1.ClearSelection();
            lblID.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var query = txtSearchBox.Text;
            var searchId = cbSearchBox.SelectedValue.ToString();
            if (searchId == "1")
                listCV = db.tblCVdis.Where(m => m.noidung.ToLower().Contains(query.ToLower()) == true).ToList();
            else
                listCV = db.tblCVdis.Where(m => m.socongvan.ToLower().Contains(query.ToLower()) == true).ToList();
            CurrentPageIndex = 1;
            if (listCV.Count % ItemPerPage == 0)
                TotalPageIndex = listCV.Count / ItemPerPage;
            else
                TotalPageIndex = listCV.Count / ItemPerPage + 1;
            listCV = listCV.OrderByDescending(m => m.ngaygui).ToList();
            listParseObject = helper.ConvertObjectCVDi(listCV);
            dataGridView1.DataSource = listParseObject.Take(ItemPerPage).ToList();
            btnPrev.Enabled = false;
            if (CurrentPageIndex == TotalPageIndex)
                btnNext.Enabled = false;
            else
                btnNext.Enabled = true;
            lblCurrentPage.Text = CurrentPageIndex.ToString();
            lblTotalPage.Text = TotalPageIndex.ToString();
        }
        #region[Xem anh]
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dia = new FolderBrowserDialog();
            dia.RootFolder = Environment.SpecialFolder.Desktop;
            dia.Description = "+++Select Folder+++";
            dia.ShowNewFolderButton = true;
            if (dia.ShowDialog() == DialogResult.OK)
            {
                txtAnhScan.Text = dia.SelectedPath;
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            if (opf.ShowDialog() == DialogResult.OK)
            {
                txtAnhScan.Text = opf.FileName;
            }
        }

        private void btnSeeImage_Click(object sender, EventArgs e)
        {
            try
            {
                var path = txtAnhScan.Text;
                if (!String.IsNullOrEmpty(path))
                {
                    Process.Start(path);
                }
                else
                    MessageBox.Show(this, "Đường dẫn ảnh trống không thể mở thư mục!", "Lỗi");
            }
            catch
            {
                MessageBox.Show(this, "Không thể mở thư mục!", "Lỗi");
            }
        }
        #endregion

        public static DataTable ToDataTable(List<CVDiExcel> items)
        {
            DataTable dataTable = new DataTable(typeof(CVDiExcel).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(CVDiExcel).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (CVDiExcel item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var tb = ToDataTable(helper.ConvertExcelCVDi(listParseObject));
            var temp = "";
            FolderBrowserDialog dia = new FolderBrowserDialog();
            dia.RootFolder = Environment.SpecialFolder.Desktop;
            dia.Description = "+++Select Folder+++";
            dia.ShowNewFolderButton = false;
            if (dia.ShowDialog() == DialogResult.OK)
            {
                var test = dia.SelectedPath;
                if (helper.IsLogicalDrive(test))
                    temp = dia.SelectedPath.Replace(@"\", "\\") + "cvden-" + DateTime.Now.ToString("ddMMyyyy-hhmmss") + ".xlsx";
                else
                    temp = dia.SelectedPath.Replace(@"\", "\\") + "\\cvden-" + DateTime.Now.ToString("ddMMyyyy-hhmmss") + ".xlsx";

                helper.WriteDataTableToExcel(tb, "Sheet 1", temp, "Thông tin chi tiết");
                FileInfo fi = new FileInfo(temp);
                if (fi.Exists)
                {
                    MessageBox.Show(this, "Tạo file" + temp + " thành công", "Ghi file thành công!!!");
                }
                else
                {
                    MessageBox.Show(this, "Không thể tạo file", "Lỗi!!!");
                }
            }
        }
        #region[Next & Prev Button]
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPageIndex > 1)
            {
                CurrentPageIndex = CurrentPageIndex - 1;
                if(CurrentPageIndex == 1)
                {
                    btnPrev.Enabled = false;
                    btnNext.Enabled = true;
                }
                else
                {
                    btnNext.Enabled = btnPrev.Enabled = true;
                }
                PaginateClick(CurrentPageIndex);

            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (CurrentPageIndex < TotalPageIndex)
            {
                CurrentPageIndex += 1;
                if(CurrentPageIndex == TotalPageIndex)
                {
                    btnNext.Enabled = false;
                    btnPrev.Enabled = true;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnPrev.Enabled = true;
                }
                PaginateClick(CurrentPageIndex);
            }
        }

        private void PaginateClick(int current)
        {
            var tempCV = listParseObject.Skip((current - 1) * ItemPerPage).Take(ItemPerPage).ToList();
            lblCurrentPage.Text = current.ToString();
            dataGridView1.DataSource = tempCV;
        }
        #endregion 



        private void Congvandi_Load(object sender, EventArgs e)
        {
            lblID.Visible = false;
            lblCurrentPage.Text = "1";
            lblTotalPage.Text = "1";
            ReloadData();
            var items = new[] {
                new { Text = "Tìm theo nội dung", Value = "1" },
                new { Text = "Tìm theo mã công văn", Value = "2" }
             };
            cbSearchBox.DisplayMember = "Text";
            cbSearchBox.ValueMember = "Value";
            cbSearchBox.DataSource = items;

            Application.EnableVisualStyles();
            txtSoCV.ShortcutsEnabled = txtNoidung.ShortcutsEnabled 
                = txtNoinhan.ShortcutsEnabled= txtGhichu.ShortcutsEnabled = txtAnhScan.ShortcutsEnabled;
        }
        private void ReloadData()
        {
            var tempCV = db.tblCVdis.ToList();
            CurrentPageIndex = 1;
            if (tempCV.Count % ItemPerPage == 0)
                TotalPageIndex = tempCV.Count / ItemPerPage;
            else
                TotalPageIndex = tempCV.Count / ItemPerPage + 1;
            listCV = tempCV.OrderByDescending(m => m.ngaygui).ToList();
            listParseObject = helper.ConvertObjectCVDi(listCV);
            dataGridView1.DataSource = listParseObject.Take(ItemPerPage).ToList();
            btnPrev.Enabled = false;
            if (CurrentPageIndex == TotalPageIndex)
                btnNext.Enabled = false;
            else
                btnNext.Enabled = true;
            lblCurrentPage.Text = CurrentPageIndex.ToString();
            lblTotalPage.Text = TotalPageIndex.ToString();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                setNull();
                if (dataGridView1.Rows[e.RowIndex].Cells["ID"].Value != null)
                {
                    lblID.Text = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                    var tempObject = db.tblCVdis.Where(m => m.STT.ToString() == lblID.Text).FirstOrDefault();
                    if (tempObject != null)
                    {
                        dpNgaygui.Value = Convert.ToDateTime(tempObject.ngaygui);
                        txtSoCV.Text = tempObject.socongvan;
                        txtNoidung.Text = tempObject.noidung;
                        txtGhichu.Text = tempObject.ghichu;
                        txtAnhScan.Text = tempObject.anhscan;
                        txtNoinhan.Text = tempObject.noinhan;
                    }
                }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            ReloadData();
        }
    }
}
