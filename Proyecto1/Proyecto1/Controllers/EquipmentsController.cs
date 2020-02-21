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
    public class EquipmentsController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public EquipmentsController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/Equipments
        [HttpGet]
        public IEnumerable<Equipment> GetEquipment()
        {
            return _context.Equipment;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<Equipment> GetEquipments([FromQuery]string serviceTag, string computerName, string partOfLeasing, string nextRenewal, string model, string purchaseMonth, string type,
            string status, string condition, string location, string mainFunctionDevice, string validatedOnMonth, string assignedBy, string exUser, string storage)
        {
            var equipments = _context.Equipment.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(serviceTag))
            {
                equipments = equipments.Where(p => p.ServiceTag.Contains(serviceTag));
            }
            if (!String.IsNullOrEmpty(computerName))
            {
                equipments = equipments.Where(p => p.ComputerName.Contains(computerName));
            }
            if (!String.IsNullOrEmpty(partOfLeasing))
            {
                equipments = equipments.Where(p => p.PartOfLeasing.Contains(partOfLeasing));
            }
            if (!String.IsNullOrEmpty(nextRenewal))
            {
                equipments = equipments.Where(p => p.NextRenewal.Contains(nextRenewal));
            }
            if (!String.IsNullOrEmpty(model))
            {
                equipments = equipments.Where(p => p.Model.Contains(model));
            }
            if (!String.IsNullOrEmpty(purchaseMonth))
            {
                equipments = equipments.Where(p => p.PurchaseDate.Month.Equals(purchaseMonth));
            }
            if (!String.IsNullOrEmpty(type))
            {
                equipments = equipments.Where(p => p.Location.Contains(type));
            }
            if (!String.IsNullOrEmpty(status))
            {
                equipments = equipments.Where(p => p.Status.Contains(status));
            }
            if (!String.IsNullOrEmpty(condition))
            {
                equipments = equipments.Where(p => p.Condition.Contains(condition));
            }
            if (!String.IsNullOrEmpty(location))
            {
                equipments = equipments.Where(p => p.Location.Contains(location));
            }
            if (!String.IsNullOrEmpty(mainFunctionDevice))
            {
                equipments = equipments.Where(p => p.MainFunctionDevice.Contains(mainFunctionDevice));
            }
            if (!String.IsNullOrEmpty(validatedOnMonth))
            {
                equipments = equipments.Where(p => p.ValidatedOn.Month.Equals(validatedOnMonth));
            }
            if (!String.IsNullOrEmpty(assignedBy))
            {
                equipments = equipments.Where(p => p.AssignedBy.Contains(assignedBy));
            }
            if (!String.IsNullOrEmpty(exUser))
            {
                equipments = equipments.Where(p => p.ExUser.Contains(exUser));
            }
            if (!String.IsNullOrEmpty(storage))
            {
                equipments = equipments.Where(p => p.Storage.Contains(storage));
            }



            return equipments;
        }

        // GET: api/Equipments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEquipment([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var equipment = await _context.Equipment.FindAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            return Ok(equipment);
        }

        // PUT: api/Equipments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipment([FromRoute] string id, [FromBody] Equipment equipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != equipment.ServiceTag)
            {
                return BadRequest();
            }

            _context.Entry(equipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentExists(id))
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

        // POST: api/Equipments
        [HttpPost]
        public async Task<IActionResult> PostEquipment([FromBody] Equipment equipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Equipment.Add(equipment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EquipmentExists(equipment.ServiceTag))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEquipment", new { id = equipment.ServiceTag }, equipment);
        }

        // DELETE: api/Equipments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipment([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var equipment = await _context.Equipment.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();

            return Ok(equipment);
        }

        private bool EquipmentExists(string id)
        {
            return _context.Equipment.Any(e => e.ServiceTag == id);
        }
    }
}