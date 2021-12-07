using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;
using Thitrachnghiem.Quanlykithi.Models.Entities;

namespace Thitrachnghiem.Quanlykithi.Models.Functions
{
    public class F_Matrandethi
    {
        thitracnghiemContext thitracnghiemContext;
        public F_Matrandethi()
        {
            thitracnghiemContext = new thitracnghiemContext();
        }

        public Matrandethi GetMatrandethiByUuidWithFalse(Guid? uuid)
        {
            return thitracnghiemContext.Matrandethis.Where(x => x.Uuid == uuid).FirstOrDefault();

        }
        public List<Matrandethi> GetMatrandethis()
        {
            return thitracnghiemContext.Matrandethis.Where(x => x.Status == true).ToList();
        }

        public Matrandethi GetMatrandethisByUuid(Guid uuid)
        {
            return thitracnghiemContext.Matrandethis.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
        }
        public Matrandethi GetMatrandethisById(int id)
        {
            return thitracnghiemContext.Matrandethis.Where(x => x.Id == id && x.Status == true).FirstOrDefault();
        }
       
        public Matrandethi Update(Matrandethi user)
        {
            Matrandethi user1 = thitracnghiemContext.Matrandethis.Where(x => x.Uuid == user.Uuid && x.Status == true).FirstOrDefault();
            user1 = user;
            thitracnghiemContext.SaveChanges();
            return user1;
        }
        public Matrandethi Create(Matrandethi user)
        {
            thitracnghiemContext.Matrandethis.Add(user);
            thitracnghiemContext.SaveChanges();
            return user;
        }      

        public Matrandethi Delete(Guid uuid)
        {
            Matrandethi user1 = thitracnghiemContext.Matrandethis.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
            user1.Status = false;           
            thitracnghiemContext.SaveChanges();
            return user1;
        }

        public List<Matrandethi> GetMatrandethiWithKithiid(int? id)
        {
            return thitracnghiemContext.Matrandethis.Where(x => x.Status == true && x.Kithiid == id).ToList();
        }

    }
}
