using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Users.Models.Entities
{
    public partial class Userrole
    {
        public int Id { get; set; }
        public int? Userid { get; set; }
        public int? Roleid { get; set; }
    }
}
