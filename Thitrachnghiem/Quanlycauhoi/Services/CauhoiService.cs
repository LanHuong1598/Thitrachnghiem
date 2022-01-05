using System;
using System.Collections.Generic;
using System.IO;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;
using Thitrachnghiem.Quanlycauhoi.Models.Functions;
using Thitrachnghiem.Quanlycauhoi.Models.Schemas;
using Microsoft.AspNetCore.Http;

namespace Thitrachnghiem.Quanlycauhoi.Services
{
    public class CauhoiService :ICauhoiService
    {
        public CauhoiGet convert(Cauhoi cauhoi)
        {
            if (cauhoi == null)
                return null;
            CauhoiGet result = new CauhoiGet();

            result.Id = cauhoi.Id;
            result.Noidung = cauhoi.Noidung;
            result.Uuid = cauhoi.Uuid;
            result.Bac = cauhoi.Bac;

            try
            {
                F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                var chuyennganh = f_Chuyennganh.GetChuyennganhsById((int)cauhoi.Idchuyennganh);
                result.ChuyennganhGuid = (Guid)chuyennganh.Uuid;
                result.Chuyennganh = chuyennganh.Ten;
                result.Trinhdodaotao = chuyennganh.Trinhdodaotao;
            }
            catch
            {
            }

            F_Cautraloi f_Cautraloi = new F_Cautraloi();
            result.Cautralois = f_Cautraloi.GetCautraloiWithCauhoiid(cauhoi.Id).ConvertAll(x => new CautraloiGet(x));


            return result;
        }
        public List<CauhoiGet> GetCauhoibyChuyennganh(string he, string chuyennganhuuid, int bac, string keyword)
        {
            if (he!= null && he != "")
            {
                if (chuyennganhuuid != null && chuyennganhuuid != "")
                {
                    F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                    Chuyennganh chuyennganh = f_Chuyennganh.GetChuyennganhsByUuid(new Guid(chuyennganhuuid));
                    if (chuyennganh == null)
                        throw new Exception("uuid chuyen nganh khong hop le");
                    if (bac != 0)
                    {

                        List<Cauhoi> cauhois = new F_Cauhoi().GetCauhois(he, chuyennganh.Id, bac, keyword);
                        if (cauhois != null)
                        {
                            return cauhois.ConvertAll(x => convert(x));
                        }
                        else
                            return null;
                    }
                    else
                    {
                        List<Cauhoi> cauhois = new F_Cauhoi().GetCauhois(he, chuyennganh.Id, keyword);
                        if (cauhois != null)
                        {
                            return cauhois.ConvertAll(x => convert(x));
                        }
                        else
                            return null;
                    }

                }
                else
                {
                    List<Cauhoi> cauhois = new F_Cauhoi().GetCauhois(he, keyword);
                    if (cauhois != null)
                    {
                        return cauhois.ConvertAll(x => convert(x));
                    }
                    else
                        return null;
                }
              
            }
            {
                List<Cauhoi> cauhois = new F_Cauhoi().GetCauhois(keyword);
                if (cauhois != null)
                {
                    return cauhois.ConvertAll(x => convert(x));
                }
                else
                    return null;
            }
        }


        public bool DeleteCauhoisbyChuyennganh(string chuyennganhuuid, int bac)
        {
            if (chuyennganhuuid != null && chuyennganhuuid != "")
            {
                F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                Chuyennganh chuyennganh = f_Chuyennganh.GetChuyennganhsByUuid(new Guid(chuyennganhuuid));
                F_Cauhoi f_Cauhoi = new F_Cauhoi();
                if (chuyennganh == null)
                    throw new Exception("uuid chuyen nganh khong hop le");
                if (bac != 0)
                {
                    List<Cauhoi> cauhois = f_Cauhoi.GetCauhois(chuyennganh.Trinhdodaotao, chuyennganh.Id, bac, "");
                    if (cauhois != null)
                    {
                        try
                        {
                            foreach (var u in cauhois)
                            {
                                f_Cauhoi.Delete((Guid)u.Uuid);
                            }
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                    else
                        return true;
                }
                else
                {
                    List<Cauhoi> cauhois = new F_Cauhoi().GetCauhois(chuyennganh.Trinhdodaotao, chuyennganh.Id, "");
                    try
                    {
                        foreach (var u in cauhois)
                        {
                            f_Cauhoi.Delete((Guid)u.Uuid);
                        }
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }

            }
            return true;
        }


        public CauhoiGet GetCauhoiByUuid(Guid guid)
        {
            return convert(new F_Cauhoi().GetCauhoisByUuid(guid));
        }

        public CauhoiGet CreateCauhoi(CauhoiCreate cauhoiCreate)
        {
            Cauhoi cauhoi = cauhoiCreate.convert();

            try
            {
                F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                var chuyennganh = f_Chuyennganh.GetChuyennganhsByUuid(cauhoiCreate.ChuyennganhGuid);
                cauhoi.Idchuyennganh = chuyennganh.Id;
                cauhoi.Trinhdodaotao = chuyennganh.Trinhdodaotao;
            }
            catch
            {
            }

            cauhoi.Status = true;
            cauhoi = new F_Cauhoi().Create(cauhoi);

            F_Cautraloi f_Cautraloi = new F_Cautraloi();
            foreach (var i in cauhoiCreate.Cautralois)
            {
                Cautraloi cautraloi = i.convert();
                cautraloi.Cauhoiid = cauhoi.Id;
                cautraloi.Status = true;
                f_Cautraloi.Create(cautraloi);
            }
            
            return convert(cauhoi);
        }

        public List<CauhoiGet> CreateDanhsachCauhoi(ListCauhoiCreate listCauhoiCreate)
        {
            List<CauhoiGet> rs = new List<CauhoiGet>();
            foreach (var u in listCauhoiCreate.Danhsachcauhoi)
                rs.Add(CreateCauhoi(u));

            return rs;
        }



            public CauhoiGet UpdateCauhoi(CauhoiUpdate cauhoiUpdate)
        {
            F_Cauhoi f_Cauhoi = new F_Cauhoi();
            Cauhoi cauhoi = f_Cauhoi.GetCauhoisByUuid(cauhoiUpdate.Uuid);
            if (cauhoi == null)
                throw new InvalidDataException("Mã uuid không tồn tại");
           
            cauhoi.Noidung = cauhoiUpdate.Noidung;
            cauhoi.Bac = cauhoiUpdate.Bac;

            try
            {
                F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                var chuyennganh = f_Chuyennganh.GetChuyennganhsByUuid(cauhoiUpdate.ChuyennganhGuid);
                cauhoi.Idchuyennganh = chuyennganh.Id;
                cauhoi.Trinhdodaotao = chuyennganh.Trinhdodaotao;
            }
            catch
            {
            }

            F_Cautraloi f_Cautraloi = new F_Cautraloi();

            var list = f_Cautraloi.GetCautraloiWithCauhoiid(cauhoi.Id);
            foreach (var i in list)
            {
                f_Cautraloi.Delete((Guid)i.Uuid);
            }

            foreach (var i in cauhoiUpdate.Cautralois)
            {
                Cautraloi cautraloi = i.convert();
                cautraloi.Cauhoiid = cauhoi.Id;
                cautraloi.Status = true;
                f_Cautraloi.Create(cautraloi);
            }

            return convert(f_Cauhoi.Update(cauhoi));
        }

        public CauhoiGet DeleteCauhoi(Guid guid)
        {
            F_Cauhoi f_Cauhoi = new F_Cauhoi();
            Cauhoi cauhoi = f_Cauhoi.GetCauhoisByUuid(guid);
            F_Cautraloi f_Cautraloi = new F_Cautraloi();

            var list = f_Cautraloi.GetCautraloiWithCauhoiid(cauhoi.Id);
            foreach (var i in list)
            {
                f_Cautraloi.Delete((Guid)i.Uuid);
            }
            return convert(f_Cauhoi.Delete(guid));
        }

        public List<CauhoiGet> UploadFile(IFormFile file)
        {
            if (file != null)
            {

                String kq = "";
                if (file != null) kq = file.FileName;

                bool isSaveSuccess = false;

                string fileName;
                try
                {
                    var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                    if (extension != null) kq = kq + " , " + extension.ToString();
                    fileName = DateTime.Now.Ticks + extension; //Create a new Name for the file due to security reasons.

                    var pathBuilt = "C:\\inetpub\\huong\\Upload\\Files\\Dethi";

                    if (!Directory.Exists(pathBuilt))
                    {
                        Directory.CreateDirectory(pathBuilt);
                    }

                    var path = Path.Combine("C:\\inetpub\\huong\\Upload\\Files\\Dethi",
                       fileName);

                    kq = kq + "," + path.ToString();

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    kq = kq + "," + "thanh cong";
                    isSaveSuccess = true;
                }
                catch (Exception ex)
                {
                    throw new Exception(kq);
                }
                if (isSaveSuccess == true)
                {
                    List<Cauhoi> cauhois = new F_Cauhoi().GetCauhois("");
                    if (cauhois != null)
                    {
                        return cauhois.ConvertAll(x => convert(x));
                    }
                    else
                        return null;
                }

            }
            return null;

        }

        public List<CauhoiGet> Getall()
        {
            List<Cauhoi> cauhois = new F_Cauhoi().GetCauhois("");
            if (cauhois != null)
            {
                return cauhois.ConvertAll(x => convert(x));
            }
            else
                return null;
        }
    }
}
