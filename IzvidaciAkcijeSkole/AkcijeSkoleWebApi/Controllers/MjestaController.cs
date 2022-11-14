
using Microsoft.AspNetCore.Mvc;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkoleWebApi.DTOs;
using AkcijeSkole.Repositories.SqlServer;
using BaseLibrary;
using AkcijeSkole.Domain.Models;
using System.Data;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using System;

namespace AkcijeSkoleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MjestaController : ControllerBase
    {
        private readonly AkcijeSkoleDbContext _context;
        private readonly MjestoRepository _mjestoRepository;

        public MjestaController(MjestoRepository mjestoRepository)
        {
            _mjestoRepository = mjestoRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DTOs.Mjesto>> GetAllMjesta()
        {
            var mjestoResult = _mjestoRepository.GetAll()
            .Map(mjesto => mjesto.Select(DtoMapping.ToDto));

            return mjestoResult
                ? Ok(mjestoResult.Data)
                : Problem(mjestoResult.Message, statusCode: 500);
        }


        [HttpGet("pbrMjesta")]
        public ActionResult<DTOs.Mjesto> GetMjesto(int pbr)
        {
            var mjestoResult = _mjestoRepository.Get(pbr).Map(DtoMapping.ToDto);

            return mjestoResult switch
            {
                { IsSuccess: true } => Ok(mjestoResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(mjestoResult.Message, statusCode: 500)
            };
        }

        [HttpGet("/AggregateAkcijaMjesto/{id}")]
        public ActionResult<MjestoAkcijeAggregate> GetAkcijaAggregate(int id)
        {
            var potrebaResult = _mjestoRepository.GetAkcijaAggregate(id).Map(DtoMapping.ToAkcijeAggregateDto);

            return potrebaResult switch
            {
                { IsSuccess: true } => Ok(potrebaResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(potrebaResult.Message, statusCode: 500)
            };
        }

        [HttpGet("/AggregateAktivnostMjesto/{id}")]
        public ActionResult<MjestoAkcijeAggregate> GetAktivnostAggregate(int id)
        {
            var potrebaResult = _mjestoRepository.GetAktivnostAggregate(id).Map(DtoMapping.ToAktivostiAggregateDto);

            return potrebaResult switch
            {
                { IsSuccess: true } => Ok(potrebaResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(potrebaResult.Message, statusCode: 500)
            };
        }

        [HttpGet("/AggregateEdukacijaMjesto/{id}")]
        public ActionResult<MjestoAkcijeAggregate> GetEdukacijaAggregate(int id)
        {
            var potrebaResult = _mjestoRepository.GetEdukacijaAggregate(id).Map(DtoMapping.ToEdukacijaAggregateDto);

            return potrebaResult switch
            {
                { IsSuccess: true } => Ok(potrebaResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(potrebaResult.Message, statusCode: 500)
            };
        }

        [HttpGet("/AggregateSkolaMjesto/{id}")]
        public ActionResult<MjestoAkcijeAggregate> GetSkolaAggregate(int id)
        {
            var potrebaResult = _mjestoRepository.GetSkoleAggregate(id).Map(DtoMapping.ToSkolaAggregateDto);

            return potrebaResult switch
            {
                { IsSuccess: true } => Ok(potrebaResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(potrebaResult.Message, statusCode: 500)
            };
        }

        [HttpGet("/AggregateTerenskeLokacijeMjesto/{id}")]
        public ActionResult<MjestoAkcijeAggregate> GetTerenskeLokacijeAggregate(int id)
        {
            var potrebaResult = _mjestoRepository.GetTerenskeLokacijeAggregate(id).Map(DtoMapping.ToTerLokacijaAggregateDto);

            return potrebaResult switch
            {
                { IsSuccess: true } => Ok(potrebaResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(potrebaResult.Message, statusCode: 500)
            };
        }

        [HttpPost("AssignToAkcija/{id}")]
        public IActionResult AssignMjestoToAkcija(int pbr, DTOs.Akcija akcijaAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mjestoResult = _mjestoRepository.GetAkcijaAggregate(pbr);
            if (mjestoResult.IsFailure)
            {
                return NotFound();
            }
            if (mjestoResult.IsException)
            {
                return Problem(mjestoResult.Message, statusCode: 500);
            }

            var mjesto = mjestoResult.Data;

            var domainAkcijaAssignment = akcijaAssignment.ToDomain();
            var validationResult = domainAkcijaAssignment.IsValid();

            if (!validationResult)
            {
                return Problem(validationResult.Message, statusCode: 500);
            }

            mjesto.AssignAkcija(domainAkcijaAssignment);

            var updateResult =
                mjesto.IsValid()
                .Bind(() => _mjestoRepository.UpdateAggregate(mjesto));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }

        [HttpPost("AssignToAktivnost/{id}")]
        public IActionResult AssignMjestoToAktivnost(int pbr, DTOs.Aktivnost aktivnostAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mjestoResult = _mjestoRepository.GetAktivnostAggregate(pbr);
            if (mjestoResult.IsFailure)
            {
                return NotFound();
            }
            if (mjestoResult.IsException)
            {
                return Problem(mjestoResult.Message, statusCode: 500);
            }

            var mjesto = mjestoResult.Data;

            var domainAktivnostAssignment = aktivnostAssignment.ToDomain();
            var validationResult = domainAktivnostAssignment.IsValid();

            if (!validationResult)
            {
                return Problem(validationResult.Message, statusCode: 500);
            }

            mjesto.AssignAktivnost(domainAktivnostAssignment);

            var updateResult =
                mjesto.IsValid()
                .Bind(() => _mjestoRepository.UpdateAggregate(mjesto));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }

        [HttpPost("AssignToEdukacija/{id}")]
        public IActionResult AssignMjestoToEdukacija(int pbr, DTOs.Edukacija edukacijaAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mjestoResult = _mjestoRepository.GetEdukacijaAggregate(pbr);
            if (mjestoResult.IsFailure)
            {
                return NotFound();
            }
            if (mjestoResult.IsException)
            {
                return Problem(mjestoResult.Message, statusCode: 500);
            }

            var mjesto = mjestoResult.Data;

            var domainEdukacijaAssignment = edukacijaAssignment.toDomain();
            var validationResult = domainEdukacijaAssignment.IsValid();

            if (!validationResult)
            {
                return Problem(validationResult.Message, statusCode: 500);
            }

            mjesto.AssignEdukacija(domainEdukacijaAssignment);

            var updateResult =
                mjesto.IsValid()
                .Bind(() => _mjestoRepository.UpdateAggregate(mjesto));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }

        [HttpPost("AssignToSkola/{id}")]
        public IActionResult AssignMjestoToSkola(int pbr, DTOs.Skola skolaAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mjestoResult = _mjestoRepository.GetSkoleAggregate(pbr);
            if (mjestoResult.IsFailure)
            {
                return NotFound();
            }
            if (mjestoResult.IsException)
            {
                return Problem(mjestoResult.Message, statusCode: 500);
            }

            var mjesto = mjestoResult.Data;

            var domainSkolaAssignment = skolaAssignment.toDomain();
            var validationResult = domainSkolaAssignment.IsValid();

            if (!validationResult)
            {
                return Problem(validationResult.Message, statusCode: 500);
            }

            mjesto.AssignSkola(domainSkolaAssignment);

            var updateResult =
                mjesto.IsValid()
                .Bind(() => _mjestoRepository.UpdateAggregate(mjesto));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }

        [HttpPost("AssignToTerenskaLokacija/{id}")]
        public IActionResult AssignMjestoToTerenskaLokacija(int pbr, DTOs.TerenskaLokacija terenskaLokacijaAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mjestoResult = _mjestoRepository.GetTerenskeLokacijeAggregate(pbr);
            if (mjestoResult.IsFailure)
            {
                return NotFound();
            }
            if (mjestoResult.IsException)
            {
                return Problem(mjestoResult.Message, statusCode: 500);
            }

            var mjesto = mjestoResult.Data;

            var domainLokacijaAssignment = terenskaLokacijaAssignment.ToDomain();
            var validationResult = domainLokacijaAssignment.IsValid();

            if (!validationResult)
            {
                return Problem(validationResult.Message, statusCode: 500);
            }

            mjesto.AssignTerenskaLokacija(domainLokacijaAssignment);

            var updateResult =
                mjesto.IsValid()
                .Bind(() => _mjestoRepository.UpdateAggregate(mjesto));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }


        [HttpPost("DismissFromAkcija/{id}")]
        public IActionResult DismissMjestoFromAkcija(int pbr, DTOs.Akcija akcija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mjestoResult = _mjestoRepository.GetAkcijaAggregate(pbr);
            if (mjestoResult.IsFailure)
            {
                return NotFound();
            }
            if (mjestoResult.IsException)
            {
                return Problem(mjestoResult.Message, statusCode: 500);
            }

            var mjesto = mjestoResult.Data;

            var domainAkcija = akcija.ToDomain();

            if (!mjesto.DismissFromAkcija(domainAkcija))
            {
                return NotFound($"Akcija nije pronadena.");
            }

            var updateResult =
                mjesto.IsValid()
                .Bind(() => _mjestoRepository.UpdateAggregate(mjesto));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }

        [HttpPost("DismissFromAktivnost/{id}")]
        public IActionResult DismissMjestoFromAktivnost(int pbr, DTOs.Aktivnost aktivnost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mjestoResult = _mjestoRepository.GetAktivnostAggregate(pbr);
            if (mjestoResult.IsFailure)
            {
                return NotFound();
            }
            if (mjestoResult.IsException)
            {
                return Problem(mjestoResult.Message, statusCode: 500);
            }

            var mjesto = mjestoResult.Data;

            var domainAktivnost = aktivnost.ToDomain();

            if (!mjesto.DismissFromAktivnost(domainAktivnost))
            {
                return NotFound($"Aktivnost nije pronadena.");
            }

            var updateResult =
                mjesto.IsValid()
                .Bind(() => _mjestoRepository.UpdateAggregate(mjesto));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }

        [HttpPost("DismissFromEdukacija/{id}")]
        public IActionResult DismissMjestoFromEdukacija(int pbr, DTOs.Edukacija edukacija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mjestoResult = _mjestoRepository.GetEdukacijaAggregate(pbr);
            if (mjestoResult.IsFailure)
            {
                return NotFound();
            }
            if (mjestoResult.IsException)
            {
                return Problem(mjestoResult.Message, statusCode: 500);
            }

            var mjesto = mjestoResult.Data;

            var domainEdukacija = edukacija.toDomain();

            if (!mjesto.DismissFromEdukacija(domainEdukacija))
            {
                return NotFound($"Edukacija nije pronadena.");
            }

            var updateResult =
                mjesto.IsValid()
                .Bind(() => _mjestoRepository.UpdateAggregate(mjesto));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }

        [HttpPost("DismissFromSkola/{id}")]
        public IActionResult DismissMjestoFromSkola(int pbr, DTOs.Skola skola)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mjestoResult = _mjestoRepository.GetSkoleAggregate(pbr);
            if (mjestoResult.IsFailure)
            {
                return NotFound();
            }
            if (mjestoResult.IsException)
            {
                return Problem(mjestoResult.Message, statusCode: 500);
            }

            var mjesto = mjestoResult.Data;

            var domainSkola = skola.toDomain();

            if (!mjesto.DismissFromSkola(domainSkola))
            {
                return NotFound($"Skola nije pronadena.");
            }

            var updateResult =
                mjesto.IsValid()
                .Bind(() => _mjestoRepository.UpdateAggregate(mjesto));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }

        [HttpPost("DismissFromTerenskaLokacija/{id}")]
        public IActionResult DismissMjestoFromTerenskaLokacija(int pbr, DTOs.TerenskaLokacija terenskaLokacija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mjestoResult = _mjestoRepository.GetTerenskeLokacijeAggregate(pbr);
            if (mjestoResult.IsFailure)
            {
                return NotFound();
            }
            if (mjestoResult.IsException)
            {
                return Problem(mjestoResult.Message, statusCode: 500);
            }

            var mjesto = mjestoResult.Data;

            var domainLokacija = terenskaLokacija.ToDomain();

            if (!mjesto.DismissFromTerenskaLokacija(domainLokacija))
            {
                return NotFound($"Terenska lokacija nije pronadena.");
            }

            var updateResult =
                mjesto.IsValid()
                .Bind(() => _mjestoRepository.UpdateAggregate(mjesto));

            return updateResult
                ? Accepted()
                : Problem(updateResult.Message, statusCode: 500);
        }


        [HttpPut("{id}")]
        public IActionResult EditMjesto(int pbr, DTOs.Mjesto mjesto)
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

            var domainMjesto = mjesto.ToDomain();

            var result =
                domainMjesto.IsValid()
                .Bind(() => _mjestoRepository.Update(domainMjesto));

            return result
                ? AcceptedAtAction("EditMjesto", mjesto)
                : Problem(result.Message, statusCode: 500);
        }

        [HttpPost]
        public ActionResult<DTOs.Mjesto> CreateMjesto(DTOs.Mjesto mjesto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainMjesto = mjesto.ToDomain();

            var validationResult = domainMjesto.IsValid();
            if (!validationResult)
            {
                return Problem(validationResult.Message, statusCode: 500);
            }

            var result =
                domainMjesto.IsValid()
                .Bind(() => _mjestoRepository.Insert(domainMjesto));

            return result
                ? CreatedAtAction("Get", new { id = mjesto.PbrMjesta }, mjesto)
                : Problem(result.Message, statusCode: 500);
        }

        [HttpDelete("pbrMjesta")]
        public IActionResult DeleteMjesto(int pbr)
        {
            if(!_mjestoRepository.Exists(pbr))
            return NotFound();

            var deleteResult = _mjestoRepository.Remove(pbr);
            return deleteResult
                ? NoContent()
                : Problem(deleteResult.Message, statusCode: 500);
        }




    }
}

