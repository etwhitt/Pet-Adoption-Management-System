using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;       
using PetAdoptions.Data;                  
using PetAdoptions.Models;                
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetAdoptions.Pages.Pets;

    public class CreateModel : PageModel
    {
        private readonly PetAdoptionContext _context;

        public CreateModel(PetAdoptionContext context)
        {
            _context = context;
        }

        // For creation of new pet.
        [BindProperty]
        public Pet Pet { get; set; } = default!;

        // List of adopters. 
        public List<Adopter> Adopters { get; set; } = new List<Adopter>();

        // Handles GET requests for creation page.
        public async Task<IActionResult> OnGetAsync()
        {
            Adopters = await _context.Adopters.ToListAsync();
            return Page();
        }

        // Handles POST requests for pet creation.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Adopters = await _context.Adopters.ToListAsync();
                return Page();
            }

            // Adds and saves pet to database.
            _context.Pets.Add(Pet);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }