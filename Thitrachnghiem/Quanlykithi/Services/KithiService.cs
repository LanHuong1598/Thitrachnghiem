using System;
using System.Collections.Generic;
using System.IO;
using Thitrachnghiem.Quanlykithi.Models.Entities;
using Thitrachnghiem.Quanlykithi.Models.Functions;
using Microsoft.AspNetCore.Http;
using Thitrachnghiem.Quanlycauhoi.Models.Functions;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;
using Thitrachnghiem.Quanlykithi.Models.Schemas;
using Thitrachnghiem.Quanlythisinh.Models.Functions;
using Thitrachnghiem.Quanlycauhoi.Models.Schemas;
using System.Linq;

namespace Thitrachnghiem.Quanlykithi.Services
{
    public class KithiService :IKithiService
    {

        public MatrandethiGet convertMatra(Matrandethi matrandethi)
        {
            MatrandethiGet result = new MatrandethiGet();
            result.Id = matrandethi.Id;
            result.Uuid = matrandethi.Uuid;
            result.Bac = matrandethi.Bac;
            result.Tile = matrandethi.Tile;

            try
            {
                F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                var chuyennganh = f_Chuyennganh.GetChuyennganhsById((int)matrandethi.Chuyennganhid);
                result.ChuyennganhGuid = (Guid)chuyennganh.Uuid;
                result.Chuyennganh = chuyennganh.Ten;
                result.Trinhdodaotao = chuyennganh.Trinhdodaotao;
            }
            catch
            {
            }
            return result;
        }
        public KithiGet convert(Kithi kithi)
        {
            if (kithi == null)
                return null;
            KithiGet result = new KithiGet();

            result.Id = kithi.Id;
            result.Uuid = kithi.Uuid;
            result.Bac = kithi.Bac;
            result.Thoigianbatdau = kithi.Thoigianbatdau;
            result.Thoigianketthuc = kithi.Thoigianketthuc;

            try
            {
                F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                var chuyennganh = f_Chuyennganh.GetChuyennganhsById((int)kithi.Chuyennganhid);
                result.ChuyennganhGuid = (Guid)chuyennganh.Uuid;
                result.Chuyennganh = chuyennganh.Ten;
                result.Trinhdodaotao = chuyennganh.Trinhdodaotao;

                F_Thisinh f_Thisinh = new F_Thisinh();
                var ts = f_Thisinh.GetThisinhWithKithiid(kithi.Id);
                if (ts != null && ts.Count != 0)
                {
                    var u = GetKithiThisinhs((Guid)kithi.Uuid);
                    result.Sothisinh = u.Count;
                    result.Sothisinhdat = u.Where(x => x.Diem != null && x.Diem >= 15).Count();
                    result.Sothisinhtruot = result.Sothisinh - result.Sothisinhdat;
                }
                else
                {
                    result.Sothisinh = 0;
                    result.Sothisinhtruot = 0;
                    result.Sothisinhdat = 0;
                }
            }
            catch
            {
            }



            return result;
        }

        public KithiDetail convertDetail(Kithi kithi)
        {
            if (kithi == null)
                return null;
            KithiDetail result = new KithiDetail();

            result.Id = kithi.Id;
            result.Uuid = kithi.Uuid;
            result.Bac = kithi.Bac;
            result.Thoigianbatdau = kithi.Thoigianbatdau;
            result.Thoigianketthuc = kithi.Thoigianketthuc;

            try
            {
                F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                var chuyennganh = f_Chuyennganh.GetChuyennganhsById((int)kithi.Chuyennganhid);
                result.ChuyennganhGuid = (Guid)chuyennganh.Uuid;
                result.Chuyennganh = chuyennganh.Ten;
                result.Trinhdodaotao = chuyennganh.Trinhdodaotao;
            }
            catch
            {
            }

            
            F_Matrandethi f_Matrandethi = new F_Matrandethi();
            var list = f_Matrandethi.GetMatrandethiWithKithiid(kithi.Id);
            if (list != null)
                result.Matrandethis = list.ConvertAll(x => convertMatra(x));

            return result;

        }
        public List<KithiGet> GetkithibyChuyennganh(string he, string chuyennganhuuid, int bac, string keyword)
        {
            if (he != null && he != "")
            {
                if (chuyennganhuuid != null && chuyennganhuuid != "")
                {
                    F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                    Chuyennganh chuyennganh = f_Chuyennganh.GetChuyennganhsByUuid(new Guid(chuyennganhuuid));
                    if (chuyennganh == null)
                        throw new Exception("uuid chuyen nganh khong hop le");
                    if (bac != 0)
                    {
                        List<Kithi> kithis = new F_Kithi().GetKithis(he, chuyennganh.Id, bac, keyword);
                        if (kithis != null)
                        {
                            return kithis.ConvertAll(x => convert(x));
                        }
                        else
                            return null;
                    }
                    else
                    {
                        List<Kithi> kithis = new F_Kithi().GetKithis(he, chuyennganh.Id, keyword);
                        if (kithis != null)
                        {
                            return kithis.ConvertAll(x => convert(x));
                        }
                        else
                            return null;
                    }

                }
                else
                {
                    List<Kithi> kithis = new F_Kithi().GetKithis(he, keyword);
                    if (kithis != null)
                    {
                        return kithis.ConvertAll(x => convert(x));
                    }
                    else
                        return null;
                }

            }
            {
                List<Kithi> kithis = new F_Kithi().GetKithis(keyword);
                if (kithis != null)
                {
                    return kithis.ConvertAll(x => convert(x));
                }
                else
                    return null;
            }
        }

        public KithiDetail GetkithiByUuid(Guid guid)
        {
            return convertDetail(new F_Kithi().GetKithisByUuid(guid));
        }

        public KithiDetail Createkithi(KithiCreate kithiCreate)
        {
            Kithi kithi = kithiCreate.Convert();

            try
            {
                F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                var chuyennganh = f_Chuyennganh.GetChuyennganhsByUuid(kithiCreate.ChuyennganhGuid);
                kithi.Chuyennganhid = chuyennganh.Id;
                kithi.Trinhdodaotao = chuyennganh.Trinhdodaotao;
            }
            catch
            {
            }

            kithi.Status = true;
            kithi.Dangthi = false;
            kithi.Socauhoi = 25;
            kithi = new F_Kithi().Create(kithi);

            F_Matrandethi f_Matrandethi = new F_Matrandethi();
            foreach (var i in kithiCreate.Matrandethis)
            {
                Matrandethi matrandethi = new Matrandethi();
                matrandethi.Kithiid = kithi.Id;
                matrandethi.Tile = i.Tile;
                matrandethi.Bac = i.Bac;
                try
                {
                    F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                    var chuyennganh = f_Chuyennganh.GetChuyennganhsByUuid(i.ChuyennganhGuid);
                    matrandethi.Chuyennganhid = chuyennganh.Id;
                    matrandethi.Status = true;
                    f_Matrandethi.Create(matrandethi);
                }
                catch
                {
                }

            }

            return convertDetail(kithi);
        }

        public KithiDetail Updatekithi(KithiUpdate kithiUpdate)
        {
            F_Kithi F_Kithi = new F_Kithi();
            Kithi kithi = F_Kithi.GetKithisByUuid((Guid)kithiUpdate.Uuid);
            if (kithi == null)
                throw new InvalidDataException("Mã uuid không tồn tại");

            kithi.Bac = kithiUpdate.Bac;
            kithi.Thoigianbatdau = kithiUpdate.Thoigianbatdau;
            kithi.Thoigianketthuc = kithiUpdate.Thoigianketthuc;
            try
            {
                F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                var chuyennganh = f_Chuyennganh.GetChuyennganhsByUuid(kithiUpdate.ChuyennganhGuid);
                kithi.Chuyennganhid = chuyennganh.Id;
                kithi.Trinhdodaotao = chuyennganh.Trinhdodaotao;
            }
            catch
            {
            }

            F_Matrandethi f_Matrandethi = new F_Matrandethi();

            var list = f_Matrandethi.GetMatrandethiWithKithiid(kithi.Id);
            foreach (var i in list)
            {
                f_Matrandethi.Delete((Guid)i.Uuid);
            }

            foreach (var i in kithiUpdate.Matrandethis)
            {
                Matrandethi matrandethi = new Matrandethi();
                matrandethi.Kithiid = kithi.Id;
                matrandethi.Tile = i.Tile;
                matrandethi.Bac = i.Bac;
                try
                {
                    F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                    var chuyennganh = f_Chuyennganh.GetChuyennganhsByUuid(i.ChuyennganhGuid);
                    matrandethi.Chuyennganhid = chuyennganh.Id;
                    matrandethi.Status = true;
                    f_Matrandethi.Create(matrandethi);
                }
                catch
                {
                }

            }

            return convertDetail(F_Kithi.Update(kithi));
        }

        public KithiGet Deletekithi(Guid guid)
        {
            F_Kithi F_Kithi = new F_Kithi();
            Kithi kithi = F_Kithi.GetKithisByUuid(guid);
            F_Matrandethi f_Matrandethi = new F_Matrandethi();

            var list = f_Matrandethi.GetMatrandethiWithKithiid(kithi.Id);
            foreach (var i in list)
            {
                f_Matrandethi.Delete((Guid)i.Uuid);
            }
            return convert(F_Kithi.Delete(guid));
        }


        public List<KithiGet> Getall()
        {
            List<Kithi> kithis = new F_Kithi().GetKithis("");
            if (kithis != null)
            {
                return kithis.ConvertAll(x => convert(x));
            }
            else
                return null;
        }

        public PhienthiGet convert(Phienthi phienthi)
        {
            PhienthiGet phienthiGet = new PhienthiGet();
            phienthiGet.Id = phienthi.Id;
            phienthiGet.Thoigianbatdau = phienthi.Thoigianbatdau;
            phienthiGet.Thoigianketthuc = phienthi.Thoigianketthuc;
            phienthiGet.Uuid = phienthi.Uuid;
            F_Kithi F_Kithi = new F_Kithi();
            Kithi kithi = F_Kithi.GetKithisById((int)phienthi.Kithiid);
            if (kithi != null)
            {
                phienthiGet.Kithiuuid = (Guid)kithi.Uuid;
                phienthiGet.Bac = kithi.Bac;
                try
                {
                    F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                    var chuyennganh = f_Chuyennganh.GetChuyennganhsById((int)kithi.Chuyennganhid);
                    phienthiGet.ChuyennganhGuid = (Guid)chuyennganh.Uuid;
                    phienthiGet.Chuyennganh = chuyennganh.Ten;
                    phienthiGet.Trinhdodaotao = chuyennganh.Trinhdodaotao;
                }
                catch
                {
                }
            }
            return phienthiGet;
        }

        public PhienthiGet OpenPhienthi(Guid Kithiuuid)
        {
            F_Kithi F_Kithi = new F_Kithi();
            Kithi kithi = F_Kithi.GetKithisByUuid(Kithiuuid);

            Phienthi phienthi = new Phienthi();
            phienthi.Kithiid = kithi.Id;
            phienthi.Thoigianbatdau = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            phienthi.Thoigianketthuc = DateTime.Now.AddMinutes(30).ToString("yyyy/MM/dd HH:mm:ss");

            kithi.Dangthi = true;
            F_Kithi.Update(kithi);
            F_Phienthi f_Phienthi = new F_Phienthi();
            var hientai = f_Phienthi.GetPhienthiDangMoByKithi(kithi.Id);
            if (hientai != null)
                throw new Exception("Dang mo roi");


            return convert(f_Phienthi.Create(phienthi));
        }

        public PhienthiGet closePhienthi(Guid Kithiuuid)
        {
            F_Kithi F_Kithi = new F_Kithi();
            Kithi kithi = F_Kithi.GetKithisByUuid(Kithiuuid);

            F_Phienthi f_Phienthi = new F_Phienthi();
            Phienthi phienthi = f_Phienthi.GetPhienthiDangMoByKithi(kithi.Id);
            phienthi.Kithiid = kithi.Id;
            phienthi.Thoigianketthuc = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            kithi.Dangthi = false;
            F_Kithi.Update(kithi);
            f_Phienthi.Update(phienthi);

            return convert(phienthi);
        }


        public List<PhienthiGet> GetPhienthiGetsisOpen()
        {
            F_Phienthi f_Phienthi = new F_Phienthi();
            var pt = f_Phienthi.GetPhienthisIsOpen();

            return pt.ConvertAll(x => convert(x));


        }
        public List<PhienthiGet> GetPhienthis()
        {
            F_Phienthi f_Phienthi = new F_Phienthi();
            var pt = f_Phienthi.GetPhienthis();

            return pt.ConvertAll(x => convert(x));


        }

        public PhienthiThisinhGet convertphienthiThisinhGet(PhienthiThisinh phienthiThisinh)
        {
            PhienthiThisinhGet rs = new PhienthiThisinhGet();
            rs.Made = phienthiThisinh.Made;
            rs.Thoigianbatdau = phienthiThisinh.Thoigianbatdau;
            rs.Thoigianketthuc = phienthiThisinh.Thoigianketthuc;
            rs.Dethiuuid = phienthiThisinh.Dethiuuid;
            try
            {
                F_Thisinh f_Thisinh = new F_Thisinh();
                var ts = f_Thisinh.GetThisinhsById((int)phienthiThisinh.Thisinhid);
                rs.Tenthisinh = ts.Name;
                rs.Email = ts.Email;
            }catch
            {

            }
            return rs;


        }

        public List<PhienthiThisinhGet> GetPhienthiThisinhs(Guid Phienthiuuid)
        {
            F_Phienthi f_Phienthi = new F_Phienthi();
            var pt = f_Phienthi.GetPhienthisByUuid(Phienthiuuid);

            var ts = f_Phienthi.GetThisinhsByPhienthiid(pt.Id);

            return ts.ConvertAll(x => convertphienthiThisinhGet(x));
        }

        public  ThisinhTraloiGet convertThisinhTraloi(ThisinhTraloi thisinhTraloi)
        {
            ThisinhTraloiGet rs = new ThisinhTraloiGet();
            rs.Id = thisinhTraloi.Id;

            F_Cautraloi f_Cautraloi = new F_Cautraloi();
            var tl = f_Cautraloi.GetCautraloisById((int)thisinhTraloi.Cautraloiid);

            if (tl.Trangthai == true)
                rs.IsTrue = true;
            else rs.IsTrue = false;

            rs.Thoigiantraloi = thisinhTraloi.Thoigiantraloi;

            F_Cauhoi f_Cauhoi = new F_Cauhoi();
            var cauhoi = f_Cauhoi.GetCauhoiByidWithFalse(thisinhTraloi.Cauhoiid);
            if (cauhoi != null)
                rs.Cauhoi = cauhoi.Noidung;
            try
            {
                rs.Cautralois = new List<CautraloiluachonGet>();
                var list = f_Cautraloi.GetCautraloiWithCauhoiid(cauhoi.Id);
                foreach(var i in list)
                {
                    CautraloiluachonGet u = new CautraloiluachonGet();
                    u.Ladapandung = i.Trangthai;
                    u.Noidung = i.Noidung;
                    if (tl.Id == i.Id)
                        u.Duocchon = true;
                    else
                        u.Duocchon = false;
                    rs.Cautralois.Add(u);                                    

                }
            }
            catch { }

            return rs;

        }

        public Bailamthisinh Getcautraloidethi(Guid Dethiuuid)
        {
            Bailamthisinh bailamthisinh = new Bailamthisinh();
            bailamthisinh.Diem = 0;

            F_Dethi f_Dethi = new F_Dethi();
            var dethi = f_Dethi.GetDethiByUuidWithFalse(Dethiuuid);

            F_Chitietdethi f_Chitietdethi = new F_Chitietdethi();
            var listcauhoi = f_Chitietdethi.GetChitietdethiWithDethiid(dethi.Id);
            F_ThisinhTraloi f_Phienthi = new F_ThisinhTraloi();
            F_Cautraloi f_Cautraloi = new F_Cautraloi();

            List<ThisinhTraloiGet> rs = new List<ThisinhTraloiGet>();
            foreach (var i in listcauhoi)
            {
                F_Cauhoi f_Cauhoi = new F_Cauhoi();
                var cauhoi = f_Cauhoi.GetCauhoiByidWithFalse(i.Cauhoiid);
                if (cauhoi != null)
                {
                    var listcautlcuathisinh = f_Phienthi.GetListThisinhTraloiWithDethiidandCauhoiidandThisinhid(cauhoi.Id, dethi.Id, (int)dethi.Thisinhid);
                    var listcautl = f_Cautraloi.GetCautraloiWithCauhoiid(cauhoi.Id);
                    ThisinhTraloiGet thisinhTraloiGet = new ThisinhTraloiGet();

                    if (cauhoi != null)
                        thisinhTraloiGet.Cauhoi = cauhoi.Noidung;
                    thisinhTraloiGet.Cautralois = new List<CautraloiluachonGet>();

                    foreach (var ii in listcautl)
                    {
                        CautraloiluachonGet u = new CautraloiluachonGet();
                        u.Uuid = ii.Uuid;
                        u.Ladapandung = ii.Trangthai;
                        u.Noidung = ii.Noidung;
                        u.Duocchon = false;
                        thisinhTraloiGet.Cautralois.Add(u);
                    }

                    if (listcautlcuathisinh != null || listcautlcuathisinh.Count != 0)
                    {
                        thisinhTraloiGet.Id = listcautlcuathisinh[0].Id;

                        foreach (var thisinhTraloi in listcautlcuathisinh)
                        {
                            var tl = f_Cautraloi.GetCautraloisById((int)thisinhTraloi.Cautraloiid);
                            thisinhTraloiGet.Thoigiantraloi = thisinhTraloi.Thoigiantraloi;
                            try
                            {
                                var u = thisinhTraloiGet.Cautralois.Where(x => x.Uuid == tl.Uuid).FirstOrDefault();
                                u.Duocchon = true;
                            }
                            catch { }
                        }
                    }

                    int soluong = thisinhTraloiGet.Cautralois.Where(x => x.Duocchon == x.Ladapandung).Count();
                    try
                    {
                        if (soluong == listcautl.Count()) bailamthisinh.Diem = bailamthisinh.Diem + 1;

                            }
                    catch { }
                        
                }
            }

            bailamthisinh.Cauhois = rs;
            return bailamthisinh;
        }

        public List<KithiThisinhGet> GetKithiThisinhs(Guid Kithiuuid)
        {
            F_Kithi f_Kithi = new F_Kithi();
            return f_Kithi.GetKithiThisinhs(Kithiuuid);
        }
    }
}
