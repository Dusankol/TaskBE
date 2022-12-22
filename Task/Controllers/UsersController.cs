using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Models;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDTO>>> GetUsers()
        {


            var users = await _context.Users.Select(x => new GetUserDTO
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DOB = x.DOB,
                PhoneNumber = x.PhoneNumber,
                Position = x.Position,
                CompanyId = x.CompanyId,
                CompanyName = x.Company.Name
            }).ToListAsync();

            return users;

        }

        //GET api/Users/Company/4

        [HttpGet("company/{id}")]
        public async Task<ActionResult<IEnumerable<GetUserDTO>>> GetUsersFromCompany(Guid id)
        {

            var users = await _context.Users.Select(x => new GetUserDTO
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DOB = x.DOB,
                PhoneNumber = x.PhoneNumber,
                Position = x.Position,
                CompanyId = x.CompanyId,
                CompanyName = x.Company.Name
            }).Where(y=>y.CompanyId==id).ToListAsync();

            return users;
            
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDTO>> GetUser(Guid id)
        {

            var user = await _context.Users.FindAsync(id);
           

            if (user == null)
            {
                return NotFound();
            }
           
            return new GetUserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DOB = user.DOB,
                PhoneNumber = user.PhoneNumber,
                Position = user.Position,
                CompanyId = user.CompanyId,
                CompanyName = user.Company.Name


            };
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, CreateUserDTO userDTO)
        {

            try
            {
              var user=  await _context.Users.FindAsync(id);

                user.FirstName = userDTO.FirstName;
                user.LastName= userDTO.LastName;
                user.PhoneNumber = userDTO.PhoneNumber;
                user.Position= userDTO.Position;
                user.CompanyId = userDTO.CompanyId;
                user.DOB= userDTO.DOB;


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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Guid>> PostUser(CreateUserDTO userDTO)
        {

            var user = new User
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Position = userDTO.Position,
                PhoneNumber = userDTO.PhoneNumber,
                DOB = userDTO.DOB,
                CompanyId = userDTO.CompanyId,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user.Id);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
