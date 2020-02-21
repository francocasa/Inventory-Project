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
    public class UserAccessoriesController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public UserAccessoriesController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/UserAccessories
        [HttpGet]
        public IEnumerable<UserAccessory> GetUserAccessory()
        {
            return _context.UserAccessory;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<UserAccessory> GetUserAccessories([FromQuery]string userName, string identification)
        {
            var userAccessories = _context.UserAccessory.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(identification))
            {
                userAccessories = userAccessories.Where(p => p.Identification.Contains(identification));
            }
            if (!String.IsNullOrEmpty(userName))
            {
                userAccessories = userAccessories.Where(p => p.UserName.Contains(userName));
            }

            return userAccessories;
        }
        // GET: api/UserAccessories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAccessory([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userAccessory = await _context.UserAccessory.FindAsync(id);

            if (userAccessory == null)
            {
                return NotFound();
            }

            return Ok(userAccessory);
        }

        // PUT: api/UserAccessories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAccessory([FromRoute] string id, [FromBody] UserAccessory userAccessory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userAccessory.UserName)
            {
                return BadRequest();
            }

            _context.Entry(userAccessory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAccessoryExists(id))
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

        // POST: api/UserAccessories
        [HttpPost]
        public async Task<IActionResult> PostUserAccessory([FromBody] UserAccessory userAccessory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserAccessory.Add(userAccessory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserAccessoryExists(userAccessory.UserName))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserAccessory", new { id = userAccessory.UserName }, userAccessory);
        }

        // DELETE: api/UserAccessories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAccessory([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userAccessory = await _context.UserAccessory.FindAsync(id);
            if (userAccessory == null)
            {
                return NotFound();
            }

            _context.UserAccessory.Remove(userAccessory);
            await _context.SaveChangesAsync();

            return Ok(userAccessory);
        }

        private bool UserAccessoryExists(string id)
        {
            return _context.UserAccessory.Any(e => e.UserName == id);
        }
    }
}