using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdoptions.Data;
using PetAdoptions.Models;
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

        public IList<Adopter> Adopters { get; set; } = new List<Adopter>();

        public async Task OnGetAsync()
        {
            Adopters = await _context.Adopters.ToListAsync();
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
