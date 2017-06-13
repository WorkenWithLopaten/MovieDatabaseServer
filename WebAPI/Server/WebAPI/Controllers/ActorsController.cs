namespace WebAPI.Controllers
{
    using MovieDb.Data;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using SqlLiteData.Models;
    using System.Collections.Generic;
    using Models.Actors;
    using Models.Movies;
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ActorsController : ApiController
    {
        private IRepository<Actors> actors;
        private IRepository<MoviesLite> movies;
        public ActorsController(IRepository<Actors> actors, IRepository<MoviesLite> movies)

        {
            this.actors = actors;
            this.movies = movies;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var actors = this.actors.All.ToList();

            return this.Ok(actors.Select(x=>x.Name));
        }

        [HttpGet]
        public IHttpActionResult AllMoviesForActorId(int id)
        {
            ICollection<MoviesLite> movies = this.actors.All.Where(x => x.Id == id).SelectMany(y => y.Movies).ToList();

            return this.Ok(movies.Select(x=>x.Name));
        }

        [HttpPost]
        public IHttpActionResult Add(ActorAddModel actor)
        {
            var actors = this.actors.All.Where(x => x.Name == actor.Name).FirstOrDefault();
            if (actors != null)
            {
                return this.BadRequest("Already have this actor");
            }
            var actorToAdd = new Actors() { Name = actor.Name };
            try
            {
                this.actors.Add(actorToAdd);
                this.actors.SaveChanges();
            }
            catch
            {
                return this.BadRequest("Ups something went wrong");
            }

            return this.Ok("Actor Added successfully");
        }

        [HttpPost]
        public IHttpActionResult Delete(ActorAddModel actor)
        {
            var actors = this.actors.All.Where(x => x.Name == actor.Name).FirstOrDefault();
            if (actors == null)
            {
                return this.BadRequest("No such actor");
            }
            try
            {
                this.actors.Delete(actors);
                this.actors.SaveChanges();
            }
            catch
            {
                return this.BadRequest("Ups something went wrong");
            }

            return this.Ok("Actor Deleted successfully");
        }


        [HttpPost]
        public IHttpActionResult GetAllActorsForMovieName(MoviesCreateModel movie)
        {
            var movies = this.movies.All.Where(x => x.Name.Contains(movie.Name)).FirstOrDefault();
            if (movies == null)
            {
                return this.BadRequest("No such movie");
            }

            var actors = movies.Actors.Select(x => x.Name);

            return this.Ok(actors);
        }

        [HttpPost]
        public IHttpActionResult ConnectActorToMovie(ActorToMovieModel data)
        {
            var actor= this.actors.All.Where(x => x.Name.Contains(data.ActorName)).FirstOrDefault();
            var movie = this.movies.All.Where(x => x.Name.Contains(data.MovieName)).FirstOrDefault();

            if (actor == null)
            {
                return this.BadRequest("Actor doesn`t exist");
            }
            else if (movie == null)
            {
                return this.BadRequest("Movie doesn`t exist");
            }
            else
            {
                //first check if exists connection
                var alreadyExists = movie.Actors.Select(x => x.Name).Where(y => y.Contains(data.ActorName)).FirstOrDefault();

                if (alreadyExists != null)
                {
                    return this.BadRequest("Already connected");
                }
                else
                {
                    try
                    {
                        movie.Actors.Add(actor);
                        this.movies.SaveChanges();
                        return this.Ok("Actor added successfully");
                    }
                    catch
                    {
                        return this.BadRequest("Ups something went wrong");
                    }
                }
            }
        }



    }
}
