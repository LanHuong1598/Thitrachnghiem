using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Models.Model
{
    public partial class Kithi
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Thoigianbatdau { get; set; }
        public string Thoigianketthuc { get; set; }
        public int? Chuyennganhid { get; set; }
        public int? Bac { get; set; }
        public string Trinhdodaotao { get; set; }
        public bool? Status { get; set; }
        public bool? Dangthi { get; set; }
        public int? Socauhoi { get; set; }
    }
}
