using AkcijeSkoleWebApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AkcijeSkoleWebApi.Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace AkcijeSkoleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MjestaController : ControllerBase
    {
        private readonly AkcijeSkoleDbContext _context;

        public MjestaController(AkcijeSkoleDbContext context)
        {
            _context = context;
        }

        // GET: /mjesta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mjesta>>> PrikaziSvaMjesta()
        {
            return await _context.Mjesta.ToListAsync();
        }

        [HttpGet("pbrMjesta")]
        public async Task<ActionResult<IEnumerable<Mjesta>>> PrikaziMjesto(int pbrMjesta) 
        {
            var mjesto = await _context.Mjesta.FindAsync(pbrMjesta);

            if (mjesto == null)
            {
                return NotFound("Za navedeni poštanski broj ne postoji mjesto.");
            }
            return Ok(mjesto);

        }
          
        

    }
}
