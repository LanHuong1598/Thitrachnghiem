﻿using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;
using Thitrachnghiem.Quanlykithi.Models.Entities;
using Thitrachnghiem.Quanlythisinh.Models.Entities;

namespace Thitrachnghiem.Quanlythisinh.Models.Functions
{
    public class F_Thisinh
    {
        thitracnghiemContext thitracnghiemContext;
        public F_Thisinh()
        {
            thitracnghiemContext = new thitracnghiemContext();
        }

        public Thisinh GetThisinhByUuidWithFalse(Guid? uuid)
        {
            return thitracnghiemContext.Thisinhs.Where(x => x.Uuid == uuid).FirstOrDefault();

        }
        public List<Thisinh> GetThisinhs()
        {
            return thitracnghiemContext.Thisinhs.Where(x => x.Status == true).ToList();
        }

        public Thisinh GetThisinhsByUuid(Guid uuid)
        {
            return thitracnghiemContext.Thisinhs.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
        }
        public Thisinh GetThisinhsByGmail(string gmail)
        {
            return thitracnghiemContext.Thisinhs.Where(x => x.Email == gmail && x.Status == true).FirstOrDefault();
        }
        public Thisinh GetThisinhsById(int id)
        {
            return thitracnghiemContext.Thisinhs.Where(x => x.Id == id && x.Status == true).FirstOrDefault();
        }
       
        public Thisinh Update(Thisinh user)
        {
            Thisinh user1 = thitracnghiemContext.Thisinhs.Where(x => x.Uuid == user.Uuid && x.Status == true).FirstOrDefault();
            user1 = user;
            thitracnghiemContext.SaveChanges();
            return user1;
        }
        public Thisinh Create(Thisinh user)
        {
            thitracnghiemContext.Thisinhs.Add(user);
            thitracnghiemContext.SaveChanges();
            return user;
        }      

        public Thisinh Delete(Guid uuid)
        {
            Thisinh user1 = thitracnghiemContext.Thisinhs.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
            user1.Status = false;           
            thitracnghiemContext.SaveChanges();
            return user1;
        }

        public List<Thisinh> GetThisinhWithKithiid(int? id)
        {
            return thitracnghiemContext.Thisinhs.Where(x => x.Status == true && x.Kithiid == id).ToList();
        }

    }
}
