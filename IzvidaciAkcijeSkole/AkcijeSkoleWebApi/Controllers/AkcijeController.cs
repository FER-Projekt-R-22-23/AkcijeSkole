using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AkcijeSkole.Repositories;
using AkcijeSkoleWebApi.DTOs;
using DbModels = AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkole.Commons;
using AkcijeSkole.Repositories.SqlServer;
using System.Data;
using BaseLibrary;
using AkcijeSkole.Domain.Models;

namespace AkcijeSkoleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AkcijeController : ControllerBase
    {
        private readonly IAkcijeRepository _context;

        public AkcijeController(IAkcijeRepository context)
        {
            _context = context;
        }

        // GET: api/Akcije
        [HttpGet]
        public ActionResult<IEnumerable<DTOs.Akcija>> GetAkcije()
        {
            var akcijeResults = _context.GetAll()
                .Map(akcije => akcije.Select(DtoMapping.ToDto));

            return akcijeResults
                ? Ok(akcijeResults.Data)
                : Problem(akcijeResults.Message, statusCode: 500);
        }

        // GET: api/Akcije/5
        [HttpGet("{id}")]
        public ActionResult<DTOs.Akcija> GetAkcija(int id)
        {
            var akcijaResult = _context.Get(id).Map(DtoMapping.ToDto);

            return akcijaResult switch
            {
                { IsSuccess: true } => Ok(akcijaResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(akcijaResult.Message, statusCode: 500)
            };
        }

        // GET: api/Akcije/5
        [HttpGet("/polaznici/{polaznik}")]
        public ActionResult<IEnumerable<DTOs.AkcijaPolaznik>> GetAkcijaPolaznik(int polaznik)
        {
            var akcijaResult = _context.GetPolaznik(polaznik).Map(akcija => akcija.Select(DtoMapping.ToDtoPolaznik));

            return akcijaResult switch
            {
                { IsSuccess: true } => Ok(akcijaResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(akcijaResult.Message, statusCode: 500)
            };
        }

        // PUT: api/Akcije/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult UpdateAkcija(int id, DTOs.Akcija akcija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != akcija.IdAkcije)
            {
                return BadRequest();
            }

            if (!_context.Exists(id))
            {
                return NotFound();
            }

            var domainAkcija = akcija.ToDomain();

            var result =
                domainAkcija.IsValid()
                .Bind(() => _context.Update(domainAkcija));
            return result
                ? AcceptedAtAction("UpdateAkcija", akcija)
                : Problem(result.Message, statusCode: 500);
        }

        // POST: api/Akcije
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<DTOs.Akcija> CreateSkola(DTOs.Akcija akcija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainAkcija = akcija.ToDomain();

            var result =
                domainAkcija.IsValid()
                .Bind(() => _context.Insert(domainAkcija));

            return result
                ? CreatedAtAction("GetAkcija", new { id = akcija.IdAkcije }, akcija)
                : Problem(result.Message, statusCode: 500);
        }

        // DELETE: api/Akcije/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAkcija(int id)
        {
            if (!_context.Exists(id))
                return NotFound();

            var deleteResult = _context.Remove(id);
            return deleteResult
                ? NoContent()
                : Problem(deleteResult.Message, statusCode: 500);
        }

        [HttpGet("/AggregateAktivnost/{id}")]
        public ActionResult<MatPotrebeAkcijeAggregate> GetAkcijaAktivnostAggregate(int id)
        {
            var akcijaResult = _context.GetAggregate(id).Map(DtoMapping.ToAktivnostiAggregateDto);

            return akcijaResult switch
            {
                { IsSuccess: true } => Ok(akcijaResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(akcijaResult.Message, statusCode: 500)
            };
        }


    }
}
