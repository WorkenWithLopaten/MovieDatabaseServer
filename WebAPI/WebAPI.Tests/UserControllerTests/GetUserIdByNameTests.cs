using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Http.Results;
using WebAPI.Controllers;
using WebAPI.Models.Users;
using WebAPI.Test.TestObjects;

namespace WebAPI.Tests.UserControllerTests
{
    [TestFixture]
    public class GetUserIdByNameTests
    {
        [Test]
        public void Get_Should_Return_Ok_AndSetParametesrCorrectly_WhenUserIsValid()
        {
            var filePath = @"http:/google.bg";
            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
               userCityRepository, userCountryRepository, userAdressRepository);

            var httpMethodPost = HttpMethod.Post;
            var request = new HttpRequestMessage(httpMethodPost, filePath);

            var content = new StringContent("User 0");
            request.Content = content;

            var result = controller.GetUserIdByName(request);

            var expected = result as OkNegotiatedContentResult<int>;

            var id = expected.Content;

            Assert.IsNotNull(expected);

            Assert.AreEqual(id, 0);
        }

        [Test]
        public void Get_Should_Return_BadRequest_WhenUserIsValid()
        {
            var filePath = @"http:/google.bg";
            var badReqMessage = "No such";
            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
               userCityRepository, userCountryRepository, userAdressRepository);

            var httpMethodPost = HttpMethod.Post;
            var request = new HttpRequestMessage(httpMethodPost, filePath);

            var content = new StringContent("User 333");
            request.Content = content;

            var result = controller.GetUserIdByName(request);

            var expected = result as BadRequestErrorMessageResult;

            Assert.IsTrue(expected.Message.ToLower().Contains(badReqMessage.ToLower()));

        }
    }
}
