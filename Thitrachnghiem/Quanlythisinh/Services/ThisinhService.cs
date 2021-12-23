using System;
using System.Collections.Generic;
using System.IO;
using Thitrachnghiem.Quanlythisinh.Models.Entities;
using Thitrachnghiem.Quanlythisinh.Models.Functions;
using Thitrachnghiem.Quanlythisinh.Models.Schemas;
using Microsoft.AspNetCore.Http;
using Thitrachnghiem.Users.Models.Functions;
using Thitrachnghiem.Users.Models.Entities;
using Thitrachnghiem.Quanlycauhoi.Models.Functions;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;
using Thitrachnghiem.Quanlykithi.Models.Functions;
using Thitrachnghiem.Quanlykithi.Models.Entities;
using System.Linq;

namespace Thitrachnghiem.Quanlythisinh.Services
{
    public class ThisinhService  :IThisinhService
    {
        public ThisinhGet convert(Thisinh thisinh)
        {
            if (thisinh == null)
                return null;
            ThisinhGet result = new ThisinhGet();

            result.Id = thisinh.Id;
            result.Bacdanggiu = thisinh.Bacdanggiu;
            result.Uuid = (Guid)thisinh.Uuid;
            result.Bacthi = thisinh.Bacthi;
            result.Chuyennganhhoc = thisinh.Chuyennganhhoc;
            result.Chucvu = thisinh.Chucvu;
            result.Capbac = thisinh.Capbac;
            result.Email = thisinh.Email;
            result.Name = thisinh.Name;
            result.Donvi = thisinh.Donvi;
            result.Bacluong = thisinh.Bacluong;
            result.Trinhdo = thisinh.Trinhdo;
            result.Namsinh = thisinh.Namsinh;
            try
            {
                F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                var chuyennganh = f_Chuyennganh.GetChuyennganhsById((int)thisinh.Chuyennganhthiid);
                result.Chuyennganhuuid = chuyennganh.Uuid.ToString();
                result.Chuyennganhthi = chuyennganh.Ten;
                result.Trinhdodaotao = chuyennganh.Trinhdodaotao;
            }
            catch
            {
            }
            try
            {
                F_Kithi f_Kithi = new F_Kithi();
                var kithi = f_Kithi.GetKithisById((int)thisinh.Kithiid);
                result.Kithiuuid = kithi.Uuid.ToString();
                result.Trinhdodaotao = kithi.Trinhdodaotao;
                result.Bacthi = kithi.Bac;
                try
                {
                    F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                    var chuyennganh = f_Chuyennganh.GetChuyennganhsById((int)kithi.Chuyennganhid);
                    result.Chuyennganhuuid = chuyennganh.Uuid.ToString();
                    result.Chuyennganhthi = chuyennganh.Ten;
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
        public List<ThisinhGet> GetThisinhbyKithiid(string kithiuuid, string keyword)
        {
            F_Kithi f_Kithi = new F_Kithi();
            Kithi kithi = f_Kithi.GetKithisByUuid(new Guid(kithiuuid));
            if (kithi == null)
                throw new Exception("Sai ma ki thi");

            if (keyword == null || keyword == "null") keyword = "";
           
            List<Thisinh> thisinhs = new F_Thisinh().GetThisinhWithKithiid(kithi.Id).Where(x=> x.Name.Contains(keyword);
            if (thisinhs != null)
            {
                return thisinhs.ConvertAll(x => convert(x));
            }
            else
                return null;
        }
 

        public ThisinhGet GetThisinhByUuid(Guid guid)
        {
            return convert(new F_Thisinh().GetThisinhsByUuid(guid));
        }

        public ThisinhGet CreateThisinh(ThisinhCreate thisinhCreate)
        {
            Thisinh thisinh = new Thisinh();
            thisinh.Bacdanggiu = thisinhCreate.Bacdanggiu;
            thisinh.Bacthi = thisinhCreate.Bacthi;
            thisinh.Chuyennganhhoc = thisinhCreate.Chuyennganhhoc;
            thisinh.Email = thisinhCreate.Email;
            thisinh.Name = thisinhCreate.Name;
            thisinh.Chucvu = thisinhCreate.Chucvu;
            thisinh.Capbac = thisinhCreate.Capbac;
            thisinh.Namsinh = thisinhCreate.Namsinh;
            thisinh.Trinhdo = thisinhCreate.Trinhdo;
            thisinh.Bacluong = thisinhCreate.Bacluong;

            thisinh.Status = true;
            if (thisinhCreate.Kithiuuid != null && thisinhCreate.Kithiuuid != "null")
            {
                F_Kithi f_Kithi = new F_Kithi();
                Kithi kithi = f_Kithi.GetKithisByUuid(new Guid(thisinhCreate.Kithiuuid));
                if (kithi == null)
                    throw new Exception("Sai ma ki thi");

                thisinh.Trinhdodaotao = kithi.Trinhdodaotao;
                thisinh.Bacthi = kithi.Bac;
                try
                {
                    F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                    var chuyennganh = f_Chuyennganh.GetChuyennganhsById((int)kithi.Chuyennganhid);
                    thisinh.Chuyennganhthiid = kithi.Chuyennganhid;
                }
                catch
                {
                }
                thisinh.Kithiid = kithi.Id;               

            }

            thisinh.Donvi = thisinhCreate.Donvi;
      

            thisinh = new F_Thisinh().Create(thisinh);

            return convert(thisinh);
        }
        public List<ThisinhGet> CreateListThisinh(ListThisinhCreate listThisinhCreate)
        {
            List<ThisinhGet> rs = new List<ThisinhGet>();
            foreach (var u in listThisinhCreate.Danhsachthisinh)
                rs.Add(CreateThisinh(u));

            return rs;
        }

        public ThisinhGet UpdateThisinh(ThisinhUpdate thisinhUpdate)
        {
            F_Thisinh f_Thisinh = new F_Thisinh();
            Thisinh thisinh = f_Thisinh.GetThisinhsByUuid(thisinhUpdate.Uuid);
            if (thisinh == null)
                throw new InvalidDataException("Mã uuid không tồn tại");

            thisinh.Bacdanggiu = thisinhUpdate.Bacdanggiu;
            thisinh.Bacthi = thisinhUpdate.Bacthi;
            thisinh.Chuyennganhhoc = thisinhUpdate.Chuyennganhhoc;
            thisinh.Email = thisinhUpdate.Email;
            thisinh.Name = thisinhUpdate.Name;
            thisinh.Status = true;
            thisinh.Donvi = thisinhUpdate.Donvi;
            thisinh.Chucvu = thisinhUpdate.Chucvu;
            thisinh.Capbac = thisinhUpdate.Capbac;

            thisinh.Namsinh = thisinhUpdate.Namsinh;
            thisinh.Trinhdo = thisinhUpdate.Trinhdo;
            thisinh.Bacluong = thisinhUpdate.Bacluong;
            if (thisinhUpdate.Kithiuuid != null && thisinhUpdate.Kithiuuid != "null")
            {
                F_Kithi f_Kithi = new F_Kithi();
                Kithi kithi = f_Kithi.GetKithisByUuid(new Guid(thisinhUpdate.Kithiuuid));
                if (kithi == null)
                    throw new Exception("Sai ma ki thi");

                thisinh.Kithiid = kithi.Id;

            }

            try
            {
                F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
                var chuyennganh = f_Chuyennganh.GetChuyennganhsByUuid(new Guid(thisinhUpdate.Chuyennganhuuid));
                thisinh.Chuyennganhthiid = chuyennganh.Id;
            }
            catch
            {
            }

            return convert(f_Thisinh.Update(thisinh));
        }

        public ThisinhGet DeleteThisinh(Guid guid)
        {
            F_Thisinh f_Thisinh = new F_Thisinh();
            Thisinh thisinh = f_Thisinh.GetThisinhsByUuid(guid);

            return convert(f_Thisinh.Delete(guid));
        }

        public List<ThisinhGet> UploadFile(IFormFile file)
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

                    var pathBuilt = "D:\\inetpub\\huong\\Upload\\Files\\Dethi";

                    if (!Directory.Exists(pathBuilt))
                    {
                        Directory.CreateDirectory(pathBuilt);
                    }

                    var path = Path.Combine("D:\\inetpub\\huong\\Upload\\Files\\Dethi",
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
                    List<Thisinh> thisinhs = new F_Thisinh().GetThisinhs();
                    if (thisinhs != null)
                    {
                        return thisinhs.ConvertAll(x => convert(x));
                    }
                    else
                        return null;
                }

            }
            return null;

        }

        public List<ThisinhGet> Getall()
        {
            List<Thisinh> thisinhs = new F_Thisinh().GetThisinhs();
            if (thisinhs != null)
            {
                return thisinhs.ConvertAll(x => convert(x));
            }
            else
                return null;
        }

        public bool DeleteThisinh(List<Guid> Danhsachuuid)
        {
            F_Thisinh f_Thisinh = new F_Thisinh();
            return f_Thisinh.Delete(Danhsachuuid);
        }

    }
}
