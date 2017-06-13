using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDb.Models.Actors
{
    public class MoviesLite
    {
        public MoviesLite()
        {
            Actors = new HashSet<Actor>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Name { get; set; }
        public virtual ICollection<Actor> Actors { get; set; }
    }
}
