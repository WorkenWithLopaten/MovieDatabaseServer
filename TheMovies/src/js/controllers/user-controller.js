let userControllers = {
    get(templates) {
        return {
            register() {
                templates.get('register')
                    .then(template => {
                        let compiledTemplate = Handlebars.compile(template);
                        $('#container').html(compiledTemplate);
                    })

            },
            signup() {
                //do logic....
                location.hash = "#/home";
            }
        }
    }
};