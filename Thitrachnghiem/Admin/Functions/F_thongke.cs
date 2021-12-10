using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thitrachnghiem.Admin.Schemas;

namespace Thitrachnghiem.Admin.Functions
{
    public class F_Thongke
    {
        thitracnghiemContext thitracnghiemContext;
        public F_Thongke()
        {
            thitracnghiemContext = new thitracnghiemContext();
        }

        public ThongkeGet ThongkeAdmin()
        {
            ThongkeGet thongkeGet = new ThongkeGet();
            var nguoidung = thitracnghiemContext.Users.Where(x => x.Status == true);
            if (nguoidung != null)
                thongkeGet.Nguoidung = nguoidung.Count();

            var donvi = thitracnghiemContext.Donvis.Where(x => x.Status == true);
            if (donvi != null)
                thongkeGet.Donvi = donvi.Count();

            var chuyennganh = thitracnghiemContext.Chuyennganhs.Where(x => x.Status == true);
            if (chuyennganh != null)
                thongkeGet.Chuyennganh = chuyennganh.Count();

            var cauhoi = thitracnghiemContext.Cauhois.Where(x => x.Status == true);
            if (nguoidung != null)
                thongkeGet.Cauhoi = cauhoi.Count();

            var ts = thitracnghiemContext.Thisinhs.Where(x => x.Status == true);
            if (ts != null)
                thongkeGet.Thisinh = ts.Count();


            var kithi = thitracnghiemContext.Kithis.Where(x => x.Status == true);
            if (kithi != null)
                thongkeGet.Kithi = kithi.Count();

            string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            var pt =  thitracnghiemContext.Phienthis.Where(x => x.Thoigianketthuc.CompareTo(now) > 0);
            if (pt != null)
                thongkeGet.Phienthidanghoatdong = pt.Count();

            var ptts = thitracnghiemContext.PhienthiThisinhs.Where(x => x.Thoigianketthuc.CompareTo(now) > 0);
            if (ptts != null)
                thongkeGet.Phienthidanghoatdong = ptts.ToLookup(p => p.Id).Select(coll => coll.First()).Count();

            return thongkeGet;

        }
       

    }
}
