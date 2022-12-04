using T09_API_BookLibrary.Models;
using T09_API_BookLibrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace T09_API_BookLibrary.Controllers;

[ApiController]
[Route("[controller]")]
public class ShelfController : ControllerBase
{
    public ShelfController()
    {
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Shelf>> GetAll() => ShelfService.GetAll();

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Shelf> Get(int id)
    {
        var shelf = ShelfService.Get(id);

        if (shelf == null)
            return NotFound();

        return shelf;
    }

    // POST action
    [HttpPost]
    public IActionResult Create(Shelf shelf)
    {
        ShelfService.Add(shelf);
        return CreatedAtAction(nameof(Create), new { id = shelf.Id }, shelf);
    }

    // PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, Shelf shelf)
    {
        if (id != shelf.Id)
            return BadRequest();

        var existingShelf = ShelfService.Get(id);
        if (existingShelf is null)
            return NotFound();

        ShelfService.Update(shelf);

        return NoContent();
    }
    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var shelf = ShelfService.Get(id);

        if (shelf is null)
            return NotFound();

        ShelfService.Delete(id);

        return NoContent();
    }
}
