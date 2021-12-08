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
using Thitrachnghiem.Services;

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
        private string extractUserstring()
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                string uuid = identity.FindFirst(ClaimTypes.Sid).Value;
                UsersService usersService = new UsersService();
                UserGet user = usersService.GetUsersByUuid(uuid);
                return user.Username;
            }
            return "";
        }

        // GET: api/Dethi/5
        /// <summary>
        /// get Dethi by uuid cua Dethi
        /// </summary>
        /// <returns></returns>
        [HttpGet("Me")]
        [Authorize]
        public async Task<ActionResult> Getthongtinthisinh()
        {
            int user = extractUser();
            ThiService tsinhservice = new ThiService();
            var  ts = tsinhservice.Getthongtinthisinh(user);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = ts
            });
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticatethisinh([FromBody] UserLogin userlogin)
        {
            try
            {
                UsersService usersService = new UsersService();
                var user = usersService.LoginThisinh(userlogin);

                if (user == null)
                    return NotFound(new { message = "User or password invalid" });

                var token = TokenService.CreateToken(user);
                return new
                {
                    user = user,
                    token = token
                };
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
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
        public async Task<ActionResult> Guidapan([FromBody] CautraloiThisinh cautraloi)
        {
            DethiService dethiService = new DethiService();
            float diem = dethiService.Guicautraloi(cautraloi);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = "Ok"
            });
        }

        [HttpGet("Layketqua")]
        [Authorize]
        public async Task<ActionResult> Layketqua()
        {

            float diem = 25;
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = 25
            });
        }

        [HttpGet("Kiemtraphienthidamo")]
        [Authorize]
        public async Task<ActionResult> Kiemtraphienthidamohaychua()
        {
            string user = extractUserstring();

            DethiService dethiService = new DethiService();
            var ok = dethiService.Kiemtraphienthidamohaychua(user);
                return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = ok
            });
        }
    }
}
