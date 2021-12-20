using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem.Users.Models.Functions
{
    public class F_Donvi
    {
        thitracnghiemContext thitracnghiemContext;
        public F_Donvi()
        {
            thitracnghiemContext = new thitracnghiemContext();
        }

        public int getDVidbyuuid(Guid? uuid)
        {
            var res = thitracnghiemContext.Donvis.Where(x => x.Uuid == uuid).OrderByDescending(x => x.Id).ToList();
            if (res.Count == 0)
            {
                return 0;
            }
            else
            {
                return res[0].Id;
            }


        }
        public List<Donvi> GetDonvis(string keyword)
        {
            if (keyword == null || keyword =="")
            return thitracnghiemContext.Donvis.Where(x => x.Status == true).OrderByDescending(x => x.Id).ToList();
            else
                return thitracnghiemContext.Donvis.Where(x => x.Status == true && x.Ten.Contains(keyword)).OrderByDescending(x => x.Id).ToList();
        }

        public Donvi GetDonvisByUuid(Guid uuid)
        {
            return thitracnghiemContext.Donvis.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
        }
        public Donvi GetDonvisById(int id)
        {
            return thitracnghiemContext.Donvis.Where(x => x.Id == id && x.Status == true).FirstOrDefault();
        }
       
        public Donvi Update(Donvi user)
        {
            Donvi user1 = thitracnghiemContext.Donvis.Where(x => x.Uuid == user.Uuid && x.Status == true).FirstOrDefault();
            user1 = user;
            thitracnghiemContext.SaveChanges();
            return user1;
        }
        public Donvi Create(Donvi user)
        {
            thitracnghiemContext.Donvis.Add(user);
            thitracnghiemContext.SaveChanges();
            return user;
        }      

        public Donvi Delete(Guid uuid)
        {
            Donvi user1 = thitracnghiemContext.Donvis.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
            user1.Status = false;
            List<Donvi> donvis = GetDonviWithCon(user1.Id);
            foreach (var i in donvis)
                Delete((Guid)i.Uuid);

            thitracnghiemContext.SaveChanges();
            return user1;
        }

        public List<Donvi> GetDonviWithCon(int? id)
        {
            return thitracnghiemContext.Donvis.Where(x => x.Status == true && x.Macha == id).OrderByDescending(x => x.Id).ToList();
        }

    }
}
