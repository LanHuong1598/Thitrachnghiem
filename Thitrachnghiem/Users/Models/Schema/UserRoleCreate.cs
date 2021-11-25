using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem.Users.Models.Schema
{
    public class UserRoleCreate
    {
        public Guid  Useruuid { get; set; }
        public Guid  Roleuuid { get; set; }
    }
}
