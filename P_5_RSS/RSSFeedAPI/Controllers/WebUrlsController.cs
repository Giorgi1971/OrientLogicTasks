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
    public class WebUrlsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WebUrlsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/WebUrls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WebSiteEntity>>> GetUrls()
        {
          if (_context.Urls == null)
          {
              return NotFound();
          }
            return await _context.Urls.ToListAsync();
        }

        // GET: api/WebUrls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WebSiteEntity>> GetWebSiteEntity(int id)
        {
          if (_context.Urls == null)
          {
              return NotFound();
          }
            var webSiteEntity = await _context.Urls.FindAsync(id);

            if (webSiteEntity == null)
            {
                return NotFound();
            }

            return webSiteEntity;
        }

        // PUT: api/WebUrls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWebSiteEntity(int id, WebSiteEntity webSiteEntity)
        {
            if (id != webSiteEntity.WebSiteEntityId)
            {
                return BadRequest();
            }

            _context.Entry(webSiteEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebSiteEntityExists(id))
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

        // POST: api/WebUrls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WebSiteEntity>> PostWebSiteEntity(WebSiteEntity webSiteEntity)
        {
          if (_context.Urls == null)
          {
              return Problem("Entity set 'AppDbContext.Urls'  is null.");
          }
            _context.Urls.Add(webSiteEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWebSiteEntity", new { id = webSiteEntity.WebSiteEntityId }, webSiteEntity);
        }

        // DELETE: api/WebUrls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebSiteEntity(int id)
        {
            if (_context.Urls == null)
            {
                return NotFound();
            }
            var webSiteEntity = await _context.Urls.FindAsync(id);
            if (webSiteEntity == null)
            {
                return NotFound();
            }

            _context.Urls.Remove(webSiteEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WebSiteEntityExists(int id)
        {
            return (_context.Urls?.Any(e => e.WebSiteEntityId == id)).GetValueOrDefault();
        }
    }
}
