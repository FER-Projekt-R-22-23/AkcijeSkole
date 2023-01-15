using AkcijeSkole.Repositories;
using AkcijeSkoleWebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using BaseLibrary;
using System;
using System.Data;
using AkcijeSkole.Domain.Models;
using DTOs = AkcijeSkoleWebApi.DTOs;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkole.Repositories.SqlServer;

namespace AkcijeSkoleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EdukacijaController : ControllerBase
    {
        private readonly IEdukacijeRepository _edukacijaRepository;

        public EdukacijaController(IEdukacijeRepository context)
        {
            _edukacijaRepository = context;
        }

        // GET: api/EdukacijaPredavaci
        [HttpGet]
        public ActionResult<IEnumerable<DTOs.Edukacija>> GetAllEdukacija()
        {
            var edukacijaResults = _edukacijaRepository.GetAll()
                .Map(edukacija => edukacija.Select(DtoMapping.ToDto));

            return edukacijaResults
                ? Ok(edukacijaResults.Data)
                : Problem(edukacijaResults.Message, statusCode: 500);
        }

        // GET: api/EdukacijaPredavaci/5
        [HttpGet("{id}")]
        public ActionResult<DTOs.Edukacija> GetEdukacija(int id)
        {
            var edukacijaResult = _edukacijaRepository.Get(id).Map(DtoMapping.ToDto);

            return edukacijaResult switch
            {
                { IsSuccess: true } => Ok(edukacijaResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(edukacijaResult.Message, statusCode: 500)
            };
        }

        [HttpGet("EdukacijaAggregate/{id}")]
        public ActionResult<EdukacijaAggregate> EdukacijaAggregate(int id)
        {
            var edukacijaResult = _edukacijaRepository.GetAggregate(id).Map(DtoMapping.ToAggregateDto);

            return edukacijaResult switch
            {
                { IsSuccess: true } => Ok(edukacijaResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(edukacijaResult.Message, statusCode: 500)
            };
        }

        [HttpPost("DodajPredavaca/{edukacijaId}")]
        public IActionResult DodajaPredavaca(int edukacijaId, DTOs.PredavacNaEdukaciji predavacNaEdukaciji)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var edukacijaResult = _edukacijaRepository.GetAggregate(edukacijaId);
            if (edukacijaResult.IsFailure)
            {
                return NotFound();
            }
            if (edukacijaResult.IsException)
            {
                return Problem(edukacijaResult.Message, statusCode: 500);
            }

            var edukacija = edukacijaResult.Data;

            var domainPredavacNaEdukaciji = predavacNaEdukaciji.ToDomain();
            var validationResult = domainPredavacNaEdukaciji.IsValid();

            if (!validationResult)
            {
                return Problem(validationResult.Message, statusCode: 500);
            }

            edukacija.newPredavac(domainPredavacNaEdukaciji);

            var updateResult =
                edukacija.IsValid()
                .Bind(() => _edukacijaRepository.UpdateAggregate(edukacija));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }

        [HttpPost("UkloniPredavaca/{edukacijaId}")]
        public IActionResult UkloniPredavaca(int edukacijaId, int predavacId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var edukacijaResult = _edukacijaRepository.GetAggregate(edukacijaId);
            if (edukacijaResult.IsFailure)
            {
                return NotFound();
            }
            if (edukacijaResult.IsException)
            {
                return Problem(edukacijaResult.Message, statusCode: 500);
            }

            var edukacija = edukacijaResult.Data;


            if (!edukacija.removePredavac(predavacId))
            {
                return NotFound($"Couldn't find predavac {predavacId} on person");
            }

            var updateResult =
                edukacija.IsValid()
                .Bind(() => _edukacijaRepository.UpdateAggregate(edukacija));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }

        // PUT: api/edukacije/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult EditEdukacija(int id, DTOs.Edukacija edukacija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != edukacija.IdEdukacija)
            {
                return BadRequest();
            }

            if (!_edukacijaRepository.Exists(id))
            {
                return NotFound();
            }

            var domainEdukacija = edukacija.toDomain();

            var result =
                domainEdukacija.IsValid()
                .Bind(() => _edukacijaRepository.Update(domainEdukacija));

            return result
                ? AcceptedAtAction("EditEdukacija", edukacija)
                : Problem(result.Message, statusCode: 500);
        }

        // POST: api/Edukacije
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<DTOs.Edukacija> CreateEdukacije(DTOs.Edukacija edukacija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainRole = edukacija.toDomain();

            var result =
                domainRole.IsValid()
                .Bind(() => _edukacijaRepository.Insert(domainRole));

            return result
                ? CreatedAtAction("GetEdukacija", new { id = edukacija.IdEdukacija }, edukacija)
                : Problem(result.Message, statusCode: 500);
        }

        // DELETE: api/Skole/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEdukacija(int id)
        {
            if (!_edukacijaRepository.Exists(id))
                return NotFound();



            var edukacijaResult = _edukacijaRepository.GetAggregate(id);
            if (edukacijaResult.IsFailure)
            {
                return NotFound();
            }
            if (edukacijaResult.IsException)
            {
                return Problem(edukacijaResult.Message, statusCode: 500);
            }
            var edukacija = edukacijaResult.Data;
            foreach(var predavac in edukacija.PredavaciNaEdukaciji)
            {
                edukacija.removePredavac(predavac.idPredavac);
                edukacija.IsValid()
                .Bind(() => _edukacijaRepository.UpdateAggregate(edukacija));
            }

            var deleteResult = _edukacijaRepository.Remove(id);
            return deleteResult
                ? NoContent()
                : Problem(deleteResult.Message, statusCode: 500);
        }

        [HttpPost("PrijaviPolaznika/{edukacijaId}")]
        public IActionResult PrijaviPolaznika(int edukacijaId, DTOs.PrijavljenClanNaEdukaciju prijavljeni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var edukacijaResult = _edukacijaRepository.GetAggregate(edukacijaId);
            if (edukacijaResult.IsFailure)
            {
                return NotFound();
            }
            if (edukacijaResult.IsException)
            {
                return Problem(edukacijaResult.Message, statusCode: 500);
            }

            var edukacija = edukacijaResult.Data;

            var domainPrijavljeni = prijavljeni.ToDomain();
            var validationResult = domainPrijavljeni.IsValid();

            if (!validationResult)
            {
                return Problem(validationResult.Message, statusCode: 500);
            }

            edukacija.newPrijavljeni(domainPrijavljeni);

            var updateResult =
                edukacija.IsValid()
                .Bind(() => _edukacijaRepository.UpdateAggregate(edukacija));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }

        [HttpPost("OdjaviPolaznika/{edukacijaId}")]
        public IActionResult OdjaviPolaznika(int edukacijaId, int polaznikId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var edukacijaResult = _edukacijaRepository.GetAggregate(edukacijaId);
            if (edukacijaResult.IsFailure)
            {
                return NotFound();
            }
            if (edukacijaResult.IsException)
            {
                return Problem(edukacijaResult.Message, statusCode: 500);
            }

            var edukacija = edukacijaResult.Data;


            if (!edukacija.removePrijavljeni(polaznikId))
            {
                return NotFound($"Couldn't find polaznik {polaznikId}");
            }

            var updateResult =
                edukacija.IsValid()
                .Bind(() => _edukacijaRepository.UpdateAggregate(edukacija));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }

        [HttpPost("DolaziNaEdukaciju/{edukacijaId}")]
        public IActionResult DolaziNaEdukaciju(int edukacijaId, DTOs.PolaznikNaEdukaciji polaznik)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var edukacijaResult = _edukacijaRepository.GetAggregate(edukacijaId);
            if (edukacijaResult.IsFailure)
            {
                return NotFound();
            }
            if (edukacijaResult.IsException)
            {
                return Problem(edukacijaResult.Message, statusCode: 500);
            }

            var edukacija = edukacijaResult.Data;

            var domainPolaznik = polaznik.ToDomain();
            var validationResult = domainPolaznik.IsValid();

            if (!validationResult)
            {
                return Problem(validationResult.Message, statusCode: 500);
            }

            edukacija.newPolaznik(domainPolaznik);

            var updateResult =
                edukacija.IsValid()
                .Bind(() => _edukacijaRepository.UpdateAggregate(edukacija));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }

        [HttpPost("NeDolaziNaEdukaciju/{edukacijaId}")]
        public IActionResult NeDolaziNaEdukaciju(int edukacijaId, int polaznikId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var edukacijaResult = _edukacijaRepository.GetAggregate(edukacijaId);
            if (edukacijaResult.IsFailure)
            {
                return NotFound();
            }
            if (edukacijaResult.IsException)
            {
                return Problem(edukacijaResult.Message, statusCode: 500);
            }

            var edukacija = edukacijaResult.Data;


            if (!edukacija.removePolaznik(polaznikId))
            {
                return NotFound($"Couldn't find polaznik {polaznikId}");
            }

            var updateResult =
                edukacija.IsValid()
                .Bind(() => _edukacijaRepository.UpdateAggregate(edukacija));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }
    }
}
