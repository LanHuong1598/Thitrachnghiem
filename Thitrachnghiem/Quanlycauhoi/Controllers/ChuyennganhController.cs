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

namespace Thitrachnghiem.Quanlycauhoi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChuyennganhController : ControllerBase
    {
        IChuyennganhService chuyennganhService;
        public ChuyennganhController(IChuyennganhService chuyennganhService_)
        {
            chuyennganhService = chuyennganhService_;
        }

        // GET: api/Chuyennganh
        /// <summary>
        /// Get all Chuyennganh
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin,CHUYENNGANH_GET")]
        public async Task<ActionResult> GetChuyennganhs([FromQuery] Pageing pageing, string trinhdodaotao)
        {
            List<ChuyennganhGet> Chuyennganh = chuyennganhService.GetChuyennganhByTrinhdodaotaos(trinhdodaotao);
            if (Chuyennganh != null)
                return Ok(new
                {
                    header = new Header(Chuyennganh.Count, pageing.offset, pageing.limit, "true"),
                    body = Chuyennganh.ToPagedList(pageing.offset, pageing.limit)
                });
            return Ok(new
            {
                header = new Header(0, pageing.offset, pageing.limit, "true"),
                body = ""
            });
        }

        // GET: api/Chuyennganh/5
        /// <summary>
        /// get Chuyennganh by uuid cua Chuyennganh
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpGet("{uuid}")]
        [Authorize(Roles = "admin,CHUYENNGANH_GET")]
        public async Task<ActionResult> GetChuyennganh(Guid uuid)
        {
            ChuyennganhGet chuyennganh = chuyennganhService.GetChuyennganhByUuid(uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = chuyennganh
            });
        }

        /// <summary>
        /// them 1 Chuyennganh
        /// </summary>
        /// <param name="chuyennganhCreate"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,CHUYENNGANH_ADD")]
        public async Task<ActionResult> AddChuyennganh([FromBody] ChuyennganhCreate chuyennganhCreate)
        {
            ChuyennganhGet chuyennganh = chuyennganhService.CreateChuyennganh(chuyennganhCreate);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = chuyennganh
            });
        }

        /// <summary>
        /// sua 1 chuyennganh
        /// </summary>
        /// <param name="chuyennganhUpdate"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "admin,CHUYENNGANH_EDIT")]
        public async Task<ActionResult<ChuyennganhGet>> UpdateChuyennganh([FromBody] ChuyennganhUpdate chuyennganhUpdate)
        {
            ChuyennganhGet chuyennganh = chuyennganhService.UpdateChuyennganh(chuyennganhUpdate);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = chuyennganh
            });
        }

        /// <summary>
        /// xoa 1 chuyennganh
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpDelete("{uuid}")]
        [Authorize(Roles = "admin,CHUYENNGANH_DELETE")]
        public async Task<ActionResult<ChuyennganhGet>> DeleteChuyennganh(Guid uuid)
        {
            ChuyennganhGet chuyennganh = chuyennganhService.DeleteChuyennganh(uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = chuyennganh
            });
        }

     
    }
}
