using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AkcijeSkole.Repositories;
using AkcijeSkoleWebApi.DTOs;
using DbModels = AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkole.Commons;
using AkcijeSkole.Repositories.SqlServer;
using System.Data;

namespace AkcijeSkoleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EdukacijeController : ControllerBase
    {
        private readonly IEdukacijeRepository<int, DbModels.Edukacije> _edukacijaRepository;

        public EdukacijeController(IEdukacijeRepository<int, DbModels.Edukacije> context)
        {
            _edukacijaRepository = context;
        }
         
        // GET: api/edukacije
        [HttpGet]
        public ActionResult<IEnumerable<Edukacija>> GetAllEdukacije()
        {
            return Ok(_edukacijaRepository.GetAll().Select(DtoMapping.ToDto));
        }

        // GET: api/edukacije/5
        [HttpGet("{id}")]
        public ActionResult<Edukacija> GetEdukacija(int id)
        {
            var edukacijaOption = _edukacijaRepository.Get(id).Map(DtoMapping.ToDto);

            return edukacijaOption
                ? Ok(edukacijaOption.Data)
                : NotFound();
        }

        // PUT: api/edukacije/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult EditEdukacije(int id, Edukacija edukacija)
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

            return _edukacijaRepository.Update(edukacija.toDbModel())
                ? AcceptedAtAction("EditEdukacije", edukacija)
                : StatusCode(500);
        }

        // POST: api/edukacije
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Edukacija> CreateRole(Edukacija edukacija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return _edukacijaRepository.Insert(edukacija.toDbModel())
                ? CreatedAtAction("GetEdukacije", new { id = edukacija.IdEdukacija }, edukacija)
                : StatusCode(500);
        }

        // DELETE: api/edukacije/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            if (!_edukacijaRepository.Exists(id))
                return NotFound();

            return _edukacijaRepository.Remove(id)
                ? NoContent()
                : StatusCode(500);
        }
    }
}
