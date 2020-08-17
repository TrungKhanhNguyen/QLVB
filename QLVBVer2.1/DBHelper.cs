using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace QLVBVer2._1
{
    public class DBHelper
    {

        public string RemoveVietnameseTone(string text)
        {
            string result = text.ToLower();
            result = Regex.Replace(result, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
            result = Regex.Replace(result, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
            result = Regex.Replace(result, "ì|í|ị|ỉ|ĩ|/g", "i");
            result = Regex.Replace(result, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
            result = Regex.Replace(result, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
            result = Regex.Replace(result, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
            result = Regex.Replace(result, "đ", "d");
            return result;
        }
        public List<CVDiExcel> ConvertExcelCVDi(List<ParseObjectCVDi> listcvDi)
        {
            var listDi = new List<CVDiExcel>();
            foreach(var item in listcvDi)
            {
                var temp = new CVDiExcel
                {
                    NewSTT = item.NewSTT,
                    anhscan = item.anhscan,
                    ghichu = item.ghichu,
                    ngaygui = Convert.ToDateTime(item.ngaygui).ToString("dd-MM-yyyy"),
                    noidung = item.noidung,
                    noinhan = item.noinhan,
                    socongvan = item.socongvan
                };
                listDi.Add(temp);
            }
            return listDi;
        }
        public List<CvDenExcel> ConvertExcelCVDen(List<ParseObject> listcvDen)
        {
            var listDen = new List<CvDenExcel>();
            foreach (var item in listcvDen)
            {
                var temp = new CvDenExcel();
                temp.NewSTT = item.NewSTT;
                temp.ghichu = item.ghichu;
                temp.anhscan = item.anhscan;
                temp.Daxuly = item.Daxuly;
                if(item.ngaychidao!=null)
                    temp.ngaychidao = Convert.ToDateTime(item.ngaychidao).ToString("dd-MM-yyyy");
                if (item.ngayhethan != null)
                    temp.ngayhethan = Convert.ToDateTime(item.ngayhethan).ToString("dd-MM-yyyy");
                temp.ngaythang = Convert.ToDateTime(item.ngaythang).ToString("dd-MM-yyyy");
                if (item.ngaynhap != null)
                    temp.ngaynhap = Convert.ToDateTime(item.ngaynhap).ToString("dd-MM-yyyy");
                temp.nguoiky = item.nguoiky;
                temp.noidung = item.noidung;
                temp.noigui = item.noigui;
                temp.socongvan = item.socongvan;
                temp.ykienchidao = item.ykienchidao;
                temp.nguoixuly = item.nguoixuly;
                listDen.Add(temp);
            }
            return listDen;
        }

        public List<ParseObjectCVDi> ConvertObjectCVDi(List<tblCVdi> cvDi)
        {
            var listTempObject = new List<ParseObjectCVDi>();
            var count = 1;
            foreach (var item in cvDi)
            {
                var tempObject = new ParseObjectCVDi
                {
                    NewSTT = count,
                    Id = item.STT,
                    socongvan = item.socongvan,
                    ngaygui = item.ngaygui,
                    noidung = item.noidung,
                    noinhan = item.noinhan,
                    ghichu = item.ghichu,
                    anhscan = item.anhscan
                };
                listTempObject.Add(tempObject);
                count++;
            }
            return listTempObject;
        }

        public List<ParseObject> ConvertObject(List<tblCVden> cvDen)
        {
            var listTempObject = new List<ParseObject>();
            var count = 1;
            foreach(var item in cvDen)
            {
                var tempObject = new ParseObject
                {
                    NewSTT = count,
                    Id = item.STT,
                    ngaythang = item.ngaythang,
                    socongvan = item.socongvan,
                    noidung = item.noidung,
                    noigui = item.noigui,
                    nguoiky = item.nguoiky,
                    ngaychidao = item.ngaychidao,
                    ykienchidao = item.ykienchidao,
                    ghichu = item.ghichu,
                    anhscan = item.anhscan,
                    ngayhethan = item.ngayhethan,
                    Daxuly = Convert.ToBoolean(item.Daxuly),
                    nguoixuly = item.nguoixuly,
                    CategoryId = item.CategoryId,
                    ngaynhap = item.ngaynhap
                };
                listTempObject.Add(tempObject);
                count++;
            }
            return listTempObject;
        }

        public string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        //public List<ParseObject> ConvertDateObject(List<ParseObject> listObject)
        //{
        //    var listTempObject = new List<ParseObject>();
        //    foreach(var item in listObject)
        //    {
        //        try
        //        {
        //            var tempObject = new ParseObject
        //            {
        //                Anh = item.Anh,
        //                Ngaychidao = DateTime.ParseExact(item.Ngaychidao, "yyyy-MM-dd",null).ToString("dd-MM-yyyy"),
        //                Ngaythang = DateTime.ParseExact(item.Ngaythang, "yyyy-MM-dd", null).ToString("dd-MM-yyyy"),
        //                NguoikyCV = item.NguoikyCV,
        //                Noidung = item.Noidung,
        //                Noigui = item.Noigui,
        //                SoCV = item.SoCV,
        //                Ghichu = item.Ghichu,
        //                STT = item.STT,
        //                Ykienchidao = item.Ykienchidao
        //            };
        //            listTempObject.Add(tempObject);
        //        }
        //        catch
        //        {
        //        }
        //    }
        //    return listTempObject;
        //}

        public bool IsLogicalDrive(string path)
        {
            return Directory.GetLogicalDrives().Contains(path);
        }

        #region[In excel]

        //Ví dụ gọi hàm viết excel
        //WriteDataTableToExcel(table,"Sheet1",@"C:\Windows\abc.xlsx","Details");
        public bool WriteDataTableToExcel(System.Data.DataTable dataTable, string worksheetName, string saveAsLocation, string ReporType)
        {
            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook excelworkBook;
            Microsoft.Office.Interop.Excel.Worksheet excelSheet;
            Microsoft.Office.Interop.Excel.Range excelCellrange;

            try
            {
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // for making Excel visible
                excel.Visible = false;
                excel.DisplayAlerts = false;

                // Creation a new Workbook
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Workk sheet
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                excelSheet.Name = worksheetName;


                excelSheet.Cells[1, 1] = ReporType;
                excelSheet.Cells[1, 2] = "Date : " + DateTime.Now.ToString("dd/MM/yyyy");

                // loop through each row and add values to our sheet
                int rowcount = 2;

                foreach (DataRow datarow in dataTable.Rows)
                {
                    rowcount += 1;
                    for (int i = 1; i <= dataTable.Columns.Count; i++)
                    {
                        // on the first iteration we add the column headers
                        if (rowcount == 3)
                        {
                            excelSheet.Cells[2, i] = dataTable.Columns[i - 1].ColumnName;
                            excelSheet.Cells.Font.Color = System.Drawing.Color.Black;

                        }

                        excelSheet.Cells[rowcount, i] = datarow[i - 1].ToString();

                        //for alternate rows
                        if (rowcount > 3)
                        {
                            if (i == dataTable.Columns.Count)
                            {
                                if (rowcount % 2 == 0)
                                {
                                    excelCellrange = excelSheet.Range[excelSheet.Cells[rowcount, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                                    //FormattingExcelCells(excelCellrange, "#CCCCFF", System.Drawing.Color.Black, false);
                                }

                            }
                        }

                    }

                }

                // now we resize the columns
                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[rowcount, dataTable.Columns.Count]];
                excelCellrange.EntireColumn.AutoFit();
                Microsoft.Office.Interop.Excel.Borders border = excelCellrange.Borders;
                border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border.Weight = 2d;


                excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[2, dataTable.Columns.Count]];
                //FormattingExcelCells(excelCellrange, "#000099", System.Drawing.Color.White, true);

                //now save the workbook and exit Excel

                excelworkBook.SaveAs(saveAsLocation);
                excelworkBook.Close();
                excel.Quit();
                Marshal.ReleaseComObject(excelSheet);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                excelSheet = null;
                excelCellrange = null;
                excelworkBook = null;
            }

        }

        /// <summary>
        /// FUNCTION FOR FORMATTING EXCEL CELLS
        /// </summary>
        /// <param name="range"></param>
        /// <param name="HTMLcolorCode"></param>
        /// <param name="fontColor"></param>
        /// <param name="IsFontbool"></param>
        public void FormattingExcelCells(Microsoft.Office.Interop.Excel.Range range, string HTMLcolorCode, System.Drawing.Color fontColor, bool IsFontbool)
        {
            range.Interior.Color = System.Drawing.ColorTranslator.FromHtml(HTMLcolorCode);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(fontColor);
            if (IsFontbool == true)
            {
                range.Font.Bold = IsFontbool;
            }
        }
        #endregion

    }
}
