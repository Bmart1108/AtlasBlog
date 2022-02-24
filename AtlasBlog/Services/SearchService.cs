
using AtlasBlog.Data;
using AtlasBlog.Models;

namespace AtlasBlog.Services
{
    public class SearchService
    {
        private readonly ApplicationDbContext _dbContext;

        public SearchService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IOrderedQueryable<BlogPost> TermSearch(string? searchTerm)
        {
            searchTerm = searchTerm?.ToLower();

            var resultSet = _dbContext
                .BlogPosts
                .Where(b => b.BlogPostState == Enums.BlogPostState.ProductionReady && !b.IsDeleted).AsQueryable();

            //If they supplied a search term, I will look for it inside the resultSet
            if (!string.IsNullOrEmpty(searchTerm))
            {
                resultSet = resultSet.Where(bp =>
                    bp.Title.ToLower().Contains(searchTerm) ||
                    bp.Abstract.ToLower().Contains(searchTerm) ||
                    bp.Body.ToLower().Contains(searchTerm) ||
                    bp.Comments.Any(c =>
                        c.CommentBody.ToLower().Contains(searchTerm) ||
                        c.ModeratedBody!.ToLower().Contains(searchTerm) ||
                        c.Author!.FirstName.ToLower().Contains(searchTerm) ||
                        c.Author.LastName.ToLower().Contains(searchTerm) ||
                        c.Author.Email.ToLower().Contains(searchTerm)));
            }

            return resultSet.OrderByDescending(r => r.Created);
        }
    }
}