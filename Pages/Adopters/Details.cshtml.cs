using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdoptions.Data;
using PetAdoptions.Models;
using System.Threading.Tasks;

namespace PetAdoptions.Pages.Adopters
{
    public class DetailsModel : PageModel
    {
        private readonly PetAdoptionContext _context;

        public DetailsModel(PetAdoptionContext context)
        {
            _context = context;
        }

        public Adopter? Adopter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Adopter = await _context.Adopters
                .Include(a => a.Pets)
                .FirstOrDefaultAsync(a => a.AdopterId == id);

            if (Adopter == null) return NotFound();

            return Page();
        }
    }
}
