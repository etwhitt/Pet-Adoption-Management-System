using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdoptions.Data;
using PetAdoptions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetAdoptions.Pages.Adopters;

    public class IndexModel : PageModel
    {
        private readonly PetAdoptionContext _context;

        // Number of pets per page.
        private const int PageSize = 10;

        public IndexModel(PetAdoptionContext context)
        {
            _context = context;
        }

        // List of adopters.
        public IList<Adopter> Adopters { get; set; } = default!;

        // Current page.
        public int CurrentPage { get; set; }

        // Total number of pages.
        public int TotalPages { get; set; }

        // User's search input.
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        // Sort field.
        [BindProperty(SupportsGet = true)]
        public string? SortField { get; set; }

        // Sort order.
        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; } = "asc";

        // Handles GET requests. 
        public async Task OnGetAsync(int pageNumber = 1)
        {
            IQueryable<Adopter> adoptersIQ = _context.Adopters;

            // Search filtering.
            if (!string.IsNullOrEmpty(SearchString))
            {
                adoptersIQ = adoptersIQ.Where(a =>
                    a.FirstName.Contains(SearchString) ||
                    a.LastName.Contains(SearchString) ||
                    a.Phone.Contains(SearchString) ||
                    a.Email.Contains(SearchString));
            }

            // Logic for sorting. 
            adoptersIQ = SortField switch
            {
                "FirstName" => SortOrder == "asc" ? adoptersIQ.OrderBy(a => a.FirstName) : adoptersIQ.OrderByDescending(a => a.FirstName),
                "LastName" => SortOrder == "asc" ? adoptersIQ.OrderBy(a => a.LastName) : adoptersIQ.OrderByDescending(a => a.LastName),
                "Phone" => SortOrder == "asc" ? adoptersIQ.OrderBy(a => a.Phone) : adoptersIQ.OrderByDescending(a => a.Phone),
                "Email" => SortOrder == "asc" ? adoptersIQ.OrderBy(a => a.Email) : adoptersIQ.OrderByDescending(a => a.Email),
                _ => adoptersIQ.OrderBy(a => a.AdopterId)
            };

            // Pagination. 
            CurrentPage = pageNumber;
            int totalCount = await adoptersIQ.CountAsync();
            TotalPages = (int)System.Math.Ceiling(totalCount / (double)PageSize);

            // Gets results for current page.
            Adopters = await adoptersIQ
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }