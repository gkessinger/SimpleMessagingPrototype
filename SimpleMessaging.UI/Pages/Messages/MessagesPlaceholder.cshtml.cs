using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace SimpleMessaging.UI.Pages
{
    public class MessagePageModel : PageModel
    {
        private readonly ILogger<MessagePageModel> _logger;

        public MessagePageModel(ILogger<MessagePageModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}