using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdoptions.Data;
using PetAdoptions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetAdoptions.Pages.Adopters
{
    public class IndexModel : PageModel
    {
        private readonly PetAdoptionContext _context;
        private const int PageSize = 10;

        public IndexModel(PetAdoptionContext context)
        {
            _context = context;
        }

        public IList<Adopter> Adopters { get; set; } = default!;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync(int pageNumber = 1)
        {
            IQueryable<Adopter> query = _context.Adopters;

            // Search
            if (!string.IsNullOrEmpty(SearchString))
            {
                query = query.Where(a =>
                    a.FirstName.Contains(SearchString) ||
                    a.LastName.Contains(SearchString));
            }

            // Paging
            CurrentPage = pageNumber;
            int total = await query.CountAsync();
            TotalPages = (int)System.Math.Ceiling(total / (double)PageSize);

            Adopters = await query
                .OrderBy(a => a.LastName)
                .ThenBy(a => a.FirstName)
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}
