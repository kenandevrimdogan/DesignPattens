using ClosedXML.Excel;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace WebApp.ChainOfResponsibility.ChainOfResponsibility
{
    public class ExcelProcessHandler<T> : ProcessHandler
    {
        private DataTable getTable(object o)
        {
            var table = new DataTable();
            var type = typeof(T);

            type.GetProperties().ToList().ForEach(p =>
            {
                table.Columns.Add(p.Name, p.PropertyType);
            });

            var list = o as List<T>;

            list.ForEach(t => { 
                var values = type.GetProperties().Select(propertyInfo => propertyInfo.GetValue(t, null)).ToList();

                table.Rows.Add(values);
            });

            return table;
        }

        public override object Handle(object processHandler)
        {
            var wb = new XLWorkbook();
            var ds = new DataSet();

            ds.Tables.Add(getTable(processHandler));

            wb.Worksheets.Add(ds);

            var memoryStream = new MemoryStream();

            wb.SaveAs(memoryStream);

            return base.Handle(memoryStream);
        }
    }
}
