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
    public class F_Phienthi
    {
        thitracnghiemContext thitracnghiemContext;
        public F_Phienthi()
        {
            thitracnghiemContext = new thitracnghiemContext();
        }

        public List<Phienthi> GetPhienthis()
        {
            return thitracnghiemContext.Phienthis.ToList();
        }
        public List<Phienthi> GetPhienthisIsOpen()
        {
            string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            return thitracnghiemContext.Phienthis.Where( x=> x.Thoigianketthuc.CompareTo(now) > 0).ToList();
        }

        public Phienthi GetPhienthisByUuid(Guid uuid)
        {
            return thitracnghiemContext.Phienthis.Where(x => x.Uuid == uuid).FirstOrDefault();
        }
        public Phienthi GetPhienthiDangMoByKithi(int id)
        {
            string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            return thitracnghiemContext.Phienthis.Where(x => x.Kithiid == id & 
            (x.Thoigianketthuc.CompareTo(now) > 0)).FirstOrDefault();
        }
        public Phienthi GetPhienthisById(int id)
        {
            return thitracnghiemContext.Phienthis.Where(x => x.Id == id).FirstOrDefault();
        }
       
        public Phienthi Create(Phienthi user)
        {
            thitracnghiemContext.Phienthis.Add(user);
            thitracnghiemContext.SaveChanges();
            return user;
        }
        public Phienthi Update(Phienthi phienthi)
        {
            Phienthi phienthi1 = thitracnghiemContext.Phienthis.Where(x => x.Uuid == phienthi.Uuid).FirstOrDefault();
            phienthi1 = phienthi;
            thitracnghiemContext.SaveChanges();
            return phienthi1;
        }
        public Phienthi Delete(Guid uuid)
        {
            Phienthi dethi = thitracnghiemContext.Phienthis.Where(x => x.Uuid == uuid).FirstOrDefault();
            thitracnghiemContext.Phienthis.Remove(dethi);
            thitracnghiemContext.SaveChanges();
            return dethi;
        }

        public List<Phienthi> GetPhienthiWithKithiid(int? id)
        {
            return thitracnghiemContext.Phienthis.Where(x =>x.Kithiid == id).ToList();
        }

        public PhienthiThisinh CreateThisinhphienthi(PhienthiThisinh phienthiThisinh)
        {
            thitracnghiemContext.PhienthiThisinhs.Add(phienthiThisinh);
            thitracnghiemContext.SaveChanges();
            return phienthiThisinh;
        }

        public List<PhienthiThisinh> GetThisinhsByPhienthiid(int? id)
        {
            return thitracnghiemContext.PhienthiThisinhs.Where(x => x.Phienthiid == id).ToList();
        }


    }
}
