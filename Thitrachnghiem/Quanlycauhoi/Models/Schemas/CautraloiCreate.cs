using System;
using System.Collections.Generic;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;

#nullable disable

namespace Thitrachnghiem.Quanlycauhoi.Models.Schemas { 
    public partial class CautraloiCreate
    {
        public bool? Neo { get; set; }
        public bool? Ladapandung { get; set; }
        public string Noidung { get; set; }

        public Cautraloi convert()
        {
            Cautraloi result = new Cautraloi();

            result.Trangthai = this.Ladapandung;
            result.Neo = this.Neo;
            result.Noidung = this.Noidung;
            return result;
        }
    }
}
