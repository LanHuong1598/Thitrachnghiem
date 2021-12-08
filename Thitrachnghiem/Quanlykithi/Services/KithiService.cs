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

            return convert(new F_Phienthi().Create(phienthi));
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
    }
}
