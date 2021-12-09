using System;
using System.Collections.Generic;
using Thitrachnghiem.Quanlycauhoi.Models.Schemas;

#nullable disable

namespace Thitrachnghiem.Quanlykithi.Models.Schemas
{
    public partial class ThisinhTraloiGet
    {
        public int Id { get; set; }
        public Guid Cautraloiuuid { get; set; }
        public string Cautraloi { get; set; }
        public string Thoigiantraloi { get; set; }
        public string Noidung { get; set; }
        public string Cauhoi { get; set; }
        public List<CautraloiGet> Cautralois { get; set; }
        public bool IsTrue { get; set; }

    }
}
