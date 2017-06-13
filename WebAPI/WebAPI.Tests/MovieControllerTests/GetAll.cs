using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Http.Results;
using WebAPI.Controllers;
using WebAPI.Test.TestObjects;

namespace WebAPI.Tests.MovieControllerTests
{
    [TestFixture]
    public class GetAll
    {
        [Test]
        public void GetAll_ShoulReturn_CorrectOKResult()
        {
            var userRepo = TestObjectFactory.GetUserRepository();
            var likesRepo = TestObjectFactory.GetLikesRepository();
            var dislikesRepo = TestObjectFactory.GetDislikesRepository();
            var moviesRepo = TestObjectFactory.GetMovieRepository();
            var moviesLiteRepo = TestObjectFactory.GetMoviesLiteRepository();

            var controller = new MoviesController(moviesRepo, likesRepo, dislikesRepo,
                userRepo, moviesLiteRepo);

            var result = controller.GetAll();

            var expected = result as OkNegotiatedContentResult<IEnumerable<TestingMovieModel>>;

            Assert.IsNotNull(expected.Content);          
        }

        [Test]
        public void GetAll_ShoulReturn_CorrectResultWithCorrectParameters()
        {
            var userRepo = TestObjectFactory.GetUserRepository();
            var likesRepo = TestObjectFactory.GetLikesRepository();
            var dislikesRepo = TestObjectFactory.GetDislikesRepository();
            var moviesRepo = TestObjectFactory.GetMovieRepository();
            var moviesLiteRepo = TestObjectFactory.GetMoviesLiteRepository();

            var controller = new MoviesController(moviesRepo, likesRepo, dislikesRepo,
                userRepo, moviesLiteRepo);

            var expected = controller.GetAll();

            var result = expected as OkNegotiatedContentResult<IEnumerable<TestingMovieModel>>;

            var expectedResult = new TestingMovieModel()
            {
                Id = 1,
                Likes = 0,
                Dislikes = 0,
                Name = "Movie 1"
            };

            var containsExpected = false;

            foreach  (var movie in result.Content)
            {
                if(movie.Id == expectedResult.Id && movie.Name == expectedResult.Name)
                {
                    containsExpected = true;
                }
            }

            Assert.IsTrue(containsExpected);
        }
    }
}
