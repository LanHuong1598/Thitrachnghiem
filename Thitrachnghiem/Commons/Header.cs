using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitrachnghiem.Commons
{
    public class Header 
    {
        public int totalrecord { get; set; }
        public int offset { get; set; }
        public string status { get; set; }
        public int limit { get; set; }

        public Header()
        {
            totalrecord = 0;
            offset = 1;
            status = "false";
            limit = 10;
        }
        public Header(int totalrecord, int offset, int limit, string status)
        {
            this.totalrecord = totalrecord;
            this.offset = offset;
            this.status = status;
            this.limit = limit;
        }
    }
}