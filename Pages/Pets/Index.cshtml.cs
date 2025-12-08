using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdoptions.Data;
using PetAdoptions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetAdoptions.Pages.Pets
{
    public class IndexModel : PageModel
    {
        private readonly PetAdoptionContext _context;

        public IndexModel(PetAdoptionContext context)
        {
            _context = context;
        }

        public IList<Pet> Pets { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Pets = await _context.Pets
                .Include(p => p.Adopter) // Include adopter info
                .ToListAsync();
        }
    }
}
