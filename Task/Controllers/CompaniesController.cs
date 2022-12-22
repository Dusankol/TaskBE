using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Models;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCompanyDTO>>> GetCompany()
        {
            if (_context.Companies == null)
            {
                return NotFound();
            }

            var companies = await _context.Companies.Select(x => new GetCompanyDTO
            {
                Id = x.Id,
                Name = x.Name,
                City = x.City,
                Country = x.Country,
            }).ToListAsync();

            return Ok(companies);
        }



        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCompanyDTO>> GetCompany(Guid id)
        {
            if (_context.Companies == null)
            {
                return NotFound();
            }


            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return new GetCompanyDTO
            {
                Id = company.Id,
                Name = company.Name,
                City = company.City,
                Country = company.Country,

            };


        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(Guid id, CreateCompanyDTO companyDTO)
        {

            try
            {
                var company = await _context.Companies.FindAsync(id);

                company.Name=companyDTO.Name;
                company.City=companyDTO.City;
                company.Country=companyDTO.Country;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Guid>>PostCompany(CreateCompanyDTO companyDTO)
        {
            var company = new Company
            {
                Name = companyDTO.Name,
                City = companyDTO.City,
                Country = companyDTO.Country,
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompany", new { id = company.Id }, company.Id);
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            if (_context.Companies == null)
            {
                return NotFound();
            }
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExists(Guid id)
        {
            return (_context.Companies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
