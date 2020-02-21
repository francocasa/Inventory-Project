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
    public class SoftwareEquipmentsController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public SoftwareEquipmentsController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/SoftwareEquipments
        [HttpGet]
        public IEnumerable<SoftwareEquipment> GetSoftwareEquipment()
        {
            return _context.SoftwareEquipment;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<SoftwareEquipment> GetSoftwareEquipments([FromQuery]string softwareCode, string serviceTag)
        {
            var softwareEquipments = _context.SoftwareEquipment.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(softwareCode))
            {
                softwareEquipments = softwareEquipments.Where(p => p.SoftwareCode.Contains(softwareCode));
            }
            if (!String.IsNullOrEmpty(serviceTag))
            {
                softwareEquipments = softwareEquipments.Where(p => p.ServiceTag.Contains(serviceTag));
            }
            
            return softwareEquipments;
        }

        // GET: api/SoftwareEquipments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSoftwareEquipment([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var softwareEquipment = await _context.SoftwareEquipment.FindAsync(id);

            if (softwareEquipment == null)
            {
                return NotFound();
            }

            return Ok(softwareEquipment);
        }

        // PUT: api/SoftwareEquipments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSoftwareEquipment([FromRoute] string id, [FromBody] SoftwareEquipment softwareEquipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != softwareEquipment.SoftwareCode)
            {
                return BadRequest();
            }

            _context.Entry(softwareEquipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SoftwareEquipmentExists(id))
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

        // POST: api/SoftwareEquipments
        [HttpPost]
        public async Task<IActionResult> PostSoftwareEquipment([FromBody] SoftwareEquipment softwareEquipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SoftwareEquipment.Add(softwareEquipment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SoftwareEquipmentExists(softwareEquipment.SoftwareCode))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSoftwareEquipment", new { id = softwareEquipment.SoftwareCode }, softwareEquipment);
        }

        // DELETE: api/SoftwareEquipments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSoftwareEquipment([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var softwareEquipment = await _context.SoftwareEquipment.FindAsync(id);
            if (softwareEquipment == null)
            {
                return NotFound();
            }

            _context.SoftwareEquipment.Remove(softwareEquipment);
            await _context.SaveChangesAsync();

            return Ok(softwareEquipment);
        }

        private bool SoftwareEquipmentExists(string id)
        {
            return _context.SoftwareEquipment.Any(e => e.SoftwareCode == id);
        }
    }
}