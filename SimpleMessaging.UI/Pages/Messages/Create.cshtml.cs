using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SimpleMessaging.UI.Pages.Messages
{
    using SimpleMessaging.Message.Repository.Models;

    public class CreateModel : PageModel
    {
        private readonly SimpleMessaging.Message.Repository.SQL.Data.MessageRepositoryDbContext _context;

        public CreateModel(SimpleMessaging.Message.Repository.SQL.Data.MessageRepositoryDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
        ViewData["CreatorId"] = new SelectList(_context.Creators, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Message Message { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Messages.Add(Message);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
