using System;
using System.Collections.Generic;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;

#nullable disable

namespace Thitrachnghiem.Quanlycauhoi.Models.Schemas { 
    public partial class CauhoiUpdate
    {
        public Guid Uuid { get; set; }
        public string Noidung { get; set; }
        public int? Bac { get; set; }
        public Guid ChuyennganhGuid { get; set; }
        public List<CautraloiCreate> Cautralois { get; set; }

        public Cauhoi convert()
        {
            Cauhoi result = new Cauhoi();
            result.Noidung = this.Noidung;
            result.Bac = this.Bac;
            return result;
        }
    }
}
