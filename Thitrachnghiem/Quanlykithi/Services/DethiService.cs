using System;
using System.Collections.Generic;
using System.IO;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;
using Thitrachnghiem.Quanlycauhoi.Models.Functions;
using Thitrachnghiem.Quanlycauhoi.Models.Schemas;
using Microsoft.AspNetCore.Http;
using Thitrachnghiem.Quanlykithi.Models.Entities;
using Thitrachnghiem.Quanlykithi.Models.Schemas;
using Thitrachnghiem.Quanlykithi.Models.Functions;
using Thitrachnghiem.Quanlythisinh.Models.Functions;
using Thitrachnghiem.Users.Models.Functions;
using System.Linq;

namespace Thitrachnghiem.Quanlykithi.Services
{
    public class DethiService : IDethiService
    {
        public CautraloiGet convertCautraloi(Cautraloi cautraloi)
        {
            CautraloiGet cautraloiGet = new CautraloiGet();
            cautraloiGet.Id = cautraloi.Id;
            cautraloiGet.Uuid = cautraloi.Uuid;
            cautraloiGet.Neo = cautraloi.Neo;
            cautraloiGet.Noidung = cautraloi.Noidung;
            return cautraloiGet;
        }

        public CauhoiGet convertCauhoi(Cauhoi cauhoi)
        {
            if (cauhoi == null)
                return null;
            CauhoiGet result = new CauhoiGet();

            result.Id = cauhoi.Id;
            result.Uuid = cauhoi.Uuid;
            result.Noidung = cauhoi.Noidung;

            F_Cautraloi f_Cautraloi = new F_Cautraloi();
            result.Cautralois = f_Cautraloi.GetCautraloiWithCauhoiid(cauhoi.Id).ConvertAll(x => convertCautraloi(x));

            return result;
        }

        public DethiGet convertDethi(Dethi dethi)
        {
            if (dethi == null)
                return null;
            DethiGet result = new DethiGet();

            result.Id = dethi.Id;
            result.Uuid = dethi.Uuid;
            result.Madethi = dethi.Madethi;
            result.Cauhois = new List<CauhoiGet>();


            F_Chitietdethi f_Chitietdethi = new F_Chitietdethi();
            var listcauhoi = f_Chitietdethi.GetChitietdethiWithDethiid(dethi.Id);
            foreach (var i in listcauhoi)
            {
                F_Cauhoi f_Cauhoi = new F_Cauhoi();
                var cauhoi = f_Cauhoi.GetCauhoiByidWithFalse(i.Cauhoiid);
                if (cauhoi != null)
                    result.Cauhois.Add(convertCauhoi(cauhoi));
            }

            F_Kithi f_Kithi = new F_Kithi();
            try
            {
                Kithi kithi = f_Kithi.GetKithisById((int)dethi.Kithiid);
                result.Kithiuuid = kithi.Uuid.ToString();
                result.Trinhdodaotao = kithi.Trinhdodaotao;
                result.Bac = kithi.Bac;
                try
                {
                    F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                    var chuyennganh = f_Chuyennganh.GetChuyennganhsById((int)kithi.Chuyennganhid);
                    result.Chuyennganh = chuyennganh.Ten;
                    result.Trinhdodaotao = chuyennganh.Trinhdodaotao;
                }
                catch
                {
                }
            }
            catch
            {
            }
            return result;
        }
        public DethiGet convert(Dethi dethi)
        {
            if (dethi == null)
                return null;
            DethiGet result = new DethiGet();

            result.Id = dethi.Id;
            result.Uuid = dethi.Uuid;
            result.Madethi = dethi.Madethi;
            result.Thoigian = dethi.Thoigian;
            F_Kithi f_Kithi = new F_Kithi();
            try
            {
                Kithi kithi = f_Kithi.GetKithisById((int)dethi.Kithiid);
                result.Kithiuuid = kithi.Uuid.ToString();
                result.Trinhdodaotao = kithi.Trinhdodaotao;
                result.Bac = kithi.Bac;
                try
                {
                    F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                    var chuyennganh = f_Chuyennganh.GetChuyennganhsById((int)kithi.Chuyennganhid);
                    result.Chuyennganh = chuyennganh.Ten;
                    result.Trinhdodaotao = chuyennganh.Trinhdodaotao;
                }
                catch
                {
                }
            }
            catch
            {
            }

            return result;
        }
        public DethiGet GetDethiByUuid(Guid guid)
        {
            return convertDethi(new F_Dethi().GetDethisByUuid(guid));
        }
        public List<Guid> GetDanhsachCauhoiDethiByUuid(Guid guid)
        {
            List<Guid> result = new List<Guid>();
            Dethi dethi = new F_Dethi().GetDethisByUuid(guid);
            F_Chitietdethi f_Chitietdethi = new F_Chitietdethi();
            var listcauhoi = f_Chitietdethi.GetChitietdethiWithDethiid(dethi.Id);
            foreach (var i in listcauhoi)
            {
                F_Cauhoi f_Cauhoi = new F_Cauhoi();
                var cauhoi = f_Cauhoi.GetCauhoiByidWithFalse(i.Cauhoiid);
                if (cauhoi != null)
                    result.Add((Guid)cauhoi.Uuid);
            }
            return result;
        }
        public List<DethiGet> Getall()
        {
            List<Dethi> dethis = new F_Dethi().GetDethis();
            if (dethis != null)
            {
                return dethis.ConvertAll(x => convert(x));
            }
            else
                return null;
        }
        //public float Guicautraloi(Dapandethi cautraloi)
        //{
        //    F_Cautraloi f_Cautraloi = new F_Cautraloi();
        //    F_Cauhoi f_Cauhoi = new F_Cauhoi();

        //    foreach (var i in cautraloi.Cautralois)
        //    {
        //        f_Cautraloi.GetCautraloiByUuidWithFalse(new Guid(i.Cauhoiuuid));


        //    }
        //    return 100;
        //}

        public float Guicautraloi(int id, CautraloiThisinh cautraloi)
        {
            F_Thisinh f_Thisinh = new F_Thisinh();
            F_Users f_Users = new F_Users();
            var user = f_Users.GetUsersById(id);
            var ts = f_Thisinh.GetThisinhsByGmail(user.Username);
            if (ts == null)
                throw new Exception("Khong co thi sinh");

            F_Cautraloi f_Cautraloi = new F_Cautraloi();
            F_Cauhoi f_Cauhoi = new F_Cauhoi();
            F_Dethi f_Dethi = new F_Dethi();
            F_ThisinhTraloi f_ThisinhTraloi = new F_ThisinhTraloi();

            try
            {
                var dethi = f_Dethi.GetDethiByUuidWithFalse(cautraloi.Dethiuuid);
                var cauhoi = f_Cauhoi.GetCauhoiByUuidWithFalse(cautraloi.Cauhoiuuid);
                var ctl = f_Cautraloi.GetCautraloiByUuidWithFalse(cautraloi.Cautraloiuuid);
                var ctl_old = f_ThisinhTraloi.GetThisinhTraloiWithDethiidandCauhoiidandThisinhid(
                    dethi.Id, cauhoi.Id, ts.Id);
                if (ctl_old != null) {
                    f_ThisinhTraloi.Delete((Guid)ctl_old.Uuid);
                }
                ThisinhTraloi thisinhTraloi = new ThisinhTraloi();
                thisinhTraloi.Thisinhid = ts.Id;
                thisinhTraloi.Thoigiantraloi = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                thisinhTraloi.Cauhoiid = cauhoi.Id;
                thisinhTraloi.Cautraloiid = ctl.Id;
                thisinhTraloi.Dethiid = dethi.Id;
                f_ThisinhTraloi.Create(thisinhTraloi);

            }
            catch { }
            return 100;
        }

        public DethiGet MakeDethi(int id)
        {
            F_Thisinh f_Thisinh = new F_Thisinh();
            F_Users f_Users = new F_Users();
            var user = f_Users.GetUsersById(id);
            var ts = f_Thisinh.GetThisinhsByGmail(user.Username);
            if (ts == null)
                throw new Exception("Khong co thi sinh");
            F_Dethi f_Dethi = new F_Dethi();

            var phienthiid = f_Dethi.Kiemtradamochua(ts.Email);
            if (phienthiid == null)
                throw new Exception("Chua mo phien thi");

            Dethi dethi = f_Dethi.CreateDethiForThisinh((int)ts.Kithiid, ts.Id);

            PhienthiThisinh phienthiThisinh = new PhienthiThisinh();
            phienthiThisinh.Phienthiid = phienthiid;
            phienthiThisinh.Thisinhid = ts.Id;
            string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            phienthiThisinh.Thoigianbatdau = now;
            now = DateTime.Now.AddMinutes(30).ToString("yyyy/MM/dd HH:mm:ss");
            phienthiThisinh.Thoigianketthuc = now;
            phienthiThisinh.Made = dethi.Madethi;
            phienthiThisinh.Dethiuuid = dethi.Uuid.ToString();

            F_Phienthi f_Phienthi = new F_Phienthi();
            f_Phienthi.CreateThisinhphienthi(phienthiThisinh);

            return convertDethi(dethi);
        }

        public bool Kiemtraphienthidamohaychua(string user)
        {           
            F_Dethi f_Dethi = new F_Dethi();
            var u = f_Dethi.Kiemtradamochua(user);
            if (u == null) return false;
            else return true;
        }

        public List<DethiGet> getDethiByKithiuuid(Guid kithiuuid)
        {
            F_Dethi f_Dethi = new F_Dethi();
            F_Kithi f_Kithi = new F_Kithi();
            var kithi = f_Kithi.GetKithiByUuidWithFalse(kithiuuid);

            var list = f_Dethi.GetDethiWithKithiid(kithi.Id);
            return list.ConvertAll(x => convert(x));
        }


        public List<DethiGet> getDethiByChuyennganh(string he, string chuyennganhuuid, int bac, string keyword)
        {
            var list = Getall();
            try
            {
                F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                var chuyennganh = f_Chuyennganh.GetChuyennganhsByUuid(new Guid(chuyennganhuuid));
                chuyennganhuuid = chuyennganh.Ten;
            }
            catch
            {
                chuyennganhuuid = "";
            }
            if (he == "")
                return list;
            if (chuyennganhuuid == "")
                return list.Where(i => i.Trinhdodaotao.Contains(he)).ToList();        
            if (bac == 0)
                return list.Where(x => x.Trinhdodaotao.Contains(he) && x.Chuyennganh.Contains(chuyennganhuuid)).ToList();
            if (keyword == "")
                return list.Where(x => x.Trinhdodaotao.Contains(he) && x.Chuyennganh.Contains(chuyennganhuuid)
           && x.Bac == bac).ToList();

            return list.Where(x => x.Trinhdodaotao.Contains(he) && x.Chuyennganh.Contains(chuyennganhuuid)
            && x.Bac == bac && x.Thoigian == keyword).ToList();


        }

    }
}
