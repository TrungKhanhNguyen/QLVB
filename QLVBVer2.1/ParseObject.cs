using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLVBVer2._1
{
    public class ParseObject
    {
        public int NewSTT { get; set; }
        public int Id { get; set; }
        public DateTime? ngaythang { get; set; }
        public string socongvan { get; set; }
        public string noidung { get; set; }
        public string noigui { get; set; }
        public string nguoiky { get; set; }
        public DateTime? ngaychidao { get; set; }
        public string ykienchidao { get; set; }
        public string ghichu { get; set; }
        public string anhscan { get; set; }
        public DateTime? ngayhethan { get; set; }
        public bool Daxuly { get; set; }
        public string nguoixuly { get; set; }

        public string CategoryId { get; set; }
    }
}
