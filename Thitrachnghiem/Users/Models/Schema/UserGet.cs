using Thitrachnghiem.Users.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem.Users.Models.Schema
{
    public class UserGet
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Madonvi { get; set; }
        public String Tendonvi { get; set; }
        public string Madonvicha { get; set; }
        public String Tendonvicha { get; set; }
        public String Role { get; set; }
        public Guid RoleUuid { get; set; }
        public List<String> Claims { get; set; }
        public string Chucvu { get; set; }



        public UserGet(User user)
        {
            this.Id = user.Id;
            this.Uuid = user.Uuid;
            this.Name = user.Name;
            this.Username = user.Username;
            this.Chucvu = user.Chucvu;
        }

        public UserGet()
        {

        }

    }
}
