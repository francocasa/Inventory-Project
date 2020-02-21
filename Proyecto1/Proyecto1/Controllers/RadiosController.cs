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
    public class RadiosController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public RadiosController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/Radios
        [HttpGet]
        public IEnumerable<Radio> GetRadio()
        {
            return _context.Radio;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<Radio> GetRadios([FromQuery]string serviceTag, string model, string brand, string latestUpdate, string area, string location, string comment)
        {
            var radios = _context.Radio.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(serviceTag))
            {
                radios = radios.Where(p => p.ServiceTag.Contains(serviceTag));
            }
            if (!String.IsNullOrEmpty(model))
            {
                radios = radios.Where(p => p.Model.Contains(model));
            }
            if (!String.IsNullOrEmpty(brand))
            {
                radios = radios.Where(p => p.Brand.Contains(brand));
            }
            if (!String.IsNullOrEmpty(latestUpdate))
            {
                radios = radios.Where(p => p.LatestUpdate.Equals(latestUpdate));
            }
            if (!String.IsNullOrEmpty(area))
            {
                radios = radios.Where(p => p.Area.Contains(area));
            }
            if (!String.IsNullOrEmpty(location))
            {
                radios = radios.Where(p => p.Location.Contains(location));
            }
            if (!String.IsNullOrEmpty(comment))
            {
                radios = radios.Where(p => p.Comment.Contains(comment));
            }

            return radios;
        }

        // GET: api/Radios/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRadio([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var radio = await _context.Radio.FindAsync(id);

            if (radio == null)
            {
                return NotFound();
            }

            return Ok(radio);
        }

        // PUT: api/Radios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRadio([FromRoute] string id, [FromBody] Radio radio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != radio.ServiceTag)
            {
                return BadRequest();
            }

            _context.Entry(radio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RadioExists(id))
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

        // POST: api/Radios
        [HttpPost]
        public async Task<IActionResult> PostRadio([FromBody] Radio radio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Radio.Add(radio);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RadioExists(radio.ServiceTag))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRadio", new { id = radio.ServiceTag }, radio);
        }

        // DELETE: api/Radios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRadio([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var radio = await _context.Radio.FindAsync(id);
            if (radio == null)
            {
                return NotFound();
            }

            _context.Radio.Remove(radio);
            await _context.SaveChangesAsync();

            return Ok(radio);
        }

        private bool RadioExists(string id)
        {
            return _context.Radio.Any(e => e.ServiceTag == id);
        }
    }
}