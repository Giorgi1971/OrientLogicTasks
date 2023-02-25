using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P_6_Pricing_API.Data;
using P_6_Pricing_API.Data.Entity;

namespace P_6_Pricing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbInputController : ControllerBase
    {
        private readonly PricingDbContext _context;

        public DbInputController(PricingDbContext context)
        {
            _context = context;
        }

        // GET: api/DbInput
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DbInput>>> GetDbInputs()
        {
          if (_context.DbInputs == null)
          {
              return NotFound();
          }
            return await _context.DbInputs.ToListAsync();
        }

        // POST: api/DbInput
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DbInput>> PostDbInput(DbInput dbInput)
        {
          if (_context.DbInputs == null)
          {
              return Problem("Entity set 'PricingDbContext.DbInputs'  is null.");
          }
            _context.DbInputs.Add(dbInput);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDbInput", new { id = dbInput.DbInputId }, dbInput);
        }

        private bool DbInputExists(int id)
        {
            return (_context.DbInputs?.Any(e => e.DbInputId == id)).GetValueOrDefault();
        }
    }
}
