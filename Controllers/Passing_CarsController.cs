﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P4._0_backend.Data;
using P4._0_backend.Models;

namespace P4._0_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Passing_CarsController : ControllerBase
    {
        private readonly DataContext _context;

        public Passing_CarsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Passing_Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passing_Cars>>> GetPassing_Cars()
        {
            return await _context.Passing_Cars.ToListAsync();
        }

        // GET: api/Passing_Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Passing_Cars>> GetPassing_Cars(int id)
        {
            var passing_Cars = await _context.Passing_Cars.FindAsync(id);

            if (passing_Cars == null)
            {
                return NotFound();
            }

            return passing_Cars;
        }

        // PUT: api/Passing_Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassing_Cars(int id, Passing_Cars passing_Cars)
        {
            if (id != passing_Cars.ID)
            {
                return BadRequest();
            }

            _context.Entry(passing_Cars).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Passing_CarsExists(id))
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

        // POST: api/Passing_Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Passing_Cars>> PostPassing_Cars(Passing_Cars passing_Cars)
        {
            _context.Passing_Cars.Add(passing_Cars);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPassing_Cars", new { id = passing_Cars.ID }, passing_Cars);
        }

        // DELETE: api/Passing_Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassing_Cars(int id)
        {
            var passing_Cars = await _context.Passing_Cars.FindAsync(id);
            if (passing_Cars == null)
            {
                return NotFound();
            }

            _context.Passing_Cars.Remove(passing_Cars);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Passing_CarsExists(int id)
        {
            return _context.Passing_Cars.Any(e => e.ID == id);
        }
    }
}