using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;

namespace Thitrachnghiem.Quanlycauhoi.Models.Functions
{
    public class F_Cauhoi
    {
        thitracnghiemContext thitracnghiemContext;
        public F_Cauhoi()
        {
            thitracnghiemContext = new thitracnghiemContext();
        }

        public Cauhoi GetCauhoiByUuidWithFalse(Guid? uuid)
        {
            return thitracnghiemContext.Cauhois.Where(x => x.Uuid == uuid).FirstOrDefault();

        }
        public Cauhoi GetCauhoiByidWithFalse(int? id)
        {
            return thitracnghiemContext.Cauhois.Where(x => x.Id == id).FirstOrDefault();

        }
        public List<Cauhoi> GetCauhois(string keyword)
        {
            if (keyword == null || keyword =="")
            return thitracnghiemContext.Cauhois.Where(x => x.Status == true).ToList();
            else
                return thitracnghiemContext.Cauhois.Where(x => x.Status == true && x.Noidung.Contains(keyword)).ToList();
        }

        public List<Cauhoi> GetCauhois(string he, string keyword)
        {
            if (keyword == null || keyword == "")
                return thitracnghiemContext.Cauhois.Where(x => x.Trinhdodaotao == he && x.Status == true).ToList();
            else
                return thitracnghiemContext.Cauhois.Where(x => x.Status == true && x.Trinhdodaotao == he&& x.Noidung.Contains(keyword)).ToList();
        }
        public List<Cauhoi> GetCauhois(string he, int chuyennganhid, string keyword)
        {
            if (keyword == null || keyword == "")
                return thitracnghiemContext.Cauhois.Where(x => x.Idchuyennganh == chuyennganhid && x.Trinhdodaotao == he && x.Status == true).ToList();
            else
                return thitracnghiemContext.Cauhois.Where(x => x.Idchuyennganh == chuyennganhid &&  x.Status == true && x.Trinhdodaotao == he && x.Noidung.Contains(keyword)).ToList();
        }
        public List<Cauhoi> GetCauhois(string he, int chuyennganhid, int bac, string keyword)
        {
            if (keyword == null || keyword == "")
                return thitracnghiemContext.Cauhois.Where(x => x.Bac == bac && x.Idchuyennganh == chuyennganhid && x.Trinhdodaotao == he && x.Status == true).ToList();
            else
                return thitracnghiemContext.Cauhois.Where(x => x.Bac == bac && x.Idchuyennganh == chuyennganhid && x.Status == true && x.Trinhdodaotao == he && x.Noidung.Contains(keyword)).ToList();
        }

        public Cauhoi GetCauhoisByUuid(Guid uuid)
        {
            return thitracnghiemContext.Cauhois.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
        }
        public Cauhoi GetCauhoisById(int id)
        {
            return thitracnghiemContext.Cauhois.Where(x => x.Id == id && x.Status == true).FirstOrDefault();
        }
       
        public Cauhoi Update(Cauhoi cauhoi)
        {
            Cauhoi cauhoi1 = thitracnghiemContext.Cauhois.Where(x => x.Uuid == cauhoi.Uuid && x.Status == true).FirstOrDefault();
            cauhoi1 = cauhoi;
            thitracnghiemContext.SaveChanges();
            return cauhoi1;
        }
        public Cauhoi Create(Cauhoi cauhoi)
        {
            thitracnghiemContext.Cauhois.Add(cauhoi);
            thitracnghiemContext.SaveChanges();
            return cauhoi;
        }      

        public Cauhoi Delete(Guid uuid)
        {
            Cauhoi cauhoi1 = thitracnghiemContext.Cauhois.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
            cauhoi1.Status = false;           
            thitracnghiemContext.SaveChanges();
            return cauhoi1;
        }

        public List<Cauhoi> GetCauhoiWithChuyennganhid(int? id)
        {
            return thitracnghiemContext.Cauhois.Where(x => x.Status == true && x.Idchuyennganh == id).ToList();
        }
        public List<Cauhoi> GetCauhoiWithChuyennganhidAndBac(int? id, int bac)
        {
            return thitracnghiemContext.Cauhois.Where(x => x.Status == true && x.Idchuyennganh == id
            && x.Bac == bac).ToList();
        }

    }
}
