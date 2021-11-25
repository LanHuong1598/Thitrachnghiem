using Thitrachnghiem.Users.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem.Users.Models.Schema
{
    public class RoleGet
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Ten { get; set; }
        public string Mota { get; set; }
        public string Dsquyen { get; set; }

        public RoleGet()
        {

        }

        public RoleGet(Role donvi)
        {
            this.Id = donvi.Id;
            this.Ten = donvi.Ten;
            this.Uuid = donvi.Uuid;            
        }
    }
}
