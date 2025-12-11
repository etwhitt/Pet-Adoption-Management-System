using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdoptions.Data;
using PetAdoptions.Models;
using System.Threading.Tasks;

namespace PetAdoptions.Pages.Pets;

    public class DetailsModel : PageModel
    {
        private readonly PetAdoptionContext _context;

        public DetailsModel(PetAdoptionContext context)
        {
            _context = context;
        }

        // Pet to display details of.
        public Pet? Pet { get; set; } = default!;

        // Handles GET requests.
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            // Gets pet from database along with adopter, if available.
            Pet = await _context.Pets
                .Include(p => p.Adopter)
                .FirstOrDefaultAsync(p => p.PetId == id);

            if (Pet == null) return NotFound();

            return Page();
        }
    }