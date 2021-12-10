using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thitrachnghiem.Commons;
using PagedList;
using Microsoft.AspNetCore.Authorization;
using Thitrachnghiem.Quanlycauhoi.Services;
using Thitrachnghiem.Quanlycauhoi.Models.Schemas;
using Microsoft.AspNetCore.Http;
using Thitrachnghiem.Admin.Functions;

namespace Thitrachnghiem.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        // GET: api/Cauhoi 
        /// <summary>
        /// Get all Cauhoi chi cho admin
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet("Thongke")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> thongke()
        {
            F_Thongke f_Thongke = new F_Thongke();
            var u = f_Thongke.ThongkeAdmin();

            return Ok(new
            {
                header = new Header(1, 1, 1, "true"),
                body = u
            });
        }
    }

}
