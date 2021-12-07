using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Quanlykithi.Models.Entities
{
    public partial class Phienthi
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public int? Kithiid { get; set; }
        public string Thoigianbatdau { get; set; }
        public string Thoigianketthuc { get; set; }
    }
}
