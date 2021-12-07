using System;
using System.Collections.Generic;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;

#nullable disable

namespace Thitrachnghiem.Quanlycauhoi.Models.Schemas { 
    public partial class CautraloiGet
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public int? Cauhoiid { get; set; }
        public bool? Neo { get; set; }
        public bool? Ladapandung { get; set; }
        public string Noidung { get; set; }

        public CautraloiGet() { }
        public CautraloiGet(Cautraloi cautraloi)
        {
            this.Id = cautraloi.Id;
            this.Ladapandung = cautraloi.Trangthai;
            this.Neo = cautraloi.Neo;
            this.Noidung = cautraloi.Noidung;
            this.Uuid = cautraloi.Uuid;
            this.Cauhoiid = cautraloi.Cauhoiid;
        }
    }
}
