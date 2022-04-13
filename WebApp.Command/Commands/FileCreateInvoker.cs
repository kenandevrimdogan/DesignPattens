using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Command.Commands
{
    public class FileCreateInvoker
    {
        private ITableActionCommand _tableActionCommand;
        private List<ITableActionCommand> tableActionCommands = new();

        public void SetCommand(ITableActionCommand tableActionCommand)
        {
            _tableActionCommand = tableActionCommand;
        }

        public void AddCommand(ITableActionCommand tableActionCommand)
        {
            tableActionCommands.Add(tableActionCommand);
        }

        public Task<IActionResult> CreateFile()
        {
            return _tableActionCommand.Execute();
        }

        public async Task<List<IActionResult>> CreateFiles()
        {
            List<IActionResult> list = new();

            foreach (ITableActionCommand tableActionCommand in tableActionCommands)
            {
                list.Add(await tableActionCommand.Execute());
            }

            return await Task.FromResult(list);
        }
    }
}
