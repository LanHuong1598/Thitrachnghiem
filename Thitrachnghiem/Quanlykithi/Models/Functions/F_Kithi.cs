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
                return thitracnghiemContext.Kithis.Where(x => x.Status == true).OrderByDescending(x=> x.Id).ToList();
            else
                return thitracnghiemContext.Kithis.Where(x => x.Status == true  && x.Thoigianbatdau.Contains(keyword)).OrderByDescending(x=> x.Id).ToList();
        }
        public List<Kithi> GetKithis(string he, string keyword)
        {
            if (keyword == null || keyword == "")
                return thitracnghiemContext.Kithis.Where(x => x.Trinhdodaotao == he && x.Status == true).OrderByDescending(x=> x.Id).ToList();
            else
                return thitracnghiemContext.Kithis.Where(x => x.Status == true && x.Trinhdodaotao == he && x.Thoigianbatdau.Contains(keyword)).OrderByDescending(x=> x.Id).ToList();
        }
        public List<Kithi> GetKithis(string he, int chuyennganhid, string keyword)
        {
            if (keyword == null || keyword == "")
                return thitracnghiemContext.Kithis.Where(x => x.Chuyennganhid == chuyennganhid && x.Trinhdodaotao == he && x.Status == true).OrderByDescending(x=> x.Id).ToList();
            else
                return thitracnghiemContext.Kithis.Where(x => x.Chuyennganhid == chuyennganhid && x.Status == true && x.Trinhdodaotao == he && x.Thoigianbatdau.Contains(keyword)).OrderByDescending(x=> x.Id).ToList();
        }
        public List<Kithi> GetKithis(string he, int chuyennganhid, int bac, string keyword)
        {
            if (keyword == null || keyword == "")
                return thitracnghiemContext.Kithis.Where(x => x.Bac == bac && x.Chuyennganhid == chuyennganhid && x.Trinhdodaotao == he && x.Status == true).OrderByDescending(x=> x.Id).ToList();
            else
                return thitracnghiemContext.Kithis.Where(x => x.Bac == bac && x.Chuyennganhid == chuyennganhid && x.Status == true && x.Trinhdodaotao == he 
                && x.Thoigianbatdau.Contains(keyword)).OrderByDescending(x=> x.Id).ToList();
        }

        public List<Kithi> GetKithiWithHeAndBacs(string he, int bac, string keyword)
        {
            if (keyword == null || keyword == "")
                return thitracnghiemContext.Kithis.Where(x => x.Bac == bac && x.Trinhdodaotao == he && x.Status == true).OrderByDescending(x=> x.Id).ToList();
            else
                return thitracnghiemContext.Kithis.Where(x => x.Bac == bac && x.Status == true && x.Trinhdodaotao == he
                && x.Thoigianbatdau.Contains(keyword)).OrderByDescending(x=> x.Id).ToList();
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
        //public List<KithiThisinhGet> GetKithiThisinhs(Guid Kithiuuid)
        //{
        //    List<KithiThisinhGet> rs = new List<KithiThisinhGet>();
        //    var kithi = GetKithiByUuidWithFalse(Kithiuuid);
        //    if (kithi == null)
        //        throw new Exception("Khong co ki thi");

        //    var ts = thitracnghiemContext.Thisinhs.Where(x => x.Status == true && x.Kithiid == kithi.Id).ToList();

        //    var phienthis = thitracnghiemContext.Phienthis.Where(x => x.Kithiid == kithi.Id).Select(p => p.Id).ToList();

        //    foreach (var i in ts)
        //    {
        //        KithiThisinhGet kithiThisinhGet = new KithiThisinhGet();
        //        kithiThisinhGet.Tenthisinh = i.Name;
        //        kithiThisinhGet.Email = i.Email;
        //        var u = thitracnghiemContext.PhienthiThisinhs.Where(x => x.Thisinhid == i.Id).ToList();
        //        if (u != null && u.Count() != 0)
        //        {
        //            var v = u.Where(u => phienthis.Contains((int)u.Phienthiid)).OrderByDescending(x => x.Thoigianketthuc).First();
        //            if (v != null)
        //            {
        //                kithiThisinhGet.Diem = v.Diem;
        //                kithiThisinhGet.Thoigianbatdau = v.Thoigianbatdau;
        //                kithiThisinhGet.Thoigianketthuc = v.Thoigianketthuc;
        //                kithiThisinhGet.Made = v.Made;
        //                kithiThisinhGet.Dethiuuid = v.Dethiuuid;
        //            }

        //        }
        //        rs.Add(kithiThisinhGet);


        //    }
        //    return rs;
        //}

        public List<KithiThisinhGet> GetKithiThisinhs(Guid Kithiuuid)
        {
            List<KithiThisinhGet> rs = new List<KithiThisinhGet>();
            var kithi = GetKithiByUuidWithFalse(Kithiuuid);
            if (kithi == null)
                throw new Exception("Khong co ki thi");

            var ts = thitracnghiemContext.Thisinhs.Where(x => x.Status == true && x.Kithiid == kithi.Id).OrderByDescending(x=> x.Id).ToList();

            var phienthis = thitracnghiemContext.Phienthis.Where(x => x.Kithiid == kithi.Id).Select(p => p.Id).ToList();

            foreach (var i in ts)
            {
                KithiThisinhGet kithiThisinhGet = new KithiThisinhGet();

                kithiThisinhGet.Bacdanggiu = i.Bacdanggiu;
                kithiThisinhGet.Bacthi = i.Bacthi;
                kithiThisinhGet.Chuyennganhhoc = i.Chuyennganhhoc;
                kithiThisinhGet.Chucvu = i.Chucvu;
                kithiThisinhGet.Capbac = i.Capbac;
                kithiThisinhGet.Email = i.Email;
                kithiThisinhGet.Name = i.Name;
                kithiThisinhGet.Donvi = i.Donvi;
                kithiThisinhGet.Bacluong = i.Bacluong;
                kithiThisinhGet.Trinhdo = i.Trinhdo;
                kithiThisinhGet.Namsinh = i.Namsinh;
                kithiThisinhGet.Sobaodanh = i.Sobaodanh;
                try
                {
                    var chuyennganh = thitracnghiemContext.Chuyennganhs.Where(x => x.Id == i.Chuyennganhthiid).FirstOrDefault();
                    //kithiThisinhGet.Chuyennganhuuid = chuyennganh.Uuid.ToString();
                    kithiThisinhGet.Chuyennganhthi = chuyennganh.Ten;
                    kithiThisinhGet.Trinhdodaotao = chuyennganh.Trinhdodaotao;
                }
                catch
                {
                }
                try
                {
                   
                    kithiThisinhGet.Trinhdodaotao = kithi.Trinhdodaotao;
                    kithiThisinhGet.Bacthi = kithi.Bac;
                    try
                    {
                        var chuyennganh = thitracnghiemContext.Chuyennganhs.Where(x => x.Id == i.Chuyennganhthiid).FirstOrDefault();
                        //kithiThisinhGet.Chuyennganhuuid = chuyennganh.Uuid.ToString();
                        kithiThisinhGet.Chuyennganhthi = chuyennganh.Ten;
                    }
                    catch
                    {
                    }



                }
                catch
                {
                }

                var u = thitracnghiemContext.PhienthiThisinhs.Where(x => x.Thisinhid == i.Id).OrderByDescending(x=> x.Id).ToList();
                kithiThisinhGet.Diem = "0";
                kithiThisinhGet.Diemthi = 0;

                if (u != null && u.Count() != 0)
                {
                    var v = u.Where(u => phienthis.Contains((int)u.Phienthiid)).OrderByDescending(x => x.Diem).First();
                    if (v != null && v.Diem != null)
                    {
                        kithiThisinhGet.Diem = (Math.Round((float)v.Diem/25*10, 2)).ToString();
                        kithiThisinhGet.Diemthi = v.Diem;
                        //kithiThisinhGet.Thoigianbatdau = v.Thoigianbatdau;
                        //kithiThisinhGet.Thoigianketthuc = v.Thoigianketthuc;
                        kithiThisinhGet.Made = v.Made;
                        //kithiThisinhGet.Dethiuuid = v.Dethiuuid;
                    }
                    

                }
                rs.Add(kithiThisinhGet);


            }
            return rs;
        }

    }
}
