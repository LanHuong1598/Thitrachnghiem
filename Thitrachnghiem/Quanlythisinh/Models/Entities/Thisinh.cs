using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Quanlythisinh.Models.Entities
{
    public partial class Thisinh
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Donvi { get; set; }
        public string Chuyennganhhoc { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? Chuyennganhthiid { get; set; }
        public string Trinhdodaotao { get; set; }
        public int? Bacdanggiu { get; set; }
        public int? Bacthi { get; set; }
        public int? Kithiid { get; set; }
        public string Capbac { get; set; }
        public string Chucvu { get; set; }
        public bool? Status { get; set; }
        public string Namsinh { get; set; }
        public string Trinhdo { get; set; }
        public string Bacluong { get; set; }
    }
}
