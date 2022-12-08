
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
            private readonly IMaterijalnaPotrebaRepository _materijalnaPotrebaRepository;

            public MaterijalnePotrebeController(IMaterijalnaPotrebaRepository context)
            {
                _materijalnaPotrebaRepository = context;
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
            public ActionResult<DTOs.MaterijalnaPotreba> GetMaterijalnaPotreba(int idMaterijalnaPotreba)
            {
        var matPotrebaResult = _materijalnaPotrebaRepository.Get(idMaterijalnaPotreba).Map(DtoMapping.ToDto);

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
    public ActionResult<MatPotrebeSkoleAggregate> GetMatPotrebaSkolaAggregate(int id)
    {
        var matPotrebaResult = _materijalnaPotrebaRepository.GetSkolaAggregate(id).Map(DtoMapping.ToSkoleAggregateDto);

        return matPotrebaResult switch
        {
            { IsSuccess: true } => Ok(matPotrebaResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(matPotrebaResult.Message, statusCode: 500)
        };
    }

    [HttpGet("/AggregateTerenskaLokacijat/{id}")]
    public ActionResult<MatPotrebeTerLokacijeAggregate> GetMatPotrebaTerLokacijaAggregate(int id)
    {
        var matPotrebaResult = _materijalnaPotrebaRepository.GetTerenskaLokacijaAggregate(id).Map(DtoMapping.ToTerLokacijeAggregateDto);

        return matPotrebaResult switch
        {
            { IsSuccess: true } => Ok(matPotrebaResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(matPotrebaResult.Message, statusCode: 500)
        };
    }


    [HttpPost("AssignToAkcija/{IdMaterijalnPotreba}")]
    public IActionResult AssignPotrebaToAkcija(int IdMaterijalnPotreba, DTOs.Akcija akcija)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var potrebaResult = _materijalnaPotrebaRepository.GetAkcijaAggregate(IdMaterijalnPotreba);
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

    [HttpPost("AssignToSkola/{IdMaterijalnaPotreba}")]
    public IActionResult AssignPotrebaToSkola(int IdMaterijalnaPotreba, DTOs.Skola skolaAssignment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var potrebaResult = _materijalnaPotrebaRepository.GetSkolaAggregate(IdMaterijalnaPotreba);
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

    [HttpPost("AssignToTerenskaLokacija/{IdMaterijalnaPotreba}")]
    public IActionResult AssignPotrebaToLokacija(int IdMaterijalnaPotreba, DTOs.TerenskaLokacija terenskaLokacijaAssignment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var potrebaResult = _materijalnaPotrebaRepository.GetTerenskaLokacijaAggregate(IdMaterijalnaPotreba);
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

    [HttpPost("DismissFromAkcija/{IdMaterijalnaPotreba}")]
    public IActionResult DismissPotrebaFromAkcija(int IdMaterijalnaPotreba, Akcija akcija)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var potrebaResult = _materijalnaPotrebaRepository.GetAkcijaAggregate(IdMaterijalnaPotreba);
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

    [HttpPost("DismissFromSkola/{IdMaterijalnaPotreba}")]
    public IActionResult DismissPotrebaFromSkola(int IdMaterijalnaPotreba, Skola skola)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var potrebaResult = _materijalnaPotrebaRepository.GetSkolaAggregate(IdMaterijalnaPotreba);
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

    [HttpPost("DismissFromLokacija/{IdMaterijalnaPotreba}")]
    public IActionResult DismissPotrebaFromLokacija(int IdMaterijalnaPotreba, TerenskaLokacija terenskaLokacija)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var potrebaResult = _materijalnaPotrebaRepository.GetTerenskaLokacijaAggregate(IdMaterijalnaPotreba);
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
            public IActionResult EditMaterijalnaPotreba(int id, DTOs.MaterijalnaPotreba potreba)
            {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != potreba.IdMaterijalnaPotreba)
        {
            return BadRequest();
        }

        if (!_materijalnaPotrebaRepository.Exists(id))
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
            ? CreatedAtAction("CreateMaterijalnaPotreba", new { id = potreba.IdMaterijalnaPotreba }, potreba)
            : Problem(result.Message, statusCode: 500);
    }

            [HttpDelete("idMaterijalnaPotreba")]
            public IActionResult DeleteMaterijalnaPotreba(int idMaterijalnaPotreba)
            {
        if (!_materijalnaPotrebaRepository.Exists(idMaterijalnaPotreba))
            return NotFound();

        var deleteResult = _materijalnaPotrebaRepository.Remove(idMaterijalnaPotreba);
        return deleteResult
            ? NoContent()
            : Problem(deleteResult.Message, statusCode: 500);
    }




        }
    

