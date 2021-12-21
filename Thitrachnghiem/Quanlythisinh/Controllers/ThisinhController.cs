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
using Thitrachnghiem.Quanlythisinh.Services;
using Thitrachnghiem.Quanlythisinh.Models.Schemas;

namespace Thitrachnghiem.Quanlythisinh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThisinhController : ControllerBase
    {
        IThisinhService thisinhService;
        public ThisinhController(IThisinhService thisinhService_)
        {
            thisinhService = thisinhService_;
        }

        // GET: api/Thisinh 
        /// <summary>
        /// Get all Thisinh chi cho admin
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet("Getall")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> GetThisinhs([FromQuery] Pageing pageing)
        {
            List<ThisinhGet> Thisinh = thisinhService.Getall();
            return Ok(new
            {
                header = new Header(Thisinh.Count, pageing.offset, pageing.limit, "true"),
                body = Thisinh.ToPagedList(pageing.offset, pageing.limit)
            });
        }

        // GET: api/Thisinh
        /// <summary>
        /// Get  Thisinh  theo yeu cau
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin,THISINH_GET")]
        public async Task<ActionResult> GetThisinhs([FromQuery] Pageing pageing, string kithiuuid)
        {
            List<ThisinhGet> Thisinh = thisinhService.GetThisinhbyKithiid(kithiuuid);
            return Ok(new
            {
                header = new Header(Thisinh.Count, pageing.offset, pageing.limit, "true"),
                body = Thisinh.ToPagedList(pageing.offset, pageing.limit)
            });
        }

        // GET: api/Thisinh/5
        /// <summary>
        /// get Thisinh by uuid cua Thisinh
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpGet("{uuid}")]
        [Authorize(Roles = "admin,THISINH_GET")]
        public async Task<ActionResult> GetThisinh(Guid uuid)
        {
            ThisinhGet Thisinh = thisinhService.GetThisinhByUuid(uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = Thisinh
            });
        }

        /// <summary>
        /// them 1 Thisinh
        /// </summary>
        /// <param name="ThisinhCreate"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,THISINH_ADD")]
        public async Task<ActionResult> AddThisinh([FromBody] ThisinhCreate ThisinhCreate)
        {
            ThisinhGet Thisinh = thisinhService.CreateThisinh(ThisinhCreate);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = Thisinh
            });
        }

        /// <summary>
        /// them 1 mang Thisinh
        /// </summary>
        /// <returns></returns>
        [HttpPost("Danhsach")]
        [Authorize(Roles = "admin,THISINH_ADD")]
        public async Task<ActionResult> AddThisinh([FromBody] ListThisinhCreate Danhsachthisinh)
        {
            List<ThisinhGet> Thisinh = thisinhService.CreateListThisinh(Danhsachthisinh);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = Thisinh
            });
        }

        /// <summary>
        /// sua 1 Thisinh
        /// </summary>
        /// <param name="ThisinhUpdate"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "admin,THISINH_EDIT")]
        public async Task<ActionResult<ThisinhGet>> UpdateThisinh([FromBody] ThisinhUpdate ThisinhUpdate)
        {
            ThisinhGet Thisinh = thisinhService.UpdateThisinh(ThisinhUpdate);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = Thisinh
            });
        }

        /// <summary>
        /// xoa 1 Thisinh
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpDelete("{uuid}")]
        [Authorize(Roles = "admin,THISINH_DELETE")]
        public async Task<ActionResult<ThisinhGet>> DeleteThisinh(Guid uuid)
        {
            ThisinhGet Thisinh = thisinhService.DeleteThisinh(uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = Thisinh
            });
        }

        /// <summary>
        /// Xoa danh sach
        /// </summary>
        /// <param name="Danhsachuuid"></param>
        /// <returns></returns>
        [HttpPost("Xoadanhsachthisinh")]
        [Authorize(Roles = "admin,THISINH_DELETE")]
        public async Task<ActionResult> DeleteDsThisinh([FromBody] List<Guid> Danhsachuuid)
        {
            bool Thisinh = thisinhService.DeleteThisinh(Danhsachuuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = Thisinh
            });
        }


    }
}
