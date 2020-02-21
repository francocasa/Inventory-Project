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
    public class DbmodelsController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public DbmodelsController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/Dbmodels
        [HttpGet]
        public IEnumerable<Dbmodel> GetDbmodel()
        {
            return _context.Dbmodel;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<Dbmodel> GetDbmodels([FromQuery]string id, string nameTable, string characteristic)
        {
            var dbmodels = _context.Dbmodel.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(id))
            {
                dbmodels = dbmodels.Where(p => p.Id.Equals(id));
            }
            if (!String.IsNullOrEmpty(nameTable))
            {
                dbmodels = dbmodels.Where(p => p.NameTable.Contains(nameTable));
            }
            if (!String.IsNullOrEmpty(characteristic))
            {
                dbmodels = dbmodels.Where(p => p.Characteristic.Contains(characteristic));
            }



            return dbmodels;
        }
        // GET: api/Dbmodels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDbmodel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbmodel = await _context.Dbmodel.FindAsync(id);

            if (dbmodel == null)
            {
                return NotFound();
            }

            return Ok(dbmodel);
        }

        // PUT: api/Dbmodels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDbmodel([FromRoute] int id, [FromBody] Dbmodel dbmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dbmodel.Id)
            {
                return BadRequest();
            }

            _context.Entry(dbmodel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DbmodelExists(id))
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

        // POST: api/Dbmodels
        [HttpPost]
        public async Task<IActionResult> PostDbmodel([FromBody] Dbmodel dbmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Dbmodel.Add(dbmodel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DbmodelExists(dbmodel.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDbmodel", new { id = dbmodel.Id }, dbmodel);
        }

        // DELETE: api/Dbmodels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDbmodel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbmodel = await _context.Dbmodel.FindAsync(id);
            if (dbmodel == null)
            {
                return NotFound();
            }

            _context.Dbmodel.Remove(dbmodel);
            await _context.SaveChangesAsync();

            return Ok(dbmodel);
        }

        private bool DbmodelExists(int id)
        {
            return _context.Dbmodel.Any(e => e.Id == id);
        }
    }
}