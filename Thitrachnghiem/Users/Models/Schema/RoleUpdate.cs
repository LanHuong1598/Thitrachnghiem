using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem.Users.Models.Schema
{
    public class RoleUpdate
    {
        public Guid? Uuid { get; set; }
        public string Ten { get; set; }
        public string Mota { get; set; }
        public string Dsquyen { get; set; }
    }
}
