using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AkcijeSkoleWebApi.Data;
using AkcijeSkoleWebApi.Data.DbModels;

namespace AkcijeSkoleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AkcijeController : ControllerBase
    {
        private readonly AkcijeSkoleDbContext _context;

        public AkcijeController(AkcijeSkoleDbContext context)
        {
            _context = context;
        }

        // GET: api/Akcije
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Akcije>>> GetAkcije()
        {
            return await _context.Akcije.ToListAsync();
        }

        // GET: api/Akcije/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Akcije>> GetAkcije(int id)
        {
            var akcije = await _context.Akcije.FindAsync(id);

            if (akcije == null)
            {
                return NotFound();
            }

            return akcije;
        }

        // PUT: api/Akcije/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAkcije(int id, Akcije akcije)
        {
            if (id != akcije.IdAkcija)
            {
                return BadRequest();
            }

            _context.Entry(akcije).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AkcijeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Akcije
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Akcije>> PostAkcije(Akcije akcije)
        {
            _context.Akcije.Add(akcije);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAkcije", new { id = akcije.IdAkcija }, akcije);
        }

        // DELETE: api/Akcije/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAkcije(int id)
        {
            var akcije = await _context.Akcije.FindAsync(id);
            if (akcije == null)
            {
                return NotFound();
            }

            _context.Akcije.Remove(akcije);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AkcijeExists(int id)
        {
            return _context.Akcije.Any(e => e.IdAkcija == id);
        }
    }
}
