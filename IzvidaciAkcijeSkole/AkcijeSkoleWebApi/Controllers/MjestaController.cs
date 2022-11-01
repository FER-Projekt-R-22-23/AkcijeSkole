using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkole.Repositories;
using DbModels = AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkoleWebApi.DTO_s;
using System;
using AkcijeSkole.Commons;

namespace AkcijeSkoleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MjestaController : ControllerBase
    {
        private readonly AkcijeSkoleDbContext _context;
        private readonly IMjestoRepository<int, DbModels.Mjesta> _mjestoRepository;

        public MjestaController(IMjestoRepository<int, DbModels.Mjesta> mjestoRepository)
        {
            _mjestoRepository = mjestoRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Mjesto>> GetAllMjesta()
        {
            return Ok(_mjestoRepository.GetAll().Select(DtoMapping.ToDto));
        }


        [HttpGet("pbrMjesta")]
        public ActionResult<Mjesto> GetMjesto(int pbr)
        {
            var mjestoOption = _mjestoRepository.Get(pbr).Map(DtoMapping.ToDto);

            return mjestoOption
                ? Ok(mjestoOption.Data)
                : NotFound();
        }

        [HttpPut]
        public IActionResult EditMjesto(int pbr, DTO_s.Mjesto mjesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pbr != mjesto.PbrMjesta)
            {
                return BadRequest();
            }

            if (!_mjestoRepository.Exists(pbr))
            {
                return NotFound();
            }

            return _mjestoRepository.Update(mjesto.ToDbModel())
                ? AcceptedAtAction("EditMjesto", mjesto)
                : StatusCode(500);
        }

        [HttpPost]
        public ActionResult<Mjesto> CreateMjesto(DTO_s.Mjesto mjesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return _mjestoRepository.Insert(mjesto.ToDbModel())
                ? CreatedAtAction("GetMjesto", new { pbr = mjesto.PbrMjesta }, mjesto)
                : StatusCode(500);
        }

        [HttpDelete("pbrMjesta")]
        public IActionResult DeleteMjeston(int pbr)
        {
            if (!_mjestoRepository.Exists(pbr))
                return NotFound();

            return _mjestoRepository.Remove(pbr)
                ? NoContent()
                : StatusCode(500);
        }




    }
}
