using Thitrachnghiem.Users.Models.Entities;
using Thitrachnghiem.Users.Models.Functions;
using Thitrachnghiem.Users.Models.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem.Users.Services
{
    public interface IDonviService
    {
        public DonviGet convert(Donvi donvi);
        public List<DonviGet> GetDonvis(string keyword);

        public DonviGet GetDonviByUuid(Guid guid);
        public DonviGet CreateDonvi(DonviCreate donviCreate);
        public DonviGet UpdateDonvi(DonviUpdate donviUpdate);
        public DonviGet DeleteDonvi(Guid guid);
        public TreeDonviGet GetTreesDonviGetWithId(int id);
        public List<TreeDonviGet> GetTreeDonvi();
    }
}
