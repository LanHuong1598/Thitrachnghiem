using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thitrachnghiem.Commons;
using Thitrachnghiem.Users.Models.Schema;
using Thitrachnghiem.Users.Services;
using PagedList;
using Microsoft.AspNetCore.Authorization;

namespace Thitrachnghiem.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonviController : ControllerBase
    {
        IDonviService donviService;
        public DonviController(IDonviService donviService_)
        {
            donviService = donviService_;
        }

        // GET: api/Donvi
        /// <summary>
        /// Get all donvi
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin,DONVI_GET")]
        public async Task<ActionResult> GetDonvis([FromQuery] Pageing pageing, string keyword)
        {
            List<DonviGet> Donvi = donviService.GetDonvis(keyword);
            return Ok(new
            {
                header = new Header(Donvi.Count, pageing.offset, pageing.limit, "true"),
                body = Donvi.ToPagedList(pageing.offset, pageing.limit)
            });
        }

        // GET: api/Donvi/5
        /// <summary>
        /// get donvi by uuid cua don vi
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpGet("{uuid}")]
        [Authorize(Roles = "admin,DONVI_GET")]
        public async Task<ActionResult> GetDonvi(Guid uuid)
        {
            DonviGet user = donviService.GetDonviByUuid(uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = user
            });
        }

        /// <summary>
        /// them 1 don vi
        /// </summary>
        /// <param name="donviCreate"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,DONVI_ADD")]
        public async Task<ActionResult> AddDonvi([FromBody] DonviCreate donviCreate)
        {
            DonviGet user = donviService.CreateDonvi(donviCreate);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = user
            });
        }

        /// <summary>
        /// sua 1 donvi
        /// </summary>
        /// <param name="donviUpdate"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "admin,DONVI_EDIT")]
        public async Task<ActionResult<DonviGet>> UpdateDonvi([FromBody] DonviUpdate donviUpdate)
        {
            DonviGet user = donviService.UpdateDonvi(donviUpdate);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = user
            });
        }

        /// <summary>
        /// xoa 1 donvi
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpDelete("{uuid}")]
        [Authorize(Roles = "admin,DONVI_DELETE")]
        public async Task<ActionResult<DonviGet>> DeleteDonvi(Guid uuid)
        {
            DonviGet user = donviService.DeleteDonvi(uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = user
            });
        }

        /// <summary>
        /// lay tree don vi dang A-- b,c
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet("Tree")]
        [Authorize(Roles = "admin,DONVI_GET")]
        public async Task<ActionResult> GetTree([FromQuery] Pageing pageing)
        {
            List<TreeDonviGet> Donvi = donviService.GetTreeDonvi();
            return Ok(new
            {
                header = new Header(Donvi.Count, pageing.offset, pageing.limit, "true"),
                body = Donvi.ToPagedList(pageing.offset, pageing.limit)
            });
        }
    }
}
