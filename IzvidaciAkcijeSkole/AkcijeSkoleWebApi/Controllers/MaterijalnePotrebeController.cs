
using AkcijeSkole.Commons;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.Repositories.SqlServer;
using AkcijeSkoleWebApi.DTOs;
using BaseLibrary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace AkcijeSkoleWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaterijalnePotrebeController : ControllerBase
    {
            private readonly AkcijeSkoleDbContext _context;
            private readonly MaterijalnaPotrebaRepository _materijalnaPotrebaRepository;

            public MaterijalnePotrebeController(MaterijalnaPotrebaRepository materijalnaPotrebaRepository)
            {
                _materijalnaPotrebaRepository = materijalnaPotrebaRepository;
            }

            [HttpGet]
            public ActionResult<IEnumerable<DTOs.MaterijalnaPotreba>> GetAllMaterijalnePotrebe()
            {
        var matPotrebeResults = _materijalnaPotrebaRepository.GetAll()
            .Map(potreba => potreba.Select(DtoMapping.ToDto));

        return matPotrebeResults
            ? Ok(matPotrebeResults.Data)
            : Problem(matPotrebeResults.Message, statusCode: 500);
    }


            [HttpGet("{idMaterijalnaPotreba}")]
            public ActionResult<DTOs.MaterijalnaPotreba> GetMaterijalnaPotreba(int idPotreba)
            {
        var matPotrebaResult = _materijalnaPotrebaRepository.Get(idPotreba).Map(DtoMapping.ToDto);

        return matPotrebaResult switch
        {
            { IsSuccess: true } => Ok(matPotrebaResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(matPotrebaResult.Message, statusCode: 500)
        };
    }

    [HttpGet("/AggregateAkcija/{id}")]
    public ActionResult<MatPotrebeAkcijeAggregate> GetMatPotrebaAkcijaAggregate(int id)
    {
        var matPotrebaResult = _materijalnaPotrebaRepository.GetAkcijaAggregate(id).Map(DtoMapping.ToAkcijeAggregateDto);

        return matPotrebaResult switch
        {
            { IsSuccess: true } => Ok(matPotrebaResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(matPotrebaResult.Message, statusCode: 500)
        };
    }

    [HttpGet("/AggregateSkola/{id}")]
    public ActionResult<MatPotrebeAkcijeAggregate> GetMatPotrebaSkolaAggregate(int id)
    {
        var matPotrebaResult = _materijalnaPotrebaRepository.GetSkolaAggregate(id).Map(DtoMapping.ToDto);

        return matPotrebaResult switch
        {
            { IsSuccess: true } => Ok(matPotrebaResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(matPotrebaResult.Message, statusCode: 500)
        };
    }

    [HttpGet("/AggregateTerenskaLokacijat/{id}")]
    public ActionResult<MatPotrebeAkcijeAggregate> GeMatPotrebaTerLokacijaAggregate(int id)
    {
        var matPotrebaResult = _materijalnaPotrebaRepository.GetTerenskaLokacijaAggregate(id).Map(DtoMapping.ToTerLokacijeAggregateDto);

        return matPotrebaResult switch
        {
            { IsSuccess: true } => Ok(matPotrebaResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(matPotrebaResult.Message, statusCode: 500)
        };
    }


    [HttpPost("AssignToAkcija/{IdAkcija}")]
    public IActionResult AssignPotrebaToAkcija(int id, DTOs.Akcija akcija)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var potrebaResult = _materijalnaPotrebaRepository.GetAkcijaAggregate(id);
        if (potrebaResult.IsFailure)
        {
            return NotFound();
        }
        if (potrebaResult.IsException)
        {
            return Problem(potrebaResult.Message, statusCode: 500);
        }

        var potreba = potrebaResult.Data;

        var domainAkcijaAssignment = akcija.ToDomain();
        var validationResult = domainAkcijaAssignment.IsValid();

        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }

        potreba.AssignAkcija(domainAkcijaAssignment);

        var updateResult =
            potreba.IsValid()
            .Bind(() => _materijalnaPotrebaRepository.UpdateAggregate(potreba));

        return updateResult
            ? Accepted()
            : Problem(updateResult.Message, statusCode: 500);
    }

    [HttpPost("AssignToSkola/{IdSkola}")]
    public IActionResult AssignPotrebaToSkola(int id, DTOs.Skola skolaAssignment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var potrebaResult = _materijalnaPotrebaRepository.GetSkolaAggregate(id);
        if (potrebaResult.IsFailure)
        {
            return NotFound();
        }
        if (potrebaResult.IsException)
        {
            return Problem(potrebaResult.Message, statusCode: 500);
        }

        var potreba = potrebaResult.Data;

        var domainSkolaAssignment = skolaAssignment.toDomain();
        var validationResult = domainSkolaAssignment.IsValid();

        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }

        potreba.AssignSkola(domainSkolaAssignment);

        var updateResult =
            potreba.IsValid()
            .Bind(() => _materijalnaPotrebaRepository.UpdateAggregate(potreba));

        return updateResult
            ? Accepted()
            : Problem(updateResult.Message, statusCode: 500);
    }

    [HttpPost("AssignToTerenskaLokacija/{IdTerenskaLokacija}")]
    public IActionResult AssignPotrebaToLokacija(int id, DTOs.TerenskaLokacija terenskaLokacijaAssignment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var potrebaResult = _materijalnaPotrebaRepository.GetTerenskaLokacijaAggregate(id);
        if (potrebaResult.IsFailure)
        {
            return NotFound();
        }
        if (potrebaResult.IsException)
        {
            return Problem(potrebaResult.Message, statusCode: 500);
        }

        var potreba = potrebaResult.Data;

        var domaiLokacijaAssignment = terenskaLokacijaAssignment.ToDomain();
        var validationResult = domaiLokacijaAssignment.IsValid();

        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }

        potreba.AssignTerenskaLokacija(domaiLokacijaAssignment);

        var updateResult =
            potreba.IsValid()
            .Bind(() => _materijalnaPotrebaRepository.UpdateAggregate(potreba));

        return updateResult
            ? Accepted()
            : Problem(updateResult.Message, statusCode: 500);
    }

    [HttpPost("DismissFromAkcija/{IdAkcija}")]
    public IActionResult DismissPotrebaFromAkcija(int id, Akcija akcija)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var potrebaResult = _materijalnaPotrebaRepository.GetAkcijaAggregate(id);
        if (potrebaResult.IsFailure)
        {
            return NotFound();
        }
        if (potrebaResult.IsException)
        {
            return Problem(potrebaResult.Message, statusCode: 500);
        }

        var potreba = potrebaResult.Data;

        var domainAkcija = akcija.ToDomain();

        if (!potreba.DismissFromAkcija(domainAkcija))
        {
            return NotFound($"Akcija nije pronadena.");
        }

        var updateResult =
            potreba.IsValid()
            .Bind(() => _materijalnaPotrebaRepository.UpdateAggregate(potreba));

        return updateResult
            ? Accepted()
            : Problem(updateResult.Message, statusCode: 500);
    }

    [HttpPost("DismissFromSkola/{IdSkola}")]
    public IActionResult DismissPotrebaFromSkola(int id, Skola skola)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var potrebaResult = _materijalnaPotrebaRepository.GetSkolaAggregate(id);
        if (potrebaResult.IsFailure)
        {
            return NotFound();
        }
        if (potrebaResult.IsException)
        {
            return Problem(potrebaResult.Message, statusCode: 500);
        }

        var potreba = potrebaResult.Data;

        var domainSkola = skola.toDomain();

        if (!potreba.DismissFromSkola(domainSkola))
        {
            return NotFound($"Skola nije pronadena.");
        }

        var updateResult =
            potreba.IsValid()
            .Bind(() => _materijalnaPotrebaRepository.UpdateAggregate(potreba));

        return updateResult
            ? Accepted()
            : Problem(updateResult.Message, statusCode: 500);
    }

    [HttpPost("DismissFromLokacija/{IdTerenskaLokacija}")]
    public IActionResult DismissPotrebaFromLokacija(int id, TerenskaLokacija terenskaLokacija)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var potrebaResult = _materijalnaPotrebaRepository.GetTerenskaLokacijaAggregate(id);
        if (potrebaResult.IsFailure)
        {
            return NotFound();
        }
        if (potrebaResult.IsException)
        {
            return Problem(potrebaResult.Message, statusCode: 500);
        }

        var potreba = potrebaResult.Data;

        var domainLokacija = terenskaLokacija.ToDomain();

        if (!potreba.DismissFromTerenskaLokacija(domainLokacija))
        {
            return NotFound($"Skola nije pronadena.");
        }

        var updateResult =
            potreba.IsValid()
            .Bind(() => _materijalnaPotrebaRepository.UpdateAggregate(potreba));

        return updateResult
            ? Accepted()
            : Problem(updateResult.Message, statusCode: 500);
    }




    [HttpPut("{id}")]
            public IActionResult EditMaterijalnaPotreba(int idPotreba, DTOs.MaterijalnaPotreba potreba)
            {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (idPotreba != potreba.IdMaterijalnaPotreba)
        {
            return BadRequest();
        }

        if (!_materijalnaPotrebaRepository.Exists(idPotreba))
        {
            return NotFound();
        }

        var domainPotreba = potreba.ToDomain();

        var result =
            domainPotreba.IsValid()
            .Bind(() => _materijalnaPotrebaRepository.Update(domainPotreba));

        return result
            ? AcceptedAtAction("EditMaterijalnaPotreba", potreba)
            : Problem(result.Message, statusCode: 500);
    }

            [HttpPost]
            public ActionResult<DTOs.MaterijalnaPotreba> CreateMaterijalnaPotreba(DTOs.MaterijalnaPotreba potreba)
            {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var domainPotreba = potreba.ToDomain();

        var validationResult = domainPotreba.IsValid();
        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }

        var result =
            domainPotreba.IsValid()
            .Bind(() => _materijalnaPotrebaRepository.Insert(domainPotreba));

        return result
            ? CreatedAtAction("Get", new { id = potreba.IdMaterijalnaPotreba }, potreba)
            : Problem(result.Message, statusCode: 500);
    }

            [HttpDelete("idMaterijalnaPotreba")]
            public IActionResult DeleteMaterijalnaPotreba(int idPotreba)
            {
        if (!_materijalnaPotrebaRepository.Exists(idPotreba))
            return NotFound();

        var deleteResult = _materijalnaPotrebaRepository.Remove(idPotreba);
        return deleteResult
            ? NoContent()
            : Problem(deleteResult.Message, statusCode: 500);
    }




        }
    

