using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Quanlykithi.Models.Entities
{
    public partial class PhienthiThisinh
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public int? Phienthiid { get; set; }
        public int? Thisinhid { get; set; }
        public string Made { get; set; }
        public string Thoigianbatdau { get; set; }
        public string Thoigianketthuc { get; set; }
    }
}
