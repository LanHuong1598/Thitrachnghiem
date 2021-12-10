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
using Thitrachnghiem.Quanlykithi.Models.Entities;

namespace Thitrachnghiem.Quanlykithi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KithiController : ControllerBase
    {
        IKithiService KithiService;
        public KithiController(IKithiService KithiService_)
        {
            KithiService = KithiService_;
        }

        // GET: api/Kithi 
        /// <summary>
        /// Get all Kithi chi cho admin
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet("Getall")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> GetKithis([FromQuery] Pageing pageing)
        {
            List<KithiGet> Kithi = KithiService.Getall();
            return Ok(new
            {
                header = new Header(Kithi.Count, pageing.offset, pageing.limit, "true"),
                body = Kithi.ToPagedList(pageing.offset, pageing.limit)
            });
        }

        // GET: api/Kithi
        /// <summary>
        /// Get  Kithi  theo yeu cau
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin,KITHI_GET")]
        public async Task<ActionResult> GetKithis([FromQuery] Pageing pageing, string he, string chuyennganhuuid, int bac, string keyword)
        {
            List<KithiGet> Kithi = KithiService.GetkithibyChuyennganh(he, chuyennganhuuid, bac, keyword);
            return Ok(new
            {
                header = new Header(Kithi.Count, pageing.offset, pageing.limit, "true"),
                body = Kithi.ToPagedList(pageing.offset, pageing.limit)
            });
        }

        // GET: api/Kithi/5
        /// <summary>
        /// get Kithi by uuid cua Kithi
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpGet("{uuid}")]
        [Authorize(Roles = "admin,KITHI_GET")]
        public async Task<ActionResult> GetKithi(Guid uuid)
        {
            KithiDetail Kithi = KithiService.GetkithiByUuid(uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = Kithi
            });
        }

        /// <summary>
        /// them 1 Kithi
        /// </summary>
        /// <param name="KithiCreate"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,KITHI_ADD")]
        public async Task<ActionResult> AddKithi([FromBody] KithiCreate KithiCreate)
        {
            KithiDetail Kithi = KithiService.Createkithi(KithiCreate);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = Kithi
            });
        }

        /// <summary>
        /// sua 1 Kithi
        /// </summary>
        /// <param name="KithiUpdate"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "admin,KITHI_EDIT")]
        public async Task<ActionResult<KithiDetail>> UpdateKithi([FromBody] KithiUpdate KithiUpdate)
        {
            KithiDetail Kithi = KithiService.Updatekithi(KithiUpdate);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = Kithi
            });
        }

        /// <summary>
        /// xoa 1 Kithi
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpDelete("{uuid}")]
        [Authorize(Roles = "admin,KITHI_DELETE")]
        public async Task<ActionResult<KithiGet>> DeleteKithi(Guid uuid)
        {
            KithiGet Kithi = KithiService.Deletekithi(uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = Kithi
            });
        }


        /// <summary>
        /// mo 1 Kithi
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpPost("Phienthi/Mophienthi")]
        [Authorize(Roles = "admin,KITHI_EDIT")]
        public async Task<ActionResult> OpenKithi([FromBody] MoPhienthi uuid)
        {
            PhienthiGet phienthi = KithiService.OpenPhienthi(uuid.Uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = phienthi
            });
        }

        /// <summary>
        /// dong phien thi bang  Kithiuuid
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpPost("Phienthi/Dongphienthi")]
        [Authorize(Roles = "admin,KITHI_EDIT")]
        public async Task<ActionResult> CloseKithi([FromBody] MoPhienthi uuid)
        {
            PhienthiGet phienthi = KithiService.closePhienthi(uuid.Uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = phienthi
            });
        }

        // GET: api/Phienthi 
        /// <summary>
        /// Get all Phienthi dang mo
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet("Phienthi/Dangmo")]
        [Authorize(Roles = "admin,KITHI_GET")]
        public async Task<ActionResult> Getphienthis([FromQuery] Pageing pageing)
        {
            List<PhienthiGet> Kithi = KithiService.GetPhienthiGetsisOpen();
            return Ok(new
            {
                header = new Header(Kithi.Count, pageing.offset, pageing.limit, "true"),
                body = Kithi.ToPagedList(pageing.offset, pageing.limit)
            });
        }

        // GET: api/Phienthi 
        /// <summary>
        /// Get all Phienthi 
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet("Phienthi")]
        [Authorize(Roles = "admin,KITHI_GET")]
        public async Task<ActionResult> GetAllphienthis([FromQuery] Pageing pageing)
        {
            List<PhienthiGet> Kithi = KithiService.GetPhienthis();
            return Ok(new
            {
                header = new Header(Kithi.Count, pageing.offset, pageing.limit, "true"),
                body = Kithi.ToPagedList(pageing.offset, pageing.limit)
            });
        }

        // GET: api/Get thi sinh cua phien thi 
        /// <summary>
        /// Get all thi sinh cua phien thi  
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet("Phienthi/{uuid}/Thisinh")]
        [Authorize(Roles = "admin,KITHI_GET")]
        public async Task<ActionResult> GetAllthisinhphienthis(Guid uuid, [FromQuery] Pageing pageing)
        {
            var Kithi = KithiService.GetPhienthiThisinhs(uuid);
            return Ok(new
            {
                header = new Header(Kithi.Count, pageing.offset, pageing.limit, "true"),
                body = Kithi.ToPagedList(pageing.offset, pageing.limit)
            });
        }


        // GET: api/Get chi tiet bai lam thi sinh cua phien thi 
        /// <summary>
        /// Get chi tiet bai lam thi sinh, uuid la uuid cua de thi
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet("Phienthi/Bailam/{uuid}")]
        [Authorize(Roles = "admin,KITHI_GET")]
        public async Task<ActionResult> getbailam(Guid uuid, [FromQuery] Pageing pageing)
        {
            var Kithi = KithiService.Getcautraloidethi(uuid);
            return Ok(new
            {
                header = new Header(Kithi.Count, pageing.offset, pageing.limit, "true"),
                body = Kithi.ToPagedList(pageing.offset, pageing.limit)
            });
        }

        // GET: api/Get thi sinh cua phien thi 
        /// <summary>
        /// Get all thi sinh cua phien thi  
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet("Kithi/{uuid}/Thisinh")]
        [Authorize(Roles = "admin,KITHI_GET")]
        public async Task<ActionResult> GetAllthisinhKithis(Guid uuid, [FromQuery] Pageing pageing)
        {
            var Kithi = KithiService.GetKithiThisinhs(uuid);
            return Ok(new
            {
                header = new Header(Kithi.Count, pageing.offset, pageing.limit, "true"),
                body = Kithi.ToPagedList(pageing.offset, pageing.limit)
            });
        }

    }
}
