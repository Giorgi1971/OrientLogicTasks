using T09_API_BookLibrary.Models;
using T09_API_BookLibrary.Services;
using T09_API_BookLibrary.Requests;
using Microsoft.AspNetCore.Mvc;

namespace T09_API_BookLibrary.Controllers;

[ApiController]
[Route("[controller]")]

public class ShelfController : ControllerBase
{
    private readonly ShelfService _shelfService;

    public ShelfController()
    {
        _shelfService = new ShelfService();
    }

    [HttpGet]
    [Route("get/{shelfId}")]
    public Shelf? Get(int shelfId)
    {
        return _shelfService.Get(shelfId);
    }

    [HttpGet("shelves")]
    public List<Shelf> GetAll()
    {
        return _shelfService.GetAll();
    }

    [HttpPost("create")]
    public Shelf Create(CreateShelfRequest request)
    {
        return _shelfService.Create(request);
    }

    [HttpPut("rename")]
    public Shelf? Rename(RenameShelfRequest request)
    {
        return _shelfService.Rename(request);
    }

    [HttpDelete("Delete")]
    public string Delete(int Id)
    {
        return _shelfService.Delete(Id);
    }

    [HttpPost("createBook")]
    public Book? AddToShelf(BookCreateInShelf request)
    {
        return _shelfService.AddToShelf(request);
    }

    [HttpDelete("DeleteBook")]
    public Shelf? Remove(int Id)
    {
        return _shelfService.RemoveBook(Id);
    }

    [HttpPut("ChangeBookShelf")]
    public Shelf? MoveTo(MoveToShelfRequest request)
    {
        return _shelfService.MoveTo(request);
    }

    [HttpGet]
    [Route("book/{bookId}")]
    public Book? GetBook(int bookId)
    {
        return _shelfService.GetBook(bookId);
    }
}

