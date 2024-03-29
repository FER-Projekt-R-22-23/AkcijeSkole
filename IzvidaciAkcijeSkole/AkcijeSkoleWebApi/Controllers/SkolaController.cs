﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AkcijeSkole.Repositories;
using AkcijeSkoleWebApi.DTOs;
using DbModels = AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkole.Commons;
using AkcijeSkole.Repositories.SqlServer;
using System.Data;
using BaseLibrary;

namespace AkcijeSkoleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkoleController : ControllerBase
    {
        private readonly ISkoleRepository _skolaRepository;

        public SkoleController(ISkoleRepository context)
        {
            _skolaRepository = context;
        }
         
        // GET: api/skole
        [HttpGet]
        public ActionResult<IEnumerable<Skola>> GetAllSkole()
        {
            var skoleResults = _skolaRepository.GetAll().Map(skole => skole.Select(DtoMapping.ToDto));

            return skoleResults
                ? Ok(skoleResults.Data)
                : Problem(skoleResults.Message, statusCode: 500);
        }

        // GET: api/skole/5
        [HttpGet("{id}")]
        public ActionResult<Skola> GetSkola(int id)
        {
            var skolaResult = _skolaRepository.Get(id).Map(DtoMapping.ToDto);

            return skolaResult switch
            {
                { IsSuccess: true } => Ok(skolaResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(skolaResult.Message, statusCode: 500)
            };
        }

        // GET: api/skole/5
        [HttpGet("/polaznik/{polaznik}")]
        public ActionResult<IEnumerable<SkolaPolaznik>> GetSkole(int polaznik)
        {
            var skolaResult = _skolaRepository.GetSkole(polaznik).Map(skole => skole.Select(DtoMapping.ToDtoPolaznik));

            return skolaResult switch
            {
                { IsSuccess: true } => Ok(skolaResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(skolaResult.Message, statusCode: 500)
            };
        }

        [HttpGet("SkoleAggregate/{id}")]
        public ActionResult<SkolaAggregate> SkoleAggregate(int id)
        {
            var skolaResult = _skolaRepository.GetAggregate(id).Map(DtoMapping.ToAggregateDto);

            return skolaResult switch
            {
                { IsSuccess: true } => Ok(skolaResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(skolaResult.Message, statusCode: 500)
            };
        }



        // PUT: api/Skole/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult EditSkole(int id, Skola skola)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != skola.IdSkole)
            {
                return BadRequest();
            }

            if (!_skolaRepository.Exists(id))
            {
                return NotFound();
            }

            var domainSkola = skola.toDomain();

            var result =
                domainSkola.IsValid()
                .Bind(() => _skolaRepository.Update(domainSkola));

            return result
                ? AcceptedAtAction("EditSkole", skola)
                : Problem(result.Message, statusCode: 500);
        }

        // POST: api/Skole
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Skola> CreateSkola(Skola skola)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainRole = skola.toDomain();

            var result =
                domainRole.IsValid()
                .Bind(() => _skolaRepository.Insert(domainRole));

            return result
                ? CreatedAtAction("GetSkola", new { id = skola.IdSkole }, skola)
                : Problem(result.Message, statusCode: 500);
        }

        // DELETE: api/Skole/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSkola(int id)
        {
            
            if (!_skolaRepository.Exists(id))
                return NotFound();



            var skolaResult = _skolaRepository.GetAggregate(id);
            if (skolaResult.IsFailure)
            {
                return NotFound();
            }
            if (skolaResult.IsException)
            {
                return Problem(skolaResult.Message, statusCode: 500);
            }

            var deleteResult = _skolaRepository.Remove(id);
            return deleteResult
                ? NoContent()
                : Problem(deleteResult.Message, statusCode: 500);
        }

        [HttpPost("DodajEdukaciju/{skolaId}")]
        public IActionResult DodajaEdukaciju(int skolaId, DTOs.Edukacija edukacija)
        {
            edukacija.SkolaId = skolaId;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var skolaResult = _skolaRepository.GetAggregate(skolaId);
            if (skolaResult.IsFailure)
            {
                return NotFound();
            }
            if (skolaResult.IsException)
            {
                return Problem(skolaResult.Message, statusCode: 500);
            }

            var skola = skolaResult.Data;

            var domainEdukacija= edukacija.toDomain();
            var validationResult = domainEdukacija.IsValid();

            if (!validationResult)
            {
                return Problem(validationResult.Message, statusCode: 500);
            }

            skola.newEdukacija(domainEdukacija);

            var updateResult =
                skola.IsValid()
                .Bind(() => _skolaRepository.UpdateAggregate(skola));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }

        [HttpPost("UkloniEdukaciju/{skolaId}")]
        public IActionResult UkloniEdukaciju(int skolaId, int edukacijaId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var skolaResult = _skolaRepository.GetAggregate(skolaId);
            if (skolaResult.IsFailure)
            {
                return NotFound();
            }
            if (skolaResult.IsException)
            {
                return Problem(skolaResult.Message, statusCode: 500);
            }

            var skola = skolaResult.Data;


            if (!skola.removeEdukacija(edukacijaId))
            {
                return NotFound($"Couldn't find edukacija {edukacijaId}");
            }

            var updateResult =
                skola.IsValid()
                .Bind(() => _skolaRepository.UpdateAggregate(skola));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }
    }
}
