using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Entities;
using Thitrachnghiem.Users.Models.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem.Users.Models.Functions
{
    public class F_Roles
    {
        thitracnghiemContext thitracnghiemContext;
        public F_Roles()
        {
            thitracnghiemContext = new thitracnghiemContext();
        } 

        public List<Role> GetRoles()
        {
            return thitracnghiemContext.Roles.ToList();
        }

        public List<Authority> GetAuthorities()
        {
            var rs = thitracnghiemContext.Authorities.ToList();
            return rs;
        }

        public Authority GetAuthority(string id)
        {
            var rs = thitracnghiemContext.Authorities.Where( x=> x.Name == id).FirstOrDefault();
            return rs;
        }

        public Role GetRoleByUuid(Guid uuid)
        {
            return thitracnghiemContext.Roles.Where(x => x.Uuid == uuid).FirstOrDefault();

        }
        public Role GetRoleById(int id)
        {
            return thitracnghiemContext.Roles.Where(x => x.Id == id).FirstOrDefault();
        }
       
        public Role Update(Role role)
        {
            Role role1 = thitracnghiemContext.Roles.Where(x => x.Uuid == role.Uuid).FirstOrDefault();
            role1.Dsquyen = role.Dsquyen;
            role1.Ten = role.Ten;
            role1.Mota = role.Mota;
            thitracnghiemContext.SaveChanges();
            return role1;
        }
        public Role Create(Role role)
        {
            thitracnghiemContext.Roles.Add(role);
            thitracnghiemContext.SaveChanges();
            return role;
        }

        public Role Delete(Guid uuid)
        {
            Role role1 = thitracnghiemContext.Roles.Where(x => x.Uuid == uuid).FirstOrDefault();
            thitracnghiemContext.Roles.Remove(role1);
            thitracnghiemContext.SaveChanges();
            return role1;
        }
    }
}
