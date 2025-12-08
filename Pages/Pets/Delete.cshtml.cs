using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdoptions.Data;
using PetAdoptions.Models;
using System.Threading.Tasks;

namespace PetAdoptions.Pages.Pets
{
    public class DeleteModel : PageModel
    {
        private readonly PetAdoptionContext _context;

        public DeleteModel(PetAdoptionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pet? Pet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Pet = await _context.Pets
                .Include(p => p.Adopter)
                .FirstOrDefaultAsync(p => p.PetId == id);

            if (Pet == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Pet == null) return NotFound();

            var petToDelete = await _context.Pets.FindAsync(Pet.PetId);
            if (petToDelete != null)
            {
                _context.Pets.Remove(petToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
