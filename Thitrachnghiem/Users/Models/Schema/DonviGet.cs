using Thitrachnghiem.Users.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem.Users.Models.Schema
{
    public class DonviGet
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Ten { get; set; }
        public string Ma { get; set; }
        public Guid Macha { get; set; }
        public string TenCha { get; set; }
        public DonviGet()
        {

        }

        public DonviGet(Donvi donvi)
        {
            this.Id = donvi.Id;
            this.Ma = donvi.Ma;
            this.Ten = donvi.Ten;
            this.Uuid = donvi.Uuid;            
        }
    }
}
