using AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkole.Repositories;
using AkcijeSkoleWebApi.DTOs;
using BaseLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

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

    [HttpGet]
    public ActionResult<IEnumerable<TerenskaLokacija>> GetAllTerenskeLokacije()
    {
        var terenskaLokacijaResults = _terenskaLokacijaRepository.GetAll()
            .Map(terenskaLokacija => terenskaLokacija.Select(DtoMapping.ToDto));

        return terenskaLokacijaResults
            ? Ok(terenskaLokacijaResults.Data)
            : Problem(terenskaLokacijaResults.Message, statusCode: 500);
    }

    [HttpGet("CvrstiNamjenskiObjekti")]
    public ActionResult<IEnumerable<CvrstiNamjenskiObjekt>> GetAllCvrstiNamjenski()
    {
        var cvrstiNamjenskiResult = _terenskaLokacijaRepository.GetAllCvrstiNamjenski()
            .Map(cvrstiNamjenski => cvrstiNamjenski.Select(DtoMapping.ToDto));

        return cvrstiNamjenskiResult
            ? Ok(cvrstiNamjenskiResult.Data)
            : Problem(cvrstiNamjenskiResult.Message, statusCode: 500);
    }

    [HttpGet("CvrstiObjektiZaObitavanje")]
    public ActionResult<IEnumerable<CvrstiObjektZaObitavanje>> GetAllCvrstiObitavanje()
    {
        var cvrstiObitavanjeResult = _terenskaLokacijaRepository.GetAllCvrstiObitavanje()
            .Map(cvrstiObitavanje => cvrstiObitavanje.Select(DtoMapping.ToDto));

        return cvrstiObitavanjeResult
            ? Ok(cvrstiObitavanjeResult.Data)
            : Problem(cvrstiObitavanjeResult.Message, statusCode: 500);
    }

    [HttpGet("Logorista")]
    public ActionResult<IEnumerable<Logoriste>> GetAllLogorista()
    {
        var logoristeResult = _terenskaLokacijaRepository.GetAllLogoriste()
            .Map(logoriste => logoriste.Select(DtoMapping.ToDto));

        return logoristeResult
            ? Ok(logoristeResult.Data)
            : Problem(logoristeResult.Message, statusCode: 500);
    }

    [HttpGet("PrivremeniObjekti")]
    public ActionResult<IEnumerable<PrivremeniObjekt>> GetAllPrivremeni()
    {
        var privremeniResult = _terenskaLokacijaRepository.GetAllPrivremeni()
            .Map(privremeni => privremeni.Select(DtoMapping.ToDto));

        return privremeniResult
            ? Ok(privremeniResult.Data)
            : Problem(privremeniResult.Message, statusCode: 500);
    }

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

    [HttpPost("CvrstiNamjenskiObjekti")]
    public ActionResult<CvrstiNamjenskiObjekt> CreateCvrstiNamjenski(CvrstiNamjenskiObjekt cvrstiNamjenski)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var domainTerenskaLokacija = cvrstiNamjenski.ToDomain();

        var validationResult = domainTerenskaLokacija.IsValid();
        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }

        var result = domainTerenskaLokacija.IsValid()
                        .Bind(() => _terenskaLokacijaRepository.Insert(domainTerenskaLokacija));

        return result
            ? CreatedAtAction("GetTerenskaLokacija", new { id = cvrstiNamjenski.IdTerenskaLokacija }, cvrstiNamjenski)
            : Problem(result.Message, statusCode: 500);
    }

    [HttpPost("CvrstiObjektiZaObitavanje")]
    public ActionResult<CvrstiObjektZaObitavanje> CreateCvrstiObitavanje(CvrstiObjektZaObitavanje cvrstiObitavanje)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var domainTerenskaLokacija = cvrstiObitavanje.ToDomain();

        var validationResult = domainTerenskaLokacija.IsValid();
        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }

        var result = domainTerenskaLokacija.IsValid()
                        .Bind(() => _terenskaLokacijaRepository.Insert(domainTerenskaLokacija));

        return result
            ? CreatedAtAction("GetTerenskaLokacija", new { id = cvrstiObitavanje.IdTerenskaLokacija }, cvrstiObitavanje)
            : Problem(result.Message, statusCode: 500);
    }

    [HttpPost("Logorista")]
    public ActionResult<Logoriste> CreateLogoriste(Logoriste logoriste)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var domainTerenskaLokacija = logoriste.ToDomain();

        var validationResult = domainTerenskaLokacija.IsValid();
        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }

        var result = domainTerenskaLokacija.IsValid()
                        .Bind(() => _terenskaLokacijaRepository.Insert(domainTerenskaLokacija));

        return result
            ? CreatedAtAction("GetTerenskaLokacija", new { id = logoriste.IdTerenskaLokacija }, logoriste)
            : Problem(result.Message, statusCode: 500);
    }

    [HttpPost("PrivremeniObjekti")]
    public ActionResult<PrivremeniObjekt> CreatePrivremeni(PrivremeniObjekt privremeni)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var domainTerenskaLokacija = privremeni.ToDomain();

        var validationResult = domainTerenskaLokacija.IsValid();
        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }

        var result = domainTerenskaLokacija.IsValid()
                        .Bind(() => _terenskaLokacijaRepository.Insert(domainTerenskaLokacija));
        return result
        ? CreatedAtAction("GetTerenskaLokacija", new { id = privremeni.IdTerenskaLokacija }, privremeni)
        : Problem(result.Message, statusCode: 500);
    }

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

    [HttpPut("CvrstiNamjenskiObjekti/{id}")]
    public IActionResult EditCvrstiNamjenski(int id, CvrstiNamjenskiObjekt cvrstiNamjenski)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if(id != cvrstiNamjenski.IdTerenskaLokacija)
        {
            return BadRequest();
        }

        if (!_terenskaLokacijaRepository.ExistsCvrstiNamjenski(id))
        {
            return NotFound();
        }

        var domainTerenskaLokacija = cvrstiNamjenski.ToDomain();

        var result = domainTerenskaLokacija.IsValid()
                        .Bind(() => _terenskaLokacijaRepository.Update(domainTerenskaLokacija));

        return result
            ? AcceptedAtAction("EditCvrstiNamjenski", cvrstiNamjenski)
            : Problem(result.Message, statusCode: 500);
    }

    [HttpPut("CvrstiObjektiZaObitavanje/{id}")]
    public IActionResult EditCvrstiObitavanje(int id, CvrstiObjektZaObitavanje cvrstiObitavanje)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if(id != cvrstiObitavanje.IdTerenskaLokacija)
        {
            return BadRequest();
        }

        if (!_terenskaLokacijaRepository.ExistsCvrstiObitavanje(id))
        {
            return NotFound();
        }

        var domainTerenskaLokacija = cvrstiObitavanje.ToDomain();

        var result = domainTerenskaLokacija.IsValid()
                        .Bind(() => _terenskaLokacijaRepository.Update(domainTerenskaLokacija));

        return result
            ? AcceptedAtAction("EditCvrstiObitavanje", cvrstiObitavanje)
            : Problem(result.Message, statusCode: 500);
    }

    [HttpPut("Logorista/{id}")]
    public IActionResult EditLogoriste(int id, Logoriste logoriste)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if(id != logoriste.IdTerenskaLokacija)
        {
            return BadRequest();
        }

        if (!_terenskaLokacijaRepository.ExistsLogoriste(id))
        {
            return NotFound();
        }

        var domainTerenskaLokacija = logoriste.ToDomain();

        var result = domainTerenskaLokacija.IsValid()
                        .Bind(() => _terenskaLokacijaRepository.Update(domainTerenskaLokacija));

        return result
            ? AcceptedAtAction("EditLogoriste", logoriste)
            : Problem(result.Message, statusCode: 500);
    }

    [HttpPut("PrivremeniObjekti/{id}")]
    public IActionResult EditPrivremeni(int id, PrivremeniObjekt privremeni)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if(id != privremeni.IdTerenskaLokacija)
        {
            return BadRequest();
        }

        if (!_terenskaLokacijaRepository.ExistsPrivremeni(id))
        {
            return NotFound();
        }

        var domainTerenskaLokacija = privremeni.ToDomain();

        var result = domainTerenskaLokacija.IsValid()
                        .Bind(() => _terenskaLokacijaRepository.Update(domainTerenskaLokacija));

        return result
            ? AcceptedAtAction("EditPrivremeni", privremeni)
            : Problem(result.Message, statusCode: 500);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTerenskaLokacija(int id)
    {
        if (!_terenskaLokacijaRepository.Exists(id))
        {
            return NotFound();
        }    

        var deleteResult = _terenskaLokacijaRepository.Remove(id);
        return deleteResult
            ? NoContent()
            : Problem(deleteResult.Message, statusCode: 500);
    }
}