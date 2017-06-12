using MovieDb.Data;
using MovieDb.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Models.Comments;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CommentsController : ApiController
    {
        private readonly IRepository<Comment> comments;
        private readonly IRepository<User> users;
        private readonly IRepository<Movie> movies;

        public CommentsController(IRepository<Comment> comments, IRepository<User> users, IRepository<Movie> movies)
        {
            this.comments = comments;
            this.users = users;
            this.movies = movies;
        }
        public IHttpActionResult CreateComment(CreateCommentModel model)
        {
            var text = model.Text;
            var userId = model.UserId;
            var movieId = model.MovieId;

            if (string.IsNullOrEmpty(text))
            {
                return this.BadRequest("Comment can`t be empty");
            }

            var currentUser = this.GetUser(userId);

            if (currentUser == null)
            {
                return this.BadRequest("No such user");
            }

            var currentMovie = this.GetMovie(movieId);

            if (currentMovie == null)
            {
                return this.BadRequest("No such movie");
            }

            var comment = new Comment
            {
                CommentText = text,
                UserId = userId,
                MovieId = currentMovie.Id
            };

            try
            {
                this.comments.Add(comment);
                this.comments.SaveChanges();
            }
            catch
            {
                return this.BadRequest("Can`t save this comment");
            }

            var lastCommentId = this.comments.All.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn).FirstOrDefault().Id;

            return this.Ok(lastCommentId);
        }

        [HttpPut]
        public IHttpActionResult DeleteComment(DeleteCommentModel comment)
        {
            int comentId = comment.CommentId;
            var currentComent = this.comments.All.Where(x => x.Id == comentId && x.isDeleted == false).FirstOrDefault();
            if (currentComent == null)
            {
                return this.BadRequest("comment is either deleted or doesn`t exist");
            }
            try
            {
                currentComent.isDeleted = true;
                this.comments.Update(currentComent);
                this.comments.SaveChanges();
            }
            catch
            {
                return this.BadRequest("Comment can`t be deleted ");
            }

            return this.Ok();
        }

        public IHttpActionResult GetAllCommentsForAMovie(int id)
        {
            var currentMovie = this.GetMovie(id);
            if (currentMovie == null)
            {
                return this.BadRequest("no such movie");
            }
            var allMovies = this.comments.All.Where(x => x.MovieId == currentMovie.Id && x.isDeleted == false).ToList();

            return this.Ok(allMovies);
        }

        public IHttpActionResult GetAllCommentsFromAUser(int id)
        {
            var currentUser = this.GetUser(id);
            if (currentUser == null)
            {
                return this.BadRequest("no such user");
            }
            var allMovies = this.comments.All.Where(x => x.UserId == id && x.isDeleted == false).ToList();

            return this.Ok(allMovies);
        }

        private User GetUser(int userId)
        {
            return this.users.All.Where(x => x.Id == userId).FirstOrDefault();
        }
        private Movie GetMovie(int movieId)
        {
            return this.movies.All.Where(x => x.Id == movieId).FirstOrDefault();
        }
    }

}
