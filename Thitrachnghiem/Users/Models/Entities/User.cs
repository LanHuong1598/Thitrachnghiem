using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Users.Models.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool? Status { get; set; }
        public int? Madonvi { get; set; }

    }
}
