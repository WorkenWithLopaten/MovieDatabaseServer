using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreData.Models
{
    public class CinemaCity
    {
        private ICollection<Cinema> cinemas;

        public CinemaCity()
        {
            this.Cinemas = new HashSet<Cinema>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public virtual ICollection<Cinema> Cinemas
        {
            get { return this.cinemas; }
            set { this.cinemas = value; }
        }
    }
}
