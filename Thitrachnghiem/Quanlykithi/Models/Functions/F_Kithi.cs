using Thitrachnghiem.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thitrachnghiem.Quanlykithi.Models.Entities;
using Thitrachnghiem.Quanlykithi.Models.Schemas;

namespace Thitrachnghiem.Quanlykithi.Models.Functions
{
    public class F_Kithi
    {
        thitracnghiemContext thitracnghiemContext;
        public F_Kithi()
        {
            thitracnghiemContext = new thitracnghiemContext();
        }

        public Kithi GetKithiByUuidWithFalse(Guid? uuid)
        {
            return thitracnghiemContext.Kithis.Where(x => x.Uuid == uuid).FirstOrDefault();

        }

        public List<Kithi> GetKithis(string keyword)
        {
            if (keyword == null || keyword == "")
                return thitracnghiemContext.Kithis.Where(x => x.Status == true).ToList();
            else
                return thitracnghiemContext.Kithis.Where(x => x.Status == true  && x.Thoigianbatdau.Contains(keyword)).ToList();
        }
        public List<Kithi> GetKithis(string he, string keyword)
        {
            if (keyword == null || keyword == "")
                return thitracnghiemContext.Kithis.Where(x => x.Trinhdodaotao == he && x.Status == true).ToList();
            else
                return thitracnghiemContext.Kithis.Where(x => x.Status == true && x.Trinhdodaotao == he && x.Thoigianbatdau.Contains(keyword)).ToList();
        }
        public List<Kithi> GetKithis(string he, int chuyennganhid, string keyword)
        {
            if (keyword == null || keyword == "")
                return thitracnghiemContext.Kithis.Where(x => x.Chuyennganhid == chuyennganhid && x.Trinhdodaotao == he && x.Status == true).ToList();
            else
                return thitracnghiemContext.Kithis.Where(x => x.Chuyennganhid == chuyennganhid && x.Status == true && x.Trinhdodaotao == he && x.Thoigianbatdau.Contains(keyword)).ToList();
        }
        public List<Kithi> GetKithis(string he, int chuyennganhid, int bac, string keyword)
        {
            if (keyword == null || keyword == "")
                return thitracnghiemContext.Kithis.Where(x => x.Bac == bac && x.Chuyennganhid == chuyennganhid && x.Trinhdodaotao == he && x.Status == true).ToList();
            else
                return thitracnghiemContext.Kithis.Where(x => x.Bac == bac && x.Chuyennganhid == chuyennganhid && x.Status == true && x.Trinhdodaotao == he 
                && x.Thoigianbatdau.Contains(keyword)).ToList();
        }

        public Kithi GetKithisByUuid(Guid uuid)
        {
            return thitracnghiemContext.Kithis.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
        }
        public Kithi GetKithisById(int id)
        {
            return thitracnghiemContext.Kithis.Where(x => x.Id == id && x.Status == true).FirstOrDefault();
        }
       
        public Kithi Update(Kithi kithi)
        {
            Kithi kithi1 = thitracnghiemContext.Kithis.Where(x => x.Uuid == kithi.Uuid && x.Status == true).FirstOrDefault();
            kithi1 = kithi;
            thitracnghiemContext.SaveChanges();
            return kithi1;
        }
        public Kithi Create(Kithi kithi)
        {
            thitracnghiemContext.Kithis.Add(kithi);
            thitracnghiemContext.SaveChanges();
            return kithi;
        }      

        public Kithi Delete(Guid uuid)
        {
            Kithi kithi1 = thitracnghiemContext.Kithis.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
            kithi1.Status = false;           
            thitracnghiemContext.SaveChanges();
            return kithi1;
        }
        public List<KithiThisinhGet> GetKithiThisinhs(Guid Kithiuuid)
        {
            List<KithiThisinhGet> rs = new List<KithiThisinhGet>();
            var kithi = GetKithiByUuidWithFalse(Kithiuuid);
            if (kithi == null)
                throw new Exception("Khong co ki thi");

            var ts = thitracnghiemContext.Thisinhs.Where(x => x.Status == true && x.Kithiid == kithi.Id).ToList();

            var phienthis = thitracnghiemContext.Phienthis.Where(x => x.Kithiid == kithi.Id).Select(p=> p.Id).ToList();

            foreach (var i in ts)
            {
                KithiThisinhGet kithiThisinhGet = new KithiThisinhGet();
                kithiThisinhGet.Tenthisinh = i.Name;
                kithiThisinhGet.Email = i.Email;
                var u = thitracnghiemContext.PhienthiThisinhs.Where(x => x.Thisinhid == kithi.Id).ToList().Where(u => phienthis.Contains((int)u.Phienthiid)).OrderBy(x=>x.Thoigianketthuc).First();
                if (u != null)
                {
                    kithiThisinhGet.Diem = u.Diem;
                    kithiThisinhGet.Thoigianbatdau = u.Thoigianbatdau;
                    kithiThisinhGet.Thoigianketthuc = u.Thoigianketthuc;
                    kithiThisinhGet.Made = u.Made;
                    kithiThisinhGet.Dethiuuid = u.Dethiuuid;

                }


            }
            return rs;
        }
    }
}
