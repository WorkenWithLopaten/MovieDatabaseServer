[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebAPI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebAPI.App_Start.NinjectWebCommon), "Stop")]

namespace WebAPI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using MovieDb.Data;
    using MovieDb.Models;
    using SqlLiteData.Models;
    using SqlLiteData;
    using PostgreData.Models;
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel
                .Bind<IMoviesContext>()
                .To<MoviesContext>()
                .InRequestScope();
            kernel
               .Bind<IActorsContext>()
               .To<ActorsContext>()
               .InRequestScope();
            kernel
               .Bind<ICinemaContext>()
               .To<CinemaContext>()
               .InRequestScope();

            kernel.Bind(typeof(IRepository<Movie>)).To(typeof(EfGenericRepository<Movie>));
            kernel.Bind(typeof(IRepository<Adress>)).To(typeof(EfGenericRepository<Adress>));
            kernel.Bind(typeof(IRepository<City>)).To(typeof(EfGenericRepository<City>));
            kernel.Bind(typeof(IRepository<Comment>)).To(typeof(EfGenericRepository<Comment>));
            kernel.Bind(typeof(IRepository<Country>)).To(typeof(EfGenericRepository<Country>));
            kernel.Bind(typeof(IRepository<Dislike>)).To(typeof(EfGenericRepository<Dislike>));
            kernel.Bind(typeof(IRepository<Like>)).To(typeof(EfGenericRepository<Like>));
            kernel.Bind(typeof(IRepository<User>)).To(typeof(EfGenericRepository<User>));
            kernel.Bind(typeof(IRepository<UserData>)).To(typeof(EfGenericRepository<UserData>));
            kernel.Bind(typeof(IRepository<Actors>)).To(typeof(EfGenericAndSqlLite<Actors>));
            kernel.Bind(typeof(IRepository<MoviesLite>)).To(typeof(EfGenericAndSqlLite<MoviesLite>));
            kernel.Bind(typeof(IRepository<CinemaCity>)).To(typeof(EfGenericPostgre<CinemaCity>));
            kernel.Bind(typeof(IRepository<Cinema>)).To(typeof(EfGenericPostgre<Cinema>));

        }        
    }
}


