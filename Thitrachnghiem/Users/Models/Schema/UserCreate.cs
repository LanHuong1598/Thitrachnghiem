using Thitrachnghiem.Users.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem.Users.Models.Schema
{
    public class UserCreate
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Guid Madonvi { get; set; }
        public User convert()
        {
            User user = new User();
            user.Name = this.Name;
            user.Password = this.Password;
            user.Username = this.Username;
            user.Status = true;
            return user;
        }
    }
}
