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
    public class F_ChitietdethiCautraloi
    {
        thitracnghiemContext thitracnghiemContext;
        public F_ChitietdethiCautraloi()
        {
            thitracnghiemContext = new thitracnghiemContext();
        }

        public ChitietdethiCautraloi GetChitietdethiCautraloiByUuidWithFalse(Guid? uuid)
        {
            return thitracnghiemContext.ChitietdethiCautralois.Where(x => x.Uuid == uuid).FirstOrDefault();

        }
        public List<ChitietdethiCautraloi> GetChitietdethiCautralois()
        {
            return thitracnghiemContext.ChitietdethiCautralois.ToList();
        }

        public ChitietdethiCautraloi GetChitietdethiCautraloisByUuid(Guid uuid)
        {
            return thitracnghiemContext.ChitietdethiCautralois.Where(x => x.Uuid == uuid).FirstOrDefault();
        }
        public ChitietdethiCautraloi GetChitietdethiCautraloisById(int id)
        {
            return thitracnghiemContext.ChitietdethiCautralois.Where(x => x.Id == id).FirstOrDefault();
        }
       
        public ChitietdethiCautraloi Update(ChitietdethiCautraloi user)
        {
            ChitietdethiCautraloi ChitietdethiCautraloi = thitracnghiemContext.ChitietdethiCautralois.Where(x => x.Uuid == user.Uuid).FirstOrDefault();
            ChitietdethiCautraloi = user;
            thitracnghiemContext.SaveChanges();
            return ChitietdethiCautraloi;
        }
        public ChitietdethiCautraloi Create(ChitietdethiCautraloi user)
        {
            thitracnghiemContext.ChitietdethiCautralois.Add(user);
            thitracnghiemContext.SaveChanges();
            return user;
        }      

        public ChitietdethiCautraloi Delete(Guid uuid)
        {
            ChitietdethiCautraloi ChitietdethiCautraloi = thitracnghiemContext.ChitietdethiCautralois.Where(x => x.Uuid == uuid).FirstOrDefault();
            thitracnghiemContext.ChitietdethiCautralois.Remove(ChitietdethiCautraloi);         
            thitracnghiemContext.SaveChanges();
            return ChitietdethiCautraloi;
        }

        public List<ChitietdethiCautraloi> GetChitietdethiCautraloiWithCauhoiid(int? id, int? dethiid)
        {
            return thitracnghiemContext.ChitietdethiCautralois.Where(x =>x.Cauhoiid == id && x.Dethiid == dethiid).OrderBy(x=> x.Id).ToList();
        }

    }
}
