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
using Microsoft.AspNetCore.Http;

namespace Thitrachnghiem.Quanlycauhoi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CauhoiController : ControllerBase
    {
        ICauhoiService cauhoiService;
        public CauhoiController(ICauhoiService cauhoiService_)
        {
            cauhoiService = cauhoiService_;
        }

        // GET: api/Cauhoi 
        /// <summary>
        /// Get all Cauhoi chi cho admin
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet("Getall")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> GetCauhois([FromQuery] Pageing pageing)
        {
            List<CauhoiGet> Cauhoi = cauhoiService.Getall();
            return Ok(new
            {
                header = new Header(Cauhoi.Count, pageing.offset, pageing.limit, "true"),
                body = Cauhoi.ToPagedList(pageing.offset, pageing.limit)
            });
        }

        // GET: api/Cauhoi
        /// <summary>
        /// Get  Cauhoi  theo yeu cau
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin,CAUHOI_GET")]
        public async Task<ActionResult> GetCauhois([FromQuery] Pageing pageing, string He, string chuyennganhuuid, int bacId, string keyword)
        {
            List<CauhoiGet> Cauhoi = cauhoiService.GetCauhoibyChuyennganh(He, chuyennganhuuid, bacId, keyword);
            return Ok(new
            {
                header = new Header(Cauhoi.Count, pageing.offset, pageing.limit, "true"),
                body = Cauhoi.ToPagedList(pageing.offset, pageing.limit)
            });
        }

        // GET: api/Cauhoi/5
        /// <summary>
        /// get Cauhoi by uuid cua Cauhoi
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpGet("{uuid}")]
        [Authorize(Roles = "admin,CAUHOI_GET")]
        public async Task<ActionResult> GetCauhoi(Guid uuid)
        {
            CauhoiGet cauhoi = cauhoiService.GetCauhoiByUuid(uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = cauhoi
            });
        }

        /// <summary>
        /// them 1 Cauhoi
        /// </summary>
        /// <param name="cauhoiCreate"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "admin,CAUHOI_ADD")]
        public async Task<ActionResult> AddCauhoi([FromBody] CauhoiCreate cauhoiCreate)
        {
            CauhoiGet cauhoi = cauhoiService.CreateCauhoi(cauhoiCreate);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = cauhoi
            });
        }

        /// <summary>
        /// sua 1 cauhoi
        /// </summary>
        /// <param name="cauhoiUpdate"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "admin,CAUHOI_EDIT")]
        public async Task<ActionResult<CauhoiGet>> UpdateCauhoi([FromBody] CauhoiUpdate cauhoiUpdate)
        {
            CauhoiGet cauhoi = cauhoiService.UpdateCauhoi(cauhoiUpdate);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = cauhoi
            });
        }

        /// <summary>
        /// xoa 1 cauhoi
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpDelete("{uuid}")]
        [Authorize(Roles = "admin,CAUHOI_DELETE")]
        public async Task<ActionResult<CauhoiGet>> DeleteCauhoi(Guid uuid)
        {
            CauhoiGet cauhoi = cauhoiService.DeleteCauhoi(uuid);
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = cauhoi
            });
        }

        // Post: api/upload file cau hoi
        /// <summary>
        /// upload file Cauhoi
        /// </summary>
        /// <returns></returns>
        [HttpPost("UploadFile/")]
        [Authorize(Roles = "admin,CAUHOI_ADD")]
        public async Task<ActionResult> UploadFile([FromForm] IFormFile File)
        {
            List<CauhoiGet> Cauhoi = cauhoiService.UploadFile(File);
            return Ok(new
            {
                header = new Header(Cauhoi.Count, 1, 100, "true"),
                body = Cauhoi
            });
        }


    }
}
