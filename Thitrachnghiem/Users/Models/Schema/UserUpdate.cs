using Thitrachnghiem.Users.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem.Users.Models.Schema
{
    public class UserUpdate
    {
        public Guid Uuid { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Madonvi { get; set; }
        public string Chucvu { get; set; }
        public string Roleuuid { get; set; }




    }
}
