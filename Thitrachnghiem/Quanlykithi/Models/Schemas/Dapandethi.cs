using System;
using System.Collections.Generic;
using Thitrachnghiem.Quanlycauhoi.Models.Schemas;

#nullable disable

namespace Thitrachnghiem.Quanlykithi.Models.Schemas
{
    public partial class Dapandethi
    {
        public string Dethiuuid { get; set; }        
        public List<CautraloiThisinh> Cautralois { get; set; }
    }
}
