using MovieDb.Data;
using MovieDb.Models;
using NUnit.Framework;
using System.Web.Http.Results;
using WebAPI.Controllers;
using WebAPI.Models.Users;
using WebAPI.Test.TestObjects;

namespace WebAPI.Tests.UserControllerTests
{
    [TestFixture]
    public class UpdateUserDataTests
    {
        [Test]
        public void UpdateUserData_ShouldThrowBadRequest_WhenUserIsNotFound()
        {
            var badReqMessage = "No such";

            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
                userCityRepository, userCountryRepository, userAdressRepository);

            var registerModel = new RegisterUser();
            registerModel.UserName = "Invalid User";

            var result = controller.UpdateUserData(registerModel);

            var badReq = result as BadRequestErrorMessageResult;

            Assert.IsTrue(badReq.Message.ToLower().Contains(badReqMessage.ToLower()));
        }

        [Test]
        public void UpdateUserData_ShouldCorrectlyUpdateUserDetails()
        {
            const string UserName = "User 1";
            const string NewFirstName = "UserFirstNew 1";
            const string NewLastName = "UserLastNew 1";
            const string NewEmail = "EmailNew 1";
            const string NewCityName = "CityNew 1";
            const string NewCountryName = "CountryNew 1";         

            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
                userCityRepository, userCountryRepository, userAdressRepository);

            var registerModel = new RegisterUser();
            registerModel.UserName = UserName;
            registerModel.FirstName = NewFirstName;
            registerModel.LastName = NewLastName;
            registerModel.Email = NewEmail;
            registerModel.CityName = NewCityName;
            registerModel.CountryName = NewCountryName;
          
            var result = controller.UpdateUserData(registerModel);

            var expected = result as OkNegotiatedContentResult<User>;

            Assert.AreEqual(expected.Content.UserName, registerModel.UserName);
            Assert.AreEqual(expected.Content.UserData.FirstName, registerModel.FirstName);
            Assert.AreEqual(expected.Content.UserData.LastName, registerModel.LastName);
            Assert.AreEqual(expected.Content.UserData.Email, registerModel.Email);           
        }

        [Test]
        public void UpdateUserData_ReturnOKAndDoNothing_WhenTHereIsNoDataToUpdate()
        {
            var NoDataToUpdate = "No data";

            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
                userCityRepository, userCountryRepository, userAdressRepository);

            var registerModel = new RegisterUser();

            registerModel.UserName = "User 0";
            registerModel.isMale = true;

            var result = controller.UpdateUserData(registerModel);

            var expected = result as OkNegotiatedContentResult<string>;

            Assert.IsTrue(expected.Content.ToLower().Contains(NoDataToUpdate.ToLower()));      
        }

        [Test]
        public void UpdateUserData_ShouldReturnInvalidDataBadRequest_WhenInvalidParametersForUpdateAreInserted()
        {
            const string InvalidData = "InvalidData";
            const string UserName = "User 1";
            const string FirstName = "UserFirstNew 1";
            const string LastName = "UserLastNew 1";
            const string Email = "EmailNew 1";
            const string CityName = "CityNew 1";
            const string CountryName = "CountryNew 1";

            var dbContext = new MoviesContext();

            dbContext.Users.Add(new User()
            {
                UserName = UserName
            });

            var userRepostiroyReal = new EfGenericRepository<User>(dbContext);
        
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepostiroyReal, userDataRepostory,
                userCityRepository, userCountryRepository, userAdressRepository);
        

            var registerModel = new RegisterUser();
            registerModel.UserName = UserName;
            registerModel.FirstName = FirstName;
            registerModel.LastName = LastName;
            registerModel.Email = Email;
            registerModel.CityName = CityName;
            registerModel.CountryName = CountryName;

            var result = controller.UpdateUserData(registerModel);

            var expected = result as BadRequestErrorMessageResult;

            Assert.IsTrue(expected.Message.ToLower().Contains(InvalidData.ToLower()));
        }
    }
}
