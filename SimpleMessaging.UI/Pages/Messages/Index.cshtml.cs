using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SimpleMessaging.UI.Pages.Messages
{
    using SimpleMessaging.Message.Repository.Models;

    public class IndexModel : PageModel
    {
        private readonly SimpleMessaging.Message.Repository.SQL.Data.MessageRepositoryDbContext _context;

        public IndexModel(SimpleMessaging.Message.Repository.SQL.Data.MessageRepositoryDbContext context)
        {
            _context = context;
        }

        public IList<Message> Message { get;set; } = default!;

        public async Task OnGetAsync()
        {
            // TODO: Add Category and Creator filtering (prototype hard coded below)
            if (_context.Messages != null)
            {
                Message = await _context.Messages.Where(x => x.Category.Name == "Reasons" && x.Creator.FirstName == "Greg")
                .Include(m => m.Category)
                .Include(m => m.Creator).ToListAsync();
            }
        }
    }
}
