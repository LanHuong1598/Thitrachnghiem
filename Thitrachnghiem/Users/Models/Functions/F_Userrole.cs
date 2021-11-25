using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Entities;
using Thitrachnghiem.Users.Models.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem.Users.Models.Functions
{
    public class F_Userrole
    {
        thitracnghiemContext thitracnghiemContext;
        public F_Userrole()
        {
            thitracnghiemContext = new thitracnghiemContext();
        }

        public List<Userrole> GetRoles()
        {
            return thitracnghiemContext.Userroles.ToList();
        }
               
        public UserGet GetUserRoleById(int userId)
        {
           
            UserGet result = new UserGet();
            result.Role = new List<string>();
            result.RoleUuids = new List<Guid>();


            List<User> users = thitracnghiemContext.Users.ToList();
            List<Userrole> userroles = thitracnghiemContext.Userroles.ToList();
            List<Role> roles = thitracnghiemContext.Roles.ToList();

            var us = from usrole in userroles
                     join user in users on usrole.Userid equals user.Id
                     join role in roles on usrole.Roleid equals role.Id
                     where user.Id == userId
                     select role;

            foreach (var u in us)
            {
                result.Role.Add(u.Ten);
                result.RoleUuids.Add((Guid)u.Uuid);
            }

            return result;
        }

        public UserGet GetRoleClaimById(int userId)
        {

            UserGet result = new UserGet();
            result.Role = new List<string>();

            List<User> users = thitracnghiemContext.Users.ToList();
            List<Userrole> userroles = thitracnghiemContext.Userroles.ToList();
            List<Role> roles = thitracnghiemContext.Roles.ToList();

            var us = from usrole in userroles
                     join user in users on usrole.Userid equals user.Id
                     join role in roles on usrole.Roleid equals role.Id
                     where user.Id == userId
                     select role;

            foreach (var u in us)
                result.Role.AddRange(u.Dsquyen.Split(','));

            return result;
        }

        public Userrole Create(Userrole role)
        {
            thitracnghiemContext.Userroles.Add(role);
            thitracnghiemContext.SaveChanges();
            return role;
        }

        public Userrole Delete(int userId, int roleId)
        {
            Userrole role1 = thitracnghiemContext.Userroles.Where(x => x.Userid == userId && x.Roleid == roleId).FirstOrDefault();
            thitracnghiemContext.Userroles.Remove(role1);
            thitracnghiemContext.SaveChanges();
            return role1;
        }

     
    }
}
