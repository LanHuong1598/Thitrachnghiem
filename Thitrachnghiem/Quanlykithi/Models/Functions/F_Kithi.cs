using Thitrachnghiem.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thitrachnghiem.Quanlykithi.Models.Entities;

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
    }
}
