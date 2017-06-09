let generalControllers = {
    get(userService, templates) {
        return {
            home() {
                Promise.all([
                        templates.get('home'),
                        userService.getAllUsers()
                    ])
                    .then(([template, users]) => {
                        let compiledTemplate = Handlebars.compile(template);
                        console.log(users);
                        let html = compiledTemplate(users);
                        $('#container').html(html);
                    });
            }
        }
    }
};

$('#container').on('click', '#user-container', function() {
    var $user = $(this);
    var userId = $user.attr('user-id');

    localStorage.userId = userId;

    var userUsername = $user.find('.username-name').html();

    localStorage.username = userUsername;

    console.log(userId);

    $('.logged-user-demo').html('Welcome, ' + localStorage.username + '!');
})