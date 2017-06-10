using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDb.Models.Actors
{
    public class Actor
    {
        public Actor()
        {
            Movies = new HashSet<MoviesLite>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<MoviesLite> Movies { get; set; }
    }
}
