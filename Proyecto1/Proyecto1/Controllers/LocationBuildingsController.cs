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
    public class LocationBuildingsController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public LocationBuildingsController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/LocationBuildings
        [HttpGet]
        public IEnumerable<LocationBuilding> GetLocationBuilding()
        {
            return _context.LocationBuilding;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<LocationBuilding> GetLocationBuildings([FromQuery]string location, string building)
        {
            var locationBuildings = _context.LocationBuilding.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(location))
            {
                locationBuildings = locationBuildings.Where(p => p.Location.Contains(location));
            }
            if (!String.IsNullOrEmpty(building))
            {
                locationBuildings = locationBuildings.Where(p => p.Building.Contains(building));
            }

            return locationBuildings;
        }

        // GET: api/LocationBuildings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationBuilding([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationBuilding = await _context.LocationBuilding.FindAsync(id);

            if (locationBuilding == null)
            {
                return NotFound();
            }

            return Ok(locationBuilding);
        }

        // PUT: api/LocationBuildings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocationBuilding([FromRoute] string id, [FromBody] LocationBuilding locationBuilding)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locationBuilding.Location)
            {
                return BadRequest();
            }

            _context.Entry(locationBuilding).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationBuildingExists(id))
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

        // POST: api/LocationBuildings
        [HttpPost]
        public async Task<IActionResult> PostLocationBuilding([FromBody] LocationBuilding locationBuilding)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.LocationBuilding.Add(locationBuilding);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LocationBuildingExists(locationBuilding.Location))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLocationBuilding", new { id = locationBuilding.Location }, locationBuilding);
        }

        // DELETE: api/LocationBuildings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationBuilding([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationBuilding = await _context.LocationBuilding.FindAsync(id);
            if (locationBuilding == null)
            {
                return NotFound();
            }

            _context.LocationBuilding.Remove(locationBuilding);
            await _context.SaveChangesAsync();

            return Ok(locationBuilding);
        }

        private bool LocationBuildingExists(string id)
        {
            return _context.LocationBuilding.Any(e => e.Location == id);
        }
    }
}