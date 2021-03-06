using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P4._0_backend.Data;
using P4._0_backend.Helpers;
using P4._0_backend.Models;
using P4._0_backend.Services;

namespace P4._0_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly DataContext _context;

        public UsersController(IUserService userService, DataContext context)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/Users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                return await _context.Users.ToListAsync();
            }

            return Unauthorized();
            
        }
        [Authorize]
        [HttpGet("count")]
        public async Task<ActionResult<Count>> GetAmount()
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                var amount = await _context.Users.CountAsync();
                Count counter = new Count();
                counter.count = amount;

                return counter;
            }
            return Unauthorized();
            
        }

        // GET: api/Users/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserID").Value) == id || Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                var users = await _context.Users.FindAsync(id);

                if (users == null)
                {
                    return NotFound();
                }

                return users;
            }

            return Unauthorized();
           
        }



        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            var entity = await _context.Users.FindAsync(id);
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserID").Value) == id && Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) != 1)
            {
                if (id != users.ID)
                {
                    return BadRequest();
                }
                
                if (users.Password != null && users.Password != "")
                {
                    using (SHA1 sha1Hash = SHA1.Create())
                    {
                        byte[] sourceBytes = Encoding.UTF8.GetBytes(users.Password);
                        byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                        string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                        entity.Password = hash;
                    }
                }
                if (users.LastName != null && users.LastName != "")
                {
                    entity.LastName = users.LastName;
                }
                if (users.FirstName != null && users.FirstName != "")
                {
                    entity.FirstName = users.FirstName;
                }
                if (users.email != null && users.email != "")
                {
                    entity.email = users.email;
                }
               




                users.userLevel = 2;

                _context.Entry(entity).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(id))
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
            else if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                if (id != users.ID)
                {
                    return BadRequest();
                }
                if (users.Password != null && users.Password != "")
                {
                    using (SHA1 sha1Hash = SHA1.Create())
                    {
                        byte[] sourceBytes = Encoding.UTF8.GetBytes(users.Password);
                        byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                        string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                        entity.Password = hash;
                    }
                }
                if (users.LastName != null && users.LastName != "")
                {
                    entity.LastName = users.LastName;
                }
                if (users.FirstName != null && users.FirstName != "")
                {
                    entity.FirstName = users.FirstName;
                }
                if (users.email != null && users.email != "")
                {
                    entity.email = users.email;
                }
                if (users.userLevel != 0)
                {
                    entity.userLevel = users.userLevel;
                }


                _context.Entry(entity).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users users)
        {
            if (User.Claims.FirstOrDefault() != null)
            {
                if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
                {
                    using (SHA1 sha1Hash = SHA1.Create())
                    {
                        byte[] sourceBytes = Encoding.UTF8.GetBytes(users.Password);
                        byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                        string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                        users.Password = hash;
                    }


                    _context.Users.Add(users);
                    await _context.SaveChangesAsync();
                    var user2 = _userService.Authenticate(users.email, users.Password);

                    return CreatedAtAction("GetUsers", new { id = users.ID }, user2);
                }
            }
            
            using (SHA1 sha1Hash = SHA1.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(users.Password);
                byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                users.Password = hash;
            }
            users.userLevel = 2;
            
            _context.Users.Add(users);
            await _context.SaveChangesAsync();
            var user = _userService.Authenticate(users.email, users.Password);

            return CreatedAtAction("GetUsers", new { id = users.ID }, user);
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Users userParam)
        {
            using (SHA1 sha1Hash = SHA1.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(userParam.Password);
                byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                userParam.Password = hash;
            }
            var user = _userService.Authenticate(userParam.email, userParam.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(user);
        }

        // DELETE: api/Users/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            if (Int32.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserLevel").Value) == 1)
            {
                var users = await _context.Users.FindAsync(id);
                if (users == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(users);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            return Unauthorized();
           
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
