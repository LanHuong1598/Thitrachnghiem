using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Schema;
using Thitrachnghiem.Users.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;

namespace Thitrachnghiem.Thongke
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongkeController : ControllerBase
    {

       
        IUsersService usersService;

        public ThongkeController( IUsersService usersService_)
        {
            usersService = usersService_;
        }

        private int extractUser()
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                string uuid = identity.FindFirst(ClaimTypes.Sid).Value;
                UserGet user = usersService.GetUsersByUuid(uuid);
                return user.Id;
            }
            return 0;
        }

        [HttpGet("Nhatkitruycap/")]
        [Authorize]
        public async Task<ActionResult<NhatkyTruycap>> Thongketruycap()
        {
            ThongkeService thongkeService = new ThongkeService();
            var thongke = thongkeService.getTruycap();

            return Ok(new
            {
                header = new Header(1, 1, 1, "true"),
                body = thongke
            });
        }

        [HttpGet("Nhatkitruycap/Chitiet")]
        [Authorize]
        public async Task<ActionResult<Thongkeadmin>> Thongketruycapchitiet([FromQuery] string keyword, [FromQuery]  string type, [FromQuery]  Pageing pageing)
        {
            ThongkeService thongkeService = new ThongkeService();
            var duans = thongkeService.getNhatky(type, keyword);

            if (duans == null || duans.Count == 0)
                return Ok(new
                {
                    header = new Header(0, pageing.offset, pageing.limit, "true"),
                    body = ""
                });
            return Ok(new
            {
                header = new Header(duans.Count, pageing.offset, pageing.limit, "true"),
                body = duans.ToPagedList(pageing.offset, pageing.limit)
            });
        }

        [HttpGet("Bachup")]
        //[Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<ActionResult> DownloadFile()
        {
            BackupService thongkeService = new BackupService();

            var provider = new FileExtensionContentTypeProvider();
            var temp = thongkeService.backup();
            if (temp == null)
            {
                throw new Exception("Không có file yêu cầu");
            }
            if (!provider.TryGetContentType(temp, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(temp);
            return File(bytes, contentType, Path.GetFileName(temp));
        }
    }


}
