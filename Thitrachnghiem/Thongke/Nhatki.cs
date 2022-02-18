using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Thongke
{
    public partial class Nhatki
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public int? Userid { get; set; }
        public string Username { get; set; }
        public string Ten { get; set; }
        public string Thoigian { get; set; }
        public string Noidung { get; set; }
        public string Ip { get; set; }
    }
}
