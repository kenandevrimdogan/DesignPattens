using ClosedXML.Excel;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Command.Commands
{
    public class ExcelFile<T>
    {
        public readonly List<T> _list;

        public string FileName => $"{typeof(T).Name}.xlsx";

        public string FileType => $"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public ExcelFile(List<T> list)
        {
            _list = list;
        }

        public Task<MemoryStream> Create()
        {
            var workBook = new XLWorkbook();
            var dataSet = new DataSet();

            dataSet.Tables.Add(GetTable());
            workBook.Worksheets.Add(dataSet);

            var excelMemory = new MemoryStream();

            workBook.SaveAs(excelMemory);

            return Task.FromResult(excelMemory);
        }

        public DataTable GetTable()
        {
            var dataTable = new DataTable();

            var type = typeof(T);

            type.GetProperties().ToList().ForEach(x => dataTable.Columns.Add(x.Name, x.PropertyType));

            _list.ForEach(x =>
            {
                var values = typeof(T).GetProperties().Select(propertyInfo => propertyInfo.GetValue(x, null)).ToArray();

                dataTable.Rows.Add(values);
            });

            return dataTable;
        }

    }
}
