using MovieDb.Data;
using MovieDb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using WebAPI.Models.Movies;
using WebAPI.Models.Users;
using System.Data.Entity;

namespace WebAPI.Importer
{
    public class JsonImporter
    {
        private const string JsonFilePath = "bin\\JsonFiles\\";
        private IMoviesContext db;
        private string directory;

        public JsonImporter(IMoviesContext db)
        {
            this.db = db;
            this.directory = HostingEnvironment.ApplicationPhysicalPath;
        }

        public void ImportMovies()
        {
            var moviesToAdd = Directory
                .GetFiles(this.directory + JsonFilePath)
                .Where(file => file.EndsWith("Movies.json"))
                .Select(file => File.ReadAllText(file))
                .Select(s => JSONSerializer<IEnumerable<MoviesCreateModel>>.DeserializeJson(s))
                .ToList();

            var addedMovies = new HashSet<string>();
            var moviesToAddWithoutDublication = new List<Movie>();

            foreach (var file in moviesToAdd)
            {
                foreach (var movie in file)
                {
                    if (!addedMovies.Contains(movie.Name))
                    {
                        var movieToAdd = new Movie { Name = movie.Name };
                        moviesToAddWithoutDublication.Add(movieToAdd);
                        addedMovies.Add(movie.Name);

                    }
                }
            }

            var moviesWithOutEmptyNamesOrDuplication = moviesToAddWithoutDublication.Where(x => !string.IsNullOrWhiteSpace(x.Name));

            try
            {
                this.db.Movies.AddRange(moviesWithOutEmptyNamesOrDuplication);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public void ImportUsers()
        {
            var usersToAdd = Directory
                .GetFiles(this.directory + JsonFilePath)
                .Where(file => file.EndsWith("Users.json"))
                .Select(file => File.ReadAllText(file))
                .Select(s => JSONSerializer<IEnumerable<RegisterUser>>.DeserializeJson(s))
                .ToList();

            var countryAdded = new Dictionary<string, Country>();
            var cityAdded = new Dictionary<string, City>();
            var adressAdded = new Dictionary<string, Adress>();
            var username = new HashSet<string>();

            var allUsersFromDatabase = this.db.Users.Include(x => x.UserData.Adress.City.Country)
                                                     .Include(x => x.UserData.Adress.City)
                                                     .Include(x => x.UserData.Adress)
                                                     .ToList();


            foreach (var file in usersToAdd)
            {
                foreach (var user in file)
                {
                    if (!username.Contains(user.UserName))
                    {
                        //if the user is already in the database       
                        if (allUsersFromDatabase.Where(x => x.UserName == user.UserName).Count() > 0)
                        {
                            continue;
                        }

                        var country = allUsersFromDatabase.Select(x => x.UserData.Adress.City.Country).Where(x => x.Name == user.CountryName)
                                                          .FirstOrDefault();


                        if (country == null)
                        {
                            if (countryAdded.ContainsKey(user.CountryName))
                            {
                                country = countryAdded[user.CountryName];
                            }
                            else
                            {
                                country = new Country() { Name = user.CountryName };
                                countryAdded.Add(country.Name, country);
                            }
                        }

                        //city name is unique so no need to check for country
                        var city = allUsersFromDatabase.Select(x => x.UserData.Adress.City).Where(x => x.Name == user.CityName)
                                                          .FirstOrDefault();

                        if (city == null)
                        {
                            if (cityAdded.ContainsKey(user.CityName))
                            {
                                city = cityAdded[user.CityName];
                            }
                            else
                            {
                                city = new City() { Name = user.CityName, Country = country };
                                cityAdded.Add(city.Name, city);
                            }
                        }
                        //adress text is not unique 
                        var address = allUsersFromDatabase.Select(x => x.UserData.Adress)
                                                          .Where(x => x.City.Country.Name == user.CountryName &&
                                                                 x.City.Name == user.CityName &&
                                                                 x.AdressText == user.Adress
                                                                 )
                                                          .FirstOrDefault();

                        if (address == null)
                        {     //check the cache
                            if (adressAdded.ContainsKey(user.Adress) && adressAdded[user.Adress].City == city)
                            {
                                address = adressAdded[user.Adress];
                            }
                            else
                            {
                                address = new Adress() { AdressText = user.Adress, City = city };
                            }  
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
                            this.db.Users.Add(newUser);
                            this.db.SaveChanges();
                            username.Add(user.UserName);
                        }
                        catch (Exception ex)
                        {
                            
                        }
                    }
                }
            }      
        }

    }
}