using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Quanlykithi.Models.Schemas { 
    public partial class MatrandethiGet
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public int? Bac { get; set; }
        public Guid ChuyennganhGuid { get; set; }
        public string Chuyennganh { get; set; }
        public string Trinhdodaotao { get; set; }
        public int? Tile { get; set; }
        public int? Kithiid { get; set; }
    }
}
