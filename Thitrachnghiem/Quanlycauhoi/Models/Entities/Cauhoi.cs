using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Quanlycauhoi.Models.Entities
{
    public partial class Cauhoi
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Noidung { get; set; }
        public int? Bac { get; set; }
        public int? Idchuyennganh { get; set; }
        public string Trinhdodaotao { get; set; }
        public bool? Status { get; set; }
    }
}
