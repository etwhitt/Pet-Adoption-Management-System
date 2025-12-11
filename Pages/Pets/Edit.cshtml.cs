using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdoptions.Data;
using PetAdoptions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetAdoptions.Pages.Pets;

    public class EditModel : PageModel
    {
        private readonly PetAdoptionContext _context;

        public EditModel(PetAdoptionContext context)
        {
            _context = context;
        }

        // Pet being edited.
        [BindProperty]
        public Pet? Pet { get; set; } = default!;

        // List of adopters for dropdown.
        public List<Adopter> Adopters { get; set; } = new List<Adopter>();

        // Handles GET requests to display form for specific pet.
        // Shows pet information as well as adopter information, if available.
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Pet = await _context.Pets
                .Include(p => p.Adopter)
                .FirstOrDefaultAsync(p => p.PetId == id);

            if (Pet == null) return NotFound();

            Adopters = await _context.Adopters.ToListAsync();

            return Page();
        }

        // Handles POST requests when form submitted.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Re-loads dropdown if validation fails.
                Adopters = await _context.Adopters.ToListAsync();
                return Page();
            }

            _context.Attach(Pet!).State = EntityState.Modified;

            try
            {
                // Saves edits to database.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Pets.Any(p => p.PetId == Pet!.PetId))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("Index");
        }
    }