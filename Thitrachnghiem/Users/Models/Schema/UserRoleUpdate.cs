using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem.Users.Models.Schema
{
    public class UserRoleUpdate
    {
        public Guid Useruuid { get; set; }
        public List<Guid> Roleuuids { get; set; }
    }
}
