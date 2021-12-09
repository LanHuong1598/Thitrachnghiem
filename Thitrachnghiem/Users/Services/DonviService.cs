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
    public class DonviService :IDonviService
    {
        public DonviGet convert(Donvi donvi)
        {
            if (donvi == null)
                return null;
            DonviGet result = new DonviGet(donvi);
            result.TenCha = "";
            if (donvi.Macha != null)
            {
                donvi = new F_Donvi().GetDonvisById((int)donvi.Macha);
                if (donvi != null)
                {
                    result.TenCha = donvi.Ten;
                    result.Macha = (Guid)donvi.Uuid;
                }
            }

            while (donvi.Macha != null)
            {
                donvi = new F_Donvi().GetDonvisById((int)donvi.Macha);
                if (donvi != null)
                {
                    
                    result.TenCha = result.TenCha + ", " + donvi.Ten;
                }
                else break;
            }
            return result;
        }
        public List<DonviGet> GetDonvis(string keyword)
        {
            List<Donvi> donvis = new F_Donvi().GetDonvis(keyword);
            if (donvis != null)
            {
                return donvis.ConvertAll(x => convert(x));
            }
            else
                return null;
        }

        public DonviGet GetDonviByUuid(Guid guid)
        {
            return convert(new F_Donvi().GetDonvisByUuid(guid));
        }

        public DonviGet CreateDonvi(DonviCreate donviCreate)
        {
            Donvi donvi = donviCreate.convert();
            if (donviCreate.Macha != "null")
            {
                Guid guid = new Guid(donviCreate.Macha);
                Donvi donvicha = new F_Donvi().GetDonvisByUuid(guid);
                if (donvicha != null)
                    donvi.Macha = donvicha.Id;
                else donvi.Macha = null;
            }
            else donvi.Macha = null;

            donvi.Status = true;
            return convert(new F_Donvi().Create(donvi));
        }

        public DonviGet UpdateDonvi(DonviUpdate donviUpdate)
        {
            F_Donvi f_Donvi = new F_Donvi();
            Donvi donvi = f_Donvi.GetDonvisByUuid(donviUpdate.Uuid);
            if (donvi == null)
                throw new InvalidDataException("Mã uuid đơn vị không tồn tại");

            if (donviUpdate.Macha != "null") {
                Guid guid = new Guid(donviUpdate.Macha);
                Donvi donvicha = new F_Donvi().GetDonvisByUuid(guid);
                if (donvicha != null)
                    donvi.Macha = donvicha.Id;
                else donvi.Macha = null;
            }
            else donvi.Macha = null;

            if (donviUpdate.Ten != null)
                donvi.Ten = donviUpdate.Ten;
            donvi.Ma = donviUpdate.Ma;
                    
            return convert(f_Donvi.Update(donvi));
        }

        public DonviGet DeleteDonvi(Guid guid)
        {
            return convert(new F_Donvi().Delete(guid));
        }

 
        public TreeDonviGet GetTreesDonviGetWithId(int id)
        {
            F_Donvi f_Donvi = new F_Donvi();
            Donvi donvi = f_Donvi.GetDonvisById(id);
            List<Donvi> donvis = f_Donvi.GetDonviWithCon(id);
            TreeDonviGet us = new TreeDonviGet(donvi);
            us.Donvicon = new List<TreeDonviGet>();
            foreach(var i in donvis)
                us.Donvicon.Add(GetTreesDonviGetWithId(i.Id));
            return us;
        }

        public List<TreeDonviGet> GetTreeDonvi()
        {
            F_Donvi f_Donvi = new F_Donvi();
            List<Donvi> donvis = f_Donvi.GetDonviWithCon(null);
            List<TreeDonviGet> result = new List<TreeDonviGet>();
            foreach(var i in donvis)
                result.Add(GetTreesDonviGetWithId(i.Id));

            return result;
        }
    }
}
