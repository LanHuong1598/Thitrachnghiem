using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thitrachnghiem.Commons;
using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Functions;
using Thitrachnghiem.Users.Services;

namespace Thitrachnghiem.Thongke
{
    public class ThongkeService
    {
        public NhatkyTruycap getTruycap()
        {
            thitracnghiemContext thitracnghiemContext = new thitracnghiemContext();
            string now = DateTime.Now.ToString("yyyy/MM/dd");
            int homnay = thitracnghiemContext.Nhatkis.Where(x => (x.Thoigian.CompareTo(now) >= 0 && x.Thoigian.CompareTo(now + " 24:00:00" ) < 0))
            .Count();
            var thu = DateTime.Now.DayOfWeek;
            var ngay = 0;
            if (thu == 0) ngay = 6;
            else ngay = ((int)thu) - 1;

            now = DateTime.Now.AddDays(-ngay).ToString("yyyy/MM/dd");

            int trongtuan = thitracnghiemContext.Nhatkis.Where(x => (x.Thoigian.CompareTo(now) >= 0 && x.Thoigian.CompareTo(now + " 24:00:00") < 0))
                .Count();

            now = DateTime.Now.ToString("yyyy/MM");
            var cuoithang  = DateTime.Now.ToString("yyyy/MM/31");

            int trongthang = thitracnghiemContext.Nhatkis.Where(x => (x.Thoigian.CompareTo(now) >= 0 && x.Thoigian.CompareTo(cuoithang + " 24:00:00") < 0))
                        .Count();

            int all = thitracnghiemContext.Nhatkis.Count();

            NhatkyTruycap nhatkyTruycap = new NhatkyTruycap();
            nhatkyTruycap.Homnay = homnay;
            nhatkyTruycap.Tatca = all;
            nhatkyTruycap.Trongthang = trongthang;
            nhatkyTruycap.Trongtuan = trongtuan;

            return nhatkyTruycap;
        }

        public List<Nhatki> getNhatky(string type, string keyword)
        {
            if (keyword == null)
                keyword = "";
            thitracnghiemContext qlnhamayContext = new thitracnghiemContext();

            if (type == "ALL")
                  return qlnhamayContext.Nhatkis.OrderByDescending(x => x.Id).Where(x => x.Ten.Contains(keyword) ||
                      x.Username.Contains(keyword) || x.Ip.Contains(keyword)).OrderByDescending(x=> x.Id)
                  .ToList();

            if (type == "TODAY")
            {
                string now = DateTime.Now.ToString("yyyy/MM/dd");
                return qlnhamayContext.Nhatkis.
                    Where(x => (x.Thoigian.CompareTo(now) >= 0 && x.Thoigian.CompareTo(now + " 24:00:00") < 0) &&
                    (x.Ten.Contains(keyword) ||
                      x.Username.Contains(keyword) || x.Ip.Contains(keyword))).OrderByDescending(x => x.Id).ToList()     ;
            }

            if (type == "WEEK")
            {
                var thu = DateTime.Now.DayOfWeek;
                var ngay = 0;
                if (thu == 0) ngay = 6;
                else ngay = ((int)thu) - 1;

                var now = DateTime.Now.AddDays(-ngay).ToString("yyyy/MM/dd");

                return qlnhamayContext.Nhatkis.Where(x => (x.Thoigian.CompareTo(now) >= 0 && x.Thoigian.CompareTo(now + " 24:00:00") < 0)
                && (x.Ten.Contains(keyword) ||
                      x.Username.Contains(keyword) || x.Ip.Contains(keyword))).OrderByDescending(x => x.Id).ToList()                    ;
            }
            if (type == "MONTH")
            {
                var now = DateTime.Now.ToString("yyyy/MM");
                var cuoithang = DateTime.Now.ToString("yyyy/MM/31");

                return qlnhamayContext.Nhatkis.Where(x => (x.Thoigian.CompareTo(now) >= 0 && x.Thoigian.CompareTo(cuoithang + " 24:00:00") < 0) && (x.Ten.Contains(keyword) ||
                         x.Username.Contains(keyword) || x.Ip.Contains(keyword))).OrderByDescending(x => x.Id).ToList();
            }
            return null;


        }
    }
}
