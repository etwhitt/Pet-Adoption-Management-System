using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdoptions.Data;
using PetAdoptions.Models;
using System.Threading.Tasks;

namespace PetAdoptions.Pages.Adopters;

    public class DeleteModel : PageModel
    {
        private readonly PetAdoptionContext _context;

        public DeleteModel(PetAdoptionContext context)
        {
            _context = context;
        }

        // Property for adopter to be deleted.
        [BindProperty]
        public Adopter? Adopter { get; set; }

        // Handles GET requests.
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            // Gets adopter and their adopted pets.
            Adopter = await _context.Adopters
                .Include(a => a.Pets)
                .FirstOrDefaultAsync(a => a.AdopterId == id);

            if (Adopter == null) return NotFound();

            return Page();
        }

        // Handles POST requests. 
        public async Task<IActionResult> OnPostAsync()
        {
            if (Adopter == null) return NotFound();

            // Gets adopter and their adopted pets.
            var adopter = await _context.Adopters
                .Include(a => a.Pets)
                .FirstOrDefaultAsync(a => a.AdopterId == Adopter.AdopterId);

            if (adopter == null) return NotFound();

            // Prevents deletion of adopter if pets are assigned.
            if (adopter.Pets.Count > 0)
            {
                ModelState.AddModelError("", "Cannot delete an adopter who still has pets assigned.");
                return Page();
            }

            // Deletes and saves.
            _context.Adopters.Remove(adopter);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }