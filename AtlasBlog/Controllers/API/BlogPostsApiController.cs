#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AtlasBlog.Data;
using AtlasBlog.Models;
using AtlasBlog.Enums;

namespace AtlasBlog.Controllers.API
{
    [Route("Home/Index")]
    [ApiController]
    public class BlogPostsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlogPostsApiController(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Returns the specified number of lastest Posts
        /// </summary>
        /// <remarks></remarks>
        /// <param name="num">Integer count of records</param>
        /// <returns>Returns a list of Blog Posts</returns>
        [HttpGet("GetTopXPosts/{num:int}")]
        public async Task<ActionResult<IEnumerable<BlogPost>>>GetTopXPosts(int num)
        {
            //How would I return the top X lastest production ready Posts that arent deleted?
            //And the latest posts
            var posts = await _context.BlogPosts
                        .Where(b => !b.IsDeleted &&
                               b.BlogPostState == BlogPostState.ProductionReady)
                        .OrderByDescending(b => b.Created)
                        .Take(num)
                        .ToListAsync();

            return posts;        
        }         
    }
}
