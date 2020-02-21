using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto1.Models;

namespace Proyecto1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserSoftwaresController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public UserSoftwaresController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/UserSoftwares
        [HttpGet]
        public IEnumerable<UserSoftware> GetUserSoftware()
        {
            return _context.UserSoftware;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<UserSoftware> GetUserSoftwares([FromQuery]string userName, string softwareCode)
        {
            var userSoftwares = _context.UserSoftware.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(userName))
            {
                userSoftwares = userSoftwares.Where(p => p.UserName.Contains(userName));
            }
            if (!String.IsNullOrEmpty(softwareCode))
            {
                userSoftwares = userSoftwares.Where(p => p.SoftwareCode.Contains(softwareCode));
            }

            return userSoftwares;
        }

        // GET: api/UserSoftwares/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserSoftware([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userSoftware = await _context.UserSoftware.FindAsync(id);

            if (userSoftware == null)
            {
                return NotFound();
            }

            return Ok(userSoftware);
        }

        // PUT: api/UserSoftwares/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserSoftware([FromRoute] string id, [FromBody] UserSoftware userSoftware)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userSoftware.UserName)
            {
                return BadRequest();
            }

            _context.Entry(userSoftware).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSoftwareExists(id))
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

        // POST: api/UserSoftwares
        [HttpPost]
        public async Task<IActionResult> PostUserSoftware([FromBody] UserSoftware userSoftware)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserSoftware.Add(userSoftware);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserSoftwareExists(userSoftware.UserName))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserSoftware", new { id = userSoftware.UserName }, userSoftware);
        }

        // DELETE: api/UserSoftwares/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSoftware([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userSoftware = await _context.UserSoftware.FindAsync(id);
            if (userSoftware == null)
            {
                return NotFound();
            }

            _context.UserSoftware.Remove(userSoftware);
            await _context.SaveChangesAsync();

            return Ok(userSoftware);
        }

        private bool UserSoftwareExists(string id)
        {
            return _context.UserSoftware.Any(e => e.UserName == id);
        }
    }
}