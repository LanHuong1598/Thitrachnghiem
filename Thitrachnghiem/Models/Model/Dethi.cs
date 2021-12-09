using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Models.Model
{
    public partial class Dethi
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public int? Kithiid { get; set; }
        public bool? Status { get; set; }
        public string Madethi { get; set; }
        public int? Thisinhid { get; set; }
        public string Thoigian { get; set; }
    }
}
