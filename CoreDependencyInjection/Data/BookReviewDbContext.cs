using Microsoft.EntityFrameworkCore;
using System;

namespace CoreDependencyInjection.Data
{
    public class BookReviewDbContext : DbContext
    {
        public Guid Id { get; } = Guid.NewGuid();

        public BookReviewDbContext(DbContextOptions<BookReviewDbContext> options)
            : base(options)
        {
        }

        public DbSet<BookReview> BookReviews { get; set; }
    }
}
