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
    public class UserEquipmentsController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public UserEquipmentsController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/UserEquipments
        [HttpGet]
        public IEnumerable<UserEquipment> GetUserEquipment()
        {
            return _context.UserEquipment;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<UserEquipment> GetUserEquipments([FromQuery]string userName, string serviceTag)
        {
            var userEquipments = _context.UserEquipment.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(userName))
            {
                userEquipments = userEquipments.Where(p => p.UserName.Contains(userName));
            }
            if (!String.IsNullOrEmpty(serviceTag))
            {
                userEquipments = userEquipments.Where(p => p.ServiceTag.Contains(serviceTag));
            }
            return userEquipments;
        }

        // PUT: api/UserEquipments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserEquipment([FromRoute] string id, [FromBody] UserEquipment userEquipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userEquipment.UserName)
            {
                return BadRequest();
            }

            _context.Entry(userEquipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserEquipmentExists(id))
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

        // POST: api/UserEquipments
        [HttpPost]
        public async Task<IActionResult> PostUserEquipment([FromBody] UserEquipment userEquipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UserEquipment.Add(userEquipment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserEquipmentExists(userEquipment.UserName))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserEquipment", new { id = userEquipment.UserName }, userEquipment);
        }

        // DELETE: api/UserEquipments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserEquipment([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEquipment = await _context.UserEquipment.FindAsync(id);
            if (userEquipment == null)
            {
                return NotFound();
            }

            _context.UserEquipment.Remove(userEquipment);
            await _context.SaveChangesAsync();

            return Ok(userEquipment);
        }

        private bool UserEquipmentExists(string id)
        {
            return _context.UserEquipment.Any(e => e.UserName == id);
        }
    }
}