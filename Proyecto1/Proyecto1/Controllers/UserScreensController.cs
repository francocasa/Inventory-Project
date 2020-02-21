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
    public class UserScreensController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public UserScreensController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/UserScreens
        [HttpGet]
        public IEnumerable<UserScreen> GetUserScreen()
        {
            return _context.UserScreen;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<UserScreen> GetUserScreens([FromQuery]string userName, string serviceTag)
        {
            var userScreens = _context.UserScreen.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(userName))
            {
                userScreens = userScreens.Where(p => p.UserName.Contains(userName));
            }
            if (!String.IsNullOrEmpty(serviceTag))
            {
                userScreens = userScreens.Where(p => p.ServiceTag.Contains(serviceTag));
            }
            
            return userScreens;
        }

        // GET: api/UserScreens/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserScreen([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userScreen = await _context.UserScreen.FindAsync(id);

            if (userScreen == null)
            {
                return NotFound();
            }

            return Ok(userScreen);
        }

        // PUT: api/UserScreens/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserScreen([FromRoute] string id, [FromBody] UserScreen userScreen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userScreen.UserName)
            {
                return BadRequest();
            }

            _context.Entry(userScreen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserScreenExists(id))
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

        // POST: api/UserScreens
        [HttpPost]
        public async Task<IActionResult> PostUserScreen([FromBody] UserScreen userScreen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserScreen.Add(userScreen);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserScreenExists(userScreen.UserName))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserScreen", new { id = userScreen.UserName }, userScreen);
        }

        // DELETE: api/UserScreens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserScreen([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userScreen = await _context.UserScreen.FindAsync(id);
            if (userScreen == null)
            {
                return NotFound();
            }

            _context.UserScreen.Remove(userScreen);
            await _context.SaveChangesAsync();

            return Ok(userScreen);
        }

        private bool UserScreenExists(string id)
        {
            return _context.UserScreen.Any(e => e.UserName == id);
        }
    }
}