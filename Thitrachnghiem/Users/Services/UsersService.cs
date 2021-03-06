using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Functions;
using Thitrachnghiem.Users.Models.Entities;
using Thitrachnghiem.Users.Models.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using PagedList;
using System.Linq;

namespace Thitrachnghiem.Users.Services
{  
    public class UsersService : IUsersService
    {
        public UsersService()
        {

        }

        public UserGet convert(User user)
        {
            UserGet userGet = new UserGet();
            userGet.Id = user.Id;
            userGet.Uuid = user.Uuid;
            userGet.Name = user.Name;
            userGet.Username = user.Username;
            userGet.Chucvu = user.Chucvu;

            if (user.Madonvi != null)
            {
                F_Donvi f_Donvi = new F_Donvi();
                Donvi donvi = f_Donvi.GetDonvisById((int)user.Madonvi);
                if (donvi != null)
                {
                    userGet.Madonvi = donvi.Uuid.ToString();
                    userGet.Tendonvi = donvi.Ten;
                    if (donvi.Macha != null)
                    {
                        try
                        {
                            donvi = f_Donvi.GetDonvisById((int)donvi.Macha);
                            if (donvi != null)
                            {
                                userGet.Madonvicha = donvi.Uuid.ToString();
                                userGet.Tendonvicha = donvi.Ten;
                            }

                        }
                        catch
                        {
                        }
                    }
                }
            
            }

            F_Userrole f_Userrole = new F_Userrole();
            userGet.Role = f_Userrole.GetUserRoleById(userGet.Id).Role;
            userGet.RoleUuid = f_Userrole.GetUserRoleById(userGet.Id).RoleUuid;


            return userGet;
        }

        public UserView converttoUserChung(User user)
        {
            UserView userGet = new UserView();
            userGet.Id = user.Id;
            userGet.Uuid = user.Uuid;
            userGet.Name = user.Name;
            userGet.Username = user.Username;
            if (user.Madonvi != null)
            {
                F_Donvi f_Donvi = new F_Donvi();
                Donvi donvi = f_Donvi.GetDonvisById((int)user.Madonvi);
                if (donvi != null)
                {
                    userGet.Tendonvi = donvi.Ten;
                }

            }

            return userGet;
        }


        public UserGet GetRoleForUser(User user)
        {
            UserGet us = new UserGet();
            us.Id = user.Id;
            us.Uuid = user.Uuid;
            us.Name = user.Name;
            us.Username = user.Username;

            F_Userrole f_Userrole = new F_Userrole();
            us.Role = f_Userrole.GetUserRoleById(us.Id).Role;
            return us;
        }

        public UserGet Login(UserLogin userLogin, string remoteIpAddress)
        {

            F_Users f_users = new F_Users();


            User user = f_users.Login(userLogin.Username, userLogin.Password, remoteIpAddress);
            if (user != null)
            {
                return GetRoleForUser(user);
            }

            return null;
        }

        public UserGet LoginThisinh(UserLogin userLogin, string remoteIpAddress)
        {

            F_Users f_users = new F_Users();


            User user = f_users.Login(userLogin.Username, userLogin.Password, remoteIpAddress);
            if (user != null)
            {
                return GetRoleForUser(user);
            }

            return null;
        }

        public Pair<List<UserGet>, int> GetUsers(string keyword, Pageing pageing)
        {
            F_Users f_users = new F_Users();
            var users = f_users.GetUsers(keyword, pageing);

            if (users == null)
                return null;

            return new Pair<List<UserGet>, int>(users.ToPagedList(pageing.offset, pageing.limit).ToList().ConvertAll(x => convert(x)), users.Count);
        }

        public List<UserView> GetUsersChung(string keyword, Pageing pageing)
        {
            F_Users f_users = new F_Users();
            List<User> users = f_users.GetUsers(keyword, pageing);
            if (users == null)
                return null;

            return users.ConvertAll(x => converttoUserChung(x));
        }

        public UserGet GetUsersByid(int id)
        {

            F_Users f_users = new F_Users();

            User user = f_users.GetUsersById(id);
            if (user == null)
                throw new InvalidDataException();

            return convert(user);
        }

        public UserGet GetUsersByUuid(string uuid)
        {
            Guid guid = new Guid(uuid);

            F_Users f_users = new F_Users();

            User user = f_users.GetUsersByUuid(guid);
            if (user == null)
                throw new InvalidDataException();

            return convert(user);
        }

        public UserGet Create(UserCreate entity)
        {
            F_Users f_users = new F_Users();
            if (entity.Username == null || entity.Password == null)
                throw new InvalidDataException("Username hoặc password không được trống");

            User user = f_users.GetUsersByUsername(entity.Username);
            if (user != null)
                throw new InvalidDataException("Username đã tồn tại");

            user = entity.convert();
            if (entity.Madonvi != null && entity.Madonvi != "null")
            {
                F_Donvi f_Donvi = new F_Donvi();
                Donvi donvi = f_Donvi.GetDonvisByUuid(new Guid(entity.Madonvi));
                if (donvi == null)
                    throw new InvalidDataException("Mã đơn vị không đúng");
                user.Madonvi = donvi.Id;
            }
            user = f_users.Create(user);

            try
            {
                UserRoleCreate userrole = new UserRoleCreate();
                userrole.Roleuuid = new Guid(entity.Roleuuid);
                userrole.Useruuid = (Guid)user.Uuid;
                CreateUserRole(userrole);
            }
            catch
            {
            }
            return convert(user);
        }

        public int CreateList(ListUserCreate entity)
        {
            int dem = 0;
            foreach(var u in entity.Danhsachnguoidungs)
            {
                try
                {
                    if (u.Roleuuid.Equals("ADMIN"))
                        u.Roleuuid = "ef30f807-75f8-45dc-8327-548d80b18873";
                    else
                        u.Roleuuid = "0eea26a4-c8c7-4bdd-8c48-efc6c0ed08b4";

                    Create(u);
                    dem = dem + 1;
                }
                catch
                {
                    
                }
            }
            return dem;
        }


        public UserGet Update(UserUpdate entity)
        {
            F_Users f_users = new F_Users();
            User user = f_users.GetUsersByUuid(entity.Uuid);
            if (user == null)
                throw new InvalidDataException("Username không tồn tại");
            if (entity.Name != null)
                user.Name = entity.Name;
            if (entity.Password != null && entity.Password != "") 
                user.Password = entity.Password;

            user.Chucvu = entity.Chucvu;
            if (entity.Madonvi != "null")
            {
                F_Donvi f_Donvi = new F_Donvi();
                var donvi = f_Donvi.GetDonvisByUuid(new Guid(entity.Madonvi));
                if (donvi == null)
                    throw new InvalidDataException("Mã đơn vị không đúng");
                user.Madonvi = donvi.Id;
            }
            user = f_users.Update(user);
            try
            {
                UserRoleUpdate userrole = new UserRoleUpdate();
                userrole.Roleuuid = new Guid(entity.Roleuuid);
                userrole.Useruuid = (Guid)user.Uuid;
                UpdateUserRole(userrole);
            }
            catch
            {
            }
            return convert(user);
        }
        public UserGet Delete(string uuid)
        {
            F_Users f_users = new F_Users();
            Guid guid = new Guid(uuid);

            User user = f_users.GetUsersByUuid(guid);
            if (user == null)
                throw new InvalidDataException("Username không tồn tại");
     
            user = f_users.Delete(guid);
            return convert(user);
        }

        #region role

        public RoleGet convert(Role role)
        {
            RoleGet roleGet = new RoleGet(role);
            roleGet.Dsquyen = "";
            string[] aus = role.Dsquyen.Split(',');
            foreach (var u in aus)
            {
                F_Roles f = new F_Roles();
                var authority = f.GetAuthority(u);
                if (authority != null)
                    roleGet.Dsquyen = roleGet.Dsquyen  + authority.Tenhienthi + ", ";
            }
            return roleGet;
        }

        public List<RoleGet> GetRoles()
        {
            return new F_Roles().GetRoles().ConvertAll(x => convert(x));
        }

        public List<Authority> GetAuthorities()
        {
            return new F_Roles().GetAuthorities();
        }

        public Role GetRoleByUuid(Guid guid)
        {
            return new F_Roles().GetRoleByUuid(guid);
        }

        public Role CreateRole(RoleCreate role)
        {
            Role role1 = new Role();
            role1.Dsquyen = role.Dsquyen;
            role1.Mota = role.Mota;
            role1.Ten = role.Ten;
            return new F_Roles().Create(role1);
        }

        public Role UpdateRole(RoleUpdate role)
        {
            F_Roles f_Roles = new F_Roles();
            Role role1 = f_Roles.GetRoleByUuid((Guid)role.Uuid);
            role1.Dsquyen = role.Dsquyen;
            role1.Mota = role.Mota;
            role1.Ten = role.Ten;
            return new F_Roles().Update(role1);
        }

        public Role DeleteRole(Guid guid)
        {
            return new F_Roles().Delete(guid);
        }


        #endregion
        public Userrole CreateUserRole(UserRoleCreate user)
        {
            F_Userrole f_Userrole = new F_Userrole();
            Userrole userrole = new Userrole();
            userrole.Roleid = new F_Roles().GetRoleByUuid(user.Roleuuid).Id;
            userrole.Userid = new F_Users().GetUsersByUuid(user.Useruuid).Id;
            f_Userrole.Create(userrole);
            return userrole;

        }
        public Userrole DeleteUserRole(UserRoleCreate user)
        {
            F_Userrole f_Userrole = new F_Userrole();
            Userrole userrole = new Userrole();
            userrole.Roleid = new F_Roles().GetRoleByUuid(user.Roleuuid).Id;
            userrole.Userid = new F_Users().GetUsersByUuid(user.Useruuid).Id;
            f_Userrole.Delete((int)userrole.Userid, (int)userrole.Roleid);
            return userrole;

        }
        public UserGet UpdateUserRole(UserRoleUpdate entity)
        {
            F_Users f_users = new F_Users();
            User user = f_users.GetUsersByUuid(entity.Useruuid);
            if (user == null)
                throw new InvalidDataException("Username không tồn tại");

            F_Userrole f_Userrole = new F_Userrole();
            Guid guids = f_Userrole.GetUserRoleById(user.Id).RoleUuid;
            if (guids != null)
            {               
                    UserRoleCreate userRoleCreate = new UserRoleCreate();
                    userRoleCreate.Useruuid = (Guid)user.Uuid;
                    userRoleCreate.Roleuuid = guids;

                    DeleteUserRole(userRoleCreate);              
            }

            if (entity.Roleuuid != null)
            {
                {
                    UserRoleCreate userRoleCreate = new UserRoleCreate();
                    userRoleCreate.Useruuid = (Guid)user.Uuid;
                    userRoleCreate.Roleuuid = entity.Roleuuid;

                    CreateUserRole(userRoleCreate);
                }
            }

            return GetUsersByUuid(entity.Useruuid.ToString());
        }
    }
}
