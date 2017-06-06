namespace WebAPI.Models.Users
{
    public class RegisterUser
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool isMale { get; set; }

        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string Adress { get; set; }
    }
}