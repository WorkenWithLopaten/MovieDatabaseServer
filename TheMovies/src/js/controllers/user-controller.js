let userControllers = {
    get(userService, templates) {
        return {
            register() {
                templates.get('register')
                    .then(template => {
                        let compiledTemplate = Handlebars.compile(template);
                        $('#container').html(compiledTemplate);
                    })

            },
            signup() {

                var userName = $('.username').val();
                var firstName = $('.firstname').val();
                var lastName = $('.lastname').val();
                var email = $('.email').val();

                var returnRequired = $('.male').is(':checked') ? 1 : 0;

                if (returnRequired == 1) {
                    returnRequired = true;
                } else {
                    returnRequired = false;
                }

                var city = $('.city').val();
                var country = $('.country').val();
                var address = $('.address').val();

                var bodyObject = {
                    UserName: userName,
                    FirstName: firstName,
                    LastName: lastName,
                    Email: email,
                    isMale: returnRequired,
                    CityName: city,
                    CountryName: country,
                    Adress: address
                }

                var url = 'http://localhost:51443/api/users/register';

                userService.register(url, bodyObject)
                    .then(x => {
                        alert('User succesfully created!');
                        location.hash = '#/home';
                    })
            }
        }
    }
};