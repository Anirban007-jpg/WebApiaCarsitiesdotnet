using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.RequestHelpers;

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
    public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParams searchParams)
    {
        var items = _db.PagedSearch<Item, Item>();


        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            items.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
        }
        items = searchParams.OrderBy switch
        {
            "make" => items.Sort(x => x.Ascending(a => a.Make)),
            "new" => items.Sort(x => x.Descending(a => a.CreatedAt)),
            _ => items.Sort(x => x.Ascending(a => a.AuctionEnd)),
        };
        items = searchParams.FilterBy switch
        {
            "finished" => items.Match(x => x.AuctionEnd < DateTime.UtcNow),
            "endingSoon" => items.Match(x => x.AuctionEnd > DateTime.UtcNow && x.AuctionEnd < DateTime.UtcNow),
            _ => items.Match(x => x.AuctionEnd > DateTime.UtcNow),
        };

        if (!string.IsNullOrEmpty(searchParams.Seller))
        {
            items.Match(x => x.Seller == searchParams.Seller);
        }


        if (!string.IsNullOrEmpty(searchParams.Winner))
        {
            items.Match(x => x.Winner == searchParams.Winner);
        }

        items.PageNumber(searchParams.Page);
        items.PageSize(searchParams.PageSize);
        var result = await items.ExecuteAsync();
        return Ok(new { results = result.Results, pageCount = result.PageCount, totalCount = result.TotalCount });
    }
}