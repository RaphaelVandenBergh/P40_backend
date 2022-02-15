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
    public class ParkingsController : ControllerBase
    {
        private readonly DataContext _context;

        public ParkingsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Parkings
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parking>>> GetParking()
        {
            return await _context.Parking.ToListAsync();
        }

        // GET: api/Parkings/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Parking>> GetParking(int id)
        {
            var parking = await _context.Parking.FindAsync(id);

            if (parking == null)
            {
                return NotFound();
            }

            return parking;
        }

        // PUT: api/Parkings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParking(int id, Parking parking)
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                if (id != parking.ID)
                {
                    return BadRequest();
                }

                _context.Entry(parking).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingExists(id))
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
            return Unauthorized();
           
        }

        // POST: api/Parkings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Parking>> PostParking(Parking parking)
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                parking.timestamp = DateTime.Now;
                _context.Parking.Add(parking);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetParking", new { id = parking.ID }, parking);
            }
            return Unauthorized();
        }

        // DELETE: api/Parkings/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParking(int id)
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                var parking = await _context.Parking.FindAsync(id);
                if (parking == null)
                {
                    return NotFound();
                }

                _context.Parking.Remove(parking);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            return Unauthorized();

            
        }

        private bool ParkingExists(int id)
        {
            return _context.Parking.Any(e => e.ID == id);
        }
    }
}
