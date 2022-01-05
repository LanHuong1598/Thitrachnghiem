using System;
using System.Collections.Generic;
using System.IO;
using Thitrachnghiem.Quanlykithi.Models.Entities;
using Thitrachnghiem.Quanlykithi.Models.Functions;
using Microsoft.AspNetCore.Http;
using Thitrachnghiem.Quanlycauhoi.Models.Functions;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;
using Thitrachnghiem.Quanlykithi.Models.Schemas;

namespace Thitrachnghiem.Quanlykithi.Services
{
    public interface IKithiService
    {
        public List<KithiGet> GetkithibyChuyennganh(string he, string chuyennganhuuid, int? bac, string keyword);
        public KithiDetail GetkithiByUuid(Guid guid);

        public KithiDetail Createkithi(KithiCreate kithiCreate);

        public KithiDetail Updatekithi(KithiUpdate kithiUpdate);

        public KithiGet Deletekithi(Guid guid);
        public List<KithiGet> Getall();
        public PhienthiGet OpenPhienthi(Guid Kithiuuid);
        public PhienthiGet closePhienthi(Guid Kithiuuid);
        public List<PhienthiGet> GetPhienthiGetsisOpen();
        public List<PhienthiGet> GetPhienthis();
        public List<PhienthiThisinhGet> GetPhienthiThisinhs(Guid Phienthiuuid);
        public Bailamthisinh Getcautraloidethi(Guid Dethiuuid);
        public List<KithiThisinhGet> GetKithiThisinhs(Guid Kithiuuid);
    }
}
