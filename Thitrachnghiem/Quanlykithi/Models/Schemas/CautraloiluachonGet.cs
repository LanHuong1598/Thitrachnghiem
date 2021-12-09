using System;
using System.Collections.Generic;
using Thitrachnghiem.Quanlykithi.Models.Entities;

#nullable disable

namespace Thitrachnghiem.Quanlykithi.Models.Schemas { 
    public partial class CautraloiluachonGet
    {
        public Guid? Uuid { get; set; }
        public bool? Ladapandung { get; set; }
        public string Noidung { get; set; }
        public bool? Duocchon { get; set; }
    }
}
