using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;

namespace Thitrachnghiem.Quanlycauhoi.Models.Functions
{
    public class F_Cautraloi
    {
        thitracnghiemContext thitracnghiemContext;
        public F_Cautraloi()
        {
            thitracnghiemContext = new thitracnghiemContext();
        }

        public Cautraloi GetCautraloiByUuidWithFalse(Guid? uuid)
        {
            return thitracnghiemContext.Cautralois.Where(x => x.Uuid == uuid).FirstOrDefault();

        }
        public List<Cautraloi> GetCautralois(string keyword)
        {
            if (keyword == null || keyword =="")
            return thitracnghiemContext.Cautralois.Where(x => x.Status == true).OrderByDescending(x => x.Id).ToList();
            else
                return thitracnghiemContext.Cautralois.Where(x => x.Status == true && x.Noidung.Contains(keyword)).OrderByDescending(x => x.Id).ToList();
        }

        public Cautraloi GetCautraloisByUuid(Guid uuid)
        {
            return thitracnghiemContext.Cautralois.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
        }
        public Cautraloi GetCautraloisById(int id)
        {
            return thitracnghiemContext.Cautralois.Where(x => x.Id == id && x.Status == true).FirstOrDefault();
        }
       
        public Cautraloi Update(Cautraloi user)
        {
            Cautraloi user1 = thitracnghiemContext.Cautralois.Where(x => x.Uuid == user.Uuid && x.Status == true).FirstOrDefault();
            user1 = user;
            thitracnghiemContext.SaveChanges();
            return user1;
        }
        public Cautraloi Create(Cautraloi user)
        {
            thitracnghiemContext.Cautralois.Add(user);
            thitracnghiemContext.SaveChanges();
            return user;
        }      

        public Cautraloi Delete(Guid uuid)
        {
            Cautraloi user1 = thitracnghiemContext.Cautralois.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
            user1.Status = false;           
            thitracnghiemContext.SaveChanges();
            return user1;
        }

        public List<Cautraloi> GetCautraloiWithCauhoiid(int? id)
        {
            return thitracnghiemContext.Cautralois.Where(x => x.Status == true && x.Cauhoiid == id).OrderBy(x => x.Id).ToList();
        }

    }
}
