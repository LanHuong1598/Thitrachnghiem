using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Quanlycauhoi.Models.Schemas
{
    public partial class CauhoiGet
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Noidung { get; set; }
        public int? Bac { get; set; }
        public Guid ChuyennganhGuid { get; set; }
        public string Chuyennganh { get; set; }
        public string Trinhdodaotao { get; set; }
        public bool? Status { get; set; }
        public List<CautraloiGet> Cautralois { get; set; }
    }
}
