using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IzvidaciAkcijeSkoleWebApi.Data;
using IzvidaciAkcijeSkoleWebApi.Data.Models;

namespace IzvidaciAkcijeSkoleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerenskeLokacijesController : ControllerBase
    {
        private readonly AkcijeSkoleContext _context;

        public TerenskeLokacijesController(AkcijeSkoleContext context)
        {
            _context = context;
        }

        // GET: api/TerenskeLokacijes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TerenskeLokacije>>> GetTerenskeLokacije()
        {
            return await _context.TerenskeLokacije.ToListAsync();
        }

        // GET: api/TerenskeLokacijes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TerenskeLokacije>> GetTerenskeLokacije(int id)
        {
            var terenskeLokacije = await _context.TerenskeLokacije.FindAsync(id);

            if (terenskeLokacije == null)
            {
                return NotFound();
            }

            return terenskeLokacije;
        }

        // PUT: api/TerenskeLokacijes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTerenskeLokacije(int id, TerenskeLokacije terenskeLokacije)
        {
            if (id != terenskeLokacije.IdTerenskeLokacije)
            {
                return BadRequest();
            }

            _context.Entry(terenskeLokacije).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerenskeLokacijeExists(id))
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

        // POST: api/TerenskeLokacijes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TerenskeLokacije>> PostTerenskeLokacije(TerenskeLokacije terenskeLokacije)
        {
            _context.TerenskeLokacije.Add(terenskeLokacije);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTerenskeLokacije", new { id = terenskeLokacije.IdTerenskeLokacije }, terenskeLokacije);
        }

        // DELETE: api/TerenskeLokacijes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTerenskeLokacije(int id)
        {
            var terenskeLokacije = await _context.TerenskeLokacije.FindAsync(id);
            if (terenskeLokacije == null)
            {
                return NotFound();
            }

            _context.TerenskeLokacije.Remove(terenskeLokacije);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TerenskeLokacijeExists(int id)
        {
            return _context.TerenskeLokacije.Any(e => e.IdTerenskeLokacije == id);
        }
    }
}
