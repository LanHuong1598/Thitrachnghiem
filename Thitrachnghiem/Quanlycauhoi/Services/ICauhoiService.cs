using Thitrachnghiem.Users.Models.Entities;
using Thitrachnghiem.Users.Models.Functions;
using Thitrachnghiem.Users.Models.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;
using Thitrachnghiem.Quanlycauhoi.Models.Functions;
using Thitrachnghiem.Quanlycauhoi.Models.Schemas;
using Microsoft.AspNetCore.Http;

namespace Thitrachnghiem.Quanlycauhoi.Services
{
    public interface ICauhoiService 
    {
        public CauhoiGet convert(Cauhoi cauhoi);
        public List<CauhoiGet> GetCauhoibyChuyennganh(string he, string chuyennganhuuid, int bac, string keyword);
        public List<CauhoiGet> UploadFile(IFormFile File);
        public List<CauhoiGet> Getall();
        public CauhoiGet GetCauhoiByUuid(Guid guid);
        public CauhoiGet CreateCauhoi(CauhoiCreate cauhoiCreate);
        public List<CauhoiGet> CreateDanhsachCauhoi(ListCauhoiCreate listCauhoiCreate);
        public CauhoiGet UpdateCauhoi(CauhoiUpdate cauhoiUpdate);
        public CauhoiGet DeleteCauhoi(Guid guid);
        public bool DeleteCauhoisbyChuyennganh(string chuyennganhuuid, int bac);

    }
}
