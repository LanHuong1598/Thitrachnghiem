using System;
using System.Collections.Generic;
using Thitrachnghiem.Quanlycauhoi.Models.Schemas;

#nullable disable

namespace Thitrachnghiem.Quanlykithi.Models.Schemas
{
    public partial class CautraloiThisinh
    {
        public Guid Cauhoiuuid { get; set; }        
        public Guid Cautraloiuuid { get; set; }
        public Guid Dethiuuid { get; set; }

        public bool Status { get; set; }

    }
}
