using MovieDb.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Http.Results;
using WebAPI.Controllers;
using WebAPI.Test.TestObjects;

namespace WebAPI.Tests.CommentsControllerTests
{
    [TestFixture]
    public class GetAllComments
    {
        [Test]
        public void GetAllCommentsForAMovie_ShouldThrowBadReqiestMessage_WhenInvalidIDIsPassed()
        {
            var badReqMessage = "such movie";

            var userRepo = TestObjectFactory.GetUserRepository();
            var movieRepo = TestObjectFactory.GetMovieRepository();
            var commentRepo = TestObjectFactory.GetCommentRepository();

            var controller = new CommentsController(commentRepo, userRepo, movieRepo);

            var result = controller.GetAllCommentsForAMovie(2000);

            var expected = result as BadRequestErrorMessageResult;

            Assert.IsTrue(expected.Message.ToLower().Contains(badReqMessage.ToLower()));
        }

        [Test]
        public void GetAllCommentsForAMovie_ShouldReturnOkWithListWithAllCommentsForAMovie_WhenValidArgsArePassed()
        {        
            var userRepo = TestObjectFactory.GetUserRepository();
            var movieRepo = TestObjectFactory.GetMovieRepository();
            var commentRepo = TestObjectFactory.GetCommentRepository();

            var controller = new CommentsController(commentRepo, userRepo, movieRepo);

            var result = controller.GetAllCommentsForAMovie(1);

            var expected = result as OkNegotiatedContentResult<List<Comment>>;

            Assert.IsNotNull(expected);
       
        }

        [Test]
        public void GetAllCommentsFromAUser_ShouldThrowBadRequest_WhenInvalidParametersArepassed()
        {
            var badReqMessage = "such user";

            var userRepo = TestObjectFactory.GetUserRepository();
            var movieRepo = TestObjectFactory.GetMovieRepository();
            var commentRepo = TestObjectFactory.GetCommentRepository();

            var controller = new CommentsController(commentRepo, userRepo, movieRepo);

            var result = controller.GetAllCommentsFromAUser(88888);

            var expected = result as BadRequestErrorMessageResult;

            Assert.IsTrue(expected.Message.ToLower().Contains(badReqMessage.ToLower()));
        }

        [Test]
        public void GetAllCommentsFromAUser_ShouldReturnOk_WithAListOfComments_WhenParametersAreValid()
        {    
            var userRepo = TestObjectFactory.GetUserRepository();
            var movieRepo = TestObjectFactory.GetMovieRepository();
            var commentRepo = TestObjectFactory.GetCommentRepository();

            var controller = new CommentsController(commentRepo, userRepo, movieRepo);

            var result = controller.GetAllCommentsFromAUser(1);

            var expected = result as OkNegotiatedContentResult<List<Comment>>; ;

            Assert.IsNotNull(expected);
        }
    }
}
