using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    private readonly DB _db;

    public SearchController(DB db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems(string searchTerm)
    {
        var items = _db.Find<Item>();
        items.Sort(x => x.Ascending(a => a.Make));
        if (!string.IsNullOrEmpty(searchTerm))
        {
            items.Match(Search.Full, searchTerm).SortByTextScore();
        }
        var result = await items.ExecuteAsync();
        return result;
    }
}