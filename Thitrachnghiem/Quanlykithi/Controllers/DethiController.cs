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

namespace Thitrachnghiem.QuanlyDethi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DethiController : ControllerBase
    {
        IDethiService DethiService;
        public DethiController(IDethiService DethiService_)
        {
            DethiService = DethiService_;
        }

        // GET: api/Dethi 
        /// <summary>
        /// Get all Dethi chi cho admin
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet("Getall")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> GetDethis([FromQuery] Pageing pageing)
        {
            List<DethiGet> Dethi = DethiService.Getall();
            return Ok(new
            {
                header = new Header(Dethi.Count, pageing.offset, pageing.limit, "true"),
                body = Dethi.ToPagedList(pageing.offset, pageing.limit)
            });
        }

        // GET: api/Dethi
        /// <summary>
        /// Get  Dethi  theo yeu cau
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin,Dethi_GET")]
        public async Task<ActionResult> GetDethis([FromQuery] Pageing pageing, string he, 
            string chuyennganhuuid, int bac, string keyword)
        {
            List<DethiGet> Dethi = DethiService.getDethiByChuyennganh(he, chuyennganhuuid, bac, keyword);
            return Ok(new
            {
                header = new Header(Dethi.Count, pageing.offset, pageing.limit, "true"),
                body = Dethi.ToPagedList(pageing.offset, pageing.limit)
            });
        }

        // GET: api/Dethi/5
        /// <summary>
        /// get Dethi by uuid cua Dethi
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpGet("{uuid}")]
        [Authorize(Roles = "admin,Dethi_GET")]
        public async Task<ActionResult> GetDethi(Guid uuid)
        {
            DethiGet Dethi = DethiService.GetDethiByUuid(uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = Dethi
            });
        }
    }
}
