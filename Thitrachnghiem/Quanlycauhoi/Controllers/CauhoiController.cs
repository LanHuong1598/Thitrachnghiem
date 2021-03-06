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

        // GET: api/Cauhoi
        /// <summary>
        /// Get  Cauhoi  theo yeu cau
        /// </summary>
        /// <param name="pageing"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "admin,CAUHOI_GET")]
        public async Task<ActionResult> GetCauhois([FromQuery] Pageing pageing, string he, string chuyennganhuuid, int bac, string keyword)
        {
            List<CauhoiGet> Cauhoi = cauhoiService.GetCauhoibyChuyennganh(he, chuyennganhuuid, bac, keyword);
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



        /// <summary>
        /// Xoa danh sach cau hoi bang chuyennganh va bac
        /// </summary>
        /// <param name="chuyennganhuuid"></param>
        /// <param name="bac"></param>
        /// <returns></returns>
        [HttpDelete("Xoatheochuyennganhvabac")]
        [Authorize(Roles = "admin,CAUHOI_DELETE")]
        public async Task<ActionResult> DeleteCauhois(string chuyennganhuuid, int bac)
        {
            bool Cauhoi = cauhoiService.DeleteCauhoisbyChuyennganh(chuyennganhuuid, bac);
            return Ok(new
            {
                header = new Header(1,1,1, "true"),
                body = Cauhoi
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

        [HttpGet("Thongkecauhoi/Theochuyennganh")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Gethongke()
        {
            var cauhoi = cauhoiService.ThongkeTheoChuyennganh();
            return Ok(new
            {
                header = new Header(1, 0, 1, "true"),
                body = cauhoi
            });
        }

        [HttpGet("Thongkecauhoi/Theobac")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> GethongkeTheobac()
        {
            var cauhoi = cauhoiService.ThongkeTheoBac();
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
        /// them ds Cauhoi
        /// </summary>
        /// <param name="cauhoiCreate"></param>
        /// <returns></returns>
        [HttpPost("Danhsach")]
        [Authorize(Roles = "admin,CAUHOI_ADD")]
        public async Task<ActionResult> AddDanhsachCauhoi([FromBody] ListCauhoiCreate cauhoiCreate)
        {
            List<CauhoiGet> cauhoi = cauhoiService.CreateDanhsachCauhoi(cauhoiCreate);
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
