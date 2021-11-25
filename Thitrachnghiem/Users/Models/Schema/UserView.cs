using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem.Users.Models.Schema
{
    public class UserView
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public String Tendonvi { get; set; }
    }
}
