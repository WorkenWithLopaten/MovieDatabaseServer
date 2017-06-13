using System.Linq;
using MovieDb.Data;
using NUnit.Framework;
using WebAPI.Controllers;
using WebAPI.Models.Users;
using WebAPI.Test.TestObjects;
using MovieDb.Models;
using System.Web.Http.Results;

namespace WebAPI.Tests
{
    [TestFixture]
    public class RegisterTests
    {
        [Test]
        public void Register_ShouldReturn_BadRequestWithMessageUserIsTaken_WhenThereIsAUserWithSameUserName_InDatabase()
        {
            const string UserNameTaken = "username is taken";
            const string InvalidUserName = "User 0";

            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
                userCityRepository, userCountryRepository, userAdressRepository);

            var registerModel = new RegisterUser();

            registerModel.UserName = InvalidUserName;

            var result = controller.Register(registerModel);

            var badRequestResult = result as BadRequestErrorMessageResult;

            Assert.IsTrue(badRequestResult.Message.ToLower().Contains(UserNameTaken));
        }

        [Test]
        public void Register_ShouldReturnTheIDNumber_WhenUserIsRegisteredSuccssesfully()
        {
            const string UserName = "Lambinator";
            const string FirstName = "Lambriana";
            const string LastName = "Yanakieva";
            const string Email = "lambrianayanakieva@gmail.com";
            const string Adress = "IU Izgrev";
            const string CityName = "Burgas";
            const string CountryName = "Bulgaria";

            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
                userCityRepository, userCountryRepository, userAdressRepository);

            var registerModel = new RegisterUser();

            registerModel.UserName = UserName;
            registerModel.FirstName = FirstName;
            registerModel.LastName = LastName;
            registerModel.isMale = false;
            registerModel.Email = Email;
            registerModel.Adress = Adress;
            registerModel.CityName = CityName;
            registerModel.CountryName = CountryName;

            var result = controller.Register(registerModel);

            var ok = result as OkNegotiatedContentResult<int>;

            Assert.IsNotNull(ok);
        }

        [Test]
        public void Register_ShouldAddUserIn_UserRepository()
        {
            const string UserName = "Lambinator";
            const string FirstName = "Lambriana";
            const string LastName = "Yanakieva";
            const string Email = "lambrianayanakieva@gmail.com";
            const string Adress = "IU Izgrev";
            const string CityName = "Burgas";
            const string CountryName = "Bulgaria";


            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
                userCityRepository, userCountryRepository, userAdressRepository);

            var registerModel = new RegisterUser();

            registerModel.UserName = UserName;
            registerModel.FirstName = FirstName;
            registerModel.LastName = LastName;
            registerModel.isMale = false;
            registerModel.Email = Email;
            registerModel.Adress = Adress;
            registerModel.CityName = CityName;
            registerModel.CountryName = CountryName;

            var result = controller.Register(registerModel);

            Assert.IsTrue(userRepository.All.Where(x => x.UserName == UserName) != null);
        }

        [Test]
        public void Register_ShouldSaveChangesIn_UserRepository()
        {
            const string UserName = "Lambinator";
            const string FirstName = "Lambriana";
            const string LastName = "Yanakieva";
            const string Email = "lambrianayanakieva@gmail.com";
            const string Adress = "IU Izgrev";
            const string CityName = "Burgas";
            const string CountryName = "Bulgaria";


            var userRepository = TestObjectFactory.GetUserRepository();
            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var controller = new UsersController(userRepository, userDataRepostory,
                userCityRepository, userCountryRepository, userAdressRepository);

            var registerModel = new RegisterUser();

            registerModel.UserName = UserName;
            registerModel.FirstName = FirstName;
            registerModel.LastName = LastName;
            registerModel.isMale = false;
            registerModel.Email = Email;
            registerModel.Adress = Adress;
            registerModel.CityName = CityName;
            registerModel.CountryName = CountryName;

            var result = controller.Register(registerModel);

            Assert.AreEqual(userRepository.NumberOfSavedChanges, 1);

        }

        [Test]
        public void Register_ShouldThrowBadRequest_WhenUserNameIsOver100Chars()
        {
            const string InvalidArgs = "is not ok";
            const string UserName = "LambinatorLambinatorLambinatorLambinatorLambinatorLambinatorLambinatorLambinatorLambinatorLambinatorLambinatorambinatorLambinatorLambinatambinatorLambinatorLambinat";
            const string FirstName = "Lambriana";
            const string LastName = "Yanakieva";
            const string Email = "lambrianayanakieva@gmail.com";
            const string Adress = "IU Izgrev";
            const string CityName = "Burgas";
            const string CountryName = "Bulgaria";

            var userDataRepostory = TestObjectFactory.GetUserDataRepository();
            var userCityRepository = TestObjectFactory.GetCityRepository();
            var userCountryRepository = TestObjectFactory.GetCountrRepository();
            var userAdressRepository = TestObjectFactory.GetAdressRepository();

            var dbContext = new MoviesContext();
            var userRepositoryReal = new EfGenericRepository<User>(dbContext);

            var controller = new UsersController(userRepositoryReal, userDataRepostory,
                userCityRepository, userCountryRepository, userAdressRepository);

            var registerModel = new RegisterUser();

            registerModel.UserName = UserName;
            registerModel.FirstName = FirstName;
            registerModel.LastName = LastName;
            registerModel.isMale = false;
            registerModel.Email = Email;
            registerModel.Adress = Adress;
            registerModel.CityName = CityName;
            registerModel.CountryName = CountryName;

            var result = controller.Register(registerModel);

            var badRequest = result as BadRequestErrorMessageResult;

            Assert.IsTrue(badRequest.Message.ToLower().Contains(InvalidArgs));
        }

    }
}