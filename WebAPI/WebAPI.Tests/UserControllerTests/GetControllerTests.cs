using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Http.Results;
using WebAPI.Controllers;
using WebAPI.Models.Users;
using WebAPI.Test.TestObjects;

namespace WebAPI.Tests.UserControllerTests
{
    [TestFixture]
    public class GetControllerTests
    {
        [Test]
        public void Get_Should_Return_OkWithListWithCurrentCount()
        {
            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
               userCityRepository, userCountryRepository, userAdressRepository);

            var result = controller.Get();

            var expected = result as OkNegotiatedContentResult<List<TestingModel>>;

            Assert.IsNotNull(expected);
        }

        [Test]
        public void GetById_ShouldReturn_BadRequest_WhenIdIsLessThanZero()
        {
            const string CantBeNegative = "negative";

            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
               userCityRepository, userCountryRepository, userAdressRepository);

            var result = controller.Get(-1);

            var badRequest = result as BadRequestErrorMessageResult;

            Assert.IsTrue(badRequest.Message.ToLower().Contains(CantBeNegative));
        }

        [Test]
        public void GetById_ShouldReturn_BadRequest_WhenIDDoNotExist()
        {
            const string NotExist = "no user";

            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
               userCityRepository, userCountryRepository, userAdressRepository);

            var result = controller.Get(333);

            var badRequest = result as BadRequestErrorMessageResult;

            Assert.IsTrue(badRequest.Message.ToLower().Contains(NotExist));
        }

        [Test]
        public void Get_Should_Return_OkWithListWithCurrentCount_WhenIDExist()
        {
            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
               userCityRepository, userCountryRepository, userAdressRepository);

            var result = controller.Get(3);

            var expected = result as OkNegotiatedContentResult<TestingModel>;

            Assert.IsNotNull(expected);
        }

        [Test]
        public void GetByID_Should_SetParameters_Correctly()
        {
            const string UserName = "User 3";
            const string FirstName = "UserFirst 3";
            const string LastName = "UserLast 3";
            const string Email = "Email 3";
            const string CityName = "City 3";
            const string CountryName = "Country 3";
            const int UserId = 3;
            const bool isMale = true;

            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
               userCityRepository, userCountryRepository, userAdressRepository);

            var result = controller.Get(3);
 
            var expected = result as OkNegotiatedContentResult<TestingModel>;

            Assert.AreEqual(UserId, expected.Content.Id);
            Assert.AreEqual(UserName, expected.Content.UserName);
            Assert.AreEqual(FirstName, expected.Content.FirstName);
            Assert.AreEqual(LastName, expected.Content.LastName);
            Assert.AreEqual(Email, expected.Content.Email);
            Assert.AreEqual(isMale, expected.Content.IsMale);
            Assert.AreEqual(CityName, expected.Content.CityName);
            Assert.AreEqual(CountryName, expected.Content.CountryName);
        }

        [Test]
        public void Get_Should_SetParametersInListOfModelsCorrectly()
        {
            const string UserName = "User 1";
            const string FirstName = "UserFirst 1";
            const string LastName = "UserLast 1";
            const string Email = "Email 1";
            const string CityName = "City 1";
            const string CountryName = "Country 1";
            const int UserId = 1;
            const bool isMale = true;

            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
               userCityRepository, userCountryRepository, userAdressRepository);

            var result = controller.Get();

            var expected = result as OkNegotiatedContentResult<List<TestingModel>>;

            var testingModel = new TestingModel
            {
                UserName = UserName,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                CityName = CityName,
                CountryName = CountryName,
                Id = UserId,
                IsMale = isMale
            };

            Assert.AreEqual(expected.Content[1].UserName ,testingModel.UserName);
            Assert.AreEqual(expected.Content[1].FirstName, testingModel.FirstName);
            Assert.AreEqual(expected.Content[1].LastName, testingModel.LastName);
            Assert.AreEqual(expected.Content[1].Email, testingModel.Email);
            Assert.AreEqual(expected.Content[1].IsMale, testingModel.IsMale);
            Assert.AreEqual(expected.Content[1].CityName, testingModel.CityName);
            Assert.AreEqual(expected.Content[1].CountryName, testingModel.CountryName);
            
        }
    }
}
