using CoreDependencyInjection.Data;
using System.Collections.Generic;

namespace CoreDependencyInjection.Services
{
    public interface IReviewAggregator
    {
        IEnumerable<BookReview> Summary { get; }
    }
}
