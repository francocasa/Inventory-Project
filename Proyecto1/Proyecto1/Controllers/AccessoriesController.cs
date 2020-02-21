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
    public class AccessoriesController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public AccessoriesController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/Accessories
        [HttpGet]
        public IEnumerable<Accessory> GetAccessory()
        {
            return _context.Accessory;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<Accessory> GetAccessories([FromQuery]string identification, string equipment, string comment)
        {
            var accessories = _context.Accessory.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(identification))
            {
                accessories = accessories.Where(p => p.Identification.Contains(identification));
            }
            if (!String.IsNullOrEmpty(equipment))
            {
                accessories = accessories.Where(p => p.Equipment.Contains(equipment));
            }
            if (!String.IsNullOrEmpty(comment))
            {
                accessories = accessories.Where(p => p.Comment.Contains(comment));
            }

            return accessories;
        }

        // GET: api/Accessories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccessory([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accessory = await _context.Accessory.FindAsync(id);

            if (accessory == null)
            {
                return NotFound();
            }

            return Ok(accessory);
        }

        // PUT: api/Accessories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccessory([FromRoute] string id, [FromBody] Accessory accessory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accessory.Identification)
            {
                return BadRequest();
            }

            _context.Entry(accessory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessoryExists(id))
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

        // POST: api/Accessories
        [HttpPost]
        public async Task<IActionResult> PostAccessory([FromBody] Accessory accessory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Accessory.Add(accessory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccessoryExists(accessory.Identification))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccessory", new { id = accessory.Identification }, accessory);
        }

        // DELETE: api/Accessories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccessory([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accessory = await _context.Accessory.FindAsync(id);
            if (accessory == null)
            {
                return NotFound();
            }

            _context.Accessory.Remove(accessory);
            await _context.SaveChangesAsync();

            return Ok(accessory);
        }

        private bool AccessoryExists(string id)
        {
            return _context.Accessory.Any(e => e.Identification == id);
        }
    }
}