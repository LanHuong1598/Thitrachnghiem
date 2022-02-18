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
            List<DethiGet> Cauhoi = DethiService.Getall();
            if (Cauhoi != null)
                return Ok(new
                {
                    header = new Header(Cauhoi.Count, pageing.offset, pageing.limit, "true"),
                    body = Cauhoi.ToPagedList(pageing.offset, pageing.limit)
                });
            return Ok(new
            {
                header = new Header(0, pageing.offset, pageing.limit, "true"),
                body = ""
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
            List<DethiGet> Cauhoi = DethiService.getDethiByChuyennganh(he, chuyennganhuuid, bac, keyword);
            if (Cauhoi != null)
                return Ok(new
                {
                    header = new Header(Cauhoi.Count, pageing.offset, pageing.limit, "true"),
                    body = Cauhoi.ToPagedList(pageing.offset, pageing.limit)
                });
            return Ok(new
            {
                header = new Header(0, pageing.offset, pageing.limit, "true"),
                body = ""
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

        /// <summary>
        /// Xoa de thi by uuid
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpDelete("{uuid}")]
        [Authorize(Roles = "admin,Dethi_DELETE")]
        public async Task<ActionResult> XoaDethi(Guid uuid)
        {
            bool Dethi = DethiService.DeleteDethiByUuid(uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = Dethi
            });
        }



        [HttpGet("Thongkedethi/Theochuyennganh")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Gethongke([FromQuery] string nam, string trinhdodaotao, string chuyennganhuuid)
        {
            var cauhoi = DethiService.thongkedethitheochuyennganh(nam, trinhdodaotao, chuyennganhuuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = cauhoi
            });
        }

    }
}
