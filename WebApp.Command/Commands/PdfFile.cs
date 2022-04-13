using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Command.Commands
{
    public class PdfFile<T>
    {
        public readonly List<T> _list;
        public readonly HttpContext _httpContext;

        public string FileName => $"{typeof(T).Name}.pdf";

        public string FileType => $"application/octet-stream";

        public PdfFile(List<T> list, HttpContext httpContext)
        {
            _list = list;
            _httpContext = httpContext;
        }

        public Task<MemoryStream> Create()
        {
            var type = typeof(T);

            var stringBuilder = new StringBuilder();

            stringBuilder.Append($@"
            <html>
                <head></head>
                <body>
                    <div class='text-center'>
                        <h1>{type.Name} Table</h1>
                        <table class='table table-striped' align='center'>
                            <thead>
                                <tr>
                                    {
                                       CreateThead()
                                    }
                                </tr>
                            </thead>
                            <tbody>
                                  {
                                    CreateTbody()
                                  } 
                            </tbody>
                        </table>
                    </div>
                </body>
            </html>");


            var htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings =
                {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                },
                Objects = {
                        new ObjectSettings() {
                            PagesCount = true,
                            HtmlContent = stringBuilder.ToString(),
                            WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet= Path.Combine(Directory.GetCurrentDirectory(), "wwroot/lib/bootstrap/dist/css/bootstrap.min.css") },
                            HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                        }
                    }
            };

            var converter = _httpContext.RequestServices.GetRequiredService<IConverter>();

            byte[] pdf = converter.Convert(htmlToPdfDocument);

            var pdfMemory = new MemoryStream(pdf);

            return Task.FromResult(pdfMemory);
        }

        private string CreateThead()
        {
            var stringBuilder = new StringBuilder();

            var names = typeof(T).GetProperties().Select(propertyInfo => propertyInfo.Name).ToList();

            stringBuilder.Append("<tr>");

            names.ForEach(name =>
            {
                stringBuilder.Append($"<th>{name}<th>");
            });

            stringBuilder.Append("</tr>");

            return stringBuilder.ToString();
        }

        private string CreateTbody()
        {
            var stringBuilder = new StringBuilder();

            foreach (var item in _list)
            {
                var values = typeof(T).GetProperties().Select(propertyInfo => propertyInfo.GetValue(item, null)).ToList();

                stringBuilder.Append("<tr>");

                values.ForEach(value =>
                {
                    stringBuilder.Append($"<td>{value}<td>");
                });

                stringBuilder.Append("</tr>");
            }

            return stringBuilder.ToString();
        }
    }
}
