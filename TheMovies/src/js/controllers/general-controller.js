let generalControllers = {
    get(templates) {
        return {
            home() {

                $('#container').html("Home page");

                // Promise.all([
                //     blogService.getAllCategories(),
                //     blogService.getAllPosts(),
                //     templates.get('home')
                // ])
                // .then(([categories, posts, template]) => {
                //     let compiledTemplate = Handlebars.compile(template),
                //         data = {},
                //         html;

                //     data.categories = categories.val();
                //     data.posts = posts.val();
                //     html = compiledTemplate(data);
                //     $('#container').html(html);

                //     $('.main-nav li.active').removeClass('active');
                //     $('a[href^="#/home"]').parent('li').addClass('active');
                //     window.scrollTo(0, 0);
                // })
                // .catch((error) => console.log(error));
            }
        }
    }
};