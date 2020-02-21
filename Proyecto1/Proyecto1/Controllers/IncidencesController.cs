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
    public class IncidencesController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public IncidencesController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/Incidences
        [HttpGet]
        public IEnumerable<Incidence> GetIncidence()
        {
            return _context.Incidence;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<Incidence> GetIncidences([FromQuery]string userName, string incidenceType, string comment)
        {
            var incidence = _context.Incidence.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(userName))
            {
                incidence = incidence.Where(p => p.UserName.Contains(userName));
            }
            if (!String.IsNullOrEmpty(incidenceType))
            {
                incidence = incidence.Where(p => p.IncidenceType.Contains(incidenceType));
            }
            if (!String.IsNullOrEmpty(comment))
            {
                incidence = incidence.Where(p => p.Comment.Contains(comment));
            }

            return incidence;
        }

        // GET: api/Incidences/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncidence([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var incidence = await _context.Incidence.FindAsync(id);

            if (incidence == null)
            {
                return NotFound();
            }

            return Ok(incidence);
        }

        // PUT: api/Incidences/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncidence([FromRoute] string id, [FromBody] Incidence incidence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != incidence.UserName)
            {
                return BadRequest();
            }

            _context.Entry(incidence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidenceExists(id))
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

        // POST: api/Incidences
        [HttpPost]
        public async Task<IActionResult> PostIncidence([FromBody] Incidence incidence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Incidence.Add(incidence);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IncidenceExists(incidence.UserName))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetIncidence", new { id = incidence.UserName }, incidence);
        }

        // DELETE: api/Incidences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncidence([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var incidence = await _context.Incidence.FindAsync(id);
            if (incidence == null)
            {
                return NotFound();
            }

            _context.Incidence.Remove(incidence);
            await _context.SaveChangesAsync();

            return Ok(incidence);
        }

        private bool IncidenceExists(string id)
        {
            return _context.Incidence.Any(e => e.UserName == id);
        }
    }
}