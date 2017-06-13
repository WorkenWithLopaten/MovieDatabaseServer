using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Http.Results;
using WebAPI.Controllers;
using WebAPI.Test.TestObjects;

namespace WebAPI.Tests.MovieControllerTests
{
    [TestFixture]
    public class GetTopLikedMovies
    {
        [Test]
        public void GetTopLikedMovies_ShouldReturnCorrectNumberOfFIlmsWitchAreLiked()
        {
            var userRepo = TestObjectFactory.GetUserRepository();
            var likesRepo = TestObjectFactory.GetLikesRepository();
            var dislikesRepo = TestObjectFactory.GetDislikesRepository();
            var moviesRepo = TestObjectFactory.GetMovieRepository();
            var moviesLiteRepo = TestObjectFactory.GetMoviesLiteRepository();

            var controller = new MoviesController(moviesRepo, likesRepo, dislikesRepo,
                userRepo, moviesLiteRepo);

            var result = controller.GetTopLikedMovies(3);

            var expected = result as OkNegotiatedContentResult<IEnumerable<TestingMovieModel>>;

            var trueCount = 0;
            var countEl = 0;

            foreach (var movie in expected.Content)
            {
                countEl++;

                if(movie.Likes == 9 || movie.Likes == 8 || movie.Likes == 7)
                {
                    trueCount++;
                }
            }

            Assert.IsTrue(trueCount == 3);

            Assert.IsTrue(countEl == 3);
        }

        [Test]
        public void GetTopLikedMovies_ShouldBeOrderedByDescenging()
        {
            var userRepo = TestObjectFactory.GetUserRepository();
            var likesRepo = TestObjectFactory.GetLikesRepository();
            var dislikesRepo = TestObjectFactory.GetDislikesRepository();
            var moviesRepo = TestObjectFactory.GetMovieRepository();
            var moviesLiteRepo = TestObjectFactory.GetMoviesLiteRepository();

            var controller = new MoviesController(moviesRepo, likesRepo, dislikesRepo,
                userRepo, moviesLiteRepo);

            var result = controller.GetTopLikedMovies(10);

            var expected = result as OkNegotiatedContentResult<IEnumerable<TestingMovieModel>>;

            TestingMovieModel firstMovie = null;
            TestingMovieModel secondMovie = null;

            foreach (var movie in expected.Content)
            {
                if(movie.Likes == 9)
                {
                    firstMovie = movie;
                    break;
                }         
            }

            Assert.IsNull(secondMovie);
        }


    }
}
