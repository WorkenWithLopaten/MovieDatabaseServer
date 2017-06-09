using Owin;
using Microsoft.Owin;
using Ninject;
using MovieDb.Data;
using WebAPI.Importer;
using WebAPI.App_Start;
using System.Configuration;
using PDFReportGeneration;

[assembly: OwinStartup(typeof(WebAPI.Startup))]
namespace WebAPI
{
    
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            if (bool.Parse(ConfigurationManager.AppSettings["importJson"]))
            {
                var context = new MoviesContext();
                var jsonImporter = new JsonImporter(context);
                jsonImporter.ImportMovies();
                jsonImporter.ImportUsers();
            }
            if (bool.Parse(ConfigurationManager.AppSettings["generateReport"]))
            {
                PdfReporter.GeneratePDFAggregatedSalesReport();
            }
        }
    }
}
