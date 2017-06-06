let router = new Navigo(null, true);

let generalControllerInstance = generalControllers.get(templates);
let userControllerInstance = userControllers.get(templates);
let moviesControllerInstance = movieControllers.get(movieService, templates);

location.hash = "#/home";

router.on({
    "home": generalControllerInstance.home,
    "register": userControllerInstance.register,
    "signup": userControllerInstance.signup,
    "movies": moviesControllerInstance.getAll
}).resolve();