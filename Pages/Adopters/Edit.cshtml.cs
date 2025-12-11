using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdoptions.Data;
using PetAdoptions.Models;
using System.Threading.Tasks;
using System.Linq;

namespace PetAdoptions.Pages.Adopters;

    public class EditModel : PageModel
    {
        private readonly PetAdoptionContext _context;

        public EditModel(PetAdoptionContext context)
        {
            _context = context;
        }

        // Adopter property.
        [BindProperty]
        public Adopter Adopter { get; set; } = default!;

        // Handles GET requests for adopters.
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            // Gets adopter from database.
            Adopter? adopter = await _context.Adopters.FindAsync(id);
            if (adopter == null) return NotFound();

            Adopter = adopter;

            return Page();
        }

        // Handles POST requests. 
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Marks entity as modified for EF Core updating.
            _context.Attach(Adopter).State = EntityState.Modified;

            try
            {
                // Saves changes.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // In case another user has deleted record already.
                if (!_context.Adopters.Any(a => a.AdopterId == Adopter.AdopterId))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("Index");
        }
    }