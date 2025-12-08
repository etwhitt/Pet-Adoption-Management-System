using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;       // For ToListAsync()
using PetAdoptions.Data;                  // Your PetAdoptionContext
using PetAdoptions.Models;                // Pet and Adopter classes
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetAdoptions.Pages.Pets
{
    public class CreateModel : PageModel
    {
        private readonly PetAdoptionContext _context;

        public CreateModel(PetAdoptionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pet Pet { get; set; } = default!;

        public List<Adopter> Adopters { get; set; } = new List<Adopter>();

        public async Task<IActionResult> OnGetAsync()
        {
            Adopters = await _context.Adopters.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Adopters = await _context.Adopters.ToListAsync();
                return Page();
            }

            _context.Pets.Add(Pet);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
