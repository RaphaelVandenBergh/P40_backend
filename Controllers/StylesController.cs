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
    public class StylesController : ControllerBase
    {
        private readonly DataContext _context;

        public StylesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Styles
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Style>>> GetStyle()
        {
            return await _context.Style.ToListAsync();
        }

        // GET: api/Styles/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Style>> GetStyle(int id)
        {
            var style = await _context.Style.FindAsync(id);

            if (style == null)
            {
                return NotFound();
            }

            return style;
        }

        // PUT: api/Styles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStyle(int id, Style style)
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                if (id != style.ID)
                {
                    return BadRequest();
                }

                _context.Entry(style).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StyleExists(id))
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

        // POST: api/Styles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Style>> PostStyle(Style style)
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                _context.Style.Add(style);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetStyle", new { id = style.ID }, style);
            }

            return Unauthorized();

            
        }

        // DELETE: api/Styles/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStyle(int id)
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                var style = await _context.Style.FindAsync(id);
                if (style == null)
                {
                    return NotFound();
                }

                _context.Style.Remove(style);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            return Unauthorized();

            
        }

        private bool StyleExists(int id)
        {
            return _context.Style.Any(e => e.ID == id);
        }
    }
}
