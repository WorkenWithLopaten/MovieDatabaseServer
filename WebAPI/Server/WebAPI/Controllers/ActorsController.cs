//namespace WebAPI.Controllers
//{
//    using Models.Users;
//    using MovieDb.Data;
//    using MovieDb.Models;
//    using System;
//    using System.Data.Entity;
//    using System.Linq;
//    using System.Net.Http;
//    using System.Web.Http;
//    using System.Web.Http.Cors;
//    using SqLite;

//    [EnableCors(origins: "*", headers: "*", methods: "*")]
//    public class ActorsController : ApiController
//    {
//        private MoviesEntities2hh db;
//        public ActorsController()

//        {
//            this.db = new MoviesEntities2hh();
//        }

//        [HttpGet]
//        public IHttpActionResult All()
//        {
//            var actors = this.db.Actors.ToList();

//            return this.Ok(actors);
//        }

//        [HttpGet]
//        public IHttpActionResult AllMoviesForActorId(int id)
//        {
//            var actors = this.db.ActorsMovies.Where(x => x.ActorsId == id).Select(x => x.Movies.Name);

//            return this.Ok(actors);
//        }

//    }
//}
