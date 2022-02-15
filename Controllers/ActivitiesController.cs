using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
    public class ActivitiesController : ControllerBase
    {
        private readonly DataContext _context;

        public ActivitiesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Activities
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivity([FromQuery] PaginationParameters parameters)
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                var activitylist = await _context.Activity.OrderByDescending(x => x.ID).ToListAsync();
                var paginationMetaData = new PaginationMetadata(parameters.PageNumber, activitylist.Count(), parameters.PageSize);
                Response.Headers.Add("X-Pagination" , JsonSerializer.Serialize(paginationMetaData));
                return activitylist.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize).ToList();

            }

            return Unauthorized();
            
        }

        // GET: api/Activities/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(int id)
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                var activity = await _context.Activity.FindAsync(id);

                if (activity == null)
                {
                    return NotFound();
                }

                return activity;
            }

            return Unauthorized();
            
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(int id, Activity activity)
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                if (id != activity.ID)
                {
                    return BadRequest();
                }

                _context.Entry(activity).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExists(id))
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

        // POST: api/Activities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Activity>> PostActivity(Activity activity)
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                activity.Created_at = DateTime.Now;
                _context.Activity.Add(activity);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetActivity", new { id = activity.ID }, activity);
            }

            return Unauthorized();
            
        }

        // DELETE: api/Activities/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                var activity = await _context.Activity.FindAsync(id);
                if (activity == null)
                {
                    return NotFound();
                }

                _context.Activity.Remove(activity);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            return Unauthorized();
            
        }

        private bool ActivityExists(int id)
        {
            return _context.Activity.Any(e => e.ID == id);
        }
    }
}
