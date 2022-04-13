using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.Command.Commands
{
    public class CreatePdfTableActionCommand<T> : ITableActionCommand
    {
        private readonly PdfFile<T> _pdfFile;

        public CreatePdfTableActionCommand(PdfFile<T> pdfFile)
        {
            _pdfFile = pdfFile;
        }

        public async Task<IActionResult> Execute()
        {
            var pdfMemoryStream = await _pdfFile.Create();

            var fileContentResult = new FileContentResult(pdfMemoryStream.ToArray(), _pdfFile.FileType)
            {
                FileDownloadName = _pdfFile.FileName
            };

            return await Task.FromResult(fileContentResult);
        }
    }
}
