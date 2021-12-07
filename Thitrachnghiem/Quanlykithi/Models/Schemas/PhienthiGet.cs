using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Quanlykithi.Models.Schemas
{
    public partial class PhienthiGet
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Thoigianbatdau { get; set; }
        public string Thoigianketthuc { get; set; }
        public int? Bac { get; set; }
        public Guid ChuyennganhGuid { get; set; }
        public string Chuyennganh { get; set; }
        public string Trinhdodaotao { get; set; }
        public Guid Kithiuuid { get; set; }

    }
}
