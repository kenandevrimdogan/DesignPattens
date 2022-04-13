using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApp.Command.Commands
{
    public class CreateExcelTableActionCommand<T> : ITableActionCommand
    {
        private readonly ExcelFile<T> _excelFile;

        public CreateExcelTableActionCommand(ExcelFile<T> excelFile)
        {
            _excelFile = excelFile;
        }

        public async Task<IActionResult> Execute()
        {
            var excelMemoryStream = await _excelFile.Create();

            var fileContentResult = new FileContentResult(excelMemoryStream.ToArray(), _excelFile.FileType)
            {
                FileDownloadName = _excelFile.FileName
            };

            return await Task.FromResult(fileContentResult);
        }
    }
}
