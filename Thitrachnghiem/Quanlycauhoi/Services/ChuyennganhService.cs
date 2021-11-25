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
    public class ChuyennganhService :IChuyennganhService
    {
        public ChuyennganhGet convert(Chuyennganh chuyennganh)
        {
            if (chuyennganh == null)
                return null;
            ChuyennganhGet result = new ChuyennganhGet();

            result.Id = chuyennganh.Id;
            result.Sobac = chuyennganh.Sobac;
            result.Ten = chuyennganh.Ten;
            result.Uuid = chuyennganh.Uuid;
            result.Trinhdodaotao = chuyennganh.Trinhdodaotao;
            return result;
        }
        public List<ChuyennganhGet> GetChuyennganhByTrinhdodaotaos(string trinhdodaotao)
        {
            List<Chuyennganh> chuyennganhs = new F_Chuyennganh().GetChuyennganhWithTrinhdodaotao(trinhdodaotao);
            if (chuyennganhs != null)
            {
                return chuyennganhs.ConvertAll(x => convert(x));
            }
            else
                return null;
        }

        public ChuyennganhGet GetChuyennganhByUuid(Guid guid)
        {
            return convert(new F_Chuyennganh().GetChuyennganhsByUuid(guid));
        }

        public ChuyennganhGet CreateChuyennganh(ChuyennganhCreate chuyennganhCreate)
        {
            Chuyennganh chuyennganh = chuyennganhCreate.convert();           

            chuyennganh.Status = true;
            return convert(new F_Chuyennganh().Create(chuyennganh));
        }

        public ChuyennganhGet UpdateChuyennganh(ChuyennganhUpdate chuyennganhUpdate)
        {
            F_Chuyennganh f_Chuyennganh = new F_Chuyennganh();
            Chuyennganh chuyennganh = f_Chuyennganh.GetChuyennganhsByUuid(chuyennganhUpdate.Uuid);
            if (chuyennganh == null)
                throw new InvalidDataException("Mã uuid không tồn tại");

            chuyennganh.Ten = chuyennganhUpdate.Ten;
            chuyennganh.Sobac = chuyennganhUpdate.Sobac;
            chuyennganh.Trinhdodaotao = chuyennganhUpdate.Trinhdodaotao;            
                    
            return convert(f_Chuyennganh.Update(chuyennganh));
        }

        public ChuyennganhGet DeleteChuyennganh(Guid guid)
        {
            return convert(new F_Chuyennganh().Delete(guid));
        }

    }
}
