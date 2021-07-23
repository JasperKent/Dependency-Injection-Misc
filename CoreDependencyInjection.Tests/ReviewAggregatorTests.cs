using CoreDependencyInjection.Data;
using CoreDependencyInjection.Services;
using Moq;
using Xunit;

namespace CoreDependencyInjection.Tests
{
    public class ReviewAggregatorTests
    {
        [Fact]
        public void Summary()
        {
            Mock<IBookReviewRepository> mockRepository = new();

            mockRepository.SetupGet(r => r.All).Returns(new[] {
                new BookReview { Title = "book-1", Rating = 4 },
                new BookReview { Title = "book-1", Rating = 3 },
                new BookReview { Title = "book-2", Rating = 2 },
                new BookReview { Title = "book-2", Rating = 1 }
            });

            ReviewAggregator aggregator = new(mockRepository.Object);

            var summary = aggregator.Summary;

            Assert.Collection(summary,
               r =>
               {
                   Assert.Equal("book-1", r.Title);
                   Assert.Equal(3.5, r.Rating);
               },
               r =>
               {
                   Assert.Equal("book-2", r.Title);
                   Assert.Equal(1.5, r.Rating);
               }
           );
        }
    }
}
