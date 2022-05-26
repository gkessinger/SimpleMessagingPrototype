using Microsoft.AspNetCore.Mvc;

namespace SimpleMessaging.UI.Areas.Documentation.Controllers
{
    [Area("Documentation")]
    [Route("[controller]")]
    public class DocumentationController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;

        public DocumentationController(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        public IActionResult Index()
        {
            // TODO: list documents
            return View();
        }

        [Route("{fileName}")]
        [Produces("application/pdf")]
        public FileResult Index([FromRoute] string fileName)
        {
            var root = _hostingEnvironment.ContentRootPath;
            var path = Path.Combine(root, Path.Combine("Documentation", fileName));
            FileStream fs = new(path, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);

            byte[] databytes = br.ReadBytes((int)fs.Length);

            fs.Close();

            FileResult fileResult = new FileContentResult(databytes, "application/pdf")
            {
                FileDownloadName = fileName                 
            };

            var contentLength = databytes.Length;

            Response.Headers.Add("Content-Type", "application/pdf");
            Response.Headers.Add("Content-Length", $"{contentLength}");
            Response.Headers.Add("Content-Disposiiton", "inline");

            return fileResult;
        }
    }
}