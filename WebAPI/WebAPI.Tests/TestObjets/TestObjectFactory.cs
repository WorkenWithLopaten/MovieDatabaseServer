using MovieDb.Models;
using SqlLiteData.Models;
using System;

namespace WebAPI.Test.TestObjects
{
    public static class TestObjectFactory
    {
        public static InMemoryRepository<User> GetUserRepository()
        {
            var repository = new InMemoryRepository<User>();

            for (int i = 0; i < 10; i++)
            {
                repository.Add(
                new User()
                {
                    UserName = "User " + i,
                    Id = i,
                    UserData = new UserData()
                    {
                        FirstName = "UserFirst " + i,
                        LastName = "UserLast " + i,
                        Email = "Email " + i,
                        isMale = true,
                        Adress = new Adress()
                        {
                            City = new City()
                            {
                                Name = "City " + i,
                                Country = new Country()
                                {
                                    Name = "Country " + i,
                                }
                            }
                        }
                    }
                }
                );
            }

            return repository;
        }

        public static InMemoryRepository<UserData> GetUserDataRepository()
        {
            var repository = new InMemoryRepository<UserData>();

            for (int i = 0; i < 10; i++)
            {
                repository.Add(
                new UserData()
                {
                    FirstName = "First " + i,
                    LastName = "Last " + i,
                    Email = "Email " + i,
                    isMale = true,
                    UserId = i
                });
            }

            return repository;
        }

        public static InMemoryRepository<City> GetCityRepository()
        {
            var repository = new InMemoryRepository<City>();

            for (int i = 0; i < 10; i++)
            {
                repository.Add(
                new City()
                {
                    Name = "City " + i
                });
            }

            return repository;
        }

        public static InMemoryRepository<Country> GetCountrRepository()
        {
            var repository = new InMemoryRepository<Country>();

            for (int i = 0; i < 10; i++)
            {
                repository.Add(
                new Country()
                {
                    Name = "City " + i
                });
            }

            return repository;
        }

        public static InMemoryRepository<Adress> GetAdressRepository()
        {
            var repository = new InMemoryRepository<Adress>();

            for (int i = 0; i < 10; i++)
            {
                repository.Add(
                new Adress()
                {
                    AdressText = "Aress " + i
                });
            }

            return repository;
        }

        public static InMemoryRepository<Movie> GetMovieRepository()
        {
            var repository = new InMemoryRepository<Movie>();

            for (int i = 0; i < 10; i++)
            {
               repository.Add(
               new Movie()
               {
                   DislikesNumber = i,
                   LikesNumber = i,
                   Name = "Movie " + i,
                   Id = i
               }
               );
            }

            return repository;
        }

        public static InMemoryRepository<MoviesLite> GetMoviesLiteRepository()
        {
            var repository = new InMemoryRepository<MoviesLite>();

            for (int i = 0; i < 10; i++)
            {

                var movieLite = new MoviesLite()
                {
                    Name = "Movie " + i,
                    Id = i,
                };
                movieLite.Actors.Add(new Actors() { Name = "Gosho" + i });
                repository.Add(movieLite);

            }

            return repository;
        }

        public static InMemoryRepository<Like> GetLikesRepository()
        {
            var repository = new InMemoryRepository<Like>();

            for (int i = 0; i < 10; i++)
            {
                repository.Add(
                new Like()
                {
                   Id = i,
                   MovieId = i,
                }
                );
            }

            return repository;
        }

        public static InMemoryRepository<Dislike> GetDislikesRepository()
        {
            var repository = new InMemoryRepository<Dislike>();

            for (int i = 0; i < 10; i++)
            {
                repository.Add(
                new Dislike()
                {
                    Id = i,
                    MovieId = i,
                }
                );
            }

            return repository;
        }

        public static InMemoryRepository<Comment> GetCommentRepository()
        {
            var repository = new InMemoryRepository<Comment>();

            for (int i = 0; i < 10; i++)
            {
                repository.Add(
                new Comment()
                {
                    Id = i,
                    CommentText = "Text " + i,
                    CreatedOn = new DateTime(i),
                    isDeleted = false,
                    UserId = i,
                    MovieId = i

                }
                );
            }

            return repository;
        }
    }
}
