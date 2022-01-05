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

namespace Thitrachnghiem.Quanlykithi.Services
{
    public interface IDethiService
    {

        public DethiGet GetDethiByUuid(Guid guid);
        public bool DeleteDethiByUuid(Guid guid);

        public List<Guid> GetDanhsachCauhoiDethiByUuid(Guid guid);
        public List<DethiGet> Getall();
        public float Guicautraloi(int id, CautraloiThisinh cautraloi);
        public DethiGet MakeDethi(int id);
        public bool Kiemtraphienthidamohaychua(string user);
        public List<DethiGet> getDethiByKithiuuid(Guid kithiuuid);
        public List<DethiGet> getDethiByChuyennganh(string he, string chuyennganhuuid, int bac, string keyword);

    }
}
