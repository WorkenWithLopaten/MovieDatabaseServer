using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreData.Models
{
    public class Cinema
    {
        private ICollection<CinemaCity> cities;

        public Cinema()
        {
            this.Cities = new HashSet<CinemaCity>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public virtual ICollection<CinemaCity> Cities
        {
            get { return this.cities; }
            set { this.cities = value; }
        }
    }
}
