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
    public class UserLinesController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public UserLinesController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/UserLines
        [HttpGet]
        public IEnumerable<UserLine> GetUserLine()
        {
            return _context.UserLine;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<UserLine> GetUserLines([FromQuery]string userName, string number)
        {
            var userLines = _context.UserLine.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(userName))
            {
                userLines = userLines.Where(p => p.UserName.Contains(userName));
            }
            if (!String.IsNullOrEmpty(number))
            {
                userLines = userLines.Where(p => p.Number.Contains(number));
            }

            return userLines;
        }

        // GET: api/UserLines/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserLine([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userLine = await _context.UserLine.FindAsync(id);

            if (userLine == null)
            {
                return NotFound();
            }

            return Ok(userLine);
        }

        // PUT: api/UserLines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserLine([FromRoute] string id, [FromBody] UserLine userLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userLine.UserName)
            {
                return BadRequest();
            }

            _context.Entry(userLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLineExists(id))
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

        // POST: api/UserLines
        [HttpPost]
        public async Task<IActionResult> PostUserLine([FromBody] UserLine userLine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserLine.Add(userLine);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserLineExists(userLine.UserName))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserLine", new { id = userLine.UserName }, userLine);
        }

        // DELETE: api/UserLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserLine([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userLine = await _context.UserLine.FindAsync(id);
            if (userLine == null)
            {
                return NotFound();
            }

            _context.UserLine.Remove(userLine);
            await _context.SaveChangesAsync();

            return Ok(userLine);
        }

        private bool UserLineExists(string id)
        {
            return _context.UserLine.Any(e => e.UserName == id);
        }
    }
}