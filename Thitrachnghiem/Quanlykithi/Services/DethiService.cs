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
using Thitrachnghiem.QuanlyDethi.Models.Schemas;

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

        public CauhoiGet convertCauhoi(Cauhoi cauhoi, int dethiid)
        {
            if (cauhoi == null)
                return null;
            CauhoiGet result = new CauhoiGet();

            result.Id = cauhoi.Id;
            result.Uuid = cauhoi.Uuid;
            result.Noidung = cauhoi.Noidung;

            F_ChitietdethiCautraloi f_ChitietdethiCautraloi = new F_ChitietdethiCautraloi();
            var listctlid = f_ChitietdethiCautraloi.GetChitietdethiCautraloiWithCauhoiid(cauhoi.Id, dethiid);

            var listctl = new List<CautraloiGet>();
            F_Cautraloi f_Cautraloi = new F_Cautraloi();

            foreach (var i in listctlid)
            {
                var ctl = f_Cautraloi.GetCautraloisById((int)i.Cautraloiid);
                listctl.Add(convertCautraloi(ctl));
            }

            result.Cautralois = listctl;


            return result;
        }


        public CauhoiGet createCauhoiToChitietdethi_Cautraloi(Cauhoi cauhoi, int dethiid)
        {
            if (cauhoi == null)
                return null;
            CauhoiGet result = new CauhoiGet();

            result.Id = cauhoi.Id;
            result.Uuid = cauhoi.Uuid;
            result.Noidung = cauhoi.Noidung;

            F_Cautraloi f_Cautraloi = new F_Cautraloi();
            var listctl = f_Cautraloi.GetCautraloiWithCauhoiid(cauhoi.Id).ConvertAll(x => convertCautraloi(x)).ToList();
            var listkhongneo = listctl.Where(x => x.Neo == false).ToList();
            F_ChitietdethiCautraloi f_ChitietdethiCautraloi = new F_ChitietdethiCautraloi();
            List<CautraloiGet> list = new List<CautraloiGet>();
            foreach (var i in listctl)
            {
                if (i.Neo == true)
                {
                    list.Add(i);
                    ChitietdethiCautraloi chitietdethiCautraloi = new ChitietdethiCautraloi();
                    chitietdethiCautraloi.Cautraloiid = i.Id;
                    chitietdethiCautraloi.Dethiid = dethiid;
                    chitietdethiCautraloi.Cauhoiid = cauhoi.Id;
                    f_ChitietdethiCautraloi.Create(chitietdethiCautraloi);
                }
                else
                {
                    var u = listkhongneo.OrderBy(c => Guid.NewGuid()).First();
                    list.Add(u);
                    listkhongneo.Remove(u);
                    ChitietdethiCautraloi chitietdethiCautraloi = new ChitietdethiCautraloi();
                    chitietdethiCautraloi.Cautraloiid = u.Id;
                    chitietdethiCautraloi.Dethiid = dethiid;
                    chitietdethiCautraloi.Cauhoiid = cauhoi.Id;
                    f_ChitietdethiCautraloi.Create(chitietdethiCautraloi);
                }
            }

            result.Cautralois = list;



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
            var listcauhoi = f_Chitietdethi.GetChitietdethiWithDethiid(dethi.Id).OrderBy(x => x.Id);
            foreach (var i in listcauhoi)
            {
                F_Cauhoi f_Cauhoi = new F_Cauhoi();
                var cauhoi = f_Cauhoi.GetCauhoiByidWithFalse(i.Cauhoiid);
                if (cauhoi != null)
                    result.Cauhois.Add(convertCauhoi(cauhoi, dethi.Id));
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
                    result.Thoigian = kithi.Thoigianbatdau + " - " + kithi.Thoigianketthuc;

                }
                catch
                {
                    result.Chuyennganh = "";
                }
            }
            catch
            {
                result.Bac = 0;
                result.Chuyennganh = "";
                result.Trinhdodaotao = "";
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
                result.Thoigian = kithi.Thoigianbatdau + " - " + kithi.Thoigianketthuc;
                try
                {
                    F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                    var chuyennganh = f_Chuyennganh.GetChuyennganhsById((int)kithi.Chuyennganhid);
                    result.Chuyennganh = chuyennganh.Ten;
                    result.Trinhdodaotao = chuyennganh.Trinhdodaotao;
                }
                catch
                {
                    result.Chuyennganh = "";

                }
            }
            catch
            {

                result.Bac = 0;
                result.Chuyennganh = "";
                result.Trinhdodaotao = "";
            }

            return result;
        }
        public DethiGet GetDethiByUuid(Guid guid)
        {
            return convertDethi(new F_Dethi().GetDethisByUuid(guid));
        }

        public bool DeleteDethiByUuid(Guid guid)
        {

            F_Dethi f_Dethi = new F_Dethi();
            var dethi = f_Dethi.GetDethisByUuid(guid);
            if (dethi == null)
                throw new Exception("Sai ma de thi");

            f_Dethi.Delete(guid);
            return true;
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

                var ctl_old = f_ThisinhTraloi.GetThisinhTraloiWithDethiidandCauhoiidandThisinhidandCautlid(
                    dethi.Id, cauhoi.Id, ts.Id, ctl.Id);
                if (ctl_old != null && cautraloi.Status == false)
                {
                    f_ThisinhTraloi.Delete((Guid)ctl_old.Uuid);
                }
                else
                if (cautraloi.Status == true)
                {
                    ThisinhTraloi thisinhTraloi = new ThisinhTraloi();
                    thisinhTraloi.Thisinhid = ts.Id;
                    thisinhTraloi.Thoigiantraloi = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    thisinhTraloi.Cauhoiid = cauhoi.Id;
                    thisinhTraloi.Cautraloiid = ctl.Id;
                    thisinhTraloi.Dethiid = dethi.Id;
                    f_ThisinhTraloi.Create(thisinhTraloi);
                }

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

            if (ts.Thixong == true)
                throw new Exception("Thí sinh đã thi xong");
            F_Dethi f_Dethi = new F_Dethi();

            var phienthiid = f_Dethi.Kiemtradamochua(ts.Email);
            if (phienthiid == null)
                throw new Exception("Chua mo phien thi");

            Dethi dethi = f_Dethi.CreateDethiForThisinh((int)ts.Kithiid, ts.Id);


            F_Chitietdethi f_Chitietdethi = new F_Chitietdethi();
            var listcauhoi = f_Chitietdethi.GetChitietdethiWithDethiid(dethi.Id).OrderBy(x => x.Id);
            foreach (var i in listcauhoi)
            {
                F_Cauhoi f_Cauhoi = new F_Cauhoi();
                var cauhoi = f_Cauhoi.GetCauhoiByidWithFalse(i.Cauhoiid);
                if (cauhoi != null)
                    createCauhoiToChitietdethi_Cautraloi(cauhoi, dethi.Id);
                        
            }


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

            if (keyword == "null" || keyword == null)
                keyword = "";
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
            && x.Bac == bac && x.Thoigian.Contains(keyword)).ToList();


        }

        public List<ThongkedethiGet> thongkedethitheochuyennganh(string nam, string trinhdodaotao, string chuyennganhuuuid)
        {
            List<ThongkedethiGet> result = new List<ThongkedethiGet>();

            F_Dethi f_Dethi = new F_Dethi();
            F_Kithi f_Kithi = new F_Kithi();
            F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
            var chuyennganh = f_Chuyennganh.GetChuyennganhs();
            var kithi = f_Kithi.GetKithis(nam);
            if (trinhdodaotao != null && trinhdodaotao != "")
            {
                if (chuyennganhuuuid == null || chuyennganhuuuid == "")
                {
                    foreach (var u in chuyennganh)
                    {
                        ThongkedethiGet thongkecauhoiGet = new ThongkedethiGet();
                        if (u.Trinhdodaotao.Trim().Equals(trinhdodaotao))
                        {
                            thongkecauhoiGet.Ten = u.Ten;

                            var sodethi = 0;
                            var kithis = kithi.Where(x => x.Chuyennganhid == u.Id).ToList();

                            foreach (var uu in kithis)
                            {
                                var dethis = f_Dethi.GetDethiWithKithiid(uu.Id);
                                sodethi = sodethi + dethis.Count;

                            }
                            thongkecauhoiGet.Tongso = sodethi;
                            result.Add(thongkecauhoiGet);
                        }
                    }
                }
                else
                {
                    Chuyennganh chuyennganhtimkiem = f_Chuyennganh.GetChuyennganhsByUuid(new Guid(chuyennganhuuuid));
                    if (chuyennganhtimkiem == null)
                        throw new Exception("chuyen nganh khong ton tai");

                    int last = 10;
                    if (trinhdodaotao.Equals( "DAIHOC" ) || trinhdodaotao.Equals( "CAODANG")) last = 12;

                    for (int u = 1; u <= last; u++)
                    {
                        ThongkedethiGet thongkecauhoiGet = new ThongkedethiGet();

                        thongkecauhoiGet.Ten = "Bậc " + u.ToString();

                        var sodethi = 0;
                        var kithis = kithi.Where(x => x.Chuyennganhid == chuyennganhtimkiem.Id && x.Bac == u).ToList();

                        foreach (var uu in kithis)
                        {
                            var dethis = f_Dethi.GetDethiWithKithiid(uu.Id);
                            sodethi = sodethi + dethis.Count;

                        }
                        thongkecauhoiGet.Tongso = sodethi;
                        result.Add(thongkecauhoiGet);
                    }
                }
            }
            else
            {
                try
                {
                    ThongkedethiGet thongkecauhoiGet = new ThongkedethiGet();

                    thongkecauhoiGet.Ten = "Đại học";

                    var sodethi = 0;
                    var kithis = kithi.Where(x => x.Trinhdodaotao.Trim().Equals("DAIHOC")).ToList();

                    foreach (var uu in kithis)
                    {
                        var dethis = f_Dethi.GetDethiWithKithiid(uu.Id);
                        sodethi = sodethi + dethis.Count;

                    }
                    thongkecauhoiGet.Tongso = sodethi;
                    result.Add(thongkecauhoiGet);

                }
                catch { }

                try
                {
                    ThongkedethiGet thongkecauhoiGet = new ThongkedethiGet();

                    thongkecauhoiGet.Ten = "Cao đẳng";

                    var sodethi = 0;
                    var kithis = kithi.Where(x => x.Trinhdodaotao.Trim().Equals( "CAODANG")).ToList();

                    foreach (var uu in kithis)
                    {
                        var dethis = f_Dethi.GetDethiWithKithiid(uu.Id);
                        sodethi = sodethi + dethis.Count;

                    }
                    thongkecauhoiGet.Tongso = sodethi;
                    result.Add(thongkecauhoiGet);
                } catch { }

                try
                {
                    ThongkedethiGet thongkecauhoiGet = new ThongkedethiGet();

                    thongkecauhoiGet.Ten = "Trung cấp";

                    var sodethi = 0;
                    var kithis = kithi.Where(x => x.Trinhdodaotao.Trim().Equals( "TRUNGCAP")).ToList();

                    foreach (var uu in kithis)
                    {
                        var dethis = f_Dethi.GetDethiWithKithiid(uu.Id);
                        sodethi = sodethi + dethis.Count;

                    }
                    thongkecauhoiGet.Tongso = sodethi;
                    result.Add(thongkecauhoiGet);
                }
                catch { }
                try
                {
                    ThongkedethiGet thongkecauhoiGet = new ThongkedethiGet();

                    thongkecauhoiGet.Ten = "Sơ cấp";

                    var sodethi = 0;
                    var kithis = kithi.Where(x => x.Trinhdodaotao.Trim().Equals( "SOCAP")).ToList();

                    foreach (var uu in kithis)
                    {
                        var dethis = f_Dethi.GetDethiWithKithiid(uu.Id);
                        sodethi = sodethi + dethis.Count;

                    }
                    thongkecauhoiGet.Tongso = sodethi;
                    result.Add(thongkecauhoiGet);
                }
                catch { }
            }          
               

            return result;
        }

    }
}
