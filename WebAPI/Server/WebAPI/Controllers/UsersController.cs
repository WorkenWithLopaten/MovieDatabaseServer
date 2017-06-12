namespace WebAPI.Controllers
{
    using Models.Users;
    using MovieDb.Data;
    using MovieDb.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Cors;

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        private readonly IRepository<User> users;
        private readonly IRepository<UserData> userdata;
        private readonly IRepository<City> city;
        private readonly IRepository<Country> country;
        private readonly IRepository<Adress> adress;


        public UsersController(IRepository<User> users,
                               IRepository<UserData> userdata,
                               IRepository<City> city,
                               IRepository<Country> country,
                               IRepository<Adress> adress)

        {
            this.users = users;
            this.userdata = userdata;
            this.city = city;
            this.country = country;
            this.adress = adress;
        }

        [HttpPost]
        public IHttpActionResult Register(RegisterUser user)
        {
            var allUsersQuearable = this.users.All;
            var userWithThisNameCount = allUsersQuearable.Where(x => x.UserName == user.UserName).Count();

            if (userWithThisNameCount > 0)
            {
                return this.BadRequest("Username is taken");
            }

            var country = this.country.All.Where(c => c.Name == user.CountryName).FirstOrDefault();
            if (country == null)
            {
                country = new Country() { Name = user.CountryName };
            }

            var city = this.city.All.Where(c => c.Name == user.CityName).FirstOrDefault();
            if (city == null)
            {
                city = new City() { Name = user.CityName, Country = country };
            }

            var address = this.adress.All.Where(a => a.AdressText == user.Adress && a.City.Name == city.Name && a.City.Country.Name == country.Name).FirstOrDefault();
            if (address == null)
            {
                address = new Adress() { AdressText = user.Adress, City = city };
            }

            var userdata = new UserData()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Adress = address,
                Email = user.Email,
                isMale = user.isMale
            };

            var newUser = new User() { UserName = user.UserName, UserData = userdata };

            try
            {
                this.users.Add(newUser);
                this.users.SaveChanges();
            }
            catch (Exception ex)
            {
                return this.BadRequest("some argument for registration is not ok");
            }

            return this.Ok(this.users.All.Where(x => x.UserName == user.UserName).FirstOrDefault().Id);
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var res = this.users.All.Where(x => x.Expire == false)
                .Select(x =>
                new TestingModel
                {
                    FirstName = x.UserData.FirstName,
                    LastName = x.UserData.LastName,
                    UserName = x.UserName,
                    Id = x.Id,
                    IsMale = x.UserData.isMale,
                    CityName = x.UserData.Adress.City.Name,
                    CountryName = x.UserData.Adress.City.Country.Name,
                    Email = x.UserData.Email
                })
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName).ToList();

            return this.Ok(res);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id < 0)
            {
                return this.BadRequest("Id of user can`t be negative");
            }

            var res = GetUser(id);
            if (res == null)
            {
                return this.BadRequest("No user with such id");
            }

            return this.Ok(new TestingModel{
                Id = id,
                UserName = res.UserName ,
                Email = res.Email,
                FirstName =  res.FirstName,
                LastName = res.LastName,
                IsMale =  res.isMale,
                CityName =  res.CityName,
                CountryName =   res.CountryName });
        }

        [HttpPost]
        public IHttpActionResult GetUserIdByName(HttpRequestMessage request)
        {
            var username = request.Content.ReadAsStringAsync().Result.Replace("\"", "").Replace("/", "");

            var res = this.users.All.Where(x => x.Expire == false && x.UserName == username).FirstOrDefault();
            if (res == null)
            {
                return this.BadRequest("No such user");
            }

            return this.Ok(res.Id);

        }

        public IHttpActionResult UpdateUserData(RegisterUser userData)
        {
            var shouldUpdate = false;
            var changedCity = false;
            var changeCountry = false;

            var currentUser = this.users.All.Include(x => x.UserData.Adress.City.Country)
                .Where(x => x.UserName == userData.UserName).FirstOrDefault();

            if (currentUser == null)
            {
                return this.BadRequest("No such user");
            }
            Country country = currentUser.UserData.Adress.City.Country;
            if (userData.CountryName != null && currentUser.UserData.Adress.City.Country.Name != userData.CountryName)
            {
                changeCountry = true;
                country = this.country.All.Where(c => c.Name == userData.CountryName).FirstOrDefault();
                if (country == null)
                {
                    country = new Country() { Name = userData.CountryName };
                }
            }
            City city = currentUser.UserData.Adress.City;
            if (userData.CityName != null && currentUser.UserData.Adress.City.Name != userData.CityName)
            {
                changedCity = true;
                city = this.city.All.Where(c => c.Name == userData.CityName).FirstOrDefault();
                if (city == null)
                {
                    city = new City() { Name = userData.CityName, Country = country };
                }
            }

            Adress address = currentUser.UserData.Adress;
            if (userData.Adress != null && currentUser.UserData.Adress.AdressText != userData.Adress || changeCountry || changedCity)
            {
                shouldUpdate = true;
                address = this.adress.All.Where(a => a.AdressText == userData.Adress && a.City.Name == city.Name && a.City.Country.Name == country.Name).FirstOrDefault();
                if (address == null)
                {
                    address = new Adress() { AdressText = userData.Adress, City = city };
                }

            }

            UserData data = currentUser.UserData;
            if (userData.FirstName != null && currentUser.UserData.FirstName != userData.FirstName)
            {
                data.FirstName = userData.FirstName;
                shouldUpdate = true;
            }
            if (userData.LastName != null && currentUser.UserData.LastName != userData.LastName)
            {
                data.LastName = userData.LastName;
                shouldUpdate = true;
            }
            if (userData.Email != null && currentUser.UserData.Email != userData.Email)
            {
                data.Email = userData.Email;
                shouldUpdate = true;
            }
            if (userData.isMale != currentUser.UserData.isMale)
            {
                data.isMale = userData.isMale;
                shouldUpdate = true;
            }

            if (shouldUpdate)
            {
                data.Adress = address;
                currentUser.UserData = data;
                try
                {
                    this.UpdateUser(currentUser);
                }
                catch
                {
                    return this.BadRequest("InvalidData");
                }
                return this.Ok(currentUser);
            }
            else
            {
                return this.Ok("No data to update");
            }
        }

        public IHttpActionResult ExpireUser(int userId)
        {
            var user = this.users.All.Where(x => x.Id == userId && x.Expire == false).FirstOrDefault();

            if (user == null)
            {
                return this.BadRequest("No such user");
            }

            user.Expire = true;
            try
            {
                UpdateUser(user);
            }
            catch
            {
                return this.BadRequest("Can`t expire user");
            }

            return this.Ok(user);
        }

        private RegisterUser GetUser(int id)
        {
            return this.users.All.Where(x => x.Id == id && x.Expire == false)
                .Select(user => new RegisterUser()
                {
                    UserName = user.UserName,
                    FirstName = user.UserData.FirstName,
                    LastName = user.UserData.LastName,
                    Email = user.UserData.Email,
                    isMale = user.UserData.isMale,
                    Adress = user.UserData.Adress.AdressText,
                    CityName = user.UserData.Adress.City.Name,
                    CountryName = user.UserData.Adress.City.Country.Name
                }).FirstOrDefault();
        }

        private void UpdateUser(User data)
        {
            this.users.Update(data);
            this.users.SaveChanges();
        }
    }
}
