using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Quanlykithi.Models.Schemas
{
    public partial class KithiGet
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Thoigianbatdau { get; set; }
        public string Thoigiankethuc { get; set; }
        public int? Bac { get; set; }
        public Guid ChuyennganhGuid { get; set; }
        public string Chuyennganh { get; set; }
        public string Trinhdodaotao { get; set; }

    }
}
