using Microsoft.AspNetCore.Mvc;
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
    public class EdukacijaController : ControllerBase
    {
        private readonly IEdukacijeRepository _edukacijaRepository;

        public EdukacijaController(IEdukacijeRepository context)
        {
            _edukacijaRepository = context;
        }

        // GET: api/edukacije
        [HttpGet]
        public ActionResult<IEnumerable<Edukacija>> GetAllEdukacije()
        {
            var edukacijeResults = _edukacijaRepository.GetAll().Map(edukacija => edukacija.Select(DtoMapping.ToDto));

            return edukacijeResults
                ? Ok(edukacijeResults.Data)
                : Problem(edukacijeResults.Message, statusCode: 500);
        }

        // GET: api/edukacije/5
        [HttpGet("{id}")]
        public ActionResult<Skola> GetEdukacija(int id)
        {
            var edukacijeResult = _edukacijaRepository.Get(id).Map(DtoMapping.ToDto);

            return edukacijeResult switch
            {
                { IsSuccess: true } => Ok(edukacijeResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(edukacijeResult.Message, statusCode: 500)
            };
        }

        // PUT: api/edukacije/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult EditEdukacija(int id, Edukacija edukacija)
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

        // POST: api/edukacije
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Skola> CreateEdukacije(Edukacija edukacija)
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

            var deleteResult = _edukacijaRepository.Remove(id);
            return deleteResult
                ? NoContent()
                : Problem(deleteResult.Message, statusCode: 500);
        }
    }
}
