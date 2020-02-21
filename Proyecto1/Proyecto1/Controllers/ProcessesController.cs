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
    public class ProcessesController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public ProcessesController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/Processes
        [HttpGet]
        public IEnumerable<Process> GetProcess()
        {
            return _context.Process;
        }

        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<Process> GetProcesses([FromQuery]string processName, string process)
        {
            var processes = _context.Process.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(processName))
            {
                processes = processes.Where(p => p.ProcessName.Contains(processName));
            }
            if (!String.IsNullOrEmpty(process))
            {
                processes = processes.Where(p => p.ProcessTitle.Contains(process));
            }

            return processes;
        }

        // GET: api/Processes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProcess([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var process = await _context.Process.FindAsync(id);

            if (process == null)
            {
                return NotFound();
            }

            return Ok(process);
        }

        // PUT: api/Processes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcess([FromRoute] string id, [FromBody] Process process)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != process.ProcessTitle)
            {
                return BadRequest();
            }

            _context.Entry(process).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcessExists(id))
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

        // POST: api/Processes
        [HttpPost]
        public async Task<IActionResult> PostProcess([FromBody] Process process)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Process.Add(process);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProcessExists(process.ProcessTitle))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProcess", new { id = process.ProcessTitle }, process);
        }

        // DELETE: api/Processes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcess([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var process = await _context.Process.FindAsync(id);
            if (process == null)
            {
                return NotFound();
            }

            _context.Process.Remove(process);
            await _context.SaveChangesAsync();

            return Ok(process);
        }

        private bool ProcessExists(string id)
        {
            return _context.Process.Any(e => e.ProcessTitle == id);
        }
    }
}