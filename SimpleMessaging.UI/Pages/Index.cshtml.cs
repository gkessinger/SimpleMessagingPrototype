using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SimpleMessaging.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public FileResult GetPDF(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);

            byte[] databytes = br.ReadBytes((int)fs.Length);

            FileResult fileResult = new FileContentResult(databytes, "application/pdf");
            fileResult.FileDownloadName = fileName;

            var contentLength = databytes.Length;
            Response.Headers.Append("Content-Length", contentLength.ToString());
            Response.Headers.Append("Content-Disposition", "inline; filename=" + fileName);

            return fileResult;
        }
    }
}