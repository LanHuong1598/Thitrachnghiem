using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Users.Models.Entities
{
    public partial class Role
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Ten { get; set; }
        public string Mota { get; set; }
        public string Dsquyen { get; set; }
    }
}
