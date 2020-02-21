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
    public class LinesController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public LinesController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/Lines
        [HttpGet]
        public IEnumerable<Line> GetLine()
        {
            return _context.Line;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<Line> GetLines([FromQuery]string number, string supplier, string linePlan, string fixedCharge, string contractExpiration, string billedTo)
        {
            var lines = _context.Line.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(number))
            {
                lines = lines.Where(p => p.Number.Contains(number));
            }
            if (!String.IsNullOrEmpty(supplier))
            {
                lines = lines.Where(p => p.Supplier.Contains(supplier));
            }
            if (!String.IsNullOrEmpty(linePlan))
            {
                lines = lines.Where(p => p.LinePlan.Contains(linePlan));
            }
            if (!String.IsNullOrEmpty(fixedCharge))
            {
                lines = lines.Where(p => p.FixedCharge.Equals(fixedCharge));
            }
            if (!String.IsNullOrEmpty(contractExpiration))
            {
                lines = lines.Where(p => p.ContractExpiration.Equals(contractExpiration));
            }
            if (!String.IsNullOrEmpty(billedTo))
            {
                lines = lines.Where(p => p.BilledTo.Contains(billedTo));
            }
            
            return lines;
        }

        // GET: api/Lines/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLine([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var line = await _context.Line.FindAsync(id);

            if (line == null)
            {
                return NotFound();
            }

            return Ok(line);
        }

        // PUT: api/Lines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLine([FromRoute] string id, [FromBody] Line line)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != line.Number)
            {
                return BadRequest();
            }

            _context.Entry(line).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineExists(id))
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

        // POST: api/Lines
        [HttpPost]
        public async Task<IActionResult> PostLine([FromBody] Line line)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Line.Add(line);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LineExists(line.Number))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLine", new { id = line.Number }, line);
        }

        // DELETE: api/Lines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLine([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var line = await _context.Line.FindAsync(id);
            if (line == null)
            {
                return NotFound();
            }

            _context.Line.Remove(line);
            await _context.SaveChangesAsync();

            return Ok(line);
        }

        private bool LineExists(string id)
        {
            return _context.Line.Any(e => e.Number == id);
        }
    }
}