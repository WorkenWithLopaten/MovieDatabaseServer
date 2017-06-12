using MovieDb.Models;

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
                    Id = i
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
                    isMale = true
                    
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
                   Name = "City "+i               
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
    }
}
