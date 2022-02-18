using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;
using Thitrachnghiem.Quanlykithi.Models.Entities;
using Thitrachnghiem.Quanlythisinh.Models.Entities;

namespace Thitrachnghiem.Quanlykithi.Models.Functions
{
    public class F_Dethi
    {
        thitracnghiemContext thitracnghiemContext;
        public F_Dethi()
        {
            thitracnghiemContext = new thitracnghiemContext();
        }

        public Dethi GetDethiByUuidWithFalse(Guid? uuid)
        {
            return thitracnghiemContext.Dethis.Where(x => x.Uuid == uuid).FirstOrDefault();

        }
        public List<Dethi> GetDethis()
        {
            return thitracnghiemContext.Dethis.Where(x => x.Status == true).OrderByDescending(x=> x.Id).ToList();
        }

        public Dethi GetDethisByUuid(Guid uuid)
        {
            return thitracnghiemContext.Dethis.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
        }
        public Dethi GetDethisById(int id)
        {
            return thitracnghiemContext.Dethis.Where(x => x.Id == id && x.Status == true).FirstOrDefault();
        }

        public Dethi Update(Dethi user)
        {
            Dethi dethi = thitracnghiemContext.Dethis.Where(x => x.Uuid == user.Uuid && x.Status == true).FirstOrDefault();
            dethi = user;
            thitracnghiemContext.SaveChanges();
            return dethi;
        }
        public Dethi Create(Dethi user)
        {
            thitracnghiemContext.Dethis.Add(user);
            thitracnghiemContext.SaveChanges();
            return user;
        }

        public Dethi Delete(Guid uuid)
        {
            Dethi dethi = thitracnghiemContext.Dethis.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
            dethi.Status = false;
            thitracnghiemContext.SaveChanges();
            return dethi;
        }

        public List<Dethi> GetDethiWithKithiid(int? id)
        {
            return thitracnghiemContext.Dethis.Where(x => x.Status == true && x.Kithiid == id).ToList();
        }

        public Dethi CreateDethiForThisinh(int id, int thisinhid)
        {
            Kithi kithi = thitracnghiemContext.Kithis.
                Where(x => x.Id == id && x.Status == true).FirstOrDefault();

            var list = thitracnghiemContext.Matrandethis
                .Where(x => x.Status == true && x.Kithiid == kithi.Id).ToList();

            // tao de thi
            Dethi dethi = new Dethi();
            dethi.Kithiid = kithi.Id;

            Thisinh ts = thitracnghiemContext.Thisinhs.Where(x => x.Id == thisinhid).FirstOrDefault();

            var count = GetDethiWithKithiid(kithi.Id);
            if (count == null)
                dethi.Madethi = ts.Sobaodanh + "_001";
            else
                dethi.Madethi = ts.Sobaodanh + "_" + count.Count.ToString().PadLeft(3, '0');
            dethi.Status = true;
            dethi.Thisinhid = thisinhid;
            dethi.Thoigian = DateTime.Now.ToString("yyyy");

            dethi = Create(dethi);
            thitracnghiemContext.SaveChanges();

            // tao chi tiet de thi

            var socauhoi = 0;
            if (list != null && list.Count != 0)
            {
                foreach (var i in list)
                {
                    int soluong = (int)(kithi.Socauhoi * i.Tile / 100);
                    if (socauhoi + soluong > kithi.Socauhoi)
                    {
                        soluong = (int)(kithi.Socauhoi - socauhoi);
                        socauhoi = (int)kithi.Socauhoi;
                    }
                    else
                    {
                        socauhoi += soluong;
                    }

                    if (i == list.Last())
                        if (socauhoi < kithi.Socauhoi)
                        {
                            soluong = (int)(soluong + kithi.Socauhoi - socauhoi);
                            socauhoi = (int)kithi.Socauhoi;
                        }

                    var listcauhoi = thitracnghiemContext.Cauhois.Where(x => x.Status == true && x.Idchuyennganh == i.Chuyennganhid
        && x.Bac == i.Bac).OrderBy(c => Guid.NewGuid()).Take(soluong);

                    foreach (var u in listcauhoi)
                    {
                        Chitietdethi chitietdethi = new Chitietdethi();
                        chitietdethi.Cauhoiid = u.Id;
                        chitietdethi.Dethiid = dethi.Id;
                        thitracnghiemContext.Chitietdethis.Add(chitietdethi);


                    }
                    thitracnghiemContext.SaveChanges();
                }
            }
            else
            {
                var listcauhoi = thitracnghiemContext.Cauhois.Where(x => x.Status == true && x.Idchuyennganh == kithi.Chuyennganhid
      && x.Bac == kithi.Bac).OrderBy(c => Guid.NewGuid()).Take(25);

                foreach (var u in listcauhoi)
                {
                    Chitietdethi chitietdethi = new Chitietdethi();
                    chitietdethi.Cauhoiid = u.Id;
                    chitietdethi.Dethiid = dethi.Id;
                    thitracnghiemContext.Chitietdethis.Add(chitietdethi);
                }
                thitracnghiemContext.SaveChanges();

            }
           
            return dethi;
        }

        public int? Kiemtradamochua(string thisinhid)
        {
            Thisinh thisinh = thitracnghiemContext.Thisinhs.
                Where(x => x.Email == thisinhid && x.Status == true).OrderByDescending( x=> x.Id).FirstOrDefault();
            string now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            Phienthi phienthi = thitracnghiemContext.Phienthis.Where(x => x.Kithiid == thisinh.Kithiid &
             (x.Thoigianketthuc.CompareTo(now) > 0)).FirstOrDefault();

            if (phienthi != null)
                return phienthi.Id;
            else return null;

        }


    }
    }
