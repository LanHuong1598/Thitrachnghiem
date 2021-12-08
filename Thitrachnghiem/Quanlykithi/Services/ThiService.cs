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
using Thitrachnghiem.Quanlythisinh.Models.Schemas;
using Thitrachnghiem.Quanlythisinh.Models.Entities;

namespace Thitrachnghiem.Quanlykithi.Services
{
    public class ThiService
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
        public ThisinhGet Getthongtinthisinh(int id)
        {
            F_Thisinh f_Thisinh = new F_Thisinh();
            F_Users f_Users = new F_Users();
            var user = f_Users.GetUsersById(id);
            var ts = f_Thisinh.GetThisinhsByGmail(user.Username);
            return convert(ts);

        }


    }
}
