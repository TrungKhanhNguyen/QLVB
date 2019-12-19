using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
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
    public partial class Congvanden : Form
    {
        private QLVBEntities1 db = new QLVBEntities1();
        private List<tblCVden> listCV;
        private int ItemPerPage = 30;
        private int CurrentPageIndex = 1;
        private int TotalPageIndex;
        private DBHelper helper = new DBHelper();
        private List<ParseObject> listParseObject;
        private bool isHH = true;
        private bool isColor = true;
        private int outdate;
        private List<string> my_combobox = new List<string>();
        private string ipserver = "";
        private string ipmain = "";
        private List<CategoryVB> listCate = null;
        private List<CategoryVB> listCateSearch = null;
        public Congvanden()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            
            isColor = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["cbFlashColor"]);
            outdate = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["date"]);
            ipmain = System.Configuration.ConfigurationManager.AppSettings["IPMain"].ToString();
        }
        private void Congvanden_Load(object sender, EventArgs e)
        {
            string filePath = ConfigurationManager.AppSettings["Role"];
            if (filePath == "1")
            {
                btnAdd.Enabled = btnSave.Enabled = btnDelete.Enabled = btnReload.Enabled = false;
            }
            this.KeyPreview = true;
            lblID.Visible = false;
            lblCurrentPage.Text = "1";
            lblTotalPage.Text = "1";
            ReloadData();
            dpValidate.Value = DateTime.Now.AddDays(3);
            dpValidate.Enabled = false;
            cbValidate.Checked = false;
            cbSearchByDate.Enabled = true;
            var items = new[] {
                new { Text = "Tất cả các ngày", Value = "1" },
                new { Text = "Tìm theo khoảng ngày", Value = "2" },
                new { Text = "Tìm ngày chính xác", Value = "3" },
             };
            cbSearchByDate.DisplayMember = "Text";
            cbSearchByDate.ValueMember = "Value";
            cbSearchByDate.DataSource = items;

            listCate = db.CategoryVBs.ToList();

            cbCategory.DisplayMember = "NameCate";
            cbCategory.ValueMember = "Id";
            cbCategory.DataSource = listCate;

            listCateSearch = db.CategoryVBs.ToList();

            var itemSearch = new CategoryVB { Id = 0,NameCate="Tất cả" };
            listCateSearch.Insert(0,itemSearch);

            comboSearchCategory.DisplayMember = "NameCate";
            comboSearchCategory.ValueMember = "Id";
            comboSearchCategory.DataSource = listCateSearch;


            Application.EnableVisualStyles();
            txtSoCV.ShortcutsEnabled = txtAnhscan.ShortcutsEnabled 
                = txtGhichu.ShortcutsEnabled= txtNguoikyCV.ShortcutsEnabled 
                = txtNoidung.ShortcutsEnabled = txtNoiguiCV.ShortcutsEnabled 
                = txtYkienchidao.ShortcutsEnabled = true;
            btnNext.Enabled = btnPrev.Enabled = false;

            
   
        }

        private void ReloadData()
        {
            var tempCV = db.tblCVdens.OrderByDescending(m => m.ngaythang).Take(30).ToList();
            CurrentPageIndex = 1;
            if (tempCV.Count % ItemPerPage == 0)
                TotalPageIndex = tempCV.Count / ItemPerPage;
            else
                TotalPageIndex = tempCV.Count / ItemPerPage + 1;
            listCV = tempCV.OrderByDescending(m => m.ngaythang).ToList();
            listParseObject = helper.ConvertObject(listCV);
            dataGridView1.DataSource = listParseObject.Take(ItemPerPage).ToList();
            //dataGridView1.DataSource = listParseObject.ToList();
            btnPrev.Enabled = false;
            if (CurrentPageIndex == TotalPageIndex)
                btnNext.Enabled = false;
            else
                btnNext.Enabled = true;
            lblCurrentPage.Text = CurrentPageIndex.ToString();
            lblTotalPage.Text = TotalPageIndex.ToString();

            var today = DateTime.Today.Date;
            var currentDay = DateTime.Today.AddDays(outdate).Date;
            var listHH = db.tblCVdens.Where(m => m.Daxuly == false && m.ngayhethan != null && ((m.ngayhethan >= today && m.ngayhethan.Value <= currentDay) || (m.ngayhethan < today ) )).ToList();
            if (listHH != null && listHH.Count > 0)
            {
                isHH = true;
                lblNotify.Text = listHH.Count + " công văn sắp đến hạn";
            }
            else
            {
                lblNotify.Text = string.Empty;
                isHH = false;
            }
        }
        #region [Them/Sua/Xoa]
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSoCV.Text))
            {
                var cv = db.tblCVdens.Where(m => m.socongvan.ToLower() == txtSoCV.Text.ToLower()).FirstOrDefault();
                if (cv != null)
                {
                    MessageBox.Show(this, "Số công văn này đã tồn tại", "lỗi");
                }
                else{
                    try
                    {
                        var cateId = cbCategory.SelectedValue;
                        var path = txtAnhscan.Text.Replace(@"T:", "");
                        var folderPath = @"\\SERVER-NC\doi 1$\DU LIEU SCAN" + path;
                        var newCV = new tblCVden
                        {
                            ngaythang = dpNgaygui.Value,
                            socongvan = txtSoCV.Text,
                            noidung = txtNoidung.Text,
                            noigui = txtNoiguiCV.Text,
                            nguoiky = txtNguoikyCV.Text,
                            ngaychidao = dpNgayykienchidao.Value,
                            ykienchidao = txtYkienchidao.Text,
                            ghichu = txtGhichu.Text,
                            anhscan = folderPath,
                            Daxuly = cbIsDone.Checked,
                            CategoryId = cateId.ToString()
                        };
                        if (cbValidate.Checked)
                            newCV.ngayhethan = dpValidate.Value;
                        newCV.nguoixuly = txtNguoixuly.Text;

                        db.tblCVdens.Add(newCV);
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
                var item = db.tblCVdens.Where(m => m.STT.ToString() == lblID.Text).FirstOrDefault();
                if (item != null)
                {
                    var path = txtAnhscan.Text.Replace(@"T:", "");
                    var folderPath = @"\\SERVER-NC\doi 1$\DU LIEU SCAN" + path;

                    item.ngaythang = dpNgaygui.Value;
                    item.socongvan = txtSoCV.Text;
                    item.noidung = txtNoidung.Text;
                    item.noigui = txtNoiguiCV.Text;
                    item.nguoiky = txtNguoikyCV.Text;
                    item.ngaychidao = dpNgayykienchidao.Value;
                    item.ykienchidao = txtYkienchidao.Text;
                    item.ghichu = txtGhichu.Text;
                    item.anhscan = txtAnhscan.Text;
                    item.Daxuly = cbIsDone.Checked;
                    item.CategoryId = cbCategory.SelectedValue.ToString();
                    if (cbValidate.Checked)
                        item.ngayhethan = dpValidate.Value;
                    else
                        item.ngayhethan = null;
                    item.nguoixuly = txtNguoixuly.Text;
                    db.tblCVdens.AddOrUpdate(item);
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
                var item = db.tblCVdens.Where(m => m.STT.ToString() == lblID.Text).FirstOrDefault();
                if (item != null)
                {
                    db.tblCVdens.Remove(item);
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
        #endregion
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var columnHeader = dataGridView1.Columns[e.ColumnIndex].HeaderText;
                if (columnHeader == "Ảnh")
                {
                    try
                    {
                        var folderPath = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Replace(@"\", "\\");
                        Process.Start(folderPath);
                    }
                    catch
                    {
                        MessageBox.Show(this, "Không thể mở thư mục!", "Lỗi");
                    }
                }
                else
                {
                    setNull();
                    if (dataGridView1.Rows[e.RowIndex].Cells["ID"].Value != null)
                    {
                        lblID.Text = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                        var temp = db.tblCVdens.Where(m => m.STT.ToString() == lblID.Text).FirstOrDefault();
                        if (temp != null)
                        {
                            if (temp.ngayhethan != null)
                            {
                                dpValidate.Value = Convert.ToDateTime(temp.ngayhethan);
                                cbValidate.Checked = true;
                            }
                            else
                                cbValidate.Checked = false;
                            if (temp.ngaythang != null)
                                dpNgaygui.Value = Convert.ToDateTime(temp.ngaythang);
                            if (temp.socongvan != null)
                                txtSoCV.Text = temp.socongvan;
                            if (temp.noigui != null)
                                txtNoiguiCV.Text = temp.noigui;
                            if (temp.nguoiky != null)
                                txtNguoikyCV.Text = temp.nguoiky;
                            if (temp.noidung != null)
                                txtNoidung.Text = temp.noidung;
                            if (temp.ykienchidao != null)
                                txtYkienchidao.Text = temp.ykienchidao;
                            if (temp.ngaychidao != null)
                                dpNgayykienchidao.Value = Convert.ToDateTime(temp.ngaychidao);
                            if (temp.ghichu != null)
                                txtGhichu.Text = temp.ghichu;
                            if (temp.anhscan != null)
                                txtAnhscan.Text = temp.anhscan;
                            if (temp.Daxuly != null)
                                cbIsDone.Checked = Convert.ToBoolean(temp.Daxuly);
                            txtNguoixuly.Text = temp.nguoixuly;
                            var danhmucId = temp.CategoryId;
                            var x = listCate.Where(m => m.Id.ToString() == danhmucId).FirstOrDefault();
                            if (x != null)
                            {
                                cbCategory.SelectedIndex = cbCategory.FindStringExact(x.NameCate);
                            }
                            else
                                cbCategory.SelectedIndex = 0;
                        }
                    }
                }
            }
        }
        private void setNull()
        {
            txtSoCV.Text = string.Empty;
            txtNoiguiCV.Text = string.Empty;
            dpNgaygui.Value = DateTime.Now;
            dpNgayykienchidao.Value = DateTime.Now;
            txtGhichu.Text = string.Empty;
            txtNguoikyCV.Text = string.Empty;
            txtNoidung.Text = string.Empty;
            txtGhichu.Text = string.Empty;
            txtAnhscan.Text = "";
            txtYkienchidao.Text = string.Empty;
            dpValidate.Value = DateTime.Now.AddDays(3);
            cbValidate.Checked = false;
            txtNguoixuly.Text = string.Empty;
            cbCategory.SelectedIndex = 0;
        }
        #region [Image Behavior]
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dia = new FolderBrowserDialog();
            dia.RootFolder = Environment.SpecialFolder.Desktop;
            dia.Description = "+++Select Folder+++";
            dia.ShowNewFolderButton = true;
            if (dia.ShowDialog() == DialogResult.OK)
            {
                txtAnhscan.Text = dia.SelectedPath;
            }
        }
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            if (opf.ShowDialog() == DialogResult.OK)
            {
                txtAnhscan.Text = opf.FileName;
            }
        }
        private void btnSeeImage_Click(object sender, EventArgs e)
        {
            try
            {
                Process myProcess = new Process();
                var path = txtAnhscan.Text;
                if (!String.IsNullOrEmpty(path))
                {
                    Process.Start(path);
                }
                else
                    MessageBox.Show(this, "Đường dẫn ảnh trống không thể mở thư mục!", "Lỗi");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Không thể mở thư mục!", "Lỗi");
            }
        }
        #endregion

        #region [Pagination]
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPageIndex > 1)
            {
                CurrentPageIndex = CurrentPageIndex - 1;
                PaginateClick(CurrentPageIndex);
            }
            
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (CurrentPageIndex < TotalPageIndex)
            {
                CurrentPageIndex +=1;
                PaginateClick(CurrentPageIndex);
            }
        }
        private void PaginateClick(int current)
        {
            var tempCV = listParseObject.Skip((current-1) * ItemPerPage).Take(ItemPerPage).ToList();
            if (current == TotalPageIndex)
            {
                btnNext.Enabled = false;
                btnPrev.Enabled = true;
            }
            else if (current == 1)
            {
                btnPrev.Enabled = false;
                btnNext.Enabled = true;
            }
            else
            {
                btnNext.Enabled = btnPrev.Enabled = true;
            }
            lblCurrentPage.Text = current.ToString();
            dataGridView1.DataSource = tempCV;
        }
        #endregion

        public string ToUnicodeString(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var c in str)
            {
                sb.Append("\\u" + ((int)c).ToString("X4"));
            }
            return sb.ToString();
        }

        private void BindToGrid(List<tblCVden> listden)
        {
            CurrentPageIndex = 1;
            if (listden.Count % ItemPerPage == 0)
                TotalPageIndex = listden.Count / ItemPerPage;
            else
                TotalPageIndex = listden.Count / ItemPerPage + 1;
            listden = listCV.OrderByDescending(m => m.ngaythang).ToList();
            listParseObject = helper.ConvertObject(listden);
            //var bs = new BindingSource();
            //bs.DataSource = listParseObject.Take(ItemPerPage).ToList();
            dataGridView1.DataSource = listParseObject.Take(ItemPerPage).ToList();
            btnPrev.Enabled = false;
            if (CurrentPageIndex == TotalPageIndex)
                btnNext.Enabled = false;
            else
                btnNext.Enabled = true;
            lblCurrentPage.Text = CurrentPageIndex.ToString();
            lblTotalPage.Text = TotalPageIndex.ToString();
            
        }
       

        private void btnReset_Click(object sender, EventArgs e)
        {
            setNull();
            dataGridView1.ClearSelection();
            lblID.Text = "";
        }

        public static DataTable ToDataTable(List<CvDenExcel> items)
        {
            DataTable dataTable = new DataTable(typeof(CvDenExcel).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(CvDenExcel).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (CvDenExcel item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var tb = ToDataTable(helper.ConvertExcelCVDen(listParseObject));
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
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Lỗi!!!");
            }
            
        }
        private void cbValidate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbValidate.Checked)
                dpValidate.Enabled = true;
            else
                dpValidate.Enabled = false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isHH == true)
            {
                lblNotify.Visible = true;
                if (isColor)
                {
                    if (lblNotify.ForeColor != Color.Blue)
                        lblNotify.ForeColor = Color.Blue;
                    else
                        lblNotify.ForeColor = Color.Red;
                }
                else
                {
                    lblNotify.ForeColor = Color.Red;
                }
            }
            else
            {
                lblNotify.Visible = false;
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void lblNotify_DoubleClick(object sender, EventArgs e)
        {
            
        }


        private void btnSearch2_Click(object sender, EventArgs e)
        {
            listCV = new List<tblCVden>();
            var searchId = cbSearchByDate.SelectedValue.ToString();
            var searchCategoryValue = comboSearchCategory.SelectedValue.ToString();
            if (String.IsNullOrEmpty(txtSearchContent.Text))
            {
                if (String.IsNullOrEmpty(txtSearchByCode.Text))
                {
                    if (searchCategoryValue != "0")
                        listCV = db.tblCVdens.Where(m => m.CategoryId == searchCategoryValue).ToList();
                    else
                        listCV = db.tblCVdens.ToList();
                    switch (searchId)
                    {
                        case "2":
                            if(listCV !=null && listCV.Count >0)
                                listCV = listCV.Where(m => (m.ngaythang.Value.Date >= dpSearchFrDate.Value.Date) && (m.ngaythang <= dpSearchToDate.Value.Date)).ToList();
                            break;
                        case "3":
                            if(listCV !=null && listCV.Count > 0)
                                listCV = listCV.Where(m => m.ngaythang.Value.Date == dpSearchFrDate.Value.Date).ToList();
                            break;
                    }
                }
                else
                {
                    var code = txtSearchByCode.Text.ToLower();
                    if (chkExact.Checked)
                        listCV = db.tblCVdens.Where(m => m.socongvan.ToLower() == code).ToList();
                    else
                        listCV = db.tblCVdens.Where(m => m.socongvan.ToLower().Contains(code) == true).ToList();

                    if (listCV != null && listCV.Count > 0)
                    {
                        if(searchCategoryValue!="0")
                            listCV = listCV.Where(m => m.CategoryId == searchCategoryValue).ToList();
                    }
                        
                    switch (searchId)
                    {
                        case "2":
                            if(listCV!=null && listCV.Count > 0)
                                listCV = listCV.Where(m => m.ngaythang.Value.Date >= dpSearchFrDate.Value && m.ngaythang.Value.Date <= dpSearchToDate.Value).ToList();
                            break;
                        case "3":
                            if (listCV != null && listCV.Count > 0)
                                listCV = listCV.Where(m => m.ngaythang.Value.Date == dpSearchFrDate.Value).ToList();
                            break;
                    }
                }
            }
            else
            {
                if (chkExact.Checked)
                {
                    listCV = db.tblCVdens.Where(m => m.noidung.ToLower().Contains(txtSearchContent.Text.ToLower()) == true).ToList();
                }
                else
                {
                    string[] splitResult = txtSearchContent.Text.Split(' ');
                    foreach (var item in splitResult)
                    {
                        var tempList = db.tblCVdens.Where(m => m.noidung.ToLower().Contains(item.ToLower()) == true).ToList();
                        foreach (var tempItem in tempList)
                        {
                            if (listCV.IndexOf(tempItem) < 0)
                            {
                                listCV.Add(tempItem);
                            }
                        }
                    }
                }
                
                if (!String.IsNullOrEmpty(txtSearchByCode.Text))
                {
                    if (listCV != null && listCV.Count > 0)
                    {
                        if (chkExact.Checked)
                            listCV = listCV.Where(m => m.socongvan.ToLower() == txtSearchByCode.Text).ToList();
                        else
                            listCV = listCV.Where(m => m.socongvan.ToLower().Contains(txtSearchByCode.Text.ToLower()) == true).ToList();
                    }
                }
                if (listCV != null && listCV.Count > 0)
                {
                    if (searchCategoryValue != "0")
                        listCV = listCV.Where(m => m.CategoryId == searchCategoryValue).ToList();
                }

                switch (searchId)
                {
                    case "2":
                        if (listCV != null && listCV.Count > 0)
                            listCV = listCV.Where(m => m.ngaythang.Value.Date >= dpSearchFrDate.Value && m.ngaythang.Value.Date <= dpSearchToDate.Value).ToList();
                        break;
                    case "3":
                        if (listCV != null && listCV.Count > 0)
                            listCV = listCV.Where(m => m.ngaythang.Value.Date == dpSearchFrDate.Value).ToList();
                        break;
                }
            }
            BindToGrid(listCV);
        }

        private void cbSearchByDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbSearchByDate.SelectedValue.ToString())
            {
                case "2":
                    dpSearchFrDate.Enabled = dpSearchToDate.Enabled = true;
                    break;
                case "3":
                    dpSearchFrDate.Enabled = true;
                    dpSearchToDate.Enabled = false;
                    break;
                default:
                    dpSearchFrDate.Enabled = dpSearchToDate.Enabled = false;
                    break;
            }
        }

        private void Congvanden_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Alt == true && e.KeyCode == Keys.D)
            {
                btnOpenFolder.PerformClick();
            }
            if (e.Alt == true && e.KeyCode == Keys.F)
            {
                btnOpenFile.PerformClick();
            }
            if(e.Alt == true && e.KeyCode == Keys.V)
            {
                btnSeeImage.PerformClick();
            }
            if (e.Alt == true && e.KeyCode == Keys.S)
            {
                txtSearchContent.Focus();
            }
            if(e.Alt == true && e.KeyCode == Keys.E )
            {
                dataGridView1.Focus();
                if(dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows[0].Selected = true;
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch2.PerformClick();
            }
        }

        

        //private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        var columnHeader = dataGridView1.Columns["Anh"].HeaderText;
        //        if (columnHeader == "Ảnh")
        //        {
        //            try
        //            {
        //                var folderPath = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Replace(@"\", "\\");
        //                Process.Start(folderPath);
        //            }
        //            catch
        //            {
        //                MessageBox.Show(this, "Không thể mở thư mục!", "Lỗi");
        //            }
        //        }
        //    }
        //}

        //private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        var columnHeader = dataGridView1.Columns["Anh"].HeaderText;
        //        if (columnHeader == "Ảnh")
        //        {
        //            try
        //            {
        //                var folderPath = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Replace(@"\", "\\");
        //                Process.Start(folderPath);
        //            }
        //            catch
        //            {
        //                MessageBox.Show(this, "Không thể mở thư mục!", "Lỗi");
        //            }
        //        }
        //    }
        //}
    }
}
