using System.IO;

namespace WebApp.Adapter.Services
{
    public interface IImageProcess
    {
        void AddWatermark(string watermark, string fileName, Stream imageStream);
    }
}
