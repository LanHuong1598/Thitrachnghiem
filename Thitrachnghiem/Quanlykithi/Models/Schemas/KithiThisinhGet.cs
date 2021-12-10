using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Quanlykithi.Models.Schemas
{
    public partial class KithiThisinhGet
    {
        public string Thoigianbatdau { get; set; }
        public string Thoigianketthuc { get; set; }
        public string Made { get; set; }
        public string Dethiuuid { get; set; }
        public string Tenthisinh { get; set; }
        public string Email { get; set; }
        public int? Diem { get; set; }

    }
}
