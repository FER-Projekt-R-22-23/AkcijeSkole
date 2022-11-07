using AkcijeSkole.Repositories;
using AkcijeSkoleWebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using BaseLibrary;
using System;
using System.Data;
using AkcijeSkole.Domain.Models;
using DTOs = AkcijeSkoleWebApi.DTOs;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;

namespace AkcijeSkoleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AktivnostiController : ControllerBase
    {
        private readonly IAktivnostiRepository _context;

        public AktivnostiController(IAktivnostiRepository context)
        {
            _context = context;
        }

        // GET: api/Aktivnosti
        [HttpGet]
        public ActionResult<IEnumerable<DTOs.Aktivnost>> GetAktivnosti()
        {
            var aktivnostiResults = _context.GetAll()
                .Map(aktivnost => aktivnost.Select(DtoMapping.ToDto));

            return aktivnostiResults
                ? Ok(aktivnostiResults.Data)
                : Problem(aktivnostiResults.Message, statusCode: 500);
        }

        // GET: api/Aktivnosti/2
        [HttpGet("{id}")]
        public ActionResult<DTOs.Aktivnost> GetAktivnost(int id)
        {
            var aktivnostResult = _context.Get(id).Map(DtoMapping.ToDto);

            return aktivnostResult switch
            {
                { IsSuccess: true } => Ok(aktivnostResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(aktivnostResult.Message, statusCode: 500)
            };
        }
        
        [HttpPut("{id}")]
        public IActionResult EditAktivnost(int id, DTOs.Aktivnost aktivnost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != aktivnost.IdAktivnost)
            {
                return BadRequest();
            }

            if (!_context.Exists(id))
            {
                return NotFound();
            }

            var domainAktivnost = aktivnost.ToDomain();

            var result =
                domainAktivnost.IsValid()
                .Bind(() => _context.Update(domainAktivnost));

            return result
                ? AcceptedAtAction("EditAktivnost", aktivnost)
                : Problem(result.Message, statusCode: 500);
        }

        // POST: api/Aktivnosti
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<DTOs.Aktivnost> CreateAktivnost(DTOs.Aktivnost aktivnost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainAktivnost = aktivnost.ToDomain();

            var result =
                domainAktivnost.IsValid()
                .Bind(() => _context.Insert(domainAktivnost));

            return result
                ? CreatedAtAction("GetAktivnost", new { id = aktivnost.IdAktivnost }, aktivnost)
                : Problem(result.Message, statusCode: 500);
        }

        // DELETE: api/Aktivnost/4
        [HttpDelete("{id}")]
        public IActionResult DeleteAktivnost(int id)
        {
            if (!_context.Exists(id))
                return NotFound();
            
            var deleteResult = _context.Remove(id);
            return deleteResult
                ? NoContent()
                : Problem(deleteResult.Message, statusCode: 500);
        }
    }
}
