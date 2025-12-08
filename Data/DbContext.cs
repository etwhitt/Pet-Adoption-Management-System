using Microsoft.EntityFrameworkCore;
using PetAdoptions.Models;
using System;

namespace PetAdoptions.Data
{
    public class PetAdoptionContext : DbContext
    {
        public PetAdoptionContext(DbContextOptions<PetAdoptionContext> options)
            : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; } = default!;
        public DbSet<Adopter> Adopters { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Adopters
            modelBuilder.Entity<Adopter>().HasData(
                new Adopter { AdopterId = 1, FirstName = "Alice", LastName = "Smith", Email = "alice@example.com", Phone = "123-456-7890", Address = "123 Main St" },
                new Adopter { AdopterId = 2, FirstName = "Bob", LastName = "Jones", Email = "bob@example.com", Phone = "987-654-3210", Address = "456 Oak Ave" },
                new Adopter { AdopterId = 3, FirstName = "Carol", LastName = "Taylor", Email = "carol@example.com", Phone = "555-555-5555", Address = "789 Pine Rd" }
            );

            // Seed Pets (25+)
            modelBuilder.Entity<Pet>().HasData(
                new Pet { PetId = 1, Name = "Fluffy", Species = "Cat", Breed = "Siamese", Age = 2, Status = "Available", IntakeDate = new DateTime(2025,12,1), AdopterId = null },
                new Pet { PetId = 2, Name = "Buddy", Species = "Dog", Breed = "Beagle", Age = 3, Status = "Available", IntakeDate = new DateTime(2025,11,28), AdopterId = null },
                new Pet { PetId = 3, Name = "Max", Species = "Dog", Breed = "Labrador", Age = 4, Status = "Adopted", IntakeDate = new DateTime(2025,11,23), AdopterId = 1 },
                new Pet { PetId = 4, Name = "Whiskers", Species = "Cat", Breed = "Persian", Age = 1, Status = "Pending", IntakeDate = new DateTime(2025,11,26), AdopterId = 2 },
                new Pet { PetId = 5, Name = "Bella", Species = "Dog", Breed = "Poodle", Age = 5, Status = "Available", IntakeDate = new DateTime(2025,11,29), AdopterId = null },
                new Pet { PetId = 6, Name = "Charlie", Species = "Dog", Breed = "Bulldog", Age = 3, Status = "Available", IntakeDate = new DateTime(2025,11,27), AdopterId = null },
                new Pet { PetId = 7, Name = "Luna", Species = "Cat", Breed = "Maine Coon", Age = 2, Status = "Available", IntakeDate = new DateTime(2025,11,25), AdopterId = null },
                new Pet { PetId = 8, Name = "Rocky", Species = "Dog", Breed = "Boxer", Age = 4, Status = "Adopted", IntakeDate = new DateTime(2025,11,22), AdopterId = 3 },
                new Pet { PetId = 9, Name = "Simba", Species = "Cat", Breed = "Bengal", Age = 1, Status = "Available", IntakeDate = new DateTime(2025,11,30), AdopterId = null },
                new Pet { PetId = 10, Name = "Milo", Species = "Dog", Breed = "Shih Tzu", Age = 2, Status = "Pending", IntakeDate = new DateTime(2025,11,26), AdopterId = 1 },
                new Pet { PetId = 11, Name = "Coco", Species = "Cat", Breed = "Sphynx", Age = 3, Status = "Available", IntakeDate = new DateTime(2025,11,28), AdopterId = null },
                new Pet { PetId = 12, Name = "Daisy", Species = "Dog", Breed = "Golden Retriever", Age = 4, Status = "Available", IntakeDate = new DateTime(2025,11,27), AdopterId = null },
                new Pet { PetId = 13, Name = "Oscar", Species = "Cat", Breed = "Ragdoll", Age = 2, Status = "Available", IntakeDate = new DateTime(2025,11,24), AdopterId = null },
                new Pet { PetId = 14, Name = "Lily", Species = "Dog", Breed = "Cocker Spaniel", Age = 1, Status = "Adopted", IntakeDate = new DateTime(2025,11,21), AdopterId = 2 },
                new Pet { PetId = 15, Name = "Toby", Species = "Dog", Breed = "Dalmatian", Age = 3, Status = "Available", IntakeDate = new DateTime(2025,12,2), AdopterId = null },
                new Pet { PetId = 16, Name = "Nala", Species = "Cat", Breed = "British Shorthair", Age = 2, Status = "Available", IntakeDate = new DateTime(2025,12,3), AdopterId = null },
                new Pet { PetId = 17, Name = "Jack", Species = "Dog", Breed = "Doberman", Age = 5, Status = "Available", IntakeDate = new DateTime(2025,11,23), AdopterId = null },
                new Pet { PetId = 18, Name = "Chloe", Species = "Cat", Breed = "Russian Blue", Age = 3, Status = "Pending", IntakeDate = new DateTime(2025,12,4), AdopterId = 3 },
                new Pet { PetId = 19, Name = "Zeus", Species = "Dog", Breed = "German Shepherd", Age = 4, Status = "Available", IntakeDate = new DateTime(2025,11,20), AdopterId = null },
                new Pet { PetId = 20, Name = "Mittens", Species = "Cat", Breed = "Scottish Fold", Age = 1, Status = "Available", IntakeDate = new DateTime(2025,11,25), AdopterId = null },
                new Pet { PetId = 21, Name = "Ruby", Species = "Dog", Breed = "Chihuahua", Age = 2, Status = "Available", IntakeDate = new DateTime(2025,11,26), AdopterId = null },
                new Pet { PetId = 22, Name = "Leo", Species = "Cat", Breed = "Abyssinian", Age = 3, Status = "Available", IntakeDate = new DateTime(2025,11,27), AdopterId = null },
                new Pet { PetId = 23, Name = "Sasha", Species = "Dog", Breed = "Husky", Age = 4, Status = "Available", IntakeDate = new DateTime(2025,11,28), AdopterId = null },
                new Pet { PetId = 24, Name = "Oliver", Species = "Cat", Breed = "Exotic Shorthair", Age = 2, Status = "Available", IntakeDate = new DateTime(2025,11,29), AdopterId = null },
                new Pet { PetId = 25, Name = "Pepper", Species = "Dog", Breed = "Terrier", Age = 3, Status = "Available", IntakeDate = new DateTime(2025,11,30), AdopterId = null }
            );
        }
    }
}
