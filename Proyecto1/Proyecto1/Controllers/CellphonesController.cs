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
    public class CellphonesController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public CellphonesController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/Cellphones
        [HttpGet]
        public IEnumerable<Cellphone> GetCellphone()
        {
            return _context.Cellphone;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<Cellphone> GetCellphones([FromQuery]string imei, string type, string model, string brand, string status, string assignedMonth, string modifiedMonth, string modifiedBy, string modelAirwatch, string observation, string exUser)
        {
            var cellphones = _context.Cellphone.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(imei))
            {
                cellphones = cellphones.Where(p => p.Imei.Contains(imei));
            }
            if (!String.IsNullOrEmpty(type))
            {
                cellphones = cellphones.Where(p => p.Type.Contains(type));
            }
            if (!String.IsNullOrEmpty(model))
            {
                cellphones = cellphones.Where(p => p.Model.Contains(model));
            }
            if (!String.IsNullOrEmpty(brand))
            {
                cellphones = cellphones.Where(p => p.Brand.Contains(brand));
            }
            if (!String.IsNullOrEmpty(status))
            {
                cellphones = cellphones.Where(p => p.Status.Contains(status));
            }
            if (!String.IsNullOrEmpty(assignedMonth))
            {
                cellphones = cellphones.Where(p => p.AssignedDate.Month.Equals(assignedMonth));
            }
            if (!String.IsNullOrEmpty(modifiedMonth))
            {
                cellphones = cellphones.Where(p => p.ModifiedDate.Month.Equals(modifiedMonth));
            }
            if (!String.IsNullOrEmpty(modifiedBy))
            {
                cellphones = cellphones.Where(p => p.ModifiedBy.Contains(modifiedBy));
            }
            if (!String.IsNullOrEmpty(modelAirwatch))
            {
                cellphones = cellphones.Where(p => p.ModelAirwatch.Contains(modelAirwatch));
            }
            if (!String.IsNullOrEmpty(observation))
            {
                cellphones = cellphones.Where(p => p.Observation.Contains(observation));
            }
            if (!String.IsNullOrEmpty(exUser))
            {
                cellphones = cellphones.Where(p => p.ExUser.Contains(exUser));
            }



            return cellphones;
        }

        // GET: api/Cellphones/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCellphone([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cellphone = await _context.Cellphone.FindAsync(id);

            if (cellphone == null)
            {
                return NotFound();
            }

            return Ok(cellphone);
        }

        // PUT: api/Cellphones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCellphone([FromRoute] string id, [FromBody] Cellphone cellphone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cellphone.Imei)
            {
                return BadRequest();
            }

            _context.Entry(cellphone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CellphoneExists(id))
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

        // POST: api/Cellphones
        [HttpPost]
        public async Task<IActionResult> PostCellphone([FromBody] Cellphone cellphone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Cellphone.Add(cellphone);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CellphoneExists(cellphone.Imei))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCellphone", new { id = cellphone.Imei }, cellphone);
        }

        // DELETE: api/Cellphones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCellphone([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cellphone = await _context.Cellphone.FindAsync(id);
            if (cellphone == null)
            {
                return NotFound();
            }

            _context.Cellphone.Remove(cellphone);
            await _context.SaveChangesAsync();

            return Ok(cellphone);
        }

        private bool CellphoneExists(string id)
        {
            return _context.Cellphone.Any(e => e.Imei == id);
        }
    }
}