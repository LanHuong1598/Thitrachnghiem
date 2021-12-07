using System;
using System.Collections.Generic;
using Thitrachnghiem.Quanlykithi.Models.Entities;
using Thitrachnghiem.Quanlykithi.Models.Schemas;

#nullable disable

namespace Thitrachnghiem.Quanlykithi.Models.Schemas
{
    public partial class KithiCreate
    {
        public string Thoigianbatdau { get; set; }
        public string Thoigianketthuc { get; set; }
        public int? Bac { get; set; }
        public Guid ChuyennganhGuid { get; set; }
        public string Trinhdodaotao { get; set; }
        public List<MatrandethiCreate> Matrandethis { get; set; }
        public Kithi Convert()
        {
            Kithi kithi = new Kithi();
            kithi.Bac = this.Bac;
            kithi.Thoigianbatdau = this.Thoigianbatdau;
            kithi.Thoigianketthuc = this.Thoigianketthuc;
            kithi.Status = true;
            return kithi;
        }
    }
}
