using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Models.Model
{
    public partial class User
    {
        public User()
        {
            Userroles = new HashSet<Userrole>();
        }

        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }
        public int? Madonvi { get; set; }
        public string Chucvu { get; set; }

        public virtual ICollection<Userrole> Userroles { get; set; }
    }
}
