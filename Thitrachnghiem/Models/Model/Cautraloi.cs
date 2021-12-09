using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Models.Model
{
    public partial class Cautraloi
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public int? Cauhoiid { get; set; }
        public bool? Neo { get; set; }
        public bool? Trangthai { get; set; }
        public string Noidung { get; set; }
        public bool? Status { get; set; }
    }
}
