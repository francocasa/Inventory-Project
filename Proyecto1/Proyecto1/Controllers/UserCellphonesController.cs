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
    public class UserCellphonesController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public UserCellphonesController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/UserCellphones
        [HttpGet]
        public IEnumerable<UserCellphone> GetUserCellphone()
        {
            return _context.UserCellphone;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<UserCellphone> GetUserCellphones([FromQuery]string userName, string imei)
        {
            var userCellphones = _context.UserCellphone.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(userName))
            {
                userCellphones = userCellphones.Where(p => p.UserName.Contains(userName));
            }
            if (!String.IsNullOrEmpty(imei))
            {
                userCellphones = userCellphones.Where(p => p.Imei.Contains(imei));
            }
                       
            return userCellphones;
        }

        // GET: api/UserCellphones/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserCellphone([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userCellphone = await _context.UserCellphone.FindAsync(id);

            if (userCellphone == null)
            {
                return NotFound();
            }

            return Ok(userCellphone);
        }

        // PUT: api/UserCellphones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCellphone([FromRoute] string id, [FromBody] UserCellphone userCellphone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userCellphone.UserName)
            {
                return BadRequest();
            }

            _context.Entry(userCellphone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCellphoneExists(id))
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

        // POST: api/UserCellphones
        [HttpPost]
        public async Task<IActionResult> PostUserCellphone([FromBody] UserCellphone userCellphone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserCellphone.Add(userCellphone);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserCellphoneExists(userCellphone.UserName))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserCellphone", new { id = userCellphone.UserName }, userCellphone);
        }

        // DELETE: api/UserCellphones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCellphone([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userCellphone = await _context.UserCellphone.FindAsync(id);
            if (userCellphone == null)
            {
                return NotFound();
            }

            _context.UserCellphone.Remove(userCellphone);
            await _context.SaveChangesAsync();

            return Ok(userCellphone);
        }

        private bool UserCellphoneExists(string id)
        {
            return _context.UserCellphone.Any(e => e.UserName == id);
        }
    }
}