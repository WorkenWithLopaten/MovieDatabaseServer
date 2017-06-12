using NUnit.Framework;
using System.Web.Http.Results;
using WebAPI.Controllers;
using WebAPI.Models.Comments;
using WebAPI.Test.TestObjects;

namespace WebAPI.Tests.CommentsControllerTests
{
    [TestFixture]
    public class DeleteComment
    {
        [Test]
        public void DeleteComment_ShouldThrowBadRequest_WhenParametersAreInvalidOrDoesNotExistAtAll()
        {
            var badReqMessage = "deleted or doesn`t exist";
            var userRepo = TestObjectFactory.GetUserRepository();
            var movieRepo = TestObjectFactory.GetMovieRepository();
            var commentRepo = TestObjectFactory.GetCommentRepository();

            var controller = new CommentsController(commentRepo, userRepo, movieRepo);

            var commentModel = new DeleteCommentModel
            {
                CommentId = 7777              
            };

            var result = controller.DeleteComment(commentModel);

            var expected = result as BadRequestErrorMessageResult;

            Assert.IsTrue(expected.Message.ToLower().Contains(badReqMessage.ToLower()));
        }

        [Test]
        public void DeleteComment_ShouldReturnOk_WhenArgumentsAreValidAndCommentIsRemovedSuccessfuly()
        {
            var userRepo = TestObjectFactory.GetUserRepository();
            var movieRepo = TestObjectFactory.GetMovieRepository();
            var commentRepo = TestObjectFactory.GetCommentRepository();

            var controller = new CommentsController(commentRepo, userRepo, movieRepo);

            var commentModel = new DeleteCommentModel
            {
                CommentId = 1
            };

            var result = controller.DeleteComment(commentModel);

            var expected = result as OkResult;

            Assert.IsNotNull(expected);
            
        }

        [Test]
        public void DeleteComment_ShouldCallSaveChanges()
        {
            var userRepo = TestObjectFactory.GetUserRepository();
            var movieRepo = TestObjectFactory.GetMovieRepository();
            var commentRepo = TestObjectFactory.GetCommentRepository();

            var controller = new CommentsController(commentRepo, userRepo, movieRepo);

            var commentModel = new DeleteCommentModel
            {
                CommentId = 1
            };

            var result = controller.DeleteComment(commentModel);

            Assert.AreEqual(1, commentRepo.NumberOfSavedChanges);
        }
    }
}
