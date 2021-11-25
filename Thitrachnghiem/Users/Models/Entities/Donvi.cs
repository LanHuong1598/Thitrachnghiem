using System;
using System.Collections.Generic;

#nullable disable

namespace Thitrachnghiem.Users.Models.Entities
{
    public partial class Donvi
    {
        public int Id { get; set; }
        public Guid? Uuid { get; set; }
        public string Ten { get; set; }
        public string Ma { get; set; }
        public int? Macha { get; set; }
        public bool? Status { get; set; }


    }
}
