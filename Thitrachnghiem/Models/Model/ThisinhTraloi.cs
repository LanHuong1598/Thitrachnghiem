using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Models.Model
{
    public partial class ThisinhTraloi
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public int? Thisinhid { get; set; }
        public int? Cauhoiid { get; set; }
        public string Cautraloiid { get; set; }
        public string Thoigiantraloi { get; set; }
    }
}
