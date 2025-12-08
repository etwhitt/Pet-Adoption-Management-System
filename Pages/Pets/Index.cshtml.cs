using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdoptions.Data;
using PetAdoptions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetAdoptions.Pages.Pets
{
    public class IndexModel : PageModel
    {
        private readonly PetAdoptionContext _context;
        private const int PageSize = 10;

        public IndexModel(PetAdoptionContext context)
        {
            _context = context;
        }

        public IList<Pet> Pets { get; set; } = default!;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SortField { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; } = "asc";

        public async Task OnGetAsync(int pageNumber = 1)
        {
            IQueryable<Pet> petsIQ = _context.Pets.Include(p => p.Adopter);

            // Search
            if (!string.IsNullOrEmpty(SearchString))
            {
                petsIQ = petsIQ.Where(p =>
                    p.Name.Contains(SearchString) ||
                    p.Species.Contains(SearchString));
            }

            // Sort
            petsIQ = SortField switch
            {
                "Name" => SortOrder == "asc" ? petsIQ.OrderBy(p => p.Name) : petsIQ.OrderByDescending(p => p.Name),
                "Age" => SortOrder == "asc" ? petsIQ.OrderBy(p => p.Age) : petsIQ.OrderByDescending(p => p.Age),
                "Status" => SortOrder == "asc" ? petsIQ.OrderBy(p => p.Status) : petsIQ.OrderByDescending(p => p.Status),
                _ => petsIQ.OrderBy(p => p.PetId)
            };

            // Paging
            CurrentPage = pageNumber;
            int totalCount = await petsIQ.CountAsync();
            TotalPages = (int)System.Math.Ceiling(totalCount / (double)PageSize);

            Pets = await petsIQ
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}
