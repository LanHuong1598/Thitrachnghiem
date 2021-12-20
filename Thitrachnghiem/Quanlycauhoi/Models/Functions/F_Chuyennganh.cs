using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thitrachnghiem.Quanlycauhoi.Models.Entities;

namespace Thitrachnghiem.Quanlycauhoi.Models.Functions
{
    public class F_Chuyennganh
    {
        thitracnghiemContext thitracnghiemContext;
        public F_Chuyennganh()
        {
            thitracnghiemContext = new thitracnghiemContext();
        }

        public Chuyennganh GetChuyennganhByUuidWithFalse(Guid? uuid)
        {
            return thitracnghiemContext.Chuyennganhs.Where(x => x.Uuid == uuid).FirstOrDefault();

        }
        public List<Chuyennganh> GetChuyennganhs()
        {
            return thitracnghiemContext.Chuyennganhs.Where(x => x.Status == true).OrderByDescending(x => x.Id).ToList();
        }
        public Chuyennganh GetChuyennganhsByUuid(Guid uuid)
        {
            return thitracnghiemContext.Chuyennganhs.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
        }
        public Chuyennganh GetChuyennganhsById(int id)
        {
            return thitracnghiemContext.Chuyennganhs.Where(x => x.Id == id && x.Status == true).FirstOrDefault();
        }
       
        public Chuyennganh Update(Chuyennganh chuyennganh)
        {
            Chuyennganh chuyennganh1 = thitracnghiemContext.Chuyennganhs.Where(x => x.Uuid == chuyennganh.Uuid && x.Status == true).FirstOrDefault();
            chuyennganh1 = chuyennganh;
            thitracnghiemContext.SaveChanges();
            return chuyennganh1;
        }
        public Chuyennganh Create(Chuyennganh chuyennganh)
        {
            thitracnghiemContext.Chuyennganhs.Add(chuyennganh);
            thitracnghiemContext.SaveChanges();
            return chuyennganh;
        }      

        public Chuyennganh Delete(Guid uuid)
        {
            Chuyennganh chuyennganh1 = thitracnghiemContext.Chuyennganhs.Where(x => x.Uuid == uuid && x.Status == true).FirstOrDefault();
            chuyennganh1.Status = false;           
            thitracnghiemContext.SaveChanges();
            return chuyennganh1;
        }

        public List<Chuyennganh> GetChuyennganhWithTrinhdodaotao(String trinhdo)
        {
            return thitracnghiemContext.Chuyennganhs.Where(x => x.Status == true && x.Trinhdodaotao.Equals(trinhdo)).OrderByDescending(x => x.Id).ToList();
        }

    }
}
