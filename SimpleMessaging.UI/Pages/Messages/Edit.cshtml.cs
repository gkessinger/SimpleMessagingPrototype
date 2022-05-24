using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace SimpleMessaging.UI.Pages.Messages
{
    using SimpleMessaging.Message.Repository.Models;

    public class EditModel : PageModel
    {
        private readonly SimpleMessaging.Message.Repository.SQL.Data.MessageRepositoryDbContext _context;

        public EditModel(SimpleMessaging.Message.Repository.SQL.Data.MessageRepositoryDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Message Message { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message =  await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            Message = message;
           ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
           ViewData["CreatorId"] = new SelectList(_context.Creators, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(Message.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MessageExists(int id)
        {
          return _context.Messages.Any(e => e.Id == id);
        }
    }
}
