using Moq;
using MovieDb.Data;
using MovieDb.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using WebAPI.Controllers;
using WebAPI.Models.Comments;
using WebAPI.Test.TestObjects;

namespace WebAPI.Tests.CommentsControllerTests
{
    [TestFixture]
    public class CreateComment
    {
        [Test]
        public void CreateComment_ShouldThrowBadRequestMessage_WhenCommentTextIsNullOrDoNotExist()
        {
            var exMessage = "can`t be empty";
            var userRepo = TestObjectFactory.GetUserRepository();
            var movieRepo = TestObjectFactory.GetMovieRepository();
            var commentRepo = TestObjectFactory.GetCommentRepository();

            var controller = new CommentsController(commentRepo, userRepo, movieRepo);

            var commentModel = new CreateCommentModel
            {
                Text = "",
                UserId = 1,
                MovieId = 1
            };

            var result = controller.CreateComment(commentModel);

            var expected = result as BadRequestErrorMessageResult;

            Assert.IsTrue(expected.Message.ToLower().Contains(exMessage.ToLower()));
        }

        [Test]
        public void CreateComment_ShouldThrowBadRequestMessage_WhenCommentUserIDIsNullOrDoNotExist()
        {
            var exMessage = "such user";
            var userRepo = TestObjectFactory.GetUserRepository();
            var movieRepo = TestObjectFactory.GetMovieRepository();
            var commentRepo = TestObjectFactory.GetCommentRepository();

            var controller = new CommentsController(commentRepo, userRepo, movieRepo);

            var commentModel = new CreateCommentModel
            {
                Text = "Comment",
                UserId = 30000,
                MovieId = 1
            };

            var result = controller.CreateComment(commentModel);

            var expected = result as BadRequestErrorMessageResult;

            Assert.IsTrue(expected.Message.ToLower().Contains(exMessage.ToLower()));
        }

        [Test]
        public void CreateComment_ShouldThrowBadRequestMessage_WhenCommentMovieIdIsNullOrDoNotExist()
        {
            var exMessage = "such movie";
            var userRepo = TestObjectFactory.GetUserRepository();
            var movieRepo = TestObjectFactory.GetMovieRepository();
            var commentRepo = TestObjectFactory.GetCommentRepository();

            var controller = new CommentsController(commentRepo, userRepo, movieRepo);

            var commentModel = new CreateCommentModel
            {
                Text = "Comment",
                UserId = 1,
                MovieId = 6000
            };

            var result = controller.CreateComment(commentModel);

            var expected = result as BadRequestErrorMessageResult;

            Assert.IsTrue(expected.Message.ToLower().Contains(exMessage.ToLower()));
        }

        [Test]
        public void CreateComment_ShouldReturnOkWithCommentID_WhenArgumentsAreValid()
        {
            var userRepo = TestObjectFactory.GetUserRepository();
            var movieRepo = TestObjectFactory.GetMovieRepository();
            var commentRepo = TestObjectFactory.GetCommentRepository();

            var controller = new CommentsController(commentRepo, userRepo, movieRepo);

            var commentModel = new CreateCommentModel
            {
                Text = "Text 1",
                UserId = 1,
                MovieId = 1              
            };

            var result = controller.CreateComment(commentModel);

            var expected = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(expected);
        }

        [Test]
        public void CreateComment_ShouldCallSaveChanges()
        {
            var userRepo = TestObjectFactory.GetUserRepository();
            var movieRepo = TestObjectFactory.GetMovieRepository();
            var commentRepo = TestObjectFactory.GetCommentRepository();

            var controller = new CommentsController(commentRepo, userRepo, movieRepo);

            var commentModel = new CreateCommentModel
            {
                Text = "Text 1",
                UserId = 1,
                MovieId = 1
            };

            var result = controller.CreateComment(commentModel);

            var expected = result as OkNegotiatedContentResult<int>;

            Assert.AreEqual( 1, commentRepo.NumberOfSavedChanges);
        }


        [Test]
        public void CreateComment_ShouldCallAddMethod()
        {
            var userRepo = TestObjectFactory.GetUserRepository();
            var movieRepo = TestObjectFactory.GetMovieRepository();
            var commentRepoMocked = new Mock<IRepository<Comment>>();

            var comment = new Comment()
            {
                CommentText = "Text 1",
                UserId = 1,
                MovieId = 1
            };

            var resultOfAll = new List<Comment>();
            resultOfAll.Add(comment);

            commentRepoMocked.Setup(x => x.All).Returns(resultOfAll.AsQueryable());

            var controller = new CommentsController(commentRepoMocked.Object, 
                userRepo, movieRepo);


            var commentModel = new CreateCommentModel
            {
                Text = "Text 1",
                UserId = 1,
                MovieId = 1
            };

            var result = controller.CreateComment(commentModel);

            commentRepoMocked.Verify(x => x.Add(It.IsAny<Comment>()), Times.Once);
        }
    }
}
