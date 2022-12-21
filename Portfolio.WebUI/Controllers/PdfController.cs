using Microsoft.AspNetCore.Mvc;
using Portfolio.Domain.Models.DataContext;
using System.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using BigOn.Domain.AppCode.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.WebUI.Controllers
{
    [AllowAnonymous]
    public class PdfController : Controller
    {
        private readonly PortfolioDbContext db;

        public PdfController(PortfolioDbContext db)
        {
            this.db = db;
        }
        [HttpPost]
        public IActionResult Pdf()
        {
            var user = db.Users.Include(x=>x.Skills).Include(x=>x.Speciality).FirstOrDefault(x=>x.Id==2);
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .Text(user.Name)
                        .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);

                            x.Item().Text(user.Bio.ToPlainText());
                            x.Spacing(10);
                            foreach (var skill in user.Skills)
                            {
                            x.Item().Text($"{skill.Name}:{skill.Description.ToPlainText()}");
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            })
            .GeneratePdf($"C:{user.Name}.pdf");
            var file = System.IO.File.ReadAllBytes($"C:{user.Name}.pdf");
            var content = new System.IO.MemoryStream(file);
            var contentType = "APPLICATION/octet-stream";
            var fileName = $"{user.Name}.pdf";
            return File(content, contentType, fileName);

        }
    }
}
