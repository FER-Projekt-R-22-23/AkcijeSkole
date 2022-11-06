using AkcijeSkole.Repositories;
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
}