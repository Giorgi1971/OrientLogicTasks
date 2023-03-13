//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using CredoProject.Core.Db;
//using CredoProject.Core.Db.Entity;

//namespace CredoProject.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CustomerController : ControllerBase
//    {
//        private readonly CredoDbContext _context;

//        public CustomerController(CredoDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Customer
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<CustomerEntity>>> GetCustomerEntities()
//        {
//            if (_context.CustomerEntities == null)
//            {
//                return NotFound();
//            }
//            return await _context.CustomerEntities.ToListAsync();
//        }

//        // GET: api/Customer/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<CustomerEntity>> GetCustomerEntity(int id)
//        {
//            if (_context.CustomerEntities == null)
//            {
//                return NotFound();
//            }
//            var customerEntity = await _context.CustomerEntities.FindAsync(id);

//            if (customerEntity == null)
//            {
//                return NotFound();
//            }

//            return customerEntity;
//        }

//        // PUT: api/Customer/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutCustomerEntity(int id, CustomerEntity customerEntity)
//        {
//            if (id != customerEntity.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(customerEntity).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!CustomerEntityExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Customer
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<CustomerEntity>> PostCustomerEntity(CustomerEntity customerEntity)
//        {
//            if (_context.CustomerEntities == null)
//            {
//                return Problem("Entity set 'CredoDbContext.CustomerEntities'  is null.");
//            }
//            _context.CustomerEntities.Add(customerEntity);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetCustomerEntity", new { id = customerEntity.Id }, customerEntity);
//        }

//        // DELETE: api/Customer/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteCustomerEntity(int id)
//        {
//            if (_context.CustomerEntities == null)
//            {
//                return NotFound();
//            }
//            var customerEntity = await _context.CustomerEntities.FindAsync(id);
//            if (customerEntity == null)
//            {
//                return NotFound();
//            }

//            _context.CustomerEntities.Remove(customerEntity);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool CustomerEntityExists(int id)
//        {
//            return (_context.CustomerEntities?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}
