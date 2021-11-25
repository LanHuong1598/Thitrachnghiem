using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Quanlycauhoi.Models.Entities
{
    public partial class Chuyennganh
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Ten { get; set; }
        public int? Sobac { get; set; }
        public bool? Status { get; set; }
        public string Trinhdodaotao { get; set; }
    }
}
