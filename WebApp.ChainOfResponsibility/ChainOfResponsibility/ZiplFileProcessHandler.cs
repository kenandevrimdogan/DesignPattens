using System.IO;
using System.IO.Compression;

namespace WebApp.ChainOfResponsibility.ChainOfResponsibility
{
    public class ZiplFileProcessHandler<T> : ProcessHandler
    {
        public override object Handle(object processHandler)
        {
            var memoryStream = processHandler as MemoryStream;

            memoryStream.Position = 0;

            using (var packageStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(packageStream, ZipArchiveMode.Create, true))
                {
                    var zipFile = archive.CreateEntry($"{typeof(T).Name}.xlsx");

                    using (var zipEntry = zipFile.Open())
                    {
                        memoryStream.CopyTo(zipEntry);
                    }
                }
            }

            return base.Handle(memoryStream);
        }
    }
}
