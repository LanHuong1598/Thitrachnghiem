using System;
using System.Collections.Generic;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;

#nullable disable

namespace Thitrachnghiem.Quanlycauhoi.Models.Schemas { 
    public partial class ListCauhoiCreate
    {
        public List<CauhoiCreate> Danhsachcauhoi { get; set; }
    }
}
