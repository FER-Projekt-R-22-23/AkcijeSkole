using AkcijeSkole.Repositories;
using AkcijeSkole.Repositories.SqlServer;
using AkcijeSkoleWebApi.DTOs;
using BaseLibrary;
using Microsoft.AspNetCore.Mvc;

namespace AkcijeSkoleWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TerenskaLokacijaController : ControllerBase
{
    private readonly ITerenskaLokacijaRepository _terenskaLokacijaRepository;

    public TerenskaLokacijaController(ITerenskaLokacijaRepository terenskaLokacijaRepository)
    {
        _terenskaLokacijaRepository = terenskaLokacijaRepository;
    }

    // GET: api/TerenskaLokacija
    [HttpGet]
    public ActionResult<IEnumerable<TerenskaLokacija>> GetAllTerenskeLokacije()
    {
        var terenskaLokacijaResults = _terenskaLokacijaRepository.GetAll()
            .Map(terenskaLokacija => terenskaLokacija.Select(DtoMapping.ToDto));

        return terenskaLokacijaResults
            ? Ok(terenskaLokacijaResults.Data)
            : Problem(terenskaLokacijaResults.Message, statusCode: 500);
    }

    // GET: api/TerenskaLokacija/5
    [HttpGet("{id}")]
    public ActionResult<TerenskaLokacija> GetTerenskaLokacija(int id)
    {
        var terenskaLokacijaResult = _terenskaLokacijaRepository.Get(id).Map(DtoMapping.ToDto);

        return terenskaLokacijaResult switch
        {
            { IsSuccess: true } => Ok(terenskaLokacijaResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(terenskaLokacijaResult.Message, statusCode: 500)
        };
    }

    // PUT: api/TerenskaLokacija/5
    [HttpPut("{id}")]
    public IActionResult EditTerenskaLokacija(int id, TerenskaLokacija terenskaLokacija)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if(id != terenskaLokacija.IdTerenskaLokacija)
        {
            return BadRequest();
        }

        if (!_terenskaLokacijaRepository.Exists(id))
        {
            return NotFound();
        }

        var domainTerenskaLokacija = terenskaLokacija.ToDomain();

        var result = domainTerenskaLokacija.IsValid()
                        .Bind(() => _terenskaLokacijaRepository.Update(domainTerenskaLokacija));

        return result
            ? AcceptedAtAction("EditTerenskaLokacija", terenskaLokacija)
            : Problem(result.Message, statusCode: 500);
    }

    // POST: api/TerenskaLokacija
    [HttpPost]
    public ActionResult<TerenskaLokacija> CreateTerenskaLokacija(TerenskaLokacija terenskaLokacija)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var domainTerenskaLokacija = terenskaLokacija.ToDomain();

        var validationResult = domainTerenskaLokacija.IsValid();
        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }

        var result = domainTerenskaLokacija.IsValid()
                        .Bind(() => _terenskaLokacijaRepository.Insert(domainTerenskaLokacija));

        return result
            ? CreatedAtAction("GetTerenskaLokacija", new { id = terenskaLokacija.IdTerenskaLokacija }, terenskaLokacija)
            : Problem(result.Message, statusCode: 500);
    }

    // DELETE: api/TerenskaLokacija/5
    [HttpDelete("{id}")]
    public IActionResult DeleteTerenskaLokacija(int id)
    {
        if (!_terenskaLokacijaRepository.Exists(id))
            return NotFound();

        var deleteResult = _terenskaLokacijaRepository.Remove(id);
        return deleteResult
            ? NoContent()
            : Problem(deleteResult.Message, statusCode: 500);
    }
    /*
    [HttpGet("/CvrstiNamjenski/{id}")]
    public ActionResult<TerenskaLokacijaCvrstiNamjenski> GetCvrstiNamjenski(int id)
    {
        var cvrstiNamjenskiResult = _terenskaLokacijaRepository.GetAggregate(id).Map(DtoMapping.ToCvrstiNamjenskiDto);

        return cvrstiNamjenskiResult switch
        {
            { IsSuccess: true } => Ok(cvrstiNamjenskiResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(cvrstiNamjenskiResult.Message, statusCode: 500)
        };
    }

    [HttpGet("/CvrstiObitavanje/{id}")]
    public ActionResult<TerenskaLokacijaCvrstiObitavanje> GetCvrstiObitavanje(int id)
    {
        var cvrstiObitavanjeResult = _terenskaLokacijaRepository.GetAggregate(id).Map(DtoMapping.ToCvrstiObitavanjeDto);

        return cvrstiObitavanjeResult switch
        {
            { IsSuccess: true } => Ok(cvrstiObitavanjeResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(cvrstiObitavanjeResult.Message, statusCode: 500)
        };
    }

    [HttpGet("/Logoriste/{id}")]
    public ActionResult<TerenskaLokacijaLogoriste> GetLogoriste(int id)
    {
        var logoristeResult = _terenskaLokacijaRepository.GetAggregate(id).Map(DtoMapping.ToLogoristeDto);

        return logoristeResult switch
        {
            { IsSuccess: true } => Ok(logoristeResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(logoristeResult.Message, statusCode: 500)
        };
    }

    [HttpGet("/Privremeni/{id}")]
    public ActionResult<TerenskaLokacijaPrivremeniObjekt> GetPrivremenei(int id)
    {
        var privremeniResult = _terenskaLokacijaRepository.GetAggregate(id).Map(DtoMapping.ToPrivremeniDto);

        return privremeniResult switch
        {
            { IsSuccess: true } => Ok(privremeniResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(privremeniResult.Message, statusCode: 500)
        };
    }

    [HttpGet("/Objekti/{id}")]
    public ActionResult<TerenskaLokacijaObjekti> GetAggregate(int id)
    {
        var objektiResult = _terenskaLokacijaRepository.GetAggregate(id).Map(DtoMapping.ToObjektiDto);

        return objektiResult switch
        {
            { IsSuccess: true } => Ok(objektiResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(objektiResult.Message, statusCode: 500)
        };
    }

    [HttpGet("/Objekti")]
    public ActionResult<IEnumerable<TerenskaLokacijaObjekti>> GetAllAggregate()
    {
        var objektiResult = _terenskaLokacijaRepository.GetAllAggregates().Map(terenskaLokacija => terenskaLokacija.Select(DtoMapping.ToObjektiDto));

        return objektiResult
            ? Ok(objektiResult.Data)
            : Problem(objektiResult.Message, statusCode: 500);
    }*/
}