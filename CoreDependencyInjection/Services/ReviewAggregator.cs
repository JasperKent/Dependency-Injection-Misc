using CoreDependencyInjection.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreDependencyInjection.Services
{
    public class ReviewAggregator : IReviewAggregator
    {
        private readonly IBookReviewRepository _repository;

        public ReviewAggregator(IBookReviewRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<BookReview> Summary
            => _repository.All.GroupBy(r => r.Title).Select(g =>
                new BookReview
                {
                    Title = g.Key,
                    Rating = Math.Round (g.Average(r => r.Rating),2)
                }
            );
    }
}
