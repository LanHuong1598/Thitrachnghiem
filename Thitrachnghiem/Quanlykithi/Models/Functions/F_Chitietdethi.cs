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
    public class F_Chitietdethi
    {
        thitracnghiemContext thitracnghiemContext;
        public F_Chitietdethi()
        {
            thitracnghiemContext = new thitracnghiemContext();
        }

        public Chitietdethi GetChitietdethiByUuidWithFalse(Guid? uuid)
        {
            return thitracnghiemContext.Chitietdethis.Where(x => x.Uuid == uuid).FirstOrDefault();

        }
        public List<Chitietdethi> GetChitietdethis()
        {
            return thitracnghiemContext.Chitietdethis.ToList();
        }

        public Chitietdethi GetChitietdethisByUuid(Guid uuid)
        {
            return thitracnghiemContext.Chitietdethis.Where(x => x.Uuid == uuid).FirstOrDefault();
        }
        public Chitietdethi GetChitietdethisById(int id)
        {
            return thitracnghiemContext.Chitietdethis.Where(x => x.Id == id).FirstOrDefault();
        }
       
        public Chitietdethi Update(Chitietdethi user)
        {
            Chitietdethi chitietdethi = thitracnghiemContext.Chitietdethis.Where(x => x.Uuid == user.Uuid).FirstOrDefault();
            chitietdethi = user;
            thitracnghiemContext.SaveChanges();
            return chitietdethi;
        }
        public Chitietdethi Create(Chitietdethi user)
        {
            thitracnghiemContext.Chitietdethis.Add(user);
            thitracnghiemContext.SaveChanges();
            return user;
        }      

        public Chitietdethi Delete(Guid uuid)
        {
            Chitietdethi chitietdethi = thitracnghiemContext.Chitietdethis.Where(x => x.Uuid == uuid).FirstOrDefault();
            thitracnghiemContext.Chitietdethis.Remove(chitietdethi);         
            thitracnghiemContext.SaveChanges();
            return chitietdethi;
        }

        public List<Chitietdethi> GetChitietdethiWithDethiid(int? id)
        {
            return thitracnghiemContext.Chitietdethis.Where(x =>x.Dethiid == id).ToList();
        }

    }
}
