using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Quanlykithi.Models.Schemas { 
    public partial class MatrandethiCreate
    {     
        public int? Bac { get; set; }
        public Guid ChuyennganhGuid { get; set; }
        public string Trinhdodaotao { get; set; }
        public int? Tile { get; set; }
    }
}
