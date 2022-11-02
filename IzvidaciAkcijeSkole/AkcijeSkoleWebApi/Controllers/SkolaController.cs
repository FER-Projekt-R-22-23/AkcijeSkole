using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AkcijeSkole.Repositories;
using AkcijeSkoleWebApi.DTOs;
using DbModels = AkcijeSkole.DataAccess.SqlServer.Data.DbModels;
using AkcijeSkole.Commons;
using AkcijeSkole.Repositories.SqlServer;
using System.Data;

namespace ExampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkoleController : ControllerBase
    {
        private readonly ISkoleRepository<int, DbModels.Skole> _skolaRepository;

        public SkoleController(ISkoleRepository<int, DbModels.Skole> context)
        {
            _skolaRepository = context;
        }
         
        // GET: api/skole
        [HttpGet]
        public ActionResult<IEnumerable<Skola>> GetAllSkole()
        {
            return Ok(_skolaRepository.GetAll().Select(DtoMapping.ToDto));
        }

        // GET: api/skole/5
        [HttpGet("{id}")]
        public ActionResult<Skola> GetSkola(int id)
        {
            var skolaOption = _skolaRepository.Get(id).Map(DtoMapping.ToDto);

            return skolaOption
                ? Ok(skolaOption.Data)
                : NotFound();
        }

        // PUT: api/Skole/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult EditSkole(int id, Skola skola)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != skola.IdSkole)
            {
                return BadRequest();
            }

            if (!_skolaRepository.Exists(id))
            {
                return NotFound();
            }

            return _skolaRepository.Update(skola.toDbModel())
                ? AcceptedAtAction("EditSkole", skola)
                : StatusCode(500);
        }

        // POST: api/Skole
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Skola> CreateRole(Skola skola)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return _skolaRepository.Insert(skola.toDbModel())
                ? CreatedAtAction("GetSkole", new { id = skola.IdSkole }, skola)
                : StatusCode(500);
        }

        // DELETE: api/Skole/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            if (!_skolaRepository.Exists(id))
                return NotFound();

            return _skolaRepository.Remove(id)
                ? NoContent()
                : StatusCode(500);
        }


    }
}
