using System;
using System.Collections.Generic;
using Thitrachnghiem.Quanlykithi.Models.Entities;
using Thitrachnghiem.Quanlykithi.Models.Schemas;

#nullable disable

namespace Thitrachnghiem.Quanlykithi.Models.Schemas
{
    public partial class KithiUpdate
    {
        public Guid Uuid { get; set; }
        public string Thoigianbatdau { get; set; }
        public string Thoigiankethuc { get; set; }
        public int? Bac { get; set; }
        public Guid ChuyennganhGuid { get; set; }
        public string Trinhdodaotao { get; set; }
        public List<MatrandethiCreate> Matrandethis { get; set; }
      
    }
}
