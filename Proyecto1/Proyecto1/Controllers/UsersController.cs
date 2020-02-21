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
    public class UsersController : ControllerBase
    {
        private readonly ITinventoryDBContext _context;

        public UsersController(ITinventoryDBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUser()
        {
            return _context.User;
        }



        [HttpGet]
        [ActionName("*")]//url?userName=ssss
        public IEnumerable<User> GetUsers([FromQuery]string userName, string fullName, string mail, string department, string position, string supervisor, string location, string annex)
        {
            var users = _context.User.AsQueryable();//select * from User
            if (!String.IsNullOrEmpty(userName))
            {
                users = users.Where(p => p.UserName.Contains(userName));
            }
            if (!String.IsNullOrEmpty(fullName))
            {
                users = users.Where(p => p.FullName.Contains(fullName));
            }
            if (!String.IsNullOrEmpty(mail))
            {
                users = users.Where(p => p.Mail.Contains(mail));
            }
            if (!String.IsNullOrEmpty(department))
            {
                users = users.Where(p => p.Deparment.Contains(department));
            }
            if (!String.IsNullOrEmpty(position))
            {
                users = users.Where(p => p.Position.Contains(position));
            }
            if (!String.IsNullOrEmpty(supervisor))
            {
                users = users.Where(p => p.Supervisor.Contains(supervisor));
            }
            if (!String.IsNullOrEmpty(location))
            {
                users = users.Where(p => p.Location.Contains(location));
            }
            if (!String.IsNullOrEmpty(annex))
            {
                users = users.Where(p => p.Annex.Contains(annex));
            }



            return users;
        }

        


        // GET: api/Users/5
        [HttpGet("{id}")]
        [ActionName("ById")]//url/id
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [ActionName("ByUserName")]//url/id
        public async Task<IActionResult> GetUserByUserName([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user =  _context.User.AsNoTracking()
                //.Include(u=>u.UserAccesory.Select(a=>a.IdentificationNavigation.Identification))
                .Include("UserAccesory.IdentificationNavigation")
                .Where(u=>u.UserName == id).SingleOrDefault()
                ;

            

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] string id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserName)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.User.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.UserName))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.UserName }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.UserName == id);
        }
    }
}