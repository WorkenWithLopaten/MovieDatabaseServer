namespace WebAPI.Controllers
{
    using MovieDb.Data;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using SqlLiteData.Models;
    using System.Collections.Generic;
    using Models.Actors;
    using Models.Movies;
    using PostgreData.Models;
    using Models.Cinema;
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CinemaController : ApiController
    {
        private IRepository<Cinema> cinemas;
        private IRepository<CinemaCity> cities;
        public CinemaController(IRepository<Cinema> cinemas, IRepository<CinemaCity> cities)

        {
            this.cinemas = cinemas;
            this.cities = cities;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var cinemas = this.cinemas.All.ToList().Select(x=>x.Name);

            return this.Ok(cinemas);
        }

        [HttpGet]
        public IHttpActionResult AllCinemasInCIty(string id)
        {
            var name = id;
            var cinemas = this.cities.All.Where(x => x.Name == name).SelectMany(x => x.Cinemas).Select(z => z.Name).ToList();

            return this.Ok(cinemas);
        }

        [HttpPost]
        public IHttpActionResult AddCinema(CinemaToAddModel cinema)
        {
            var collectionOfCities = new List<CinemaCity>();
            foreach (var city in cinema.CinemaCities)
            {
                var alreadyExists = this.cities.All.Where(x => x.Name == city.CityName).FirstOrDefault();
                if (alreadyExists != null)
                {
                    collectionOfCities.Add(alreadyExists);
                    continue;
                }
                else
                {
                    try
                    {   var cityToAdd = new CinemaCity() { Name = city.CityName };
                        this.cities.Add(cityToAdd);
                        this.cities.SaveChanges();
                        collectionOfCities.Add(cityToAdd);
                    }
                    catch
                    {
                        return this.BadRequest("Ups couldn`t save Cinema cities something went wrong");
                    }
                   
                }
            }

            var alreadyExistsCinema = this.cinemas.All.Where(x => x.Name == cinema.Name).FirstOrDefault();
            if (alreadyExistsCinema != null)
            {
                return this.BadRequest("Cinema already exists but cities are added if not existing");
            }
            else
            {
                var cinemaNew = new Cinema()
                {
                    Name = cinema.Name
                };

                foreach (var cityToAdd in collectionOfCities)
                {
                    cinemaNew.Cities.Add(cityToAdd);
                }
                try
                {
                    this.cinemas.Add(cinemaNew);
                    this.cinemas.SaveChanges();
                    return this.Ok("Cinema added successfully !");
                }
                catch
                {
                    return this.BadRequest("Ups couldn`t save Cinema cities something went wrong");
                }
            }       
        }

        [HttpPost]
        public IHttpActionResult Delete(CinemaToDeleteModel cinema)
        {
            var cinemaToDel = this.cinemas.All.Where(x => x.Name == cinema.CinemaName).FirstOrDefault();
            if (cinemaToDel == null)
            {
                return this.BadRequest("No such cinema");
            }
            try
            {
                this.cinemas.Delete(cinemaToDel);
                this.cinemas.SaveChanges();
            }
            catch
            {
                return this.BadRequest("Ups something went wrong");
            }

            return this.Ok("Cinema Deleted successfully");

        }


        [HttpGet]
        public IHttpActionResult GetAllCitiesForCinema(string id)
        {
            var cinemaName = id;
            var cities = this.cinemas.All.Where(x => x.Name == cinemaName).SelectMany(x => x.Cities).Select(z => z.Name).ToList();

            if (cities == null)
            {
                return this.BadRequest("No such Cinema Name");
            }

            return this.Ok(cities);
        }

        [HttpPost]
        public IHttpActionResult ConnectCinemaToCity(CinemaToCityModel data)
        {
            var cinema = this.cinemas.All.Where(x => x.Name==data.CinemaName).FirstOrDefault();
            var city = this.cities.All.Where(x => x.Name == data.CityName).FirstOrDefault();

            if (cinema == null)
            {
                return this.BadRequest("Cinema doesn`t exist");
            }
            else if (city == null)
            {
                return this.BadRequest("City doesn`t exist");
            }
            else
            {
                //first check if exists connection
                var alreadyExists = cinema.Cities.Select(x => x.Name).Where(y => y==data.CityName).FirstOrDefault();

                if (alreadyExists != null)
                {
                    return this.BadRequest("Already this Cinema is connected to this City");
                }
                else
                {
                    try
                    {
                        cinema.Cities.Add(city);
                        this.cinemas.SaveChanges();
                        return this.Ok("City added successfully to Cinema");
                    }
                    catch
                    {
                        return this.BadRequest("Ups something went wrong coudn`t add Cinema to City");
                    }
                }
            }
        }



    }
}
