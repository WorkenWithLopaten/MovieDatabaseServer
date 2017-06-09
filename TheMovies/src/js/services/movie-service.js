var movieService = {

    getAllMovies() {
        console.log("working movie service");
        //requester.getJSON("localhost/api/movies/getall");
    },

    getMovie(movieObject) {
        // do logic for finding film
    },


    addMovie(url, body) {
        var header = {};
        header["contentType"] = 'application/json';
        var content = "application/json";

        requester.post(url, header, body, content);
    }
};