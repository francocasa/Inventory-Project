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
    public class ScreensController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public ScreensController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/Screens
        [HttpGet]
        public IEnumerable<Screen> GetScreen()
        {
            return _context.Screen;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<Screen> GetScreens([FromQuery]string serviceTag, string model, string purchaseDate, string partOfLeasing, string location)
        {
            var screens = _context.Screen.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(serviceTag))
            {
                screens = screens.Where(p => p.ServiceTag.Contains(serviceTag));
            }
            if (!String.IsNullOrEmpty(model))
            {
                screens = screens.Where(p => p.Model.Contains(model));
            }
            if (!String.IsNullOrEmpty(purchaseDate))
            {
                screens = screens.Where(p => p.PurchaseDate.Equals(purchaseDate));
            }
            if (!String.IsNullOrEmpty(partOfLeasing))
            {
                screens = screens.Where(p => p.PartOfLeasing.Contains(partOfLeasing));
            }
            if (!String.IsNullOrEmpty(location))
            {
                screens = screens.Where(p => p.Location.Contains(location));
            }

            return screens;
        }

        // GET: api/Screens/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetScreen([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var screen = await _context.Screen.FindAsync(id);

            if (screen == null)
            {
                return NotFound();
            }

            return Ok(screen);
        }

        // PUT: api/Screens/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScreen([FromRoute] string id, [FromBody] Screen screen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != screen.ServiceTag)
            {
                return BadRequest();
            }

            _context.Entry(screen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScreenExists(id))
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

        // POST: api/Screens
        [HttpPost]
        public async Task<IActionResult> PostScreen([FromBody] Screen screen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Screen.Add(screen);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ScreenExists(screen.ServiceTag))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetScreen", new { id = screen.ServiceTag }, screen);
        }

        // DELETE: api/Screens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScreen([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var screen = await _context.Screen.FindAsync(id);
            if (screen == null)
            {
                return NotFound();
            }

            _context.Screen.Remove(screen);
            await _context.SaveChangesAsync();

            return Ok(screen);
        }

        private bool ScreenExists(string id)
        {
            return _context.Screen.Any(e => e.ServiceTag == id);
        }
    }
}