using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thitrachnghiem.Commons;
using PagedList;
using Microsoft.AspNetCore.Authorization;
using Thitrachnghiem.Quanlykithi.Services;
using Thitrachnghiem.Quanlykithi.Models.Schemas;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Thitrachnghiem.Users.Models.Schema;
using Thitrachnghiem.Users.Services;

namespace Thitrachnghiem.Quanlykithi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThiController : ControllerBase
    {
        private int extractUser()
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                string uuid = identity.FindFirst(ClaimTypes.Sid).Value;
                UsersService usersService = new UsersService();
                UserGet user = usersService.GetUsersByUuid(uuid);
                return user.Id;
            }
            return 0;
        }
        // GET: api/Dethi/5
        /// <summary>
        /// get Dethi by uuid cua Dethi
        /// </summary>
        /// <returns></returns>
        [HttpGet("Laydethi")]
        [Authorize]
        public async Task<ActionResult> getdethi()
        {
            int user = extractUser();
            DethiService dethiService = new DethiService();
            DethiGet dethi = dethiService.MakeDethi(user);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = dethi
            });
        }

        // GET: api/Dethi/5
        /// <summary>
        /// get danh sach cau hoi by uuid cua Dethi
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpGet("Danhsachcauhoi/{uuid}")]
        [Authorize]
        public async Task<ActionResult> getdanhsachcauhoi(Guid uuid)
        {
            DethiService dethiService = new DethiService();
            List<Guid> dethi = dethiService.GetDanhsachCauhoiDethiByUuid(uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = dethi
            });
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Guidapan([FromBody] Dapandethi cautraloi)
        {
            DethiService dethiService = new DethiService();
            float diem = dethiService.Guicautraloi(cautraloi);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = diem
            });
        }

    }
}
