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
    public class F_ThisinhTraloi
    {
        thitracnghiemContext thitracnghiemContext;
        public F_ThisinhTraloi()
        {
            thitracnghiemContext = new thitracnghiemContext();
        }

        public ThisinhTraloi GetThisinhTraloiByUuidWithFalse(Guid? uuid)
        {
            return thitracnghiemContext.ThisinhTralois.Where(x => x.Uuid == uuid).FirstOrDefault();

        }
        public List<ThisinhTraloi> GetThisinhTralois()
        {
            return thitracnghiemContext.ThisinhTralois.ToList();
        }

        public ThisinhTraloi GetThisinhTraloisByUuid(Guid uuid)
        {
            return thitracnghiemContext.ThisinhTralois.Where(x => x.Uuid == uuid).FirstOrDefault();
        }
        public ThisinhTraloi GetThisinhTraloisById(int id)
        {
            return thitracnghiemContext.ThisinhTralois.Where(x => x.Id == id).FirstOrDefault();
        }
       
        public ThisinhTraloi Update(ThisinhTraloi user)
        {
            ThisinhTraloi ThisinhTraloi = thitracnghiemContext.ThisinhTralois.Where(x => x.Uuid == user.Uuid).FirstOrDefault();
            ThisinhTraloi = user;
            thitracnghiemContext.SaveChanges();
            return ThisinhTraloi;
        }
        public ThisinhTraloi Create(ThisinhTraloi user)
        {
            thitracnghiemContext.ThisinhTralois.Add(user);
            thitracnghiemContext.SaveChanges();
            return user;
        }      

        public ThisinhTraloi Delete(Guid uuid)
        {
            ThisinhTraloi ThisinhTraloi = thitracnghiemContext.ThisinhTralois.Where(x => x.Uuid == uuid).FirstOrDefault();
            thitracnghiemContext.ThisinhTralois.Remove(ThisinhTraloi);         
            thitracnghiemContext.SaveChanges();
            return ThisinhTraloi;
        }

        public List<ThisinhTraloi> GetThisinhTraloiWithDethiidandThisinhid(int? dethiid, int thisinhid)
        {
            return thitracnghiemContext.ThisinhTralois.Where(x =>x.Dethiid == dethiid && x.Thisinhid == thisinhid).ToList();
        }
        public ThisinhTraloi GetThisinhTraloiWithDethiidandCauhoiidandThisinhid(
            int cauhoiid, int? dethiid, int thisinhid)
        {
            return thitracnghiemContext.ThisinhTralois.Where(x =>
            x.Cauhoiid == cauhoiid && x.Dethiid == dethiid && x.Thisinhid == thisinhid).FirstOrDefault();
        }

    }
}
