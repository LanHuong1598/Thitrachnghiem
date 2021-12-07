using System;
using System.Collections.Generic;
using Thitrachnghiem.Quanlykithi.Models.Schemas;

#nullable disable

namespace Thitrachnghiem.Quanlykithi.Models.Schemas
{
    public partial class KithiDetail
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Thoigianbatdau { get; set; }
        public string Thoigianketthuc { get; set; }
        public int? Bac { get; set; }
        public Guid ChuyennganhGuid { get; set; }
        public string Chuyennganh { get; set; }
        public string Trinhdodaotao { get; set; }
        public List<MatrandethiGet> Matrandethis { get; set; } 
    }
}
