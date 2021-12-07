using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Models.Model
{
    public partial class Chitietdethi
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public int? Dethiid { get; set; }
        public int? Cauhoiid { get; set; }
    }
}
