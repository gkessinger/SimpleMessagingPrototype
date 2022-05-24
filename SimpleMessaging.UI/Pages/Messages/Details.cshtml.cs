using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SimpleMessaging.UI.Pages.Messages
{
    using SimpleMessaging.Message.Repository.Models;

    public class DetailsModel : PageModel
    {
        private readonly SimpleMessaging.Message.Repository.SQL.Data.MessageRepositoryDbContext _context;

        public DetailsModel(SimpleMessaging.Message.Repository.SQL.Data.MessageRepositoryDbContext context)
        {
            _context = context;
        }

      public Message Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            else 
            {
                Message = message;
            }
            return Page();
        }
    }
}
