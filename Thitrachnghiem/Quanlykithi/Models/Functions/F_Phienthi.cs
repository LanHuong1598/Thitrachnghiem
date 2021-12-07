﻿using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;
using Thitrachnghiem.Quanlykithi.Models.Entities;

namespace Thitrachnghiem.Quanlykithi.Models.Functions
{
    public class F_Phienthi
    {
        thitracnghiemContext thitracnghiemContext;
        public F_Phienthi()
        {
            thitracnghiemContext = new thitracnghiemContext();
        }

        public List<Phienthi> GetPhienthis()
        {
            return thitracnghiemContext.Phienthis.ToList();
        }
        public List<Phienthi> GetPhienthisIsOpen()
        {
            return thitracnghiemContext.Phienthis.Where( x=> x.Thoigianketthuc.CompareTo(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")) > 0).ToList();
        }

        public Phienthi GetPhienthisByUuid(Guid uuid)
        {
            return thitracnghiemContext.Phienthis.Where(x => x.Uuid == uuid).FirstOrDefault();
        }
        public Phienthi GetPhienthiDangMoByKithi(int id)
        {
            return thitracnghiemContext.Phienthis.Where(x => x.Kithiid == id & (x.Thoigianketthuc.CompareTo(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")) > 0)).FirstOrDefault();
        }
        public Phienthi GetPhienthisById(int id)
        {
            return thitracnghiemContext.Phienthis.Where(x => x.Id == id).FirstOrDefault();
        }
       
        public Phienthi Create(Phienthi user)
        {
            thitracnghiemContext.Phienthis.Add(user);
            thitracnghiemContext.SaveChanges();
            return user;
        }
        public Phienthi Update(Phienthi phienthi)
        {
            Phienthi phienthi1 = thitracnghiemContext.Phienthis.Where(x => x.Uuid == phienthi.Uuid).FirstOrDefault();
            phienthi1 = phienthi;
            thitracnghiemContext.SaveChanges();
            return phienthi1;
        }

        public List<Phienthi> GetPhienthiWithKithiid(int? id)
        {
            return thitracnghiemContext.Phienthis.Where(x =>x.Kithiid == id).ToList();
        }

    }
}