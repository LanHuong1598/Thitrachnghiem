using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Models.Model
{
    public partial class Matrandethi
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public int? Chuyennganhid { get; set; }
        public int? Bac { get; set; }
        public int? Tile { get; set; }
        public int? KithiNganhthiId { get; set; }
        public string Trinhdodaotao { get; set; }
    }
}
