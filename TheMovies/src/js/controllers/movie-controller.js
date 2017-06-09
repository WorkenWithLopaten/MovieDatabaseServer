let movieControllers = {
    get(movieService, templates) {
        return {
            getAll() {
                $('#container').html('');
                movieService.getAllMovies();
            },
            requestAdd() {
                templates.get('movie')
                    .then(template => {
                        let compiledTemplate = Handlebars.compile(template);
                        $('#container').html(compiledTemplate);
                    })
            },
            add() {
                let movieName = $('#inputinput').val();
                let url = 'http://localhost:51443/api/movies/add';
                let body = {
                    name: movieName
                }

                movieService.addMovie(url, body);
            },
            listMovies() {
                movieService.getAllMovies()
                    .then(movies => {
                        templates.get('movies')
                            .then(template => {
                                let compiledTemplate = Handlebars.compile(template);

                                $('#container').html(compiledTemplate(movies));
                            })
                    })
            }
        }
    }
};

$('#container').on('click', '#like', function() {
    let parent = $(this).parent();
    let movieId = parent.attr('data-Id');

    let likeMovieObject = { MovieId: movieId, Userid: localStorage.userId };

    movieService.likeMovie("http://localhost:51443/api/movies/LikeAMovie", likeMovieObject);
})

$('#container').on('click', '#dislike', function() {
    let parent = $(this).parent();
    let movieId = parent.attr('data-Id');

    let dislikeMovieObject = { MovieId: movieId, Userid: localStorage.userId };

    movieService.dislikeMovie("http://localhost:51443/api/movies/DislikeAMovie", dislikeMovieObject);
})

$('.search-movie-input').keyup(function(e) {
    if (e.which == 13) {
        var input = $('.search-movie-input').val();

        movieService.getAllByName(input)
            .then(movies => {
                var filteredMovies = [];
                for (movie of movies) {
                    if (movie.Name.toLowerCase().includes(input.toLowerCase())) {
                        filteredMovies.push(movie);
                    }
                }
                return filteredMovies;
            })
            .then(finalMovies => {
                templates.get('movies')
                    .then(template => {
                        let compiledTemplate = Handlebars.compile(template);
                        $('#container').html(compiledTemplate(finalMovies));
                    })
            })
    }
});