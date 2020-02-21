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
    public class UserRadiosController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public UserRadiosController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/UserRadios
        [HttpGet]
        public IEnumerable<UserRadio> GetUserRadio()
        {
            return _context.UserRadio;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<UserRadio> GetUserRadios([FromQuery]string userName, string serviceTag)
        {
            var userRadios = _context.UserRadio.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(userName))
            {
                userRadios = userRadios.Where(p => p.UserName.Contains(userName));
            }
            if (!String.IsNullOrEmpty(serviceTag))
            {
                userRadios = userRadios.Where(p => p.ServiceTag.Contains(serviceTag));
            }

            return userRadios;
        }

        // GET: api/UserRadios/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserRadio([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userRadio = await _context.UserRadio.FindAsync(id);

            if (userRadio == null)
            {
                return NotFound();
            }

            return Ok(userRadio);
        }

        // PUT: api/UserRadios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRadio([FromRoute] string id, [FromBody] UserRadio userRadio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userRadio.UserName)
            {
                return BadRequest();
            }

            _context.Entry(userRadio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRadioExists(id))
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

        // POST: api/UserRadios
        [HttpPost]
        public async Task<IActionResult> PostUserRadio([FromBody] UserRadio userRadio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserRadio.Add(userRadio);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserRadioExists(userRadio.UserName))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserRadio", new { id = userRadio.UserName }, userRadio);
        }

        // DELETE: api/UserRadios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRadio([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userRadio = await _context.UserRadio.FindAsync(id);
            if (userRadio == null)
            {
                return NotFound();
            }

            _context.UserRadio.Remove(userRadio);
            await _context.SaveChangesAsync();

            return Ok(userRadio);
        }

        private bool UserRadioExists(string id)
        {
            return _context.UserRadio.Any(e => e.UserName == id);
        }
    }
}