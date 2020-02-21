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
    public class PhonesController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public PhonesController(ITinventoryDBContext context)
        {
            _context = context;
        }

       // GET: api/Phones
        [HttpGet]
        public IEnumerable<Phone> GetPhone()
        {
            return _context.Phone;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<Phone> GetPhones([FromQuery]string mac, string model, string serialNumber, string location)
        {
            var phones = _context.Phone.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(mac))
            {
                phones = phones.Where(p => p.Mac.Contains(mac));
            }
            if (!String.IsNullOrEmpty(model))
            {
                phones = phones.Where(p => p.Model.Contains(model));
            }
            if (!String.IsNullOrEmpty(serialNumber))
            {
                phones = phones.Where(p => p.SerialNumber.Contains(serialNumber));
            }
            if (!String.IsNullOrEmpty(location))
            {
                phones = phones.Where(p => p.Location.Contains(location));
            }
            
            return phones;
        }


        // GET: api/Phones/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhone([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var phone = await _context.Phone.FindAsync(id);

            if (phone == null)
            {
                return NotFound();
            }

            return Ok(phone);
        }

        // PUT: api/Phones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhone([FromRoute] string id, [FromBody] Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phone.Mac)
            {
                return BadRequest();
            }

            _context.Entry(phone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneExists(id))
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

        // POST: api/Phones
        [HttpPost]
        public async Task<IActionResult> PostPhone([FromBody] Phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Phone.Add(phone);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PhoneExists(phone.Mac))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPhone", new { id = phone.Mac }, phone);
        }

        // DELETE: api/Phones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhone([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var phone = await _context.Phone.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }

            _context.Phone.Remove(phone);
            await _context.SaveChangesAsync();

            return Ok(phone);
        }

        private bool PhoneExists(string id)
        {
            return _context.Phone.Any(e => e.Mac == id);
        }
    }
}