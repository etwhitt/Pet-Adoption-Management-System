using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdoptions.Data;
using PetAdoptions.Models;
using System.Threading.Tasks;
using System.Linq;

namespace PetAdoptions.Pages.Adopters
{
    public class EditModel : PageModel
    {
        private readonly PetAdoptionContext _context;

        public EditModel(PetAdoptionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Adopter Adopter { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            // Use a nullable variable to satisfy the compiler
            Adopter? adopter = await _context.Adopters.FindAsync(id);
            if (adopter == null) return NotFound();

            // Safely assign to the non-nullable property
            Adopter = adopter;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Attach(Adopter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Adopters.Any(a => a.AdopterId == Adopter.AdopterId))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("Index");
        }
    }
}
