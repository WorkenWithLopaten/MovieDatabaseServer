using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Http.Results;
using WebAPI.Controllers;
using WebAPI.Test.TestObjects;

namespace WebAPI.Tests.MovieControllerTests
{
    [TestFixture]
    public class GetTopDislikedMovies
    {
        [Test]
        public void GetTopDislikedMovies_ShouldReturnCorrectNumberOfFIlmsWitchAreLiked()
        {
            var userRepo = TestObjectFactory.GetUserRepository();
            var likesRepo = TestObjectFactory.GetLikesRepository();
            var dislikesRepo = TestObjectFactory.GetDislikesRepository();
            var moviesRepo = TestObjectFactory.GetMovieRepository();
            var moviesLiteRepo = TestObjectFactory.GetMoviesLiteRepository();

            var controller = new MoviesController(moviesRepo, likesRepo, dislikesRepo,
                userRepo, moviesLiteRepo);

            var result = controller.GetTopDisLikedMovies(3);

            var expected = result as OkNegotiatedContentResult<IEnumerable<int>>;

            var countEl = 0;

            foreach (var movie in expected.Content)
            {
                countEl++;
            }

            Assert.IsTrue(countEl == 3);
        }

        [Test]
        public void GetTopDislikedMovies_ShouldBeOrderedByDescenging()
        {
            var userRepo = TestObjectFactory.GetUserRepository();
            var likesRepo = TestObjectFactory.GetLikesRepository();
            var dislikesRepo = TestObjectFactory.GetDislikesRepository();
            var moviesRepo = TestObjectFactory.GetMovieRepository();
            var moviesLiteRepo = TestObjectFactory.GetMoviesLiteRepository();

            var controller = new MoviesController(moviesRepo, likesRepo, dislikesRepo,
                userRepo, moviesLiteRepo);

            var result = controller.GetTopDisLikedMovies(10);

            var expected = result as OkNegotiatedContentResult<IEnumerable<int>>;

            int? firstMovie = null;
            int? secondMovie = null;

            foreach (var movieId in expected.Content)
            {
                if (movieId == 9)
                {
                    firstMovie = movieId;
                    break;
                }
            }

            Assert.IsNull(secondMovie);
        }
    }
}
