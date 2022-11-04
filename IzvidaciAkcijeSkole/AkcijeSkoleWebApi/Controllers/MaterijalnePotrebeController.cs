using AkcijeSkole.Commons;
using AkcijeSkole.DataAccess.SqlServer.Data;
using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkoleWebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AkcijeSkoleWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaterijalnePotrebeController : ControllerBase
    {
            private readonly AkcijeSkoleDbContext _context;
            private readonly IMaterijalnaPotrebaRepository _materijalnaPotrebaRepository;

            public MaterijalnePotrebeController(IMaterijalnaPotrebaRepository materijalnaPotrebaRepository)
            {
                _materijalnaPotrebaRepository = materijalnaPotrebaRepository;
            }

            [HttpGet]
            public ActionResult<IEnumerable<DTOs.MaterijalnaPotreba>> GetAllMaterijalnePotrebe()
            {
        var matPotrebeResults = _materijalnaPotrebaRepository.GetAll()
    .Map(people => people.Select(DtoMapping.ToDto));

        return matPotrebeResults
            ? Ok(matPotrebeResults.Data)
            : Problem(matPotrebeResults.Message, statusCode: 500);
    }


            [HttpGet("idMaterijalnaPotreba")]
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
        var matPotrebaResult = _materijalnaPotrebaRepository.GetAggregate(id).Map(DtoMapping.ToAkcijeAggregateDto);

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
        var matPotrebaResult = _materijalnaPotrebaRepository.GetAggregate(id).Map(DtoMapping.ToSkolaAggregateDto);

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
        var matPotrebaResult = _materijalnaPotrebaRepository.GetAggregate(id).Map(DtoMapping.ToTerLokacijeAggregateDto);

        return matPotrebaResult switch
        {
            { IsSuccess: true } => Ok(matPotrebaResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(matPotrebaResult.Message, statusCode: 500)
        };
    }

    [HttpPut]
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

        var domainPerson = potreba.ToDomain();

        var result =
            domainPerson.IsValid()
            .Bind(() => _materijalnaPotrebaRepository.Update(domainPerson));

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

                return _materijalnaPotrebaRepository.Insert(potreba.ToDbModel())
                    ? CreatedAtAction("GetMaterijalnaPotreba", new { idPotreba = potreba.IdMaterijalnaPotreba }, potreba)
                    : StatusCode(500);
            }

            [HttpDelete("idMaterijalnaPotreba")]
            public IActionResult DeleteMaterijalnaPotreba(int idPotreba)
            {
                if (!_materijalnaPotrebaRepository.Exists(idPotreba))
                    return NotFound();

                return _materijalnaPotrebaRepository.Remove(idPotreba)
                    ? NoContent()
                    : StatusCode(500);
            }




        }
    

