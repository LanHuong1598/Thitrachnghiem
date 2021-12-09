using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Models.Model
{
    public partial class Role
    {
        public Role()
        {
            Userroles = new HashSet<Userrole>();
        }

        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Ten { get; set; }
        public string Mota { get; set; }
        public string Dsquyen { get; set; }

        public virtual ICollection<Userrole> Userroles { get; set; }
    }
}
