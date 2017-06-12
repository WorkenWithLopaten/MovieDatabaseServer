using MovieDb.Models;
using NUnit.Framework;
using System.Web.Http.Results;
using WebAPI.Controllers;
using WebAPI.Test.TestObjects;

namespace WebAPI.Tests.UserControllerTests
{
    [TestFixture]
    public class ExpireUserTests
    {
        [Test]
        public void ExpireUser_ShuldReturnBadRequest_WhenThereIsNoSuchUserIdExistent()
        {
            const string NoSuchUser = "No such user";

            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
                userCityRepository, userCountryRepository, userAdressRepository);

            var expect = controller.ExpireUser(110);

            var result = expect as BadRequestErrorMessageResult;

            Assert.IsTrue(result.Message.ToLower().Contains(NoSuchUser.ToLower()));
        }

        [Test]
        public void ExpireUser_ShuldReturnBadRequest_WhenUserExpireIsTrue()
        {         
            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
                userCityRepository, userCountryRepository, userAdressRepository);

            var expect = controller.ExpireUser(1);

            var result = expect as OkNegotiatedContentResult<User>;

            Assert.AreEqual(result.Content.Expire, true);
        }

        [Test]
        public void Test()
        {
            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
                userCityRepository, userCountryRepository, userAdressRepository);

            var expect = controller.ExpireUser(1);

            var result = expect as BadRequestErrorMessageResult;
        }
    }
}
