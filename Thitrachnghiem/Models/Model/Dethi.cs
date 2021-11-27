using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Models.Model
{
    public partial class Dethi
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public int? Matrandethiid { get; set; }
        public string Madethi { get; set; }
    }
}
