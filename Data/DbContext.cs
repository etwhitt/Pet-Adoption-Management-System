using Microsoft.EntityFrameworkCore;
using PetAdoptions.Models;

namespace PetAdoptions.Data;

public class PetAdoptionContext : DbContext
{
    public PetAdoptionContext(DbContextOptions<PetAdoptionContext> options) : base(options) {}

    public DbSet<Pet> Pets {get; set;} = default!;

    public DbSet<Adopter> Adopters {get; set;} = default!;
}