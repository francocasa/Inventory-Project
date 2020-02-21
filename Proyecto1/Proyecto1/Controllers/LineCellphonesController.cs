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
    public class LineCellphonesController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public LineCellphonesController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/LineCellphones
        [HttpGet]
        public IEnumerable<LineCellphone> GetLineCellphone()
        {
            return _context.LineCellphone;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<LineCellphone> GetLineCellphones([FromQuery]string number, string imei)
        {
            var lineCellphones = _context.LineCellphone.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(number))
            {
                lineCellphones = lineCellphones.Where(p => p.Number.Contains(number));
            }
            if (!String.IsNullOrEmpty(imei))
            {
                lineCellphones = lineCellphones.Where(p => p.Imei.Contains(imei));
            }

            return lineCellphones;
        }

        // GET: api/LineCellphones/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLineCellphone([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lineCellphone = await _context.LineCellphone.FindAsync(id);

            if (lineCellphone == null)
            {
                return NotFound();
            }

            return Ok(lineCellphone);
        }

        // PUT: api/LineCellphones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLineCellphone([FromRoute] string id, [FromBody] LineCellphone lineCellphone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lineCellphone.Number)
            {
                return BadRequest();
            }

            _context.Entry(lineCellphone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineCellphoneExists(id))
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

        // POST: api/LineCellphones
        [HttpPost]
        public async Task<IActionResult> PostLineCellphone([FromBody] LineCellphone lineCellphone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.LineCellphone.Add(lineCellphone);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LineCellphoneExists(lineCellphone.Number))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLineCellphone", new { id = lineCellphone.Number }, lineCellphone);
        }

        // DELETE: api/LineCellphones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLineCellphone([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lineCellphone = await _context.LineCellphone.FindAsync(id);
            if (lineCellphone == null)
            {
                return NotFound();
            }

            _context.LineCellphone.Remove(lineCellphone);
            await _context.SaveChangesAsync();

            return Ok(lineCellphone);
        }

        private bool LineCellphoneExists(string id)
        {
            return _context.LineCellphone.Any(e => e.Number == id);
        }
    }
}