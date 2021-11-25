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

namespace Thitrachnghiem.Quanlycauhoi.Services
{
    public interface IChuyennganhService
    {
        public List<ChuyennganhGet> GetChuyennganhByTrinhdodaotaos(string trinhdodaotao);
        public ChuyennganhGet GetChuyennganhByUuid(Guid guid);
        public ChuyennganhGet CreateChuyennganh(ChuyennganhCreate chuyennganhCreate);
        public ChuyennganhGet UpdateChuyennganh(ChuyennganhUpdate chuyennganhUpdate);
        public ChuyennganhGet DeleteChuyennganh(Guid guid);

    }
}
