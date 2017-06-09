let router = new Navigo(null, true);

let generalControllerInstance = generalControllers.get(userService, templates);
let userControllerInstance = userControllers.get(userService, templates);
let moviesControllerInstance = movieControllers.get(movieService, templates);

router.on({
    "home": generalControllerInstance.home,
    "register": userControllerInstance.register,
    "signup": userControllerInstance.signup,
    "movies": moviesControllerInstance.listMovies,
    "requestAdd": moviesControllerInstance.requestAdd,
    "addmovie": moviesControllerInstance.add,
    "/": (() => {
        router.navigate("/home")
    })
}).resolve();