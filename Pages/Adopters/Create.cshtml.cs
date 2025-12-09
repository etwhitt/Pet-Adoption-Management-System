using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PetAdoptions.Data;
using PetAdoptions.Models;
using System.Threading.Tasks;

namespace PetAdoptions.Pages.Adopters
{
    public class CreateModel : PageModel
    {
        private readonly PetAdoptionContext _context;

        public CreateModel(PetAdoptionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Adopter Adopter { get; set; } = new();

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Adopters.Add(Adopter);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
