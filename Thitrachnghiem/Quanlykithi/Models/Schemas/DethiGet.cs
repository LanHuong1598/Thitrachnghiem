using System;
using System.Collections.Generic;
using Thitrachnghiem.Quanlycauhoi.Models.Schemas;

#nullable disable

namespace Thitrachnghiem.Quanlykithi.Models.Schemas
{
    public partial class DethiGet
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Kithiuuid { get; set; }
        public string Madethi { get; set; }
        public int? Bac { get; set; }
        public string Chuyennganh { get; set; }
        public string Trinhdodaotao { get; set; }
        public List<CauhoiGet> Cauhois { get; set; }
    }
}
