using System.Drawing;
using System.IO;

namespace WebApp.Adapter.Services
{
    public class AdvanceImageProcessAdapter: IImageProcess
    {
        private readonly IAdvanceImageProcess _advanceImageProcess;

        public AdvanceImageProcessAdapter(IAdvanceImageProcess advanceImageProcess)
        {
            _advanceImageProcess = advanceImageProcess;
        }

        public void AddWatermark(string watermark, string fileName, Stream imageStream)
        {
            _advanceImageProcess.AddWatermarkImage(imageStream, watermark, $"wwwroot/watermarks/{fileName}", Color.FromArgb(128, 255, 255, 255), Color.FromArgb(120, 255, 255, 255));
        }
    }
}
