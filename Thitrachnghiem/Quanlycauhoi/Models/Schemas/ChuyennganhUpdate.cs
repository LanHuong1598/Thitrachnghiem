using System;
using System.Collections.Generic;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;

#nullable disable

namespace Thitrachnghiem.Quanlycauhoi.Models.Schemas
{
    public partial class ChuyennganhUpdate
    {
        public Guid Uuid { get; set; }

        public string Ten { get; set; }
        public int? Sobac { get; set; }
        public string Trinhdodaotao { get; set; }
    }
}
