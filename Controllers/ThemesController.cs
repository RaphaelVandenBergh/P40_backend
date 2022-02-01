using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P4._0_backend.Data;
using P4._0_backend.Helpers;
using P4._0_backend.Models;

namespace P4._0_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemesController : ControllerBase
    {
        private readonly DataContext _context;

        public ThemesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Themes
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Theme>>> GetStyles()
        {
            return await _context.Themes.ToListAsync();
        }

        
        [HttpGet("Active")]
        public async Task<ActionResult<Theme>> GetActive()
        {
            return await _context.Themes.Where(t => t.Active == true).SingleAsync();
        }
        
        [Authorize]
        [HttpGet("count")]
        public async Task<ActionResult<Count>> GetAmount()
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                var amount = await _context.Themes.CountAsync();
                Count counter = new Count();
                counter.count = amount;
                return counter;
            }
            return Unauthorized();
            
        }

        // GET: api/Themes/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Theme>> GetTheme(int id)
        {
            var theme = await _context.Themes.FindAsync(id);

            if (theme == null)
            {
                return NotFound();
            }

            return theme;
        }

        // PUT: api/Themes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTheme(int id, Theme theme)
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                if (id != theme.ID)
                {
                    return BadRequest();
                }

                _context.Entry(theme).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThemeExists(id))
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

        // POST: api/Themes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Theme>> PostTheme(Theme theme)
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                _context.Themes.Add(theme);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTheme", new { id = theme.ID }, theme);
            }

            return Unauthorized();
            
        }

        // DELETE: api/Themes/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTheme(int id)
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                var theme = await _context.Themes.FindAsync(id);
                if (theme == null)
                {
                    return NotFound();
                }

                _context.Themes.Remove(theme);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            return Unauthorized();
            
        }

        private bool ThemeExists(int id)
        {
            return _context.Themes.Any(e => e.ID == id);
        }
    }
}
