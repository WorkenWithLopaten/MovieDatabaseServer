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
            }
        }
    }
};