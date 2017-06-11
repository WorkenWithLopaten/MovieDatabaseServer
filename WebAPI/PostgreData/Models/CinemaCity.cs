using System;
using System.Collections.Generic;
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

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Cinema> Cinemas
        {
            get { return this.cinemas; }
            set { this.cinemas = value; }
        }
    }
}
