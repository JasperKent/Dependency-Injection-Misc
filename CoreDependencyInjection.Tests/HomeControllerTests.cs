using CoreDependencyInjection.Controllers;
using CoreDependencyInjection.Data;
using CoreDependencyInjection.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CoreDependencyInjection.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index()
        {
            Mock<IBookReviewRepository> mockRepository = new();
            Mock<IReviewAggregator> mockAggregator = new();

            mockRepository.SetupGet(r => r.All).Returns(new[] { new BookReview { Title = "book-1", Rating = 4 }, new BookReview { Title = "book-2", Rating = 3 } });

            HomeController controller = new HomeController(mockRepository.Object, mockAggregator.Object);

            var result = controller.Index() as ViewResult;

            var model = result.Model as IEnumerable<BookReview>;

            Assert.Collection(model,
                r =>
                {
                    Assert.Equal("book-1", r.Title);
                    Assert.Equal(4.0, r.Rating);
                },
                r =>
                {
                    Assert.Equal("book-2", r.Title);
                    Assert.Equal(3.0, r.Rating);
                }
            );
        }

        [Fact]
        public void Summary_Shallow()
        {
            Mock<IBookReviewRepository> mockRepository = new();
            Mock<IReviewAggregator> mockAggregator = new();

            mockAggregator.SetupGet(r => r.Summary).Returns(new[] { new BookReview { Title = "book-1", Rating = 4 }, new BookReview { Title = "book-2", Rating = 3 } });

            HomeController controller = new HomeController(mockRepository.Object, mockAggregator.Object);

            var result = controller.Summary() as ViewResult;

            var model = result.Model as IEnumerable<BookReview>;

            Assert.Collection(model,
                r =>
                {
                    Assert.Equal("book-1", r.Title);
                    Assert.Equal(4.0, r.Rating);
                },
                r =>
                {
                    Assert.Equal("book-2", r.Title);
                    Assert.Equal(3.0, r.Rating);
                }
            );
        }

        [Fact]
        public void Summary_Deep()
        {
            Mock<IBookReviewRepository> mockRepository = new();

            mockRepository.SetupGet(r => r.All).Returns(new[] {
                new BookReview { Title = "book-1", Rating = 4 },
                new BookReview { Title = "book-1", Rating = 3 },
                new BookReview { Title = "book-2", Rating = 2 },
                new BookReview { Title = "book-2", Rating = 1 }
            });

            HomeController controller = new HomeController(mockRepository.Object, new ReviewAggregator(mockRepository.Object));

            var result = controller.Summary() as ViewResult;

            var model = result.Model as IEnumerable<BookReview>;

            Assert.Collection(model,
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
