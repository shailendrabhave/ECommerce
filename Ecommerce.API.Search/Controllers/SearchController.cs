﻿using Ecommerce.API.Search.Interfaces;
using Ecommerce.API.Search.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm term)
        {
            var result = await _searchService.SearchAsync(term.CustomerId);
            if (result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }
            return NoContent(); 
        }
    }
}
