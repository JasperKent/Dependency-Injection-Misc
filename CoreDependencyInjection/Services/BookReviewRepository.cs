using CoreDependencyInjection.Data;
using System.Collections.Generic;
using System.Linq;

namespace CoreDependencyInjection.Services
{
    public class BookReviewRepository : IBookReviewRepository
    {
        private readonly BookReviewDbContext _dbContext;

        public BookReviewRepository(BookReviewDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<BookReview> All => _dbContext.BookReviews;
        public IEnumerable<BookReview> ByTitle(string title) => _dbContext.BookReviews.Where(r => r.Title == title);
    }
}
