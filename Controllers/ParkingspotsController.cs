using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P4._0_backend.Data;
using P4._0_backend.Models;

namespace P4._0_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingspotsController : ControllerBase
    {
        private readonly DataContext _context;

        public ParkingspotsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Parkingspots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parkingspots>>> GetParkingspots()
        {
            return await _context.Parkingspots.ToListAsync();
        }

        // GET: api/Parkingspots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Parkingspots>> GetParkingspots(int id)
        {
            var parkingspots = await _context.Parkingspots.FindAsync(id);

            if (parkingspots == null)
            {
                return NotFound();
            }

            return parkingspots;
        }

        // PUT: api/Parkingspots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParkingspots(int id, Parkingspots parkingspots)
        {
            if (id != parkingspots.ID)
            {
                return BadRequest();
            }

            _context.Entry(parkingspots).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkingspotsExists(id))
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

        // POST: api/Parkingspots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        [HttpPost]
        public async Task<ActionResult<Parkingspots>> PostParkingspots(Parkingspots parkingspots)
        {
            _context.Parkingspots.Add(parkingspots);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParkingspots", new { id = parkingspots.ID }, parkingspots);
        }

        // DELETE: api/Parkingspots/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParkingspots(int id)
        {
            var parkingspots = await _context.Parkingspots.FindAsync(id);
            if (parkingspots == null)
            {
                return NotFound();
            }

            _context.Parkingspots.Remove(parkingspots);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParkingspotsExists(int id)
        {
            return _context.Parkingspots.Any(e => e.ID == id);
        }
    }
}
