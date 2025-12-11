using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetAdoptions.Data;
using PetAdoptions.Models;

namespace PetAdoptions.Pages.Pets;

    public class IndexModel : PageModel
{
    private readonly PetAdoptionContext _context;

    // Number of pets per page. 
    private const int PageSize = 10;

    public IndexModel(PetAdoptionContext context)
    {
        _context = context;
    }

    // Pets displayed on page.
    public IList<Pet> Pets { get; set; } = default!;

    // Current page number.
    public int CurrentPage { get; set; }

    // Total pages.
    public int TotalPages { get; set; }

    // User's search.
    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }

    // Column being sorted.
    [BindProperty(SupportsGet = true)]
    public string? SortField { get; set; }

    // How column is sorted i.e. ascending or descending.
    [BindProperty(SupportsGet = true)]
    public string? SortOrder { get; set; } = "asc";

    public async Task OnGetAsync(int pageNumber = 1)
    {
        // Pet search that includes adopter information.
        IQueryable<Pet> petsIQ = _context.Pets.Include(p => p.Adopter);

        // Filters pets by user's search.
        if (!string.IsNullOrEmpty(SearchString))
        {
            petsIQ = petsIQ.Where(p =>
                p.Name.Contains(SearchString) ||
                p.Species.Contains(SearchString));
        }

        // Handles sorting of table columns. PetId is the default.
        petsIQ = SortField switch
        {
            "Name" => SortOrder == "asc" ? petsIQ.OrderBy(p => p.Name) : petsIQ.OrderByDescending(p => p.Name),
            "Species" => SortOrder == "asc" ? petsIQ.OrderBy(p => p.Species) : petsIQ.OrderByDescending(p => p.Species),
            "Breed" => SortOrder == "asc" ? petsIQ.OrderBy(p => p.Breed) : petsIQ.OrderByDescending(p => p.Breed),
            "Age" => SortOrder == "asc" ? petsIQ.OrderBy(p => p.Age) : petsIQ.OrderByDescending(p => p.Age),
            "Status" => SortOrder == "asc" ? petsIQ.OrderBy(p => p.Status) : petsIQ.OrderByDescending(p => p.Status),
            "Adopter" => SortOrder == "asc"
                ? petsIQ.OrderBy(p => p.Adopter!.LastName).ThenBy(p => p.Adopter!.FirstName)
                : petsIQ.OrderByDescending(p => p.Adopter!.LastName).ThenByDescending(p => p.Adopter!.FirstName),
            _ => petsIQ.OrderBy(p => p.PetId)
        };

        // Pagination with total pets after filtered.
        CurrentPage = pageNumber;
        int totalCount = await petsIQ.CountAsync();
        TotalPages = (int)System.Math.Ceiling(totalCount / (double)PageSize);

        // Pets on current page.
        Pets = await petsIQ
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();
    }
}