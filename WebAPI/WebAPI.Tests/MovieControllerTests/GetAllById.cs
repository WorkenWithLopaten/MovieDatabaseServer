using MovieDb.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Http.Results;
using WebAPI.Controllers;
using WebAPI.Test.TestObjects;

namespace WebAPI.Tests.MovieControllerTests
{
    [TestFixture]
    public class GetAllById
    {
        [Test]
        public void GetAllById_ShouldReturnCorrectResult()
        {
            var userRepo = TestObjectFactory.GetUserRepository();
            var likesRepo = TestObjectFactory.GetLikesRepository();
            var dislikesRepo = TestObjectFactory.GetDislikesRepository();
            var moviesRepo = TestObjectFactory.GetMovieRepository();
            var moviesLiteRepo = TestObjectFactory.GetMoviesLiteRepository();

            var controller = new MoviesController(moviesRepo, likesRepo, dislikesRepo,
                userRepo, moviesLiteRepo);

            var result = controller.GetById(0);

            var expected = result as OkNegotiatedContentResult<Movie>;

            Assert.IsNotNull(expected.Content);

            Assert.AreEqual(0, expected.Content.Id);
        }
    }
}
