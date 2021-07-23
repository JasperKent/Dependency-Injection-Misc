using CoreDependencyInjection.Data;
using System.Collections.Generic;

namespace CoreDependencyInjection.Services
{
    public interface IBookReviewRepository
    {
        IEnumerable<BookReview> All { get; }
        IEnumerable<BookReview> ByTitle(string title);
    }
}
