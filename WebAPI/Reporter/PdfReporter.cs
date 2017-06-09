namespace PDFReportGeneration
{

    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using MovieDb.Data;
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web.Hosting;
    public static class PdfReporter
    {
        private const float FontSize = 10f;
        private const float LargeFontSize = 13f;

        public static void GeneratePDFAggregatedSalesReport()
        {
            
            //string executionFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string executionFolder= HostingEnvironment.ApplicationPhysicalPath;
            string salesReportFile = Path.Combine(executionFolder, "UsersByCityReport.pdf");

            var fs = new FileStream(salesReportFile, FileMode.Create);

            var document = new Document(PageSize.A4, 25, 25, 30, 30);

            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            var table = new PdfPTable(5);

            table.TotalWidth = 545f;
            table.LockedWidth = true;

            float[] widths = new float[] { 0.5f, 0.5f, 0.75f, 0.75f, 0.5f };
            table.SetWidths(widths);

            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            var cellTitle = new PdfPCell(new Phrase("All Users Grouped by City", new Font(Font.HELVETICA, LargeFontSize, Font.BOLD)))
            {
                Colspan = 5,
                HorizontalAlignment = 1
            };

            cellTitle.PaddingBottom = 10f;
            cellTitle.PaddingLeft = 10f;
            cellTitle.PaddingTop = 4f;
            table.AddCell(cellTitle);

            using (var msSQLServerContext = new MoviesContext())
            {
                var cityUserGroups = msSQLServerContext
                                        .Users
                                        .Select(x => new
                                        {
                                            UserName = x.UserName,
                                            FullName = x.UserData.FirstName + " " + x.UserData.LastName,
                                            Country = x.UserData.Adress.City.Country.Name,
                                            City = x.UserData.Adress.City.Name,
                                            MoviesOpinion = x.Likes.Count() + x.Dislikes.Count()
                                        })
                                        .ToList()
                                        .OrderBy(x => x.City)
                                        .GroupBy(x => x.City);


                foreach (var cityUsers in cityUserGroups)
                {
                    var CityName = cityUsers.Key;
                    bool first = true;
                    var totalOpinionsByCity = cityUsers.Sum(x => x.MoviesOpinion);

                    foreach (var user in cityUsers)
                    {
                        if (first)
                        {
                            WriteTableHeader(table, CityName, totalOpinionsByCity);
                            first = false;
                        }

                        var cellCity = new PdfPCell(new Phrase(user.City, new Font(Font.HELVETICA, FontSize, Font.NORMAL)));
                        cellCity.PaddingBottom = 10f;
                        cellCity.PaddingLeft = 10f;
                        cellCity.PaddingTop = 4f;
                        table.AddCell(cellCity);

                        var cellLocation = new PdfPCell(new Phrase(user.Country, new Font(Font.HELVETICA, FontSize, Font.NORMAL)));
                        cellLocation.PaddingBottom = 10f;
                        cellLocation.PaddingLeft = 10f;
                        cellLocation.PaddingTop = 4f;
                        table.AddCell(cellLocation);

                        var cellUserName = new PdfPCell(new Phrase(user.UserName, new Font(Font.HELVETICA, FontSize, Font.NORMAL)));
                        cellUserName.PaddingBottom = 10f;
                        cellUserName.PaddingLeft = 10f;
                        cellUserName.PaddingTop = 4f;
                        table.AddCell(cellUserName);

                        var cellFullName = new PdfPCell(new Phrase(user.FullName, new Font(Font.HELVETICA, FontSize, Font.NORMAL)));
                        cellFullName.PaddingBottom = 10f;
                        cellFullName.PaddingLeft = 10f;
                        cellFullName.PaddingTop = 4f;
                        table.AddCell(cellFullName);

                        var cellSum = new PdfPCell(new Phrase(user.MoviesOpinion.ToString(), new Font(Font.HELVETICA, FontSize, Font.NORMAL)));
                        cellSum.PaddingBottom = 10f;
                        cellSum.PaddingLeft = 10f;
                        cellSum.PaddingTop = 4f;
                        table.AddCell(cellSum);

                    }
                }

                document.Add(table);

                // Close the document
                document.Close();

                // Close the writer instance
                writer.Close();

                // Always close open filehandles explicity
                fs.Close();
            }
        }

        private static void WriteTableHeader(PdfPTable table, string CityName,int totalCount)
        {
            var cellCityName = new PdfPCell(new Phrase(CityName, new Font(Font.HELVETICA, LargeFontSize, Font.BOLD)))
            {
                Colspan = 3,
                HorizontalAlignment = 0
            };

            cellCityName.BackgroundColor = new Color(217, 217, 217);
            cellCityName.PaddingBottom = 10f;
            cellCityName.PaddingLeft = 10f;
            cellCityName.PaddingTop = 4f;
            table.AddCell(cellCityName);

            var totalCountCell = new PdfPCell(new Phrase("TotalCount: " + totalCount, new Font(Font.HELVETICA, FontSize, Font.BOLD)))
            {
                Colspan = 2,
                HorizontalAlignment = 3
            };

            totalCountCell.BackgroundColor = new Color(217, 217, 217);
            totalCountCell.PaddingBottom = 10f;
            totalCountCell.PaddingLeft = 100f;
            totalCountCell.PaddingTop = 4f;
            table.AddCell(totalCountCell);

            var cellCity = new PdfPCell(new Phrase("City", new Font(Font.HELVETICA, FontSize, Font.BOLD)));
            cellCity.BackgroundColor = new Color(217, 217, 217);
            cellCity.PaddingBottom = 10f;
            cellCity.PaddingLeft = 10f;
            cellCity.PaddingTop = 4f;
            table.AddCell(cellCity);

            var cellLocation = new PdfPCell(new Phrase("Country", new Font(Font.HELVETICA, FontSize, Font.BOLD)));
            cellLocation.BackgroundColor = new Color(217, 217, 217);
            cellLocation.PaddingBottom = 10f;
            cellLocation.PaddingLeft = 10f;
            cellLocation.PaddingTop = 4f;
            table.AddCell(cellLocation);

            var cellUserName = new PdfPCell(new Phrase("User Name", new Font(Font.HELVETICA, FontSize, Font.BOLD)));
            cellUserName.BackgroundColor = new Color(217, 217, 217);
            cellUserName.PaddingBottom = 10f;
            cellUserName.PaddingLeft = 10f;
            cellUserName.PaddingTop = 4f;
            table.AddCell(cellUserName);

            var cellFullName = new PdfPCell(new Phrase("FullName", new Font(Font.HELVETICA, FontSize, Font.BOLD)));
            cellFullName.BackgroundColor = new Color(217, 217, 217);
            cellFullName.PaddingBottom = 10f;
            cellFullName.PaddingLeft = 10f;
            cellFullName.PaddingTop = 4f;
            table.AddCell(cellFullName);

            var cellSum = new PdfPCell(new Phrase("Movie Opinions Given", new Font(Font.HELVETICA, FontSize, Font.BOLD)));
            cellSum.BackgroundColor = new Color(217, 217, 217);
            cellSum.PaddingBottom = 10f;
            cellSum.PaddingLeft = 10f;
            cellSum.PaddingTop = 4f;
            table.AddCell(cellSum);
        }
    }
}
