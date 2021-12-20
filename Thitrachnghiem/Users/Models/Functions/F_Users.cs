using PagedList;
using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Entities;
using Thitrachnghiem.Users.Models.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem.Users.Models.Functions
{
    public class F_Users
    {
        thitracnghiemContext thitracnghiemContext;
        public F_Users()
        {
            thitracnghiemContext = new thitracnghiemContext();
        }

        public User Login(string username, string password)
        {
            User user = thitracnghiemContext.Users.Where(
                x => x.Username == username && x.Password == password && x.Status == true).FirstOrDefault();
            if (user != null)
            {
                return user;
            }

            return null;
        }

        public List<User> GetUsers(string keyword, Pageing pageing)
        {
            if (keyword == null)
                return thitracnghiemContext.Users.Where(x => x.Status == true).OrderByDescending(x => x.Id).ToList();
            else
                return thitracnghiemContext.Users.Where(x => x.Status == true && x.Name.Contains(keyword)).OrderByDescending(x => x.Id).ToList();
        }

        public User GetUsersByUuid(Guid uuid)
        {
            return thitracnghiemContext.Users.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
        }
        public User GetUsersById(int id)
        {
            return thitracnghiemContext.Users.Where(x => x.Id == id && x.Status == true).FirstOrDefault();
        }

        public User GetUsersByIdHaveUserwasDeleted(int id)
        {
            return thitracnghiemContext.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        public User GetUsersByUsername(string username)
        {
            return thitracnghiemContext.Users.Where(x => x.Username == username && x.Status == true).FirstOrDefault();
        }
        public User Update(User user)
        {
            User user1 = thitracnghiemContext.Users.Where(x => x.Uuid == user.Uuid && x.Status == true).FirstOrDefault();
            user1 = user;
            thitracnghiemContext.SaveChanges();
            return user1;
        }
        public User Create(User user)
        {
            thitracnghiemContext.Users.Add(user);
            thitracnghiemContext.SaveChanges();
            return user;
        }

        public User Delete(Guid uuid)
        {
            User user1 = thitracnghiemContext.Users.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
            user1.Status = false;
            thitracnghiemContext.SaveChanges();
            return user1;
        }

        public User GetUserByUUid(Guid uuid)
        {
            var res = thitracnghiemContext.Users.Where(x => x.Uuid == uuid);
            if (res != null)
            {
                return res.ToList()[0];
            }
            else
            {
                return null;
            }
        }
        public int getUserIdbyUuid(Guid uuid)
        {
            var res = thitracnghiemContext.Users.Where(x => x.Uuid == uuid).OrderByDescending(x => x.Id).ToList();
            if (res.Count == 0)
            {
                return 0;
            }
            else
            {
                return res[0].Id;
            }
        }
        public string getNamebyUuid(Guid uuid)
        {
            var res = thitracnghiemContext.Users.Where(x => x.Uuid == uuid).OrderByDescending(x => x.Id).ToList();
            if (res.Count <= 0)
            {
                return null;
            }
            else
            {
                return res[0].Name;
            }


        }

        public Guid getUuidbyId(int id)
        {
            return (Guid)thitracnghiemContext.Users.Find(id).Uuid;
        }
    }
}
