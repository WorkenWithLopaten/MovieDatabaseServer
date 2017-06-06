namespace MovieDb.Models
{
    using System.Collections.Generic;

    public class City
    {
        private ICollection<Adress> adresses;

        public City()
        {
            this.Adresses = new HashSet<Adress>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Adress> Adresses
        {
            get
            {
                return this.adresses;
            }

            set
            {
                this.adresses = value;
            }
        }
    }
}
