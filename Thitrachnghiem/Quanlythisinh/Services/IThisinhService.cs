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

namespace Thitrachnghiem.Quanlythisinh.Services
{
    public interface IThisinhService
    {

        public List<ThisinhGet> GetThisinhbyKithiid(string kithiuuid);
        public List<ThisinhGet> CreateListThisinh(ListThisinhCreate listThisinhCreate);

        public ThisinhGet GetThisinhByUuid(Guid guid);
        public ThisinhGet CreateThisinh(ThisinhCreate thisinhCreate);
        public ThisinhGet UpdateThisinh(ThisinhUpdate thisinhUpdate);

        public ThisinhGet DeleteThisinh(Guid guid);
        public List<ThisinhGet> UploadFile(IFormFile file);
        public List<ThisinhGet> Getall();
    }
}
