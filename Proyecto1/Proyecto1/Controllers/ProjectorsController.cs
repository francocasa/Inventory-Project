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
    public class ProjectorsController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public ProjectorsController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/Projectors
        [HttpGet]
        public IEnumerable<Projector> GetProjector()
        {
            return _context.Projector;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<Projector> GetProjectors([FromQuery]string serviceTag, string model, string purchaseDate, string partOfLeasing, string location)
        {
            var projectors = _context.Projector.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(serviceTag))
            {
                projectors = projectors.Where(p => p.ServiceTag.Contains(serviceTag));
            }
            if (!String.IsNullOrEmpty(model))
            {
                projectors = projectors.Where(p => p.Model.Contains(model));
            }
            if (!String.IsNullOrEmpty(purchaseDate))
            {
                projectors = projectors.Where(p => p.PurchaseDate.Equals(purchaseDate));
            }
            if (!String.IsNullOrEmpty(partOfLeasing))
            {
                projectors = projectors.Where(p => p.PartOfLeasing.Contains(partOfLeasing));
            }
            if (!String.IsNullOrEmpty(location))
            {
                projectors = projectors.Where(p => p.Location.Contains(location));
            }

            return projectors;
        }

        // GET: api/Projectors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjector([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projector = await _context.Projector.FindAsync(id);

            if (projector == null)
            {
                return NotFound();
            }

            return Ok(projector);
        }

        // PUT: api/Projectors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjector([FromRoute] string id, [FromBody] Projector projector)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projector.ServiceTag)
            {
                return BadRequest();
            }

            _context.Entry(projector).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectorExists(id))
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

        // POST: api/Projectors
        [HttpPost]
        public async Task<IActionResult> PostProjector([FromBody] Projector projector)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Projector.Add(projector);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProjectorExists(projector.ServiceTag))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProjector", new { id = projector.ServiceTag }, projector);
        }

        // DELETE: api/Projectors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjector([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projector = await _context.Projector.FindAsync(id);
            if (projector == null)
            {
                return NotFound();
            }

            _context.Projector.Remove(projector);
            await _context.SaveChangesAsync();

            return Ok(projector);
        }

        private bool ProjectorExists(string id)
        {
            return _context.Projector.Any(e => e.ServiceTag == id);
        }
    }
}