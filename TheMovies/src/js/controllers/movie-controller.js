let movieControllers = {
    get(movieService, templates) {
        return {
            getAll() {
                $('#container').html('');
                movieService.getAllMovies();
            }
        }
    }
};