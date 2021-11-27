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

     
    }
}
