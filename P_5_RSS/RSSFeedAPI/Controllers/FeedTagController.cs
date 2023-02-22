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
    public class FeedTagController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FeedTagController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/FeedTag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedTag>>> GetFeedTag()
        {
          if (_context.FeedTag == null)
          {
              return NotFound();
          }
            return await _context.FeedTag.ToListAsync();
        }

        // GET: api/FeedTag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedTag>> GetFeedTag(int id)
        {
          if (_context.FeedTag == null)
          {
              return NotFound();
          }
            var feedTag = await _context.FeedTag.FindAsync(id);

            if (feedTag == null)
            {
                return NotFound();
            }

            return feedTag;
        }

        // PUT: api/FeedTag/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedTag(int id, FeedTag feedTag)
        {
            if (id != feedTag.FeedEntityId)
            {
                return BadRequest();
            }

            _context.Entry(feedTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedTagExists(id))
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

        // POST: api/FeedTag
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FeedTag>> PostFeedTag(FeedTag feedTag)
        {
          if (_context.FeedTag == null)
          {
              return Problem("Entity set 'AppDbContext.FeedTag'  is null.");
          }
            _context.FeedTag.Add(feedTag);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FeedTagExists(feedTag.FeedEntityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFeedTag", new { id = feedTag.FeedEntityId }, feedTag);
        }

        // DELETE: api/FeedTag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedTag(int id)
        {
            if (_context.FeedTag == null)
            {
                return NotFound();
            }
            var feedTag = await _context.FeedTag.FindAsync(id);
            if (feedTag == null)
            {
                return NotFound();
            }

            _context.FeedTag.Remove(feedTag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeedTagExists(int id)
        {
            return (_context.FeedTag?.Any(e => e.FeedEntityId == id)).GetValueOrDefault();
        }
    }
}
