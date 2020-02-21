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
    public class SoftwaresController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public SoftwaresController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/Softwares
        [HttpGet]
        public IEnumerable<Software> GetSoftware()
        {
            return _context.Software;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<Software> GetSoftwares([FromQuery]string softwareCode, string softwareName, string licensed, string contractStart, string contractFinal)
        {
            var softwares = _context.Software.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(softwareCode))
            {
                softwares = softwares.Where(p => p.SoftwareCode.Contains(softwareCode));
            }
            if (!String.IsNullOrEmpty(softwareName))
            {
                softwares = softwares.Where(p => p.SoftwareName.Contains(softwareName));
            }
            if (!String.IsNullOrEmpty(licensed))
            {
                softwares = softwares.Where(p => p.Licensed.Contains(licensed));
            }
            if (!String.IsNullOrEmpty(contractStart))
            {
                softwares = softwares.Where(p => p.ContractStart.Equals(contractStart));
            }
            if (!String.IsNullOrEmpty(contractFinal))
            {
                softwares = softwares.Where(p => p.ContractFinal.Equals(contractFinal));
            }

            return softwares;
        }

        // GET: api/Softwares/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSoftware([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var software = await _context.Software.FindAsync(id);

            if (software == null)
            {
                return NotFound();
            }

            return Ok(software);
        }

        // PUT: api/Softwares/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSoftware([FromRoute] string id, [FromBody] Software software)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != software.SoftwareCode)
            {
                return BadRequest();
            }

            _context.Entry(software).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SoftwareExists(id))
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

        // POST: api/Softwares
        [HttpPost]
        public async Task<IActionResult> PostSoftware([FromBody] Software software)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Software.Add(software);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SoftwareExists(software.SoftwareCode))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSoftware", new { id = software.SoftwareCode }, software);
        }

        // DELETE: api/Softwares/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSoftware([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var software = await _context.Software.FindAsync(id);
            if (software == null)
            {
                return NotFound();
            }

            _context.Software.Remove(software);
            await _context.SaveChangesAsync();

            return Ok(software);
        }

        private bool SoftwareExists(string id)
        {
            return _context.Software.Any(e => e.SoftwareCode == id);
        }
    }
}