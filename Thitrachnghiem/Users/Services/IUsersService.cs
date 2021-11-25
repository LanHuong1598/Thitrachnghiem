using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Functions;
using Thitrachnghiem.Users.Models.Entities;
using Thitrachnghiem.Users.Models.Schema;
using System;
using System.Collections.Generic;
using System.IO;

namespace Thitrachnghiem.Users.Services
{
   
    public interface IUsersService
    {
        public UserGet convert(User user);
        public UserView converttoUserChung(User user);

        public UserGet GetRoleForUser(User user);
        public UserGet Login(UserLogin userLogin);
        public Pair<List<UserGet>, int> GetUsers(string keyword, Pageing pageing);
        public List<UserView> GetUsersChung(string keyword, Pageing pageing);
        public UserGet GetUsersByid(int id);
        public UserGet GetUsersByUuid(string uuid);
        public UserGet Create(UserCreate entity);
        public UserGet Update(UserUpdate entity);
        public UserGet Delete(string uuid);
        public List<RoleGet> GetRoles();
        public List<Authority> GetAuthorities();
        public Role GetRoleByUuid(Guid guid);

        public Role CreateRole(RoleCreate role);

        public Role UpdateRole(RoleUpdate role);
        public Role DeleteRole(Guid guid);


        public Userrole CreateUserRole(UserRoleCreate user);
        public Userrole DeleteUserRole(UserRoleCreate user);
        public UserGet UpdateUserRole(UserRoleUpdate entity);

    }
}
