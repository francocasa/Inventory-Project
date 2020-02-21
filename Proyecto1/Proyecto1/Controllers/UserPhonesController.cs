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
    public class UserPhonesController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public UserPhonesController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/UserPhones
        [HttpGet]
        public IEnumerable<UserPhone> GetUserPhone()
        {
            return _context.UserPhone;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<UserPhone> GetUserPhones([FromQuery]string userName, string mac)
        {
            var userPhones = _context.UserPhone.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(userName))
            {
                userPhones = userPhones.Where(p => p.UserName.Contains(userName));
            }
            if (!String.IsNullOrEmpty(mac))
            {
                userPhones = userPhones.Where(p => p.Mac.Contains(mac));
            }

            return userPhones;
        }

        // GET: api/UserPhones/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserPhone([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userPhone = await _context.UserPhone.FindAsync(id);

            if (userPhone == null)
            {
                return NotFound();
            }

            return Ok(userPhone);
        }

        // PUT: api/UserPhones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPhone([FromRoute] string id, [FromBody] UserPhone userPhone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userPhone.UserName)
            {
                return BadRequest();
            }

            _context.Entry(userPhone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPhoneExists(id))
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

        // POST: api/UserPhones
        [HttpPost]
        public async Task<IActionResult> PostUserPhone([FromBody] UserPhone userPhone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserPhone.Add(userPhone);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserPhoneExists(userPhone.UserName))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserPhone", new { id = userPhone.UserName }, userPhone);
        }

        // DELETE: api/UserPhones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserPhone([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userPhone = await _context.UserPhone.FindAsync(id);
            if (userPhone == null)
            {
                return NotFound();
            }

            _context.UserPhone.Remove(userPhone);
            await _context.SaveChangesAsync();

            return Ok(userPhone);
        }

        private bool UserPhoneExists(string id)
        {
            return _context.UserPhone.Any(e => e.UserName == id);
        }
    }
}