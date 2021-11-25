using Thitrachnghiem.Users.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem.Users.Models.Schema
{
    public class DonviCreate
    {
        public string Ten { get; set; }
        public string Ma { get; set; }
        public string Macha { get; set; }

        public Donvi convert()
        {
            Donvi donvi = new Donvi();
            donvi.Ma = this.Ma;
            donvi.Ten = this.Ten;
            return donvi;
        }
    }
}
