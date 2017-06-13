using MovieDb.Data;
using MovieDb.Models;
using NUnit.Framework;
using System.Linq;
using System.Web.Http.Results;
using WebAPI.Controllers;
using WebAPI.Models.Movies;
using WebAPI.Test.TestObjects;

namespace WebAPI.Tests.MovieControllerTests
{
    [TestFixture]
    public class Add
    {
        [Test]
        public void Add_ShouldReturnBadRequestUserAlreadyExist_WhenThereIsSuchUserInDatabase()
        {
            const string AlreadyExists = "Already Exists";
            const string TestMovieName = "Movie 1";

            var userRepo = TestObjectFactory.GetUserRepository();
            var likesRepo = TestObjectFactory.GetLikesRepository();
            var dislikesRepo = TestObjectFactory.GetDislikesRepository();
            var moviesRepo = TestObjectFactory.GetMovieRepository();
            var moviesLiteRepo = TestObjectFactory.GetMoviesLiteRepository();

            var controller = new MoviesController(moviesRepo, likesRepo, dislikesRepo,
                userRepo, moviesLiteRepo);

            var movie = new MoviesCreateModel()
            {
                Name = TestMovieName
            };

            var result = controller.Add(movie);

            var expected = result as BadRequestErrorMessageResult;

            Assert.IsTrue(expected.Message.ToLower().Contains(AlreadyExists.ToLower()));
        }

        [Test]
        public void Add_ShouldReturnOkResultAndCorrectlyCreateNewMovie()
        {
            const string TestMovieName = "Movie to add";

            var userRepo = TestObjectFactory.GetUserRepository();
            var likesRepo = TestObjectFactory.GetLikesRepository();
            var dislikesRepo = TestObjectFactory.GetDislikesRepository();
            var moviesRepo = TestObjectFactory.GetMovieRepository();
            var moviesLiteRepo = TestObjectFactory.GetMoviesLiteRepository();

            var controller = new MoviesController(moviesRepo, likesRepo, dislikesRepo,
                userRepo,moviesLiteRepo);

            var movie = new MoviesCreateModel()
            {
                Name = TestMovieName
            };

            var result = controller.Add(movie);

            var expected = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(expected.Content);

            var addedMovie = moviesRepo.All.Where(x => x.Name == TestMovieName);

            Assert.IsNotNull(addedMovie);
        }

        [Test]
        public void Add_ShouldReturnBadRequestWhenInvalidMovieIsAdded()
        {
            const string InvalidMovieMessage = "Invalid movie";
            const string TestMovieName = null;

            var userRepo = TestObjectFactory.GetUserRepository();
            var likesRepo = TestObjectFactory.GetLikesRepository();
            var dislikesRepo = TestObjectFactory.GetDislikesRepository();
            var moviesLiteRepo = TestObjectFactory.GetMoviesLiteRepository();

            var dbContext = new MoviesContext();

            var movieRepositoryReal = new EfGenericRepository<Movie>(dbContext);

            var controller = new MoviesController(movieRepositoryReal, likesRepo,
                dislikesRepo, userRepo, moviesLiteRepo);

            var movie = new MoviesCreateModel()
            {
                Name = TestMovieName
            };

            var result = controller.Add(movie);

            var expected = result as BadRequestErrorMessageResult;

            Assert.IsTrue(expected.Message.ToLower().Contains(InvalidMovieMessage.ToLower()));
        }

        [Test]
        public void Add_ShouldCallSaveChanges()
        {
            const string TestMovieName = "Movie to add";

            var userRepo = TestObjectFactory.GetUserRepository();
            var likesRepo = TestObjectFactory.GetLikesRepository();
            var dislikesRepo = TestObjectFactory.GetDislikesRepository();
            var moviesRepo = TestObjectFactory.GetMovieRepository();
            var moviesLiteRepo = TestObjectFactory.GetMoviesLiteRepository();

            var controller = new MoviesController(moviesRepo, likesRepo, dislikesRepo,
                userRepo,moviesLiteRepo);

            var movie = new MoviesCreateModel()
            {
                Name = TestMovieName
            };

            var result = controller.Add(movie);

            Assert.AreEqual(1, moviesRepo.NumberOfSavedChanges);
        }
    }
}
