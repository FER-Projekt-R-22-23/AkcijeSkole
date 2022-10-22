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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mjesta>>> PrikaziSvaMjesta()
        {
            return Ok(await _context.Mjesta.ToListAsync());
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

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Mjesta>>> dodajMjesto(Mjesta mjesto) 
        {
            _context.Mjesta.Add(mjesto);
            await _context.SaveChangesAsync();
            return Ok(await _context.Mjesta.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<Mjesta>>> urediMjesta(Mjesta zahtjev)
        {
            var dbMjesto = await _context.Mjesta.FindAsync(zahtjev.PbrMjesta);
            if (dbMjesto == null)
                return NotFound("Za navedeni poštanski broj ne postoji mjesto.");

            dbMjesto.NazivMjesta = zahtjev.NazivMjesta;

            await _context.SaveChangesAsync();

            return Ok(await _context.Mjesta.ToListAsync());
        }

        [HttpDelete("pbrMjesta")]
        public async Task<ActionResult<IEnumerable<Mjesta>>> izbrisiMjesto(int pbrMjesta)
        {
            var dbMjesto = await _context.Mjesta.FindAsync(pbrMjesta);
            if(dbMjesto == null) 
                return NotFound("Za navedeni poštanski broj ne postoji mjesto.");

            _context.Mjesta.Remove(dbMjesto);
            await _context.SaveChangesAsync();

            return Ok(await _context.Mjesta.ToListAsync());
        }

          
        

    }
}
