using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Quanlykithi.Models.Entities
{
    public partial class Kithi
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Thoigianbatdau { get; set; }
        public string Thoigiankethuc { get; set; }
        public int? Chuyennganhid { get; set; }
        public int? Bac { get; set; }
        public string Trinhdodaotao { get; set; }
        public bool Status { get; set; }

  
    }
}
