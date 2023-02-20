using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
    public class FeedController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FeedController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Feed
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<FeedEntity>>> GetFeeds(int pageIndex = 0)
        {
            var AllFeeds = _context.Feeds;
            var pageSize = AllFeeds.Count() / 20 >100 ? 30 : 20;

            if (_context.Feeds == null)
            {
                return NotFound();
            }
            var result = await _context.Feeds
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .OrderBy(t => t.CreateAt)
                .ToListAsync();
            return result;
        }

        // GET: api/Feed
        [HttpGet("feeds-by-tag")]
        public async Task<ActionResult<IEnumerable<FeedEntity>>> GetFeedsByTagId(int TagId)
        {
            if (_context.Feeds == null)
            {
                return NotFound();
            }
            var result = await _context.Feeds
                .Where(x => x.FeedTags.Any(y => y.TagEntityId == TagId))
                .OrderBy(t => t.CreateAt)
                .ToListAsync();
            return result;
        }


        // GET: api/Feed/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedEntity>> GetFeedEntity(int id)
        {
          if (_context.Feeds == null)
          {
              return NotFound();
          }
            var feedEntity = await _context.Feeds.FindAsync(id);

            if (feedEntity == null)
            {
                return NotFound();
            }

            return feedEntity;
        }

        // PUT: api/Feed/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedEntity(int id, FeedEntity feedEntity)
        {
            if (id != feedEntity.FeedEntityId)
            {
                return BadRequest();
            }

            _context.Entry(feedEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedEntityExists(id))
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

        // POST: api/Feed
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FeedEntity>> PostFeedEntity(FeedEntity feedEntity)
        {
          if (_context.Feeds == null)
          {
              return Problem("Entity set 'AppDbContext.Feeds'  is null.");
          }
            _context.Feeds.Add(feedEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedEntity", new { id = feedEntity.FeedEntityId }, feedEntity);
        }

        // DELETE: api/Feed/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedEntity(int id)
        {
            if (_context.Feeds == null)
            {
                return NotFound();
            }
            var feedEntity = await _context.Feeds.FindAsync(id);
            if (feedEntity == null)
            {
                return NotFound();
            }

            _context.Feeds.Remove(feedEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeedEntityExists(int id)
        {
            return (_context.Feeds?.Any(e => e.FeedEntityId == id)).GetValueOrDefault();
        }
    }
}
