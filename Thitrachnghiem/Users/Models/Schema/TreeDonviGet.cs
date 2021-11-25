﻿using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem.Users.Models.Schema
{
    public class TreeDonviGet
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Ten { get; set; }
        public string Ma { get; set; }
        public List<TreeDonviGet> Donvicon { get; set; }

        public TreeDonviGet()
        {

        }
        public TreeDonviGet(Donvi donvi)
        {
            this.Id = donvi.Id;
            this.Ma = donvi.Ma;
            this.Ten = donvi.Ten;
            this.Uuid = donvi.Uuid;
        }
    }
}
