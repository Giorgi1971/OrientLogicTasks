using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RSSFeedAPI.Db;
using RSSFeedAPI.Db.Entity;

namespace RSSFeedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Tag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagEntity>>> GetTags()
        {
          if (_context.Tags == null)
          {
              return NotFound();
          }
            return await _context.Tags.ToListAsync();
        }

        // GET: api/Tag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagEntity>> GetTagEntity(int id)
        {
          if (_context.Tags == null)
          {
              return NotFound();
          }
            var tagEntity = await _context.Tags.FindAsync(id);

            if (tagEntity == null)
            {
                return NotFound();
            }

            return tagEntity;
        }

        // PUT: api/Tag/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTagEntity(int id, TagEntity tagEntity)
        {
            if (id != tagEntity.TagEntityId)
            {
                return BadRequest();
            }

            _context.Entry(tagEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagEntityExists(id))
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

        // POST: api/Tag
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TagEntity>> PostTagEntity(TagEntity tagEntity)
        {
          if (_context.Tags == null)
          {
              return Problem("Entity set 'AppDbContext.Tags'  is null.");
          }
            _context.Tags.Add(tagEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTagEntity", new { id = tagEntity.TagEntityId }, tagEntity);
        }

        // DELETE: api/Tag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTagEntity(int id)
        {
            if (_context.Tags == null)
            {
                return NotFound();
            }
            var tagEntity = await _context.Tags.FindAsync(id);
            if (tagEntity == null)
            {
                return NotFound();
            }

            _context.Tags.Remove(tagEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagEntityExists(int id)
        {
            return (_context.Tags?.Any(e => e.TagEntityId == id)).GetValueOrDefault();
        }
    }
}
