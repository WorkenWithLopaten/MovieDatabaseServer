namespace MovieDb.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Adress
    {
        private ICollection<UserData> users;
        public Adress()
        {
            this.Users = new HashSet<UserData>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string AdressText { get; set; }

        public virtual int CityId { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<UserData> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }


    }
}
