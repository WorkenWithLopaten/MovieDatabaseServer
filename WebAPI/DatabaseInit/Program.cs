
using PDFReportGeneration;


namespace DatabaseInit
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MoviesContext, ConfigurationMovieDB>());
            //var db = new MoviesContext();
            //db.Adresses.ToList();
            PdfReporter.GeneratePDFAggregatedSalesReport();
        }
    }
}
