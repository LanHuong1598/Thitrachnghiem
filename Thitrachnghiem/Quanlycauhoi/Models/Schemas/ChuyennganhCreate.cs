using System;
using System.Collections.Generic;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;

#nullable disable

namespace Thitrachnghiem.Quanlycauhoi.Models.Schemas
{
    public partial class ChuyennganhCreate
    {
   
        public string Ten { get; set; }
        public int? Sobac { get; set; }
        public string Trinhdodaotao { get; set; }

        public Chuyennganh convert()
        {
            Chuyennganh result = new Chuyennganh();
            result.Sobac = this.Sobac;
            result.Ten = this.Ten;
            result.Trinhdodaotao = this.Trinhdodaotao;
            return result;
        }
    }
}
