var movieService = {

    getAllMovies() {
        var header = {};
        header["contentType"] = 'application/json';

        return requester.getSql('http://localhost:51443/api/movies/GetTopLikedMovies/10', header);
    },
    dislikeMovie(url, body) {
        var header = {};
        header["contentType"] = 'application/json';
        var content = "application/json";

        requester.post(url, header, body, content)
            .then(x => {
                location.reload();
            })
    },
    likeMovie(url, body) {
        var header = {};
        header["contentType"] = 'application/json';
        var content = "application/json";

        requester.post(url, header, body, content)
            .then(x => {
                location.reload();
            })
            .then(x => {
                $('.logged-user-demo').html('Welcome, ' + localStorage.username + '!');
            })
    },
    addMovie(url, body) {
        var header = {};
        header["contentType"] = 'application/json';
        var content = "application/json";

        requester.post(url, header, body, content)
            .then(x => {
                location.hash = "#/home";
                alert("Succsefully added movie!");
            });
    },
    getAllByName(input) {
        var header = {};
        header["contentType"] = 'application/json';

        return requester.getSql('http://localhost:51443/api/movies/GetAll', header);
    }
};